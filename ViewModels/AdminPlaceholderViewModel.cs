namespace FitnessTracker.ViewModels;

/// <summary>Lightweight admin section placeholder for routes without a full screen yet.</summary>
public sealed class AdminPlaceholderViewModel : ViewModelBase
{
    public AdminPlaceholderViewModel(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public string Title { get; }
    public string Message { get; }
}
