using System.Windows;
using FitnessTracker.Data;
using LiveChartsCore;
using Microsoft.EntityFrameworkCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace FitnessTracker.ViewModels;

/// <summary>Admin home: KPI cards from SQLite and monthly exercise calories column chart.</summary>
public sealed class AdminDashboardViewModel : ViewModelBase
{
    public AdminDashboardViewModel()
    {
        RefreshCountsFromDatabase();
        BuildMonthlyChart();

        AddUserCommand = new RelayCommand(_ =>
        {
            var w = Application.Current.MainWindow;
            if (w is null)
                return;
            var created = AdminUserDialogs.PromptAddUser(w);
            if (created is not null)
            {
                RefreshCountsFromDatabase();
                BuildMonthlyChart();
            }
        });
    }

    private void RefreshCountsFromDatabase()
    {
        using var db = new AppDbContext();
        UserCount = db.Users.Count();
        MealCount = db.Meals.Count();
        ExerciseCount = db.ExerciseLogs.Count();
        OnPropertyChanged(nameof(UserCount));
        OnPropertyChanged(nameof(MealCount));
        OnPropertyChanged(nameof(ExerciseCount));
    }

    private void BuildMonthlyChart()
    {
        using var db = new AppDbContext();
        var today = DateTime.Today;
        var start = new DateTime(today.Year, today.Month, 1).AddMonths(-5);
        var end = today.AddDays(1);
        var rows = db.ExerciseLogs
            .Include(e => e.DailyLog)
            .AsEnumerable()
            .Where(e => e.DailyLog != null && e.DailyLog.Log_Date is { } dt && dt >= start && dt <= end)
            .GroupBy(e =>
            {
                var dt = e.DailyLog.Log_Date!.Value;
                return new DateTime(dt.Year, dt.Month, 1);
            })
            .Select(g => new { Month = g.Key, Total = g.Sum(x => x.Calories_Burned ?? 0) })
            .OrderBy(x => x.Month)
            .ToList();

        var labels = rows.Select(x => x.Month.ToString("MMM", System.Globalization.CultureInfo.CurrentCulture)).ToArray();
        var totals = rows.Select(x => (double)x.Total).ToArray();
        var max = totals.Length == 0 ? 1 : totals.Max();
        var heights = totals.Select(t => max <= 0 ? 0.0 : 100.0 * t / max).ToArray();

        MonthlySeries =
        [
            new ColumnSeries<double>
            {
                Name = "Calories burned",
                Values = heights,
                Fill = new SolidColorPaint(new SKColor(0, 0xA6, 0x93)),
                Stroke = null
            }
        ];

        XAxes =
        [
            new Axis
            {
                Labels = labels.Length > 0 ? labels : new[] { "—" },
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
                MaxLimit = 100,
                LabelsPaint = new SolidColorPaint(new SKColor(0x6B, 0x72, 0x80)),
                TextSize = 11,
                SeparatorsPaint = new SolidColorPaint(new SKColor(0xE5, 0xE7, 0xEB), 1)
            }
        ];

        OnPropertyChanged(nameof(MonthlySeries));
        OnPropertyChanged(nameof(XAxes));
        OnPropertyChanged(nameof(YAxes));
    }

    public int UserCount { get; private set; }
    public int MealCount { get; private set; }
    public int ExerciseCount { get; private set; }

    /// <summary>Static demo deltas (no historical snapshots in DB).</summary>
    public int UsersDelta => 0;
    public int MealsDelta => 0;
    public int ExercisesDelta => 0;

    public ISeries[] MonthlySeries { get; private set; } = [];
    public Axis[] XAxes { get; private set; } = [];
    public Axis[] YAxes { get; private set; } = [];

    public RelayCommand AddUserCommand { get; }
}
