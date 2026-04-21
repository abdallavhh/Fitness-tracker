using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data;

/// <summary>Loads and persists UI models for the logged-in user against <see cref="AppDbContext"/>.</summary>
public static class UserDataQueries
{
    private const int DefaultCaloriesTarget = 2000;
    private const int DefaultMealsTarget = 5;
    private const int DefaultExerciseTargetMinutes = 60;
    private const int DefaultWaterGlassesTarget = 8;
    private const int MlPerGlass = 250;

    public static DailyLog? GetLatestDailyLog(AppDbContext db, int userId) =>
        db.DailyLogs
            .AsNoTracking()
            .Include(d => d.Exercises)
            .Include(d => d.Meals).ThenInclude(m => m.MealItems)
            .Where(d => d.User_ID == userId)
            .OrderByDescending(d => d.Log_Date ?? DateTime.MinValue)
            .FirstOrDefault();

    public static HealthMetric? GetLatestHealthMetric(AppDbContext db, int userId) =>
        db.HealthMetrics
            .AsNoTracking()
            .Where(h => h.User_ID == userId)
            .OrderByDescending(h => h.Metric_Date)
            .FirstOrDefault();

    public static DashboardSummaryModel BuildDashboardSummary(int userId)
    {
        using var db = new AppDbContext();
        var log = GetLatestDailyLog(db, userId);
        var metric = GetLatestHealthMetric(db, userId);

        var caloriesCurrent = log?.Meals.SelectMany(m => m.MealItems).Sum(i => i.Calories ?? 0) ?? 0;
        var exerciseMinutes = log?.Exercises.Sum(e => e.Duration ?? 0) ?? 0;
        var mealsLogged = log?.Meals.Count ?? 0;

        var waterMl = metric?.Total_Water ?? 0;
        var waterGlasses = waterMl <= 0 ? 0 : (int)Math.Round(waterMl / (double)MlPerGlass);

        var goal = db.Goals.AsNoTracking().FirstOrDefault(g => g.User_ID == userId);
        var dailyGoalFraction = goal is null ? 0.65 : ComputeDailyGoalFraction(goal);

        return new DashboardSummaryModel
        {
            DailyGoalPercent = dailyGoalFraction,
            CaloriesCurrent = caloriesCurrent,
            CaloriesTarget = DefaultCaloriesTarget,
            WaterGlassesCurrent = Math.Min(waterGlasses, 24),
            WaterGlassesTarget = DefaultWaterGlassesTarget,
            ExerciseMinutesCurrent = exerciseMinutes,
            ExerciseMinutesTarget = DefaultExerciseTargetMinutes,
            MealsLogged = mealsLogged,
            MealsTarget = DefaultMealsTarget
        };
    }

    private static double ComputeDailyGoalFraction(Goal g)
    {
        if (g.Achieved)
            return 1.0;
        var p = ComputeGoalProgressPercent(g) / 100.0;
        return Math.Clamp(p, 0, 1);
    }

    public static List<ActivityFeedItemModel> BuildActivityFeed(int userId)
    {
        using var db = new AppDbContext();
        var log = GetLatestDailyLog(db, userId);
        if (log is null)
            return [];

        var items = new List<ActivityFeedItemModel>();
        var ex = log.Exercises.FirstOrDefault();
        if (ex is not null)
        {
            items.Add(new ActivityFeedItemModel
            {
                Icon = "🏃",
                Title = ex.Exercise_Name,
                Subtitle = $"{ex.Duration ?? 0} min · {ex.Exercise_Type}",
                KcalLabel = ex.Calories_Burned is { } c ? $"−{c} kcal" : string.Empty,
                AccentBg = "#E0F2FE"
            });
        }

        var meal = log.Meals.FirstOrDefault();
        if (meal is not null)
        {
            var kcal = meal.MealItems.Sum(i => i.Calories ?? 0);
            items.Add(new ActivityFeedItemModel
            {
                Icon = "🍽",
                Title = meal.Meal_Name ?? "Meal",
                Subtitle = $"{meal.Meal_Type ?? "Logged"} · {FormatLogDay(log.Log_Date)}",
                KcalLabel = kcal > 0 ? $"+{kcal} kcal" : string.Empty,
                AccentBg = "#FEF3C7"
            });
        }

        return items;
    }

    public static ObservableCollection<MealEntryModel> LoadMeals(int userId)
    {
        using var db = new AppDbContext();
        var logs = db.DailyLogs
            .AsNoTracking()
            .Include(d => d.Meals).ThenInclude(m => m.MealItems)
            .Where(d => d.User_ID == userId)
            .OrderByDescending(d => d.Log_Date ?? DateTime.MinValue)
            .ToList();

        var tiles = new List<MealEntryModel>();
        var palettes = new[] { "#FEF3C7", "#FFEDD5", "#FFE4E6", "#E0F2FE", "#D1FAE5" };
        var i = 0;
        foreach (var log in logs)
        {
            foreach (var meal in log.Meals)
            {
                var kcal = meal.MealItems.Sum(m => m.Calories ?? 0);
                var label = string.IsNullOrEmpty(meal.Meal_Name) ? "Meal" : meal.Meal_Name;
                tiles.Add(new MealEntryModel
                {
                    Name = $"{label} ({FormatLogDay(log.Log_Date)})",
                    Calories = kcal,
                    Icon = "🍽",
                    TileBg = palettes[i++ % palettes.Length]
                });
            }
        }

        return new ObservableCollection<MealEntryModel>(tiles);
    }

    public static ObservableCollection<ExerciseEntryModel> LoadExercises(int userId)
    {
        using var db = new AppDbContext();
        var rows = db.ExerciseLogs
            .AsNoTracking()
            .Include(e => e.DailyLog)
            .Where(e => e.DailyLog != null && e.DailyLog.User_ID == userId)
            .OrderByDescending(e => e.DailyLog!.Log_Date ?? DateTime.MinValue)
            .ToList();

        var list = rows.Select(e => new ExerciseEntryModel
        {
            Name = e.Exercise_Name,
            Minutes = e.Duration ?? 0,
            CaloriesBurned = e.Calories_Burned ?? 0,
            Icon = "🏃"
        }).ToList();

        return new ObservableCollection<ExerciseEntryModel>(list);
    }

    /// <summary>Last 7 days ending today: sum of calories burned per day.</summary>
    public static int[] WeeklyBurnSeries(int userId)
    {
        using var db = new AppDbContext();
        var end = DateTime.Today.Date;
        var start = end.AddDays(-6);
        var rows = db.ExerciseLogs
            .AsNoTracking()
            .Include(e => e.DailyLog)
            .Where(e => e.DailyLog != null && e.DailyLog.User_ID == userId && e.DailyLog.Log_Date != null)
            .AsEnumerable()
            .Where(e =>
            {
                var logDay = e.DailyLog!.Log_Date!.Value.Date;
                return logDay >= start && logDay <= end;
            })
            .ToList();

        var series = new int[7];
        for (var d = 0; d < 7; d++)
        {
            var day = start.AddDays(d);
            series[d] = rows
                .Where(e => e.DailyLog!.Log_Date!.Value.Date == day)
                .Sum(e => e.Calories_Burned ?? 0);
        }

        return series;
    }

    public static List<HealthMetricModel> LoadHealthMetricModels(int userId)
    {
        using var db = new AppDbContext();
        var m = db.HealthMetrics
            .AsNoTracking()
            .Where(h => h.User_ID == userId)
            .OrderByDescending(h => h.Metric_Date)
            .FirstOrDefault();

        if (m is null)
            return [];

        return
        [
            new HealthMetricModel
            {
                Name = "Heart Rate",
                Value = m.Heart_Rate is { } hr ? $"{hr} bpm" : "—",
                Icon = "❤",
                TileBg = "#FEF2F2"
            },
            new HealthMetricModel
            {
                Name = "Blood Sugar",
                Value = m.Blood_Sugar is { } bs ? $"{bs} mg/dL" : "—",
                Icon = "💧",
                TileBg = "#E0F2FE"
            },
            new HealthMetricModel
            {
                Name = "Blood Pressure",
                Value = string.IsNullOrEmpty(m.Blood_Pressure) ? "—" : m.Blood_Pressure,
                Icon = "🩺",
                TileBg = "#FEE2E2"
            },
            new HealthMetricModel
            {
                Name = "Water",
                Value = m.Total_Water is { } w && w > 0
                    ? $"{w / 1000.0:0.##} L ({(int)Math.Round(w / (double)MlPerGlass)} glasses)"
                    : "—",
                Icon = "🫙",
                TileBg = "#F0FDF4"
            }
        ];
    }

    public static ObservableCollection<GoalModel> LoadGoals(int userId)
    {
        using var db = new AppDbContext();
        var goals = db.Goals.AsNoTracking().Where(g => g.User_ID == userId).OrderBy(g => g.Goal_ID).ToList();
        var list = goals.Select(ToGoalModel).ToList();
        return new ObservableCollection<GoalModel>(list);
    }

    private static GoalModel ToGoalModel(Goal g)
    {
        var progress = ComputeGoalProgressPercent(g);
        return new GoalModel
        {
            Title = string.IsNullOrEmpty(g.Goal_Type) ? "Goal" : g.Goal_Type,
            TargetLabel = g.Target_Value is null ? "—" : g.Target_Value.Value.ToString("0.##", CultureInfo.CurrentCulture),
            StartDateDisplay = g.Start_Date?.ToString("d", CultureInfo.CurrentCulture) ?? "—",
            ProgressPercent = progress,
            StatusLabel = g.Achieved ? "Achieved ✓" : progress >= 100 ? "Complete ✓" : "In progress",
            OnTrack = g.Achieved || progress < 100
        };
    }

    public static int ComputeGoalProgressPercent(Goal g)
    {
        if (g.Achieved)
            return 100;
        if (g.Target_Value is null || g.Current_Value is null)
            return 0;
        var t = (double)g.Target_Value.Value;
        var c = (double)g.Current_Value.Value;
        var type = g.Goal_Type ?? string.Empty;
        if (type.Contains("Loss", StringComparison.OrdinalIgnoreCase) && t < c && c > 0)
            return (int)Math.Clamp(100.0 * (1 - (c - t) / c), 0, 100);
        if (type.Contains("Gain", StringComparison.OrdinalIgnoreCase) && t > c && t > 0)
            return (int)Math.Clamp(100.0 * (c / t), 0, 100);
        if (type.Contains("Maintain", StringComparison.OrdinalIgnoreCase))
            return (int)Math.Clamp(100.0 - Math.Abs(c - t) * 10, 0, 100);
        return (int)Math.Clamp(100.0 - Math.Abs(c - t), 0, 100);
    }

    public static UserProfileModel? LoadUserProfile(int userId)
    {
        using var db = new AppDbContext();
        var user = db.Users.AsNoTracking().Include(u => u.Profile).FirstOrDefault(u => u.User_ID == userId);
        if (user?.Profile is null)
            return null;

        var p = user.Profile;
        var age = DateTime.Today.Year - p.Date_of_Birth.Year;
        if (p.Date_of_Birth.Date > DateTime.Today.AddYears(-age)) age--;

        return new UserProfileModel
        {
            Username = user.User_Name,
            Gender = p.Gender,
            Age = age,
            HeightCm = (int)Math.Round(p.Height, MidpointRounding.AwayFromZero),
            WeightKg = (double)p.Weight,
            MedicalCondition = string.IsNullOrEmpty(p.Medical_Condition) ? "—" : p.Medical_Condition,
            DietaryPreference = string.Empty,
            TargetWeightKg = p.Target_Weight is null ? (double)p.Weight : (double)p.Target_Weight.Value
        };
    }

    public static void SaveUserProfile(int userId, UserProfileModel model)
    {
        using var db = new AppDbContext();
        var user = db.Users.Include(u => u.Profile).FirstOrDefault(u => u.User_ID == userId);
        if (user?.Profile is null)
            return;

        user.User_Name = model.Username.Trim();
        user.Profile.Gender = model.Gender.Trim();
        user.Profile.Height = model.HeightCm;
        user.Profile.Weight = (decimal)model.WeightKg;
        var prior = user.Profile.Date_of_Birth;
        try
        {
            user.Profile.Date_of_Birth = new DateTime(
                DateTime.Today.Year - model.Age,
                prior.Month,
                prior.Day);
        }
        catch (ArgumentOutOfRangeException)
        {
            user.Profile.Date_of_Birth = prior;
        }
        user.Profile.Medical_Condition = model.MedicalCondition.Trim() is var mc && mc != "—" ? mc : null;
        user.Profile.Target_Weight = (decimal)model.TargetWeightKg;
        db.SaveChanges();
    }

    public static int GetOrCreateDailyLogId(AppDbContext db, int userId)
    {
        var today = DateTime.Today.Date;
        var existing = db.DailyLogs.FirstOrDefault(d =>
            d.User_ID == userId && d.Log_Date.HasValue && d.Log_Date.Value.Date == today);
        if (existing is not null)
            return existing.Log_ID;

        var last = db.DailyLogs.Where(d => d.User_ID == userId).OrderByDescending(d => d.Log_Date).FirstOrDefault();
        var weight = last?.Weight ?? 70m;
        var nextId = db.DailyLogs.Any() ? db.DailyLogs.Max(d => d.Log_ID) + 1 : 1;
        var log = new DailyLog { Log_ID = nextId, User_ID = userId, Log_Date = DateTime.Today, Weight = weight };
        db.DailyLogs.Add(log);
        db.SaveChanges();
        return log.Log_ID;
    }

    public static void AddMeal(int userId, MealEntryModel model)
    {
        using var db = new AppDbContext();
        var logId = GetOrCreateDailyLogId(db, userId);
        var nextMealId = db.Meals.Any() ? db.Meals.Max(m => m.Meal_ID) + 1 : 1;
        var nextItemId = db.MealItems.Any() ? db.MealItems.Max(m => m.Meal_Item_ID) + 1 : 1;
        var meal = new Meal
        {
            Meal_ID = nextMealId,
            Log_ID = logId,
            Meal_Name = model.Name,
            Meal_Type = "Logged"
        };
        db.Meals.Add(meal);
        db.MealItems.Add(new MealItem
        {
            Meal_Item_ID = nextItemId,
            Meal_ID = meal.Meal_ID,
            Food_Name = model.Name,
            Quantity = 1,
            Calories = model.Calories,
            Protein = 0,
            Fat = 0,
            Carbs = 0
        });
        db.SaveChanges();
    }

    public static void AddExercise(int userId, ExerciseEntryModel model)
    {
        using var db = new AppDbContext();
        var logId = GetOrCreateDailyLogId(db, userId);
        var nextId = db.ExerciseLogs.Any() ? db.ExerciseLogs.Max(e => e.Exercise_ID) + 1 : 1;
        db.ExerciseLogs.Add(new ExerciseLog
        {
            Exercise_ID = nextId,
            Log_ID = logId,
            Exercise_Name = model.Name,
            Exercise_Type = "Cardio",
            Duration = model.Minutes,
            Calories_Burned = model.CaloriesBurned
        });
        db.SaveChanges();
    }

    public static void AddHealthMetric(int userId, HealthMetricModel model)
    {
        using var db = new AppDbContext();
        var nextId = db.HealthMetrics.Any() ? db.HealthMetrics.Max(m => m.Metric_ID) + 1 : 1;
        var name = model.Name.Trim();
        var val = model.Value.Trim();
        var firstInt = FirstInt(val);

        var h = new HealthMetric
        {
            Metric_ID = nextId,
            User_ID = userId,
            Metric_Date = DateTime.Today
        };

        if (name.Contains("Heart", StringComparison.OrdinalIgnoreCase) && firstInt is { } hr)
            h.Heart_Rate = hr;
        else if (name.Contains("Sugar", StringComparison.OrdinalIgnoreCase) && firstInt is { } bs)
            h.Blood_Sugar = bs;
        else if (name.Contains("Water", StringComparison.OrdinalIgnoreCase))
        {
            if (firstInt is { } ml && val.Contains("ml", StringComparison.OrdinalIgnoreCase))
                h.Total_Water = ml;
            else if (double.TryParse(
                         val.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "0",
                         NumberStyles.Any,
                         CultureInfo.CurrentCulture,
                         out var liters))
                h.Total_Water = (int)(liters * 1000);
            else if (firstInt is { } glasses)
                h.Total_Water = glasses * MlPerGlass;
        }
        else if (name.Contains("Pressure", StringComparison.OrdinalIgnoreCase) || name.Contains("BP", StringComparison.OrdinalIgnoreCase))
            h.Blood_Pressure = val.Length > 20 ? val[..20] : val;
        else if (firstInt.HasValue)
            h.Blood_Sugar = firstInt;

        if (h.Heart_Rate is null && h.Blood_Sugar is null && h.Blood_Pressure is null && h.Total_Water is null)
            h.Blood_Pressure = val.Length > 20 ? val[..20] : val;

        db.HealthMetrics.Add(h);
        db.SaveChanges();
    }

    private static int? FirstInt(string s)
    {
        var m = Regex.Match(s, @"\d+");
        return m.Success && int.TryParse(m.Value, out var v) ? v : null;
    }

    public static void AddGoal(int userId, GoalModel model)
    {
        using var db = new AppDbContext();
        var nextId = db.Goals.Any() ? db.Goals.Max(g => g.Goal_ID) + 1 : 1;
        if (!DateTime.TryParse(model.StartDateDisplay, CultureInfo.CurrentCulture, DateTimeStyles.None, out var start))
            start = DateTime.Today;
        var targetParsed = TryParseFirstDecimal(model.TargetLabel);
        db.Goals.Add(new Goal
        {
            Goal_ID = nextId,
            User_ID = userId,
            Goal_Type = model.Title,
            Target_Value = targetParsed,
            Current_Value = null,
            Start_Date = start,
            Target_Date = start.AddMonths(3),
            Achieved = model.ProgressPercent >= 100
        });
        db.SaveChanges();
    }

    private static decimal? TryParseFirstDecimal(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;
        var m = Regex.Match(text, @"[+-]?\d+(?:\.\d+)?");
        return m.Success && decimal.TryParse(m.Value, NumberStyles.Any, CultureInfo.CurrentCulture, out var d)
            ? d
            : null;
    }

    private static string FormatLogDay(DateTime? logDate) =>
        logDate.HasValue
            ? logDate.Value.ToString("MMM d", CultureInfo.CurrentCulture)
            : "—";
    public static void GenerateMockGraphData(int userId)
    {
        using var db = new AppDbContext();
        var random = new Random();
        var exerciseNames = new[] { "Running", "Cycling", "Swimming", "HIIT", "Rowing", "Elliptical" };

        for (int i = 0; i < 7; i++)
        {
            var targetDate = DateTime.Today.AddDays(-i);

            var log = db.DailyLogs.FirstOrDefault(d => 
                d.User_ID == userId && 
                d.Log_Date != null && 
                d.Log_Date.Value.Date == targetDate);

            if (log == null)
            {
                var lastLog = db.DailyLogs
                    .Where(d => d.User_ID == userId)
                    .OrderByDescending(d => d.Log_Date)
                    .FirstOrDefault();

                var weight = lastLog?.Weight ?? 70m;
                var nextLogId = db.DailyLogs.Any() ? db.DailyLogs.Max(d => d.Log_ID) + 1 : 1;

                log = new DailyLog 
                { 
                    Log_ID = nextLogId, 
                    User_ID = userId, 
                    Log_Date = targetDate, 
                    Weight = weight 
                };
                db.DailyLogs.Add(log);
                db.SaveChanges();
            }

            // Only add mock exercises if there aren't any for this log to prevent duplication on multiple runs
            var existingExercises = db.ExerciseLogs.Count(e => e.Log_ID == log.Log_ID);
            if (existingExercises == 0)
            {
                int numExercises = random.Next(1, 3); // 1 or 2 exercises
                for (int e = 0; e < numExercises; e++)
                {
                    var nextExId = db.ExerciseLogs.Any() ? db.ExerciseLogs.Max(ex => ex.Exercise_ID) + 1 : 1;
                    db.ExerciseLogs.Add(new ExerciseLog
                    {
                        Exercise_ID = nextExId,
                        Log_ID = log.Log_ID,
                        Exercise_Name = exerciseNames[random.Next(exerciseNames.Length)],
                        Exercise_Type = "Cardio",
                        Calories_Burned = random.Next(200, 701), // between 200 and 700
                        Duration = random.Next(30, 61) // between 30 and 60 mins
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
