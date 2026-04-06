using System.Globalization;
using System.Windows;
using FitnessTracker.Data;

namespace FitnessTracker.ViewModels;

/// <summary>User profile fields bound to sample profile data with save to store.</summary>
public sealed class UserProfileViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _shell;
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
        LoadFromStore();
        GoBackCommand = new RelayCommand(_ => _shell.NavigateTo("Dashboard"));
        SaveProfileCommand = new RelayCommand(_ => SaveProfile());
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

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
    public RelayCommand SaveProfileCommand { get; }

    private void LoadFromStore()
    {
        var p = SampleDataStore.Profile;
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

        var p = SampleDataStore.Profile;
        p.Username = Username.Trim();
        p.Gender = Gender.Trim();
        p.Age = age;
        p.HeightCm = h;
        p.WeightKg = w;
        p.MedicalCondition = string.IsNullOrWhiteSpace(MedicalCondition) ? "—" : MedicalCondition.Trim();
        p.DietaryPreference = DietaryPreference.Trim();
        p.TargetWeightKg = tw;

        if (!AppSession.IsAdmin)
            AppSession.DisplayName = p.Username;

        MessageBox.Show("Your profile has been saved.", "Profile", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
