using System.Collections.ObjectModel;
using System.Windows;
using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels;

/// <summary>WrapPanel of metric cards from SQLite + add dialog.</summary>
public sealed class HealthMetricsViewModel : ViewModelBase
{
    public HealthMetricsViewModel()
    {
        Metrics = [];
        ReloadMetrics();

        AddMetricCommand = new RelayCommand(_ =>
        {
            if (AppSession.CurrentUserId is not int uid)
                return;
            var owner = Application.Current.MainWindow;
            if (owner is null)
                return;
            var m = EntryDialogs.PromptMetric(owner);
            if (m is null)
                return;
            UserDataQueries.AddHealthMetric(uid, m);
            ReloadMetrics();
        }, _ => AppSession.CurrentUserId.HasValue);
    }

    public ObservableCollection<HealthMetricItemVm> Metrics { get; }
    public RelayCommand AddMetricCommand { get; }

    private void ReloadMetrics()
    {
        Metrics.Clear();
        if (AppSession.CurrentUserId is not int uid)
            return;
        foreach (var m in UserDataQueries.LoadHealthMetricModels(uid))
            Metrics.Add(HealthMetricItemVm.FromModel(m));
    }
}
