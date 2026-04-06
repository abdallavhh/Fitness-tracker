using System.Windows;
using System.Windows.Controls;

namespace FitnessTracker.Helpers;

/// <summary>Enables two-way binding of <see cref="PasswordBox.Password"/> for MVVM.</summary>
public static class PasswordBoxAssistant
{
    public static readonly DependencyProperty BoundPasswordProperty = DependencyProperty.RegisterAttached(
        "BoundPassword",
        typeof(string),
        typeof(PasswordBoxAssistant),
        new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundPasswordChanged));

    public static string GetBoundPassword(DependencyObject d) => (string)d.GetValue(BoundPasswordProperty);

    public static void SetBoundPassword(DependencyObject d, string value) => d.SetValue(BoundPasswordProperty, value);

    public static readonly DependencyProperty BindPasswordProperty = DependencyProperty.RegisterAttached(
        "BindPassword",
        typeof(bool),
        typeof(PasswordBoxAssistant),
        new PropertyMetadata(false, OnBindPasswordChanged));

    public static bool GetBindPassword(DependencyObject d) => (bool)d.GetValue(BindPasswordProperty);

    public static void SetBindPassword(DependencyObject d, bool value) => d.SetValue(BindPasswordProperty, value);

    private static bool _internalChange;

    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox box || _internalChange)
            return;
        _internalChange = true;
        box.Password = e.NewValue as string ?? string.Empty;
        _internalChange = false;
    }

    private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox box)
            return;
        if ((bool)e.NewValue)
        {
            box.PasswordChanged += OnPasswordChanged;
            SetBoundPassword(box, box.Password);
        }
        else
            box.PasswordChanged -= OnPasswordChanged;
    }

    private static void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox box || _internalChange)
            return;
        _internalChange = true;
        SetBoundPassword(box, box.Password);
        _internalChange = false;
    }
}
