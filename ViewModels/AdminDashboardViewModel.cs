using System.Collections.Specialized;
using System.Windows;
using FitnessTracker.Data;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace FitnessTracker.ViewModels;

/// <summary>Admin home: KPI cards and monthly activity column chart (LiveChartsCore).</summary>
public sealed class AdminDashboardViewModel : ViewModelBase
{
    public AdminDashboardViewModel()
    {
        SampleDataStore.AdminUsers.CollectionChanged += OnAdminUsersChanged;
        var s = SampleDataStore.AdminSummary;
        UsersDelta = s.UsersDeltaPercent;
        MealsDelta = s.MealsDeltaPercent;
        ExercisesDelta = s.ExercisesDeltaPercent;

        var columns = SampleDataStore.MonthlyActivity
            .Select(m => (double)m.HeightPercent)
            .ToArray();

        MonthlySeries =
        [
            new ColumnSeries<double>
            {
                Name = "Activity",
                Values = columns,
                Fill = new SolidColorPaint(new SKColor(0, 0xA6, 0x93)),
                Stroke = null
            }
        ];

        XAxes =
        [
            new Axis
            {
                Labels = SampleDataStore.MonthlyActivity.Select(x => x.Label).ToArray(),
                LabelsPaint = new SolidColorPaint(new SKColor(0x6B, 0x72, 0x80)),
                TextSize = 11,
                SeparatorsPaint = null
            }
        ];

        YAxes =
        [
            new Axis
            {
                MinLimit = 0,
                LabelsPaint = new SolidColorPaint(new SKColor(0x6B, 0x72, 0x80)),
                TextSize = 11,
                SeparatorsPaint = new SolidColorPaint(new SKColor(0xE5, 0xE7, 0xEB), 1)
            }
        ];

        AddUserCommand = new RelayCommand(_ =>
        {
            var w = Application.Current.MainWindow;
            if (w is null)
                return;
            var created = AdminUserDialogs.PromptAddUser(w);
            if (created is not null)
                OnPropertyChanged(nameof(UserCount));
        });
    }

    private void OnAdminUsersChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Application.Current?.Dispatcher.InvokeAsync(() => OnPropertyChanged(nameof(UserCount)));
    }

    /// <summary>Live count from the admin user list.</summary>
    public int UserCount => SampleDataStore.AdminUsers.Count;

    public int MealCount => SampleDataStore.AdminSummary.TotalMeals;
    public int ExerciseCount => SampleDataStore.AdminSummary.TotalExercises;
    public int UsersDelta { get; }
    public int MealsDelta { get; }
    public int ExercisesDelta { get; }

    public ISeries[] MonthlySeries { get; }
    public Axis[] XAxes { get; }
    public Axis[] YAxes { get; }

    public RelayCommand AddUserCommand { get; }
}
