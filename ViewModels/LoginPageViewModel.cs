using System.Windows;
using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.ViewModels;

/// <summary>Login screen: credentials, validation, and session bootstrap (demo rules).</summary>
public sealed class LoginPageViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _shell;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string? _errorMessage;
    private bool _hasError;
    private bool _maskPassword = true;

    public LoginPageViewModel(MainWindowViewModel shell)
    {
        _shell = shell;
        LoginCommand = new RelayCommand(_ => TryLogin(), _ => true);
        TogglePasswordMaskCommand = new RelayCommand(_ => MaskPassword = !MaskPassword);
        ForgotPasswordCommand = new RelayCommand(_ =>
            MessageBox.Show("Password reset would be emailed here (demo).", "Forgot password",
                MessageBoxButton.OK, MessageBoxImage.Information));
        CreateAccountCommand = new RelayCommand(_ =>
            MessageBox.Show("Registration is not enabled in this demo build.", "Create account",
                MessageBoxButton.OK, MessageBoxImage.Information));
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string? ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool HasError
    {
        get => _hasError;
        set => SetProperty(ref _hasError, value);
    }

    public bool MaskPassword
    {
        get => _maskPassword;
        set => SetProperty(ref _maskPassword, value);
    }

    public RelayCommand LoginCommand { get; }
    public RelayCommand ForgotPasswordCommand { get; }
    public RelayCommand CreateAccountCommand { get; }
    public RelayCommand TogglePasswordMaskCommand { get; }

    /// <summary>Clears fields after logout.</summary>
    public void Reset()
    {
        Username = string.Empty;
        Password = string.Empty;
        MaskPassword = true;
        ClearError();
    }

    private void ClearError()
    {
        HasError = false;
        ErrorMessage = null;
    }

    private void ShowError(string message)
    {
        ErrorMessage = message;
        HasError = true;
    }

    /// <summary>Built-in administrator account (not in SQL seed); use any seeded user for normal login.</summary>
    private const string BuiltinAdminUsername = "admin";
    private const string BuiltinAdminPassword = "admin";

    private void TryLogin()
    {
        ClearError();
        var u = Username.Trim();
        var p = Password ?? string.Empty;

        if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
        {
            ShowError("Please enter both username and password");
            return;
        }

        if (string.Equals(u, BuiltinAdminUsername, StringComparison.OrdinalIgnoreCase))
        {
            if (p != BuiltinAdminPassword)
            {
                ShowError("Invalid username or password");
                Password = string.Empty;
                return;
            }

            ApplyBuiltinAdminSession(u);
            Reset();
            _shell.OnLoginCompleted();
            return;
        }

        using var db = new AppDbContext();
        var user = db.Users.AsNoTracking().FirstOrDefault(x => x.User_Name == u && x.Password == p);
        if (user is null)
        {
            ShowError("Invalid username or password");
            Password = string.Empty;
            return;
        }

        ApplySessionFromUser(user);
        Reset();
        _shell.OnLoginCompleted();
    }

    private static void ApplyBuiltinAdminSession(string usernameEntered)
    {
        AppSession.Username = usernameEntered;
        AppSession.IsAdmin = true;
        AppSession.CurrentUserId = null;
        AppSession.DisplayName = "Administrator";
    }

    private static void ApplySessionFromUser(User user)
    {
        AppSession.Username = user.User_Name;
        AppSession.CurrentUserId = user.User_ID;
        AppSession.IsAdmin = user.IsAdmin;
        AppSession.DisplayName = user.User_Name;
    }
}
