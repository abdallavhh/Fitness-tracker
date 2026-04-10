using System.Collections.ObjectModel;
using System.Windows;
using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels;

/// <summary>Goal cards from SQLite with add-goal dialog.</summary>
public sealed class GoalsViewModel : ViewModelBase
{
    public GoalsViewModel()
    {
        Goals = AppSession.CurrentUserId is int uid
            ? UserDataQueries.LoadGoals(uid)
            : new ObservableCollection<GoalModel>();
        AddGoalCommand = new RelayCommand(_ => AddGoal(), _ => AppSession.CurrentUserId.HasValue);
    }

    public ObservableCollection<GoalModel> Goals { get; }
    public RelayCommand AddGoalCommand { get; }

    private void AddGoal()
    {
        if (AppSession.CurrentUserId is not int uid)
            return;

        var owner = Application.Current.MainWindow;
        if (owner is null)
            return;
        var g = EntryDialogs.PromptGoal(owner);
        if (g is null)
            return;

        UserDataQueries.AddGoal(uid, g);
        Goals.Clear();
        foreach (var x in UserDataQueries.LoadGoals(uid))
            Goals.Add(x);
    }
}
