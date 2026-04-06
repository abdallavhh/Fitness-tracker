using System.Collections.ObjectModel;
using System.Windows;
using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels;

/// <summary>Goal cards from sample data with add-goal dialog.</summary>
public sealed class GoalsViewModel : ViewModelBase
{
    public GoalsViewModel()
    {
        Goals = SampleDataStore.Goals;
        AddGoalCommand = new RelayCommand(_ => AddGoal());
    }

    public ObservableCollection<GoalModel> Goals { get; }
    public RelayCommand AddGoalCommand { get; }

    private void AddGoal()
    {
        var owner = Application.Current.MainWindow;
        if (owner is null)
            return;
        var g = EntryDialogs.PromptGoal(owner);
        if (g is not null)
            Goals.Add(g);
    }
}
