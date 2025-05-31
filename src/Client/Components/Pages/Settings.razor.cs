using Microsoft.AspNetCore.Components;
using Client.Services;

namespace Client.Components.Pages;

public partial class Settings : ComponentBase
{
    [Inject] public ISettingsService SettingsService { get; set; } = default!;
    protected UserProfile profile = new();
    protected UserPreferences preferences = new();

    protected override void OnInitialized()
    {
        // Simulated load
        profile = new UserProfile
        {
            DisplayName = "Dr. Taylor Jenkins",
            Email = "tjenkins@greenfield.org",
            Role = "Practitioner"
        };

        preferences = new UserPreferences
        {
            ReceiveEmailAlerts = true,
            ShowRealTimeToasts = true
        };
    }

    protected async Task Save()
    {
        Console.WriteLine($"[Save] {profile.DisplayName}, email alerts: {preferences.ReceiveEmailAlerts}");

        var settings = new UserSettings
        {
            DisplayName = profile.DisplayName,
            Email = profile.Email,
            Role = profile.Role,
            ReceiveEmailAlerts = preferences.ReceiveEmailAlerts,
            ShowRealTimeToasts = preferences.ShowRealTimeToasts
        };

        await SettingsService.SaveAsync(settings);
    }

    public class UserProfile
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UserPreferences
    {
        public bool ReceiveEmailAlerts { get; set; }
        public bool ShowRealTimeToasts { get; set; }
    }
}

