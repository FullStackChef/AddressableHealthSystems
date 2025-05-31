using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace Client.Services;

public interface ISettingsService
{
    Task<UserSettings> GetAsync(CancellationToken ct = default);
    Task SaveAsync(UserSettings settings, CancellationToken ct = default);
}

public class FileSettingsService : ISettingsService
{
    private readonly string _filePath;

    public FileSettingsService(IWebHostEnvironment env)
        : this(Path.Combine(env.ContentRootPath, "user-settings.json"))
    {
    }

    // For tests
    public FileSettingsService(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<UserSettings> GetAsync(CancellationToken ct = default)
    {
        if (!File.Exists(_filePath))
        {
            return new UserSettings
            {
                DisplayName = "Dr. Taylor Jenkins",
                Email = "tjenkins@greenfield.org",
                Role = "Practitioner",
                ReceiveEmailAlerts = true,
                ShowRealTimeToasts = true
            };
        }

        var json = await File.ReadAllTextAsync(_filePath, ct).ConfigureAwait(false);
        return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
    }

    public async Task SaveAsync(UserSettings settings, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json, ct).ConfigureAwait(false);
    }
}

public class UserSettings
{
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool ReceiveEmailAlerts { get; set; }
    public bool ShowRealTimeToasts { get; set; }
}

