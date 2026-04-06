namespace FitnessTracker;

/// <summary>Current session after login (demo app).</summary>
public static class AppSession
{
    private static string _username = string.Empty;
    private static string _displayName = "User";

    public static string Username
    {
        get => _username;
        set => _username = value;
    }

    public static string DisplayName
    {
        get => _displayName;
        set
        {
            if (_displayName == value)
                return;
            _displayName = value;
            DisplayNameChanged?.Invoke(null, EventArgs.Empty);
        }
    }

    private static string? _profileImagePath = null;
    public static string? ProfileImagePath
    {
        get => _profileImagePath;
        set
        {
            if (_profileImagePath == value)
                return;
            _profileImagePath = value;
            ProfileImageChanged?.Invoke(null, EventArgs.Empty);
        }
    }

    public static bool IsAdmin { get; set; }

    /// <summary>Raised when <see cref="DisplayName"/> changes (e.g. profile save).</summary>
    public static event EventHandler? DisplayNameChanged;
    
    /// <summary>Raised when <see cref="ProfileImagePath"/> changes.</summary>
    public static event EventHandler? ProfileImageChanged;

    public static void Clear()
    {
        Username = string.Empty;
        IsAdmin = false;
        DisplayName = "User";
    }
}
