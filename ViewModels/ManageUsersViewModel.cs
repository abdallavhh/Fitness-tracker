using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using FitnessTracker.Data;

namespace FitnessTracker.ViewModels;

/// <summary>Admin user grid with search / ID filter and edit-delete actions (demo).</summary>
public sealed class ManageUsersViewModel : ViewModelBase
{
    private readonly CollectionViewSource _cvs = new() { Source = SampleDataStore.AdminUsers };
    private bool _filterByExactId;
    private int _exactId;
    private string _generalQuery = string.Empty;
    private string _searchText = string.Empty;
    private string _idFilterText = string.Empty;
    private string? _idMessage;
    private string _footer = string.Empty;

    public ManageUsersViewModel()
    {
        _cvs.Filter += OnFilter;
        SampleDataStore.AdminUsers.CollectionChanged += OnAdminUsersChanged;
        EditUserCommand = new RelayCommand(p =>
        {
            if (p is not UserRecord u)
                return;
            var owner = Application.Current.MainWindow;
            if (owner is null)
                return;
            if (AdminUserDialogs.PromptEditUser(owner, u))
            {
                _cvs.View?.Refresh();
                UpdateFooter();
            }
        });
        DeleteUserCommand = new RelayCommand(p =>
        {
            if (p is not UserRecord u)
                return;
            if (MessageBox.Show($"Delete user ID {u.Id}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;
            SampleDataStore.AdminUsers.Remove(u);
            RefreshFilters();
        });
        AddUserCommand = new RelayCommand(_ =>
        {
            var owner = Application.Current.MainWindow;
            if (owner is null)
                return;
            var created = AdminUserDialogs.PromptAddUser(owner);
            if (created is not null)
                RefreshFilters();
        });

        RefreshFilters();
    }

    private void OnAdminUsersChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Application.Current?.Dispatcher.InvokeAsync(() =>
        {
            RefreshFilters();
        });
    }

    public ICollectionView UsersView => _cvs.View!;

    public string Subtitle => DateTime.Now.ToString("dddd, MMMM d, yyyy", CultureInfo.CurrentCulture);

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (!SetProperty(ref _searchText, value))
                return;
            RefreshFilters();
        }
    }

    public string IdFilterText
    {
        get => _idFilterText;
        set
        {
            if (!SetProperty(ref _idFilterText, value))
                return;
            RefreshFilters();
        }
    }

    public string? IdSearchMessage
    {
        get => _idMessage;
        set
        {
            if (!SetProperty(ref _idMessage, value))
                return;
            OnPropertyChanged(nameof(ShowIdSearchMessage));
        }
    }

    public bool ShowIdSearchMessage => !string.IsNullOrEmpty(_idMessage);

    public string FooterText
    {
        get => _footer;
        set => SetProperty(ref _footer, value);
    }

    public RelayCommand EditUserCommand { get; }
    public RelayCommand DeleteUserCommand { get; }
    public RelayCommand AddUserCommand { get; }

    private void OnFilter(object sender, FilterEventArgs e)
    {
        if (e.Item is not UserRecord u)
        {
            e.Accepted = false;
            return;
        }

        if (_filterByExactId)
        {
            e.Accepted = u.Id == _exactId;
            return;
        }

        if (string.IsNullOrEmpty(_generalQuery))
        {
            e.Accepted = true;
            return;
        }

        var q = _generalQuery;
        e.Accepted = u.Username.ToLowerInvariant().Contains(q)
            || u.Email.ToLowerInvariant().Contains(q);
    }

    private void RefreshFilters()
    {
        var idRaw = IdFilterText.Trim();
        if (int.TryParse(idRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id))
        {
            _filterByExactId = true;
            _exactId = id;
            _cvs.View?.Refresh();

            var match = SampleDataStore.AdminUsers.FirstOrDefault(u => u.Id == id);
            if (match is not null)
                IdSearchMessage = null;
            else
                IdSearchMessage = $"No user with ID {id}.";
        }
        else
        {
            _filterByExactId = false;
            IdSearchMessage = null;
            _generalQuery = SearchText.Trim().ToLowerInvariant();
            _cvs.View?.Refresh();
        }

        UpdateFooter();
    }

    private static int CountVisible(ICollectionView? view)
    {
        if (view is null)
            return 0;
        var n = 0;
        foreach (var _ in view)
            n++;
        return n;
    }

    private void UpdateFooter()
    {
        var total = SampleDataStore.AdminUsers.Count;
        var visible = CountVisible(_cvs.View);
        if (_filterByExactId)
        {
            var exists = SampleDataStore.AdminUsers.Any(u => u.Id == _exactId);
            FooterText = exists
                ? $"User ID {_exactId} — profile shown below."
                : $"No profile for ID {_exactId}.";
        }
        else
        {
            FooterText = visible == total
                ? $"Showing all {total} user(s)."
                : $"Showing {visible} of {total} user(s) matching \"{SearchText.Trim()}\".";
        }
    }
}
