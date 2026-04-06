using System.Windows;
using FitnessTracker.Data;

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

        if (!ValidateCredentials(u, p))
        {
            ShowError("Invalid username or password");
            Password = string.Empty;
            return;
        }

        ApplySession(u);
        Reset();
        _shell.OnLoginCompleted();
    }

    private static bool ValidateCredentials(string username, string password)
    {
        if (username.Equals("demo", StringComparison.OrdinalIgnoreCase)
            && password.Equals("password", StringComparison.Ordinal))
            return true;

        if (username.Equals("admin", StringComparison.OrdinalIgnoreCase))
        {
            return password.Equals("password", StringComparison.Ordinal)
                || password.Equals("admin", StringComparison.Ordinal)
                || password.Equals("!", StringComparison.Ordinal);
        }

        return false;
    }

    private static void ApplySession(string username)
    {
        AppSession.Username = username;
        if (username.Equals("admin", StringComparison.OrdinalIgnoreCase))
        {
            AppSession.IsAdmin = true;
            AppSession.DisplayName = "Admin";
        }
        else
        {
            AppSession.IsAdmin = false;
            AppSession.DisplayName = SampleDataStore.Profile.Username;
        }
    }
}
