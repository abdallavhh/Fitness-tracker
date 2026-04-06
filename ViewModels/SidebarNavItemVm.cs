using MaterialDesignThemes.Wpf;

namespace FitnessTracker.ViewModels;

/// <summary>One row in the main window sidebar (icon + label + navigation key).</summary>
public sealed class SidebarNavItemVm
{
    public required string Key { get; init; }
    public required string Title { get; init; }
    public PackIconKind IconKind { get; init; }
}
