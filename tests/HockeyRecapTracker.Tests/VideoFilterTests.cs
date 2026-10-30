using HockeyRecapTracker;

namespace HockeyRecapTracker.Tests;

public class VideoFilterTests
{
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
}