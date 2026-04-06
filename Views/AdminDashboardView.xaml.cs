using System.Windows;

namespace FitnessTracker.Views;

public partial class AdminDashboardView
{
    private const double BreakpointNarrow = 520;

    public AdminDashboardView()
    {
        InitializeComponent();
        Loaded += (_, _) => ApplyResponsiveLayout(ActualWidth);
    }

    private void AdminDashboardView_OnSizeChanged(object sender, SizeChangedEventArgs e) =>
        ApplyResponsiveLayout(e.NewSize.Width);

    private void ApplyResponsiveLayout(double width)
    {
        if (width <= 0)
            return;

        // Keep primary action usable on narrow widths.
        if (width < BreakpointNarrow)
        {
            BtnAddUser.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        else
        {
            BtnAddUser.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }
}
