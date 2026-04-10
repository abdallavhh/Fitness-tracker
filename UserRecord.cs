using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace FitnessTracker;

public sealed class UserRecord : INotifyPropertyChanged
{
    private int _id;
    private string _username = string.Empty;
    private string _email = string.Empty;
    private string _status = "Active";

    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public string Username
    {
        get => _username;
        set => SetField(ref _username, value);
    }

    public string Email
    {
        get => _email;
        set => SetField(ref _email, value);
    }

    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }

    private bool _isAdmin;
    public bool IsAdmin
    {
        get => _isAdmin;
        set => SetField(ref _isAdmin, value);
    }

    public string Initials => Username.Length >= 2
        ? Username[..2].ToUpperInvariant()
        : Username.ToUpperInvariant();

    public Brush StatusBackground => Status switch
    {
        "Active" => new SolidColorBrush(Color.FromRgb(0xF0, 0xFF, 0xF4)),
        "Inactive" => new SolidColorBrush(Color.FromRgb(0xFF, 0xF5, 0xF5)),
        _ => new SolidColorBrush(Color.FromRgb(0xEE, 0xF2, 0xFF))
    };

    public Brush StatusForeground => Status switch
    {
        "Active" => new SolidColorBrush(Color.FromRgb(0, 0xA6, 0x93)),
        "Inactive" => new SolidColorBrush(Color.FromRgb(0xFC, 0x81, 0x81)),
        _ => new SolidColorBrush(Color.FromRgb(0x76, 0x5E, 0xE3))
    };

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (Equals(field, value))
            return;
        field = value!;
        OnPropertyChanged(name);
        if (name is nameof(Status))
        {
            OnPropertyChanged(nameof(StatusBackground));
            OnPropertyChanged(nameof(StatusForeground));
        }
    }
}
