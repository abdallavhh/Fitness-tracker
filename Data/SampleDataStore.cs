using System.Collections.ObjectModel;
using System.Linq;
using FitnessTracker.Models;

namespace FitnessTracker.Data;

/// <summary>In-memory sample data for the demo application.</summary>
public static class SampleDataStore
{
    public static UserProfileModel Profile { get; } = new()
    {
        Username = "Abdalla Ahmed",
        Gender = "Male",
        Age = 24,
        HeightCm = 180,
        WeightKg = 75,
        MedicalCondition = "—",
        DietaryPreference = "Low Carb",
        TargetWeightKg = 70
    };

    public static DashboardSummaryModel Dashboard { get; } = new()
    {
        DailyGoalPercent = 0.75,
        CaloriesCurrent = 1200,
        CaloriesTarget = 2000,
        WaterGlassesCurrent = 6,
        WaterGlassesTarget = 8,
        ExerciseMinutesCurrent = 30,
        ExerciseMinutesTarget = 60,
        MealsLogged = 3,
        MealsTarget = 5
    };

    public static ObservableCollection<ActivityFeedItemModel> TodayActivity { get; } = new()
    {
        new() { Icon = "🏃", Title = "Running", Subtitle = "30 min · Morning", KcalLabel = "−300 kcal", AccentBg = "#E0F2FE" },
        new() { Icon = "🚴", Title = "Cycling", Subtitle = "45 min · Afternoon", KcalLabel = "−500 kcal", AccentBg = "#D1FAE5" },
        new() { Icon = "🍳", Title = "Breakfast", Subtitle = "Oats + Eggs · 08:00", KcalLabel = "+300 kcal", AccentBg = "#FEF3C7" }
    };

    public static ObservableCollection<MealEntryModel> Meals { get; } = new()
    {
        new() { Name = "Breakfast", Calories = 300, Icon = "🍲", TileBg = "#FEF3C7" },
        new() { Name = "Lunch", Calories = 500, Icon = "🥗", TileBg = "#FFEDD5" },
        new() { Name = "Dinner", Calories = 400, Icon = "🥩", TileBg = "#FFE4E6" },
        new() { Name = "Snack", Calories = 150, Icon = "🍎", TileBg = "#E0F2FE" }
    };

    public static ObservableCollection<ExerciseEntryModel> Exercises { get; } = new()
    {
        new() { Name = "Running", Minutes = 30, CaloriesBurned = 300, Icon = "🏃" },
        new() { Name = "Cycling", Minutes = 45, CaloriesBurned = 500, Icon = "🚴" },
        new() { Name = "Gym", Minutes = 60, CaloriesBurned = 700, Icon = "🏋" }
    };

    public static ObservableCollection<HealthMetricModel> HealthMetrics { get; } = new()
    {
        new() { Name = "Heart Rate", Value = "80 bpm", Icon = "❤", TileBg = "#FEF2F2" },
        new() { Name = "Blood Sugar", Value = "90 mg/dL", Icon = "💧", TileBg = "#E0F2FE" },
        new() { Name = "Blood Pressure", Value = "120/80", Icon = "🩺", TileBg = "#FEE2E2" },
        new() { Name = "Water", Value = "2 Liters", Icon = "🫙", TileBg = "#F0FDF4" }
    };

    public static ObservableCollection<GoalModel> Goals { get; } = new()
    {
        new()
        {
            Title = "Current Goal",
            TargetLabel = "70 kg",
            StartDateDisplay = "01/01/2024",
            ProgressPercent = 65,
            StatusLabel = "On Track ✓",
            OnTrack = true
        },
        new()
        {
            Title = "Weekly workouts",
            TargetLabel = "5 sessions",
            StartDateDisplay = "03/25/2026",
            ProgressPercent = 40,
            StatusLabel = "In progress",
            OnTrack = true
        }
    };

    public static int[] WeeklyBurnSeries { get; } = [180, 220, 200, 350, 280, 400, 320];

    public static AdminSummaryModel AdminSummary { get; } = new()
    {
        TotalUsers = 120,
        TotalMeals = 340,
        TotalExercises = 220,
        UsersDeltaPercent = 12,
        MealsDeltaPercent = 8,
        ExercisesDeltaPercent = 18
    };

    public static ObservableCollection<MonthlyBarModel> MonthlyActivity { get; } = new()
    {
        new() { Label = "Jan", HeightPercent = 55 },
        new() { Label = "Feb", HeightPercent = 72 },
        new() { Label = "Mar", HeightPercent = 44 },
        new() { Label = "Apr", HeightPercent = 100, Highlight = true },
        new() { Label = "May", HeightPercent = 67 },
        new() { Label = "Jun", HeightPercent = 83 }
    };

    public static ObservableCollection<UserRecord> AdminUsers { get; } = new()
    {
        new() { Id = 1, Username = "user01", Email = "user01@example.com", Status = "Active" },
        new() { Id = 2, Username = "user02", Email = "user02@example.com", Status = "Active" },
        new() { Id = 3, Username = "user03", Email = "user03@example.com", Status = "Active" },
        new() { Id = 4, Username = "user04", Email = "user04@example.com", Status = "Active" },
        new() { Id = 5, Username = "maria_g", Email = "maria@example.com", Status = "Active" },
        new() { Id = 6, Username = "alex_k", Email = "alex@example.com", Status = "Active" },
        new() { Id = 7, Username = "sam_r", Email = "sam@example.com", Status = "Active" },
        new() { Id = 8, Username = "lisa_m", Email = "lisa@example.com", Status = "Active" },
        new() { Id = 9, Username = "chris_p", Email = "chris@example.com", Status = "Active" },
        new() { Id = 10, Username = "dana_q", Email = "dana@example.com", Status = "Inactive" },
        new() { Id = 11, Username = "evan_t", Email = "evan@example.com", Status = "Active" },
        new() { Id = 12, Username = "fiona_w", Email = "fiona@example.com", Status = "Active" }
    };

    public static int NextAdminUserId() =>
        AdminUsers.Count == 0 ? 1 : AdminUsers.Max(u => u.Id) + 1;

    public static UserRecord AddAdminUser(string username, string email, string status)
    {
        var u = new UserRecord
        {
            Id = NextAdminUserId(),
            Username = username,
            Email = email,
            Status = status
        };
        AdminUsers.Add(u);
        return u;
    }
}
