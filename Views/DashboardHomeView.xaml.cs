using System.Windows.Controls;
using FitnessTracker.Helpers;
using FitnessTracker.ViewModels;

namespace FitnessTracker.Views;

public partial class DashboardHomeView
{
    public DashboardHomeView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => ApplyArc();
        Loaded += (_, _) => ApplyArc();
    }

    private void ApplyArc()
    {
        if (DataContext is DashboardHomeViewModel vm)
            RadialProgressHelper.ApplyProgress(ProgressArc, vm.DailyGoalFraction);
    }
}
