namespace HockeyRecapTracker;

public class AppSettings
{
    public List<ChannelSettings> Channels { get; set; } = [];
}

public class ChannelSettings
{
    public string Name { get; set; } = "";
    public List<TeamSettings> Teams { get; set; } = [];
    public List<string> RecapKeywords { get; set; } = [];
}

public class TeamSettings
{
    public string Name { get; set; } = "";
    public List<string> Aliases { get; set; } = [];
}