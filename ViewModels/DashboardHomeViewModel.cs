using System.Collections.ObjectModel;
using System.Globalization;
using FitnessTracker.Data;
using FitnessTracker.Models;
using FitnessTracker;

namespace FitnessTracker.ViewModels;

/// <summary>Home dashboard: daily goal ring, summary cards, and activity feed (sample data).</summary>
public sealed class DashboardHomeViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _shell;
    private readonly string _dateDisplay;

    public DashboardHomeViewModel(MainWindowViewModel shell)
    {
        _shell = shell;
        AppSession.DisplayNameChanged += (_, _) => OnPropertyChanged(nameof(Greeting));
        AppSession.ProfileImageChanged += (_, _) => OnPropertyChanged(nameof(ProfileImagePath));
        _dateDisplay = DateTime.Now.ToString("dddd, MMMM d, yyyy", CultureInfo.CurrentCulture);

        var d = SampleDataStore.Dashboard;
        DailyGoalFraction = d.DailyGoalPercent;
        DailyGoalPercentText = $"{(int)(d.DailyGoalPercent * 100)}%";
        CaloriesCurrent = d.CaloriesCurrent;
        CaloriesTarget = d.CaloriesTarget;
        CaloriesRatioText = $"{d.CaloriesCurrent}/{d.CaloriesTarget}";
        WaterCurrent = d.WaterGlassesCurrent;
        WaterTarget = d.WaterGlassesTarget;
        WaterRatioText = $"{d.WaterGlassesCurrent}/{d.WaterGlassesTarget} Glasses";
        ExerciseCurrent = d.ExerciseMinutesCurrent;
        ExerciseTarget = d.ExerciseMinutesTarget;
        ExerciseRatioText = $"{d.ExerciseMinutesCurrent}/{d.ExerciseMinutesTarget} min";
        MealsCurrent = d.MealsLogged;
        MealsTarget = d.MealsTarget;
        MealsRatioText = $"{d.MealsLogged}/{d.MealsTarget}";
        CaloriesProgress = d.CaloriesProgress;
        WaterProgress = d.WaterProgress;
        ExerciseProgress = d.ExerciseProgress;
        MealsProgress = d.MealsProgress;

        ActivityItems = new ObservableCollection<ActivityFeedItemModel>(SampleDataStore.TodayActivity);
        OpenProfileCommand = new RelayCommand(_ => _shell.NavigateTo("Profile"));
    }

    public string Greeting => $"Hello, {AppSession.DisplayName} 👋";
    public string? ProfileImagePath => AppSession.ProfileImagePath;
    public string DateDisplay => _dateDisplay;

    /// <summary>0..1 for circular progress arc.</summary>
    public double DailyGoalFraction { get; }
    public string DailyGoalPercentText { get; }
    public int CaloriesCurrent { get; }
    public int CaloriesTarget { get; }
    public string CaloriesRatioText { get; }
    public int WaterCurrent { get; }
    public int WaterTarget { get; }
    public string WaterRatioText { get; }
    public int ExerciseCurrent { get; }
    public int ExerciseTarget { get; }
    public string ExerciseRatioText { get; }
    public int MealsCurrent { get; }
    public int MealsTarget { get; }
    public string MealsRatioText { get; }
    public int CaloriesProgress { get; }
    public int WaterProgress { get; }
    public int ExerciseProgress { get; }
    public int MealsProgress { get; }

    public ObservableCollection<ActivityFeedItemModel> ActivityItems { get; }
    public RelayCommand OpenProfileCommand { get; }
}
