using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FitnessTracker.Models;

namespace FitnessTracker;

/// <summary>Lightweight modal forms for adding user data (demo app).</summary>
public static class EntryDialogs
{
    private static Window CreateShell(string title, Window owner)
    {
        return new Window
        {
            Title = title,
            Width = 400,
            SizeToContent = SizeToContent.Height,
            MinHeight = 200,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = owner,
            Background = new SolidColorBrush(Color.FromRgb(0xF5, 0xFB, 0xF9)),
            ResizeMode = ResizeMode.NoResize,
            ShowInTaskbar = false
        };
    }

    private static TextBox LabeledField(StackPanel root, string label)
    {
        root.Children.Add(new TextBlock
        {
            Text = label,
            FontWeight = FontWeights.SemiBold,
            Foreground = new SolidColorBrush(Color.FromRgb(0x64, 0x74, 0x8B)),
            Margin = new Thickness(0, 12, 0, 4)
        });
        var tb = new TextBox
        {
            Padding = new Thickness(10, 8, 10, 8),
            FontSize = 14,
            BorderBrush = new SolidColorBrush(Color.FromRgb(0xE2, 0xE8, 0xF0))
        };
        root.Children.Add(tb);
        return tb;
    }

    public static MealEntryModel? PromptMeal(Window owner)
    {
        var w = CreateShell("Add meal", owner);
        var root = new StackPanel { Margin = new Thickness(20) };
        var name = LabeledField(root, "Meal name");
        var cal = LabeledField(root, "Calories (kcal)");
        var icon = LabeledField(root, "Emoji icon (optional)");
        icon.Text = "🍽";

        var btns = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 20, 0, 0) };
        var ok = new Button { Content = "Save", Padding = new Thickness(20, 10, 20, 10), Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(20, 10, 20, 10), IsCancel = true };
        MealEntryModel? result = null;
        ok.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show(w, "Please enter a meal name.", "Add meal", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(cal.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out var c) || c < 0)
            {
                MessageBox.Show(w, "Please enter a valid calorie amount.", "Add meal", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            result = new MealEntryModel
            {
                Name = name.Text.Trim(),
                Calories = c,
                Icon = string.IsNullOrWhiteSpace(icon.Text) ? "🍽" : icon.Text.Trim(),
                TileBg = "#E0F2FE"
            };
            w.DialogResult = true;
        };
        cancel.Click += (_, _) => { w.DialogResult = false; };
        btns.Children.Add(ok);
        btns.Children.Add(cancel);
        root.Children.Add(btns);
        w.Content = root;
        return w.ShowDialog() == true ? result : null;
    }

    public static ExerciseEntryModel? PromptExercise(Window owner)
    {
        var w = CreateShell("Add exercise", owner);
        var root = new StackPanel { Margin = new Thickness(20) };
        var name = LabeledField(root, "Activity name");
        var min = LabeledField(root, "Duration (minutes)");
        var kcal = LabeledField(root, "Calories burned");
        var icon = LabeledField(root, "Emoji (optional)");
        icon.Text = "🏃";

        var btns = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 20, 0, 0) };
        var ok = new Button { Content = "Save", Padding = new Thickness(20, 10, 20, 10), Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(20, 10, 20, 10), IsCancel = true };
        ExerciseEntryModel? result = null;
        ok.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show(w, "Please enter an activity name.", "Add exercise", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(min.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out var m) || m < 0)
            {
                MessageBox.Show(w, "Please enter valid minutes.", "Add exercise", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(kcal.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out var k) || k < 0)
            {
                MessageBox.Show(w, "Please enter valid calories burned.", "Add exercise", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            result = new ExerciseEntryModel
            {
                Name = name.Text.Trim(),
                Minutes = m,
                CaloriesBurned = k,
                Icon = string.IsNullOrWhiteSpace(icon.Text) ? "🏃" : icon.Text.Trim()
            };
            w.DialogResult = true;
        };
        cancel.Click += (_, _) => { w.DialogResult = false; };
        btns.Children.Add(ok);
        btns.Children.Add(cancel);
        root.Children.Add(btns);
        w.Content = root;
        return w.ShowDialog() == true ? result : null;
    }

    public static HealthMetricModel? PromptMetric(Window owner)
    {
        var w = CreateShell("Add health metric", owner);
        var root = new StackPanel { Margin = new Thickness(20) };
        var name = LabeledField(root, "Metric name");
        var val = LabeledField(root, "Value (e.g. 80 bpm)");
        var icon = LabeledField(root, "Emoji (optional)");
        icon.Text = "📌";

        var btns = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 20, 0, 0) };
        var ok = new Button { Content = "Save", Padding = new Thickness(20, 10, 20, 10), Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(20, 10, 20, 10), IsCancel = true };
        HealthMetricModel? result = null;
        ok.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(val.Text))
            {
                MessageBox.Show(w, "Please enter name and value.", "Add metric", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            result = new HealthMetricModel
            {
                Name = name.Text.Trim(),
                Value = val.Text.Trim(),
                Icon = string.IsNullOrWhiteSpace(icon.Text) ? "📌" : icon.Text.Trim(),
                TileBg = "#F0F9FF"
            };
            w.DialogResult = true;
        };
        cancel.Click += (_, _) => { w.DialogResult = false; };
        btns.Children.Add(ok);
        btns.Children.Add(cancel);
        root.Children.Add(btns);
        w.Content = root;
        return w.ShowDialog() == true ? result : null;
    }

    public static GoalModel? PromptGoal(Window owner)
    {
        var w = CreateShell("Add goal", owner);
        var root = new StackPanel { Margin = new Thickness(20) };
        var title = LabeledField(root, "Goal title");
        var target = LabeledField(root, "Target (e.g. 70 kg or 5 sessions)");
        var start = LabeledField(root, "Start date (MM/dd/yyyy)");
        start.Text = DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.CurrentCulture);
        var prog = LabeledField(root, "Progress % (0–100)");
        prog.Text = "0";

        var btns = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 20, 0, 0) };
        var ok = new Button { Content = "Save", Padding = new Thickness(20, 10, 20, 10), Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(20, 10, 20, 10), IsCancel = true };
        GoalModel? result = null;
        ok.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(title.Text) || string.IsNullOrWhiteSpace(target.Text))
            {
                MessageBox.Show(w, "Please enter title and target.", "Add goal", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(prog.Text.Trim(), NumberStyles.Float, CultureInfo.CurrentCulture, out var p) || p is < 0 or > 100)
            {
                MessageBox.Show(w, "Please enter progress between 0 and 100.", "Add goal", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            result = new GoalModel
            {
                Title = title.Text.Trim(),
                TargetLabel = target.Text.Trim(),
                StartDateDisplay = string.IsNullOrWhiteSpace(start.Text) ? DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.CurrentCulture) : start.Text.Trim(),
                ProgressPercent = p,
                StatusLabel = p >= 100 ? "Complete ✓" : "In progress",
                OnTrack = true
            };
            w.DialogResult = true;
        };
        cancel.Click += (_, _) => { w.DialogResult = false; };
        btns.Children.Add(ok);
        btns.Children.Add(cancel);
        root.Children.Add(btns);
        w.Content = root;
        return w.ShowDialog() == true ? result : null;
    }
}
