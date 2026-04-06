namespace FitnessTracker.Models;

public sealed class UserProfileModel
{
    public string Username { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Age { get; set; }
    public int HeightCm { get; set; }
    public double WeightKg { get; set; }
    public string MedicalCondition { get; set; } = "—";
    public string DietaryPreference { get; set; } = string.Empty;
    public double TargetWeightKg { get; set; }
}

public sealed class DashboardSummaryModel
{
    public double DailyGoalPercent { get; set; }
    public int CaloriesCurrent { get; set; }
    public int CaloriesTarget { get; set; }
    public int WaterGlassesCurrent { get; set; }
    public int WaterGlassesTarget { get; set; }
    public int ExerciseMinutesCurrent { get; set; }
    public int ExerciseMinutesTarget { get; set; }
    public int MealsLogged { get; set; }
    public int MealsTarget { get; set; }

    public int CaloriesProgress => CaloriesTarget == 0 ? 0 : (int)(100.0 * CaloriesCurrent / CaloriesTarget);
    public int WaterProgress => WaterGlassesTarget == 0 ? 0 : (int)(100.0 * WaterGlassesCurrent / WaterGlassesTarget);
    public int ExerciseProgress => ExerciseMinutesTarget == 0 ? 0 : (int)(100.0 * ExerciseMinutesCurrent / ExerciseMinutesTarget);
    public int MealsProgress => MealsTarget == 0 ? 0 : (int)(100.0 * MealsLogged / MealsTarget);
}

public sealed class ActivityFeedItemModel
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string KcalLabel { get; set; } = string.Empty;
    public string AccentBg { get; set; } = "#E0F2FE";
}

public sealed class MealEntryModel
{
    public string Name { get; set; } = string.Empty;
    public int Calories { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string TileBg { get; set; } = "#FEF3C7";
}

public sealed class ExerciseEntryModel
{
    public string Name { get; set; } = string.Empty;
    public int Minutes { get; set; }
    public int CaloriesBurned { get; set; }
    public string Icon { get; set; } = string.Empty;

    /// <summary>Preformatted line for exercise cards (e.g. "30 min · 300 Cal").</summary>
    public string DurationCalorieLabel => $"{Minutes} min · {CaloriesBurned} Cal";
}

public sealed class HealthMetricModel
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string TileBg { get; set; } = "#F8FAFC";
}

public sealed class GoalModel
{
    public string Title { get; set; } = "Current Goal";
    public string TargetLabel { get; set; } = string.Empty;
    public string StartDateDisplay { get; set; } = string.Empty;
    public double ProgressPercent { get; set; }
    public string StatusLabel { get; set; } = "On Track";
    public bool OnTrack { get; set; } = true;
}

public sealed class AdminSummaryModel
{
    public int TotalUsers { get; set; }
    public int TotalMeals { get; set; }
    public int TotalExercises { get; set; }
    public int UsersDeltaPercent { get; set; }
    public int MealsDeltaPercent { get; set; }
    public int ExercisesDeltaPercent { get; set; }
}

public sealed class MonthlyBarModel
{
    public string Label { get; set; } = string.Empty;
    public double HeightPercent { get; set; }
    public bool Highlight { get; set; }
}

public sealed class BurnByDayModel
{
    public string Day { get; set; } = string.Empty;
    public int Calories { get; set; }
}
