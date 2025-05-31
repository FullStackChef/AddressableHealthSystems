using Client.Services;

namespace Client.Tests;

public class SettingsServiceTests
{
    [Fact]
    public async Task SaveAsync_PersistsSettings()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            var service = new FileSettingsService(tempFile);
            var settings = new UserSettings
            {
                DisplayName = "Test User",
                Email = "test@example.com",
                Role = "Practitioner",
                ReceiveEmailAlerts = true,
                ShowRealTimeToasts = false
            };

            await service.SaveAsync(settings);

            var service2 = new FileSettingsService(tempFile);
            var loaded = await service2.GetAsync();

            Assert.Equal(settings.DisplayName, loaded.DisplayName);
            Assert.Equal(settings.Email, loaded.Email);
            Assert.Equal(settings.Role, loaded.Role);
            Assert.Equal(settings.ReceiveEmailAlerts, loaded.ReceiveEmailAlerts);
            Assert.Equal(settings.ShowRealTimeToasts, loaded.ShowRealTimeToasts);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}

