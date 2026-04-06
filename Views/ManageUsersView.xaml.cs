using System.Windows;
using System.Windows.Controls;

namespace FitnessTracker.Views;

public partial class ManageUsersView
{
    private const double FilterStackBreakpoint = 600;

    public ManageUsersView()
    {
        InitializeComponent();
        Loaded += (_, _) => ApplyFilterLayout(ActualWidth);
    }

    private void ManageUsersView_OnSizeChanged(object sender, SizeChangedEventArgs e) =>
        ApplyFilterLayout(e.NewSize.Width);

    private void ApplyFilterLayout(double width)
    {
        if (width <= 0)
            return;

        if (width < FilterStackBreakpoint)
        {
            FilterCol1.Width = new GridLength(0);
            Grid.SetRow(SearchBox, 0);
            Grid.SetColumn(SearchBox, 0);
            Grid.SetRow(IdFilterBox, 1);
            Grid.SetColumn(IdFilterBox, 0);
            SearchBox.Margin = new Thickness(0, 0, 0, 10);
            IdFilterBox.Margin = new Thickness(0, 0, 0, 12);
        }
        else
        {
            FilterCol1.Width = new GridLength(1, GridUnitType.Star);
            Grid.SetRow(SearchBox, 0);
            Grid.SetColumn(SearchBox, 0);
            Grid.SetRow(IdFilterBox, 0);
            Grid.SetColumn(IdFilterBox, 1);
            SearchBox.Margin = new Thickness(0, 0, 8, 12);
            IdFilterBox.Margin = new Thickness(8, 0, 0, 12);
        }
    }
}
