using System.Text.Json;
using HockeyRecapTracker;

string json = File.ReadAllText("appsettings.json");
AppSettings settings = JsonSerializer.Deserialize<AppSettings>(json)
    ?? throw new InvalidOperationException("The settings file contains no configuration.");

string apiKey = Environment.GetEnvironmentVariable("YOUTUBE_API_KEY")
    ?? throw new InvalidOperationException("Set the YOUTUBE_API_KEY environment variable.");

using var httpClient = new HttpClient();

const string seenVideosPath = "seen-videos.txt";
bool isFirstRun = !File.Exists(seenVideosPath);

var seenVideoIds = isFirstRun
    ? new HashSet<string>(StringComparer.Ordinal)
    : new HashSet<string>(File.ReadLines(seenVideosPath), StringComparer.Ordinal);

foreach (ChannelSettings channel in settings.Channels)
{
    string playlistId = Uri.EscapeDataString(channel.UploadsPlaylistId);
    string url = "https://www.googleapis.com/youtube/v3/playlistItems"
        + $"?part=snippet,contentDetails&maxResults=25&playlistId={playlistId}"
        + $"&key={Uri.EscapeDataString(apiKey)}";

    using HttpResponseMessage response = await httpClient.GetAsync(url);
    Console.WriteLine($"{channel.Name}: YouTube API {(int)response.StatusCode} {response.StatusCode}");
    response.EnsureSuccessStatusCode();

    using Stream responseBody = await response.Content.ReadAsStreamAsync();
    using JsonDocument document = await JsonDocument.ParseAsync(responseBody);

    foreach (JsonElement item in document.RootElement.GetProperty("items").EnumerateArray())
    {
        string? title = item.GetProperty("snippet").GetProperty("title").GetString();
        string? videoId = item.GetProperty("contentDetails").GetProperty("videoId").GetString();

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(videoId))
        {
            continue;
        }

        bool isNewVideo = seenVideoIds.Add(videoId);

        if (isFirstRun || !isNewVideo)
        {
            continue;
        }

        foreach (TeamSettings team in channel.Teams)
        {
            if (VideoFilter.IsMatch(title, team.Aliases, channel.RecapKeywords))
            {
                Console.WriteLine($"{channel.Name}: New recap for {team.Name} (video ID: {videoId})");
            }
        }
    }
}
File.WriteAllLines(seenVideosPath, seenVideoIds);
