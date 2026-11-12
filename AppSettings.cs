namespace HockeyRecapTracker;

public class AppSettings
{
    public List<ChannelSettings> Channels { get; set; } = [];
    public int CheckIntervalMinutes { get; set; } = 10;
}

public class ChannelSettings
{
    public string Name { get; set; } = "";
    public List<TeamSettings> Teams { get; set; } = [];
    public List<string> RecapKeywords { get; set; } = [];
    public string UploadsPlaylistId { get; set; } = "";
}

public class TeamSettings
{
    public string Name { get; set; } = "";
    public List<string> Aliases { get; set; } = [];
}
