using System.Collections.ObjectModel;
using System.Windows;
using FitnessTracker.Data;
using FitnessTracker.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace FitnessTracker.ViewModels;

/// <summary>Exercise cards plus weekly calories line chart (LiveChartsCore), backed by SQLite.</summary>
public sealed class ExerciseLogViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _shell;
    private ISeries[] _seriesCollection = [];
    private Axis[] _xAxes = [];
    private Axis[] _yAxes = [];

    public ExerciseLogViewModel(MainWindowViewModel shell)
    {
        _shell = shell;
        Exercises = AppSession.CurrentUserId is int uid
            ? UserDataQueries.LoadExercises(uid)
            : new ObservableCollection<ExerciseEntryModel>();

        DayLabels = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
        BuildChartAxes();
        RebuildSeries();

        AddExerciseCommand = new RelayCommand(_ => AddExercise(), _ => AppSession.CurrentUserId.HasValue);
        GoBackCommand = new RelayCommand(_ => _shell.NavigateTo("Dashboard"));
    }

    public ObservableCollection<ExerciseEntryModel> Exercises { get; }

    public ISeries[] SeriesCollection
    {
        get => _seriesCollection;
        private set => SetProperty(ref _seriesCollection, value);
    }

    public string[] DayLabels { get; }

    public Axis[] XAxes
    {
        get => _xAxes;
        private set => SetProperty(ref _xAxes, value);
    }

    public Axis[] YAxes
    {
        get => _yAxes;
        private set => SetProperty(ref _yAxes, value);
    }

    public RelayCommand AddExerciseCommand { get; }
    public RelayCommand GoBackCommand { get; }

    private void BuildChartAxes()
    {
        XAxes =
        [
            new Axis
            {
                Labels = DayLabels,
                LabelsPaint = new SolidColorPaint(new SKColor(0x6B, 0x72, 0x80)),
                TextSize = 12,
                SeparatorsPaint = null
            }
        ];
        YAxes =
        [
            new Axis
            {
                Name = "kcal",
                MinLimit = 0,
                LabelsPaint = new SolidColorPaint(new SKColor(0x6B, 0x72, 0x80)),
                TextSize = 12,
                SeparatorsPaint = new SolidColorPaint(new SKColor(0xE5, 0xE7, 0xEB), 1)
            }
        ];
    }

    private void RebuildSeries()
    {
        var values = AppSession.CurrentUserId is int uid
            ? UserDataQueries.WeeklyBurnSeries(uid).Select(v => (double)v).ToArray()
            : new double[7];

        SeriesCollection =
        [
            new LineSeries<double>
            {
                Name = "Calories",
                Values = values,
                Stroke = new SolidColorPaint(new SKColor(0x22, 0xC5, 0x5E), 3),
                Fill = null,
                GeometryFill = new SolidColorPaint(new SKColor(0x22, 0xC5, 0x5E)),
                GeometryStroke = null,
                LineSmoothness = 0.35f
            }
        ];
    }

    private void AddExercise()
    {
        if (AppSession.CurrentUserId is not int uid)
            return;

        var owner = Application.Current.MainWindow;
        if (owner is null)
            return;
        var ex = EntryDialogs.PromptExercise(owner);
        if (ex is null)
            return;

        UserDataQueries.AddExercise(uid, ex);
        Exercises.Clear();
        foreach (var e in UserDataQueries.LoadExercises(uid))
            Exercises.Add(e);
        RebuildSeries();
    }
}
