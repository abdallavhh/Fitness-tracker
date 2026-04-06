using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FitnessTracker.Data;

namespace FitnessTracker;

public static class AdminUserDialogs
{
    public static UserRecord? PromptAddUser(Window owner)
    {
        var w = new Window
        {
            Title = "Add user",
            Width = 420,
            SizeToContent = SizeToContent.Height,
            MinHeight = 260,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = owner,
            Background = new SolidColorBrush(Color.FromRgb(0xF5, 0xFB, 0xF9)),
            ResizeMode = ResizeMode.NoResize,
            ShowInTaskbar = false
        };

        var root = new StackPanel { Margin = new Thickness(20) };

        void AddLabel(string text) =>
            root.Children.Add(new TextBlock
            {
                Text = text,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x64, 0x74, 0x8B)),
                Margin = new Thickness(0, 12, 0, 4)
            });

        TextBox AddField(string label)
        {
            AddLabel(label);
            var tb = new TextBox { Padding = new Thickness(10, 8, 10, 8), FontSize = 14 };
            root.Children.Add(tb);
            return tb;
        }

        var username = AddField("Username");
        var email = AddField("Email");
        AddLabel("Status");
        var status = new ComboBox { Margin = new Thickness(0, 0, 0, 0), Padding = new Thickness(8, 6, 8, 6) };
        status.Items.Add("Active");
        status.Items.Add("Inactive");
        status.SelectedIndex = 0;
        root.Children.Add(status);

        var btns = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0, 20, 0, 0)
        };
        var ok = new Button { Content = "Save user", Padding = new Thickness(20, 10, 20, 10), Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(20, 10, 20, 10), IsCancel = true };
        UserRecord? created = null;

        ok.Click += (_, _) =>
        {
            var u = username.Text.Trim();
            var em = email.Text.Trim();
            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(em))
            {
                MessageBox.Show(w, "Username and email are required.", "Add user", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SampleDataStore.AdminUsers.Any(x => x.Username.Equals(u, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(w, "That username is already taken.", "Add user", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SampleDataStore.AdminUsers.Any(x => x.Email.Equals(em, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(w, "That email is already registered.", "Add user", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var st = status.SelectedItem?.ToString() ?? "Active";
            created = SampleDataStore.AddAdminUser(u, em, st);
            w.DialogResult = true;
        };
        cancel.Click += (_, _) => { w.DialogResult = false; };
        btns.Children.Add(ok);
        btns.Children.Add(cancel);
        root.Children.Add(btns);
        w.Content = root;

        return w.ShowDialog() == true ? created : null;
    }

    public static bool PromptEditUser(Window owner, UserRecord user)
    {
        var w = new Window
        {
            Title = $"Edit user #{user.Id}",
            Width = 420,
            SizeToContent = SizeToContent.Height,
            MinHeight = 260,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = owner,
            Background = new SolidColorBrush(Color.FromRgb(0xF5, 0xFB, 0xF9)),
            ResizeMode = ResizeMode.NoResize,
            ShowInTaskbar = false
        };

        var root = new StackPanel { Margin = new Thickness(20) };

        void AddLabel(string text) =>
            root.Children.Add(new TextBlock
            {
                Text = text,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x64, 0x74, 0x8B)),
                Margin = new Thickness(0, 12, 0, 4)
            });

        TextBox AddField(string label, string initial)
        {
            AddLabel(label);
            var tb = new TextBox { Padding = new Thickness(10, 8, 10, 8), FontSize = 14, Text = initial };
            root.Children.Add(tb);
            return tb;
        }

        var username = AddField("Username", user.Username);
        var email = AddField("Email", user.Email);
        AddLabel("Status");
        var status = new ComboBox { Margin = new Thickness(0, 0, 0, 0), Padding = new Thickness(8, 6, 8, 6) };
        status.Items.Add("Active");
        status.Items.Add("Inactive");
        status.SelectedItem = user.Status is "Active" or "Inactive" ? user.Status : "Active";

        var btns = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0, 20, 0, 0)
        };
        var ok = new Button { Content = "Save", Padding = new Thickness(20, 10, 20, 10), Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(20, 10, 20, 10), IsCancel = true };

        ok.Click += (_, _) =>
        {
            var u = username.Text.Trim();
            var em = email.Text.Trim();
            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(em))
            {
                MessageBox.Show(w, "Username and email are required.", "Edit user", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SampleDataStore.AdminUsers.Any(x => !ReferenceEquals(x, user) && x.Username.Equals(u, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(w, "That username is already taken.", "Edit user", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SampleDataStore.AdminUsers.Any(x => !ReferenceEquals(x, user) && x.Email.Equals(em, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(w, "That email is already registered.", "Edit user", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            user.Username = u;
            user.Email = em;
            user.Status = status.SelectedItem?.ToString() ?? "Active";
            w.DialogResult = true;
        };
        cancel.Click += (_, _) => { w.DialogResult = false; };
        btns.Children.Add(ok);
        btns.Children.Add(cancel);
        root.Children.Add(btns);
        w.Content = root;

        return w.ShowDialog() == true;
    }
}
