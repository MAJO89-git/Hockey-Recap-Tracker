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

    // Matches regardless of letter casing in the title.
    [Theory]
    [InlineData("COLORADO AVALANCHE HIGHLIGHTS")]
    [InlineData("colorado avalanche highlights")]
    [InlineData("CoLoRaDo AvAlAnChE HiGhLiGhTs")]
    public void IsMatch_WhenCasingDiffers_ReturnsTrue(string title)
    {
        // Arrange
        string[] teamAliases = ["Colorado Avalanche"];
        string[] recapKeywords = ["Highlights"];

        // Act
        bool result = VideoFilter.IsMatch(title, teamAliases, recapKeywords);

        // Assert
        Assert.True(result);
    }

    // Matches NHL highlights with the followed team on either side of the matchup.
    [Theory]
    [InlineData("Devils vs. Avalanche | NHL Highlights | Oct 28, 2026")]
    [InlineData("Avalanche vs. Predators | NHL Highlights | Oct 25, 2026")]
    public void IsMatch_WhenNhlTitleContainsTeamOnEitherSide_ReturnsTrue(
        string title)
    {
        // Arrange
        string[] teamAliases = ["Avalanche"];
        string[] recapKeywords = ["NHL Highlights"];

        // Act
        bool result = VideoFilter.IsMatch(title, teamAliases, recapKeywords);

        // Assert
        Assert.True(result);
    }

    // Rejects NHL highlights that do not include the followed team.
    [Fact]
    public void IsMatch_WhenNhlTitleDoesNotContainFollowedTeam_ReturnsFalse()
    {
        // Arrange
        string title = "Devils vs. Predators | NHL Highlights | Oct 28, 2026";
        string[] teamAliases = ["Avalanche"];
        string[] recapKeywords = ["NHL Highlights"];

        // Act
        bool result = VideoFilter.IsMatch(title, teamAliases, recapKeywords);

        // Assert
        Assert.False(result);
    }
}
