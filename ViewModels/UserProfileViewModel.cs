using System.Globalization;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels;

/// <summary>User profile fields loaded from SQLite (dietary preference is UI-only).</summary>
public sealed class UserProfileViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _shell;
    private bool _isEditMode;
    private string _username = string.Empty;
    private string _gender = string.Empty;
    private string _age = string.Empty;
    private string _heightCm = string.Empty;
    private string _weightKg = string.Empty;
    private string _medicalCondition = string.Empty;
    private string _dietaryPreference = string.Empty;
    private string _targetWeightKg = string.Empty;

    public UserProfileViewModel(MainWindowViewModel shell)
    {
        _shell = shell;
        LoadFromDatabase();
        GoBackCommand = new RelayCommand(_ => _shell.NavigateTo("Dashboard"));
        EditCommand = new RelayCommand(_ => IsEditMode = true, _ => !IsEditMode);
        SaveCommand = new RelayCommand(_ => SaveProfile(), _ => IsEditMode);
        UploadImageCommand = new RelayCommand(_ => UploadImage());

        AppSession.ProfileImageChanged += (_, _) => OnPropertyChanged(nameof(ProfileImagePath));
    }

    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            if (!SetProperty(ref _isEditMode, value))
                return;
            EditCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string? ProfileImagePath => AppSession.ProfileImagePath;

    public string Gender
    {
        get => _gender;
        set => SetProperty(ref _gender, value);
    }

    public string Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }

    public string HeightCm
    {
        get => _heightCm;
        set => SetProperty(ref _heightCm, value);
    }

    public string WeightKg
    {
        get => _weightKg;
        set => SetProperty(ref _weightKg, value);
    }

    public string MedicalCondition
    {
        get => _medicalCondition;
        set => SetProperty(ref _medicalCondition, value);
    }

    public string DietaryPreference
    {
        get => _dietaryPreference;
        set => SetProperty(ref _dietaryPreference, value);
    }

    public string TargetWeightKg
    {
        get => _targetWeightKg;
        set => SetProperty(ref _targetWeightKg, value);
    }

    public RelayCommand GoBackCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand SaveCommand { get; }
    public RelayCommand UploadImageCommand { get; }

    private void UploadImage()
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Select Profile Picture",
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
            AppSession.ProfileImagePath = openFileDialog.FileName;
    }

    private void LoadFromDatabase()
    {
        if (AppSession.CurrentUserId is not int uid)
        {
            Username = AppSession.DisplayName;
            return;
        }

        var p = UserDataQueries.LoadUserProfile(uid);
        if (p is null)
        {
            Username = AppSession.DisplayName;
            return;
        }

        Username = p.Username;
        Gender = p.Gender;
        Age = p.Age.ToString(CultureInfo.CurrentCulture);
        HeightCm = p.HeightCm.ToString(CultureInfo.CurrentCulture);
        WeightKg = p.WeightKg.ToString(CultureInfo.CurrentCulture);
        MedicalCondition = p.MedicalCondition;
        DietaryPreference = p.DietaryPreference;
        TargetWeightKg = p.TargetWeightKg.ToString(CultureInfo.CurrentCulture);
    }

    private void SaveProfile()
    {
        if (AppSession.CurrentUserId is not int uid)
        {
            MessageBox.Show("No user profile is loaded for this account.", "Profile", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        if (string.IsNullOrWhiteSpace(Username))
        {
            MessageBox.Show("Username is required.", "Profile", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!int.TryParse(Age, NumberStyles.Integer, CultureInfo.CurrentCulture, out var age) || age < 0 || age > 130)
        {
            MessageBox.Show("Please enter a valid age.", "Profile", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!int.TryParse(HeightCm, NumberStyles.Integer, CultureInfo.CurrentCulture, out var h) || h < 50 || h > 260)
        {
            MessageBox.Show("Please enter a valid height in cm.", "Profile", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!double.TryParse(WeightKg, NumberStyles.Float, CultureInfo.CurrentCulture, out var w) || w < 20 || w > 400)
        {
            MessageBox.Show("Please enter a valid weight.", "Profile", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!double.TryParse(TargetWeightKg, NumberStyles.Float, CultureInfo.CurrentCulture, out var tw) || tw < 20 || tw > 400)
        {
            MessageBox.Show("Please enter a valid target weight.", "Profile", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var p = new UserProfileModel
        {
            Username = Username.Trim(),
            Gender = Gender.Trim(),
            Age = age,
            HeightCm = h,
            WeightKg = w,
            MedicalCondition = string.IsNullOrWhiteSpace(MedicalCondition) ? "—" : MedicalCondition.Trim(),
            DietaryPreference = DietaryPreference.Trim(),
            TargetWeightKg = tw
        };

        UserDataQueries.SaveUserProfile(uid, p);

        if (!AppSession.IsAdmin)
            AppSession.DisplayName = p.Username;

        MessageBox.Show("Your profile has been saved.", "Profile", MessageBoxButton.OK, MessageBoxImage.Information);
        IsEditMode = false;
    }
}
