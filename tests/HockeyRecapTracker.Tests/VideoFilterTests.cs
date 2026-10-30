using HockeyRecapTracker;

namespace HockeyRecapTracker.Tests;

public class VideoFilterTests
{
    // Matches when the title contains both a team alias and a recap keyword.
    [Fact]
    public void IsMatch_WhenTitleContainsTeamAndRecapKeyword_ReturnsTrue()
    {
        // Arrange
        string title = "Colorado Avalanche Highlights";
        string[] teamAliases = ["Colorado Avalanche", "Colorado"];
        string[] recapKeywords = ["Highlights", "Recap"];

        // Act
        bool result = VideoFilter.IsMatch(title, teamAliases, recapKeywords);

        // Assert
        Assert.True(result);
    }
    // Rejects the title when it contains a team alias but no recap keyword.
    [Fact]
    public void IsMatch_WhenTitleContainsTeamButNoRecapKeyword_ReturnsFalse()
    {
        // Arrange
        string title = "Colorado Avalanche Interview";
        string[] teamAliases = ["Colorado Avalanche", "Colorado"];
        string[] recapKeywords = ["Highlights", "Recap"];

        // Act
        bool result = VideoFilter.IsMatch(title, teamAliases, recapKeywords);

        // Assert
        Assert.False(result);
    }
}