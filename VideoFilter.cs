namespace HockeyRecapTracker;

public static class VideoFilter
{
    public static bool IsMatch(
        string title,
        IEnumerable<string> teamAliases,
        IEnumerable<string> recapKeywords)
    {
        bool matchesTeam = teamAliases.Any(alias =>
            title.Contains(alias, StringComparison.OrdinalIgnoreCase));

        bool matchesRecap = recapKeywords.Any(keyword =>
            title.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        return matchesTeam && matchesRecap;
    }
}