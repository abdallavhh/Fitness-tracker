using System.Collections.ObjectModel;
using System.Windows;
using MaterialDesignThemes.Wpf;
using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels;

/// <summary>WrapPanel of metric cards driven by sample store + add dialog.</summary>
public sealed class HealthMetricsViewModel : ViewModelBase
{
    public HealthMetricsViewModel()
    {
        Metrics = [];
        foreach (var m in SampleDataStore.HealthMetrics)
            Metrics.Add(HealthMetricItemVm.FromModel(m));

        AddMetricCommand = new RelayCommand(_ =>
        {
            var owner = Application.Current.MainWindow;
            if (owner is null)
                return;
            var m = EntryDialogs.PromptMetric(owner);
            if (m is null)
                return;
            SampleDataStore.HealthMetrics.Add(m);
            Metrics.Add(HealthMetricItemVm.FromModel(m));
        });
    }

    public ObservableCollection<HealthMetricItemVm> Metrics { get; }
    public RelayCommand AddMetricCommand { get; }
}
