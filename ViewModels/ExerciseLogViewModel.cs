using System.Collections.ObjectModel;
using System.Windows;
using FitnessTracker.Data;
using FitnessTracker.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace FitnessTracker.ViewModels;

/// <summary>Exercise cards plus weekly calories line chart (LiveChartsCore).</summary>
public sealed class ExerciseLogViewModel : ViewModelBase
{
    public ExerciseLogViewModel(MainWindowViewModel shell)
    {
        Exercises = SampleDataStore.Exercises;
        AddExerciseCommand = new RelayCommand(_ => AddExercise());

        var values = SampleDataStore.WeeklyBurnSeries
            .Select(v => (double)v)
            .ToArray();

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

        DayLabels = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
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
        GoBackCommand = new RelayCommand(_ => shell.NavigateTo("Dashboard"));
    }

    public ObservableCollection<ExerciseEntryModel> Exercises { get; }
    public ISeries[] SeriesCollection { get; }
    public string[] DayLabels { get; }
    public Axis[] XAxes { get; }
    public Axis[] YAxes { get; }
    public RelayCommand AddExerciseCommand { get; }
    public RelayCommand GoBackCommand { get; }

    private void AddExercise()
    {
        var owner = Application.Current.MainWindow;
        if (owner is null)
            return;
        var ex = EntryDialogs.PromptExercise(owner);
        if (ex is not null)
            Exercises.Add(ex);
    }
}
