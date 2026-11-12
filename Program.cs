using System.Text.Json;
using HockeyRecapTracker;

string json = File.ReadAllText("appsettings.json");
AppSettings settings = JsonSerializer.Deserialize<AppSettings>(json)
    ?? throw new InvalidOperationException("The settings file contains no configuration.");

foreach (ChannelSettings channel in settings.Channels)
{
    foreach (TeamSettings team in channel.Teams)
    {
        Console.WriteLine($"{channel.Name}: {team.Name}");
    }
}