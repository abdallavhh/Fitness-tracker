using System.Windows;
using System.Windows.Input;
using FitnessTracker.ViewModels;

namespace FitnessTracker
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? _vm;
        private bool _mobileNavOpen = false;

        private const double CompactNavBreakpoint = 768;
        private const double SidebarWidth = 250;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainWindowViewModel();
            DataContext = _vm;

            if (_vm != null)
            {
                _vm.NavigationOccurred += (_, _) => CloseMobileNav();
                _vm.ShellChromeChanged += (_, _) =>
                {
                    if (_vm is { IsLoggedIn: false })
                        CloseMobileNav();
                    ApplyResponsiveLayout();
                };
            }

            Loaded += (_, _) => ApplyResponsiveLayout();
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void NavScrim_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseMobileNav();
            e.Handled = true;
        }

        private void CloseMobileNav()
        {
            _mobileNavOpen = false;
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (SidebarPanel == null || NavScrim == null) return;

            if (_vm is not { IsLoggedIn: true })
            {
                NavScrim.Visibility = Visibility.Collapsed;
                SidebarPanel.Visibility = Visibility.Visible;
                return;
            }

            bool compact = ActualWidth > 0 && ActualWidth < CompactNavBreakpoint;

            if (!compact)
            {
                _mobileNavOpen = false;
                NavScrim.Visibility = Visibility.Collapsed;
                SidebarPanel.Visibility = Visibility.Visible;
            }
            else
            {
                SidebarPanel.Visibility = _mobileNavOpen ? Visibility.Visible : Visibility.Collapsed;
                NavScrim.Visibility = _mobileNavOpen ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}