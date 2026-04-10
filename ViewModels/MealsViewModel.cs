using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels;

/// <summary>Meal list with search filter and add-meal dialog (persisted to SQLite).</summary>
public sealed class MealsViewModel : ViewModelBase
{
    private readonly CollectionViewSource _mealsView;
    private string _searchQuery = string.Empty;

    public MealsViewModel()
    {
        var source = AppSession.CurrentUserId is int uid
            ? UserDataQueries.LoadMeals(uid)
            : new ObservableCollection<MealEntryModel>();
        _mealsView = new CollectionViewSource { Source = source };
        _mealsView.Filter += OnMealFilter;
        MealsView = _mealsView.View!;
        AddMealCommand = new RelayCommand(_ => AddMeal(), _ => AppSession.CurrentUserId.HasValue);
    }

    public ICollectionView MealsView { get; }

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (!SetProperty(ref _searchQuery, value))
                return;
            _mealsView.View?.Refresh();
        }
    }

    public RelayCommand AddMealCommand { get; }

    private void OnMealFilter(object sender, FilterEventArgs e)
    {
        if (e.Item is not MealEntryModel m)
        {
            e.Accepted = false;
            return;
        }

        var q = SearchQuery.Trim();
        if (string.IsNullOrEmpty(q))
        {
            e.Accepted = true;
            return;
        }

        e.Accepted = m.Name.Contains(q, StringComparison.OrdinalIgnoreCase);
    }

    private void AddMeal()
    {
        if (AppSession.CurrentUserId is not int uid)
            return;

        var owner = Application.Current.MainWindow;
        if (owner is null)
            return;
        var meal = EntryDialogs.PromptMeal(owner);
        if (meal is null)
            return;

        UserDataQueries.AddMeal(uid, meal);
        if (_mealsView.Source is ObservableCollection<MealEntryModel> col)
        {
            col.Clear();
            foreach (var x in UserDataQueries.LoadMeals(uid))
                col.Add(x);
        }

        _mealsView.View?.Refresh();
    }
}
