using System.Collections.ObjectModel;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace FitnessTracker.ViewModels;

/// <summary>
/// Root view model: login surface vs shell, sidebar navigation, and cached feature screens.
/// </summary>
public sealed class MainWindowViewModel : ViewModelBase
{
    /// <summary>Raised when <see cref="IsLoggedIn"/> changes (shell layout / chrome).</summary>
    public event EventHandler? ShellChromeChanged;

    private readonly Dictionary<string, object> _viewCache = new(StringComparer.Ordinal);
    private string _activeNavKey = "Dashboard";
    private object? _currentView;
    private bool _isLoggedIn;
    private readonly LoginPageViewModel _login;

    public MainWindowViewModel()
    {
        _login = new LoginPageViewModel(this);
        NavigateCommand = new RelayCommand(p => Navigate(p?.ToString() ?? string.Empty));
        LogoutCommand = new RelayCommand(_ => Logout(), _ => IsLoggedIn);

        UserNavItems =
        [
            new SidebarNavItemVm { Key = "Dashboard", Title = "Dashboard", IconKind = PackIconKind.ViewDashboardOutline },
            new SidebarNavItemVm { Key = "Profile", Title = "Profile", IconKind = PackIconKind.Account },
            new SidebarNavItemVm { Key = "Meals", Title = "Meals", IconKind = PackIconKind.SilverwareForkKnife },
            new SidebarNavItemVm { Key = "Exercises", Title = "Exercises", IconKind = PackIconKind.Dumbbell },
            new SidebarNavItemVm { Key = "HealthMetrics", Title = "Health Metrics", IconKind = PackIconKind.HeartOutline },
            new SidebarNavItemVm { Key = "Goals", Title = "Goals", IconKind = PackIconKind.Target }
        ];

        AdminNavItems =
        [
            new SidebarNavItemVm { Key = "AdminDashboard", Title = "Dashboard", IconKind = PackIconKind.ViewDashboardOutline },
            new SidebarNavItemVm { Key = "AdminUsers", Title = "Manage Users", IconKind = PackIconKind.AccountGroup },
            new SidebarNavItemVm { Key = "AdminMeals", Title = "Meals", IconKind = PackIconKind.SilverwareForkKnife },
            new SidebarNavItemVm { Key = "AdminExercises", Title = "Exercises", IconKind = PackIconKind.Dumbbell },
            new SidebarNavItemVm { Key = "AdminGoals", Title = "Goals", IconKind = PackIconKind.Target },
            new SidebarNavItemVm { Key = "AdminMetrics", Title = "Metrics", IconKind = PackIconKind.ChartLine },
            new SidebarNavItemVm { Key = "AdminReports", Title = "Reports", IconKind = PackIconKind.FileChart }
        ];
    }

    public LoginPageViewModel Login => _login;

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        private set
        {
            if (!SetProperty(ref _isLoggedIn, value))
                return;
            OnPropertyChanged(nameof(ShowUserShell));
            OnPropertyChanged(nameof(ShowAdminShell));
            ShellChromeChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool ShowUserShell => IsLoggedIn && !AppSession.IsAdmin;
    public bool ShowAdminShell => IsLoggedIn && AppSession.IsAdmin;

    public string ActiveNavKey
    {
        get => _activeNavKey;
        private set => SetProperty(ref _activeNavKey, value);
    }

    public object? CurrentView
    {
        get => _currentView;
        private set
        {
            if (!SetProperty(ref _currentView, value))
                return;
            NavigationOccurred?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>Raised after each navigation (used to close mobile nav drawer).</summary>
    public event EventHandler? NavigationOccurred;

    public ObservableCollection<SidebarNavItemVm> UserNavItems { get; }
    public ObservableCollection<SidebarNavItemVm> AdminNavItems { get; }

    public RelayCommand NavigateCommand { get; }
    public RelayCommand LogoutCommand { get; }

    /// <summary>Called by <see cref="LoginPageViewModel"/> after successful authentication.</summary>
    public void OnLoginCompleted()
    {
        _viewCache.Clear();
        IsLoggedIn = true;
        if (AppSession.IsAdmin)
        {
            ActiveNavKey = "AdminDashboard";
            Navigate("AdminDashboard");
        }
        else
        {
            ActiveNavKey = "Dashboard";
            Navigate("Dashboard");
        }

        LogoutCommand.RaiseCanExecuteChanged();
    }

    /// <summary>Programmatic navigation (e.g. profile shortcut on dashboard).</summary>
    public void NavigateTo(string key) => Navigate(key);

    private void Navigate(string key)
    {
        if (string.IsNullOrEmpty(key))
            return;
        ActiveNavKey = key;
        CurrentView = ResolveView(key);
    }

    private object ResolveView(string key)
    {
        if (_viewCache.TryGetValue(key, out var cached))
            return cached;

        object vm = key switch
        {
            "Dashboard" => new DashboardHomeViewModel(this),
            "Profile" => new UserProfileViewModel(this),
            "Meals" => new MealsViewModel(),
            "Exercises" => new ExerciseLogViewModel(this),
            "HealthMetrics" => new HealthMetricsViewModel(),
            "Goals" => new GoalsViewModel(),
            "AdminDashboard" => new AdminDashboardViewModel(),
            "AdminUsers" => new ManageUsersViewModel(),
            "AdminMeals" => new AdminPlaceholderViewModel("Meals", "Moderate meal library and templates (demo placeholder)."),
            "AdminExercises" => new AdminPlaceholderViewModel("Exercises", "Manage exercise definitions (demo placeholder)."),
            "AdminGoals" => new AdminPlaceholderViewModel("Goals", "Organization goal templates (demo placeholder)."),
            "AdminMetrics" => new AdminPlaceholderViewModel("Metrics", "Aggregate platform metrics (demo placeholder)."),
            "AdminReports" => new AdminPlaceholderViewModel("Reports", "Exports and scheduled reports (demo placeholder)."),
            _ => new DashboardHomeViewModel(this)
        };

        _viewCache[key] = vm;
        return vm;
    }

    private void Logout()
    {
        var r = MessageBox.Show(
            "Are you sure you want to logout?",
            "Logout",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (r != MessageBoxResult.Yes)
            return;

        AppSession.Clear();
        _viewCache.Clear();
        CurrentView = null;
        IsLoggedIn = false;
        _login.Reset();
        LogoutCommand.RaiseCanExecuteChanged();
    }
}
