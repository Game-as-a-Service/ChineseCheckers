using ChineseCheckers.Domain;

namespace ChineseCheckers.Tests;

[TestClass]
public class StartGameTest
{
    [Description("""
        Given: 2 players
        When: Start game
        Then: Game is started
        """)]
    [TestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    [DataRow(6)]
    public void CanStartGame(int playerCount)
    {
        // Arrange
        var game = new Game();
        for (var i = 0; i < playerCount; i++)
        {
            game.AddPlayer(new Player($"Player {i}"));
        }

        // Act
        game.Start();

        // Assert
        Assert.IsTrue(game.IsStarted);
    }


    [Description("""
        Given: 1 player
        When: Start game
        Then: Game is not started
        """)]
    [TestMethod]
    [DataRow(1)]
    [DataRow(5)]
    [DataRow(7)]
    public void CannotStartGame(int playerCount)
    {
        // Arrange
        var game = new Game();
        for (var i = 0; i < playerCount; i++)
        {
            game.AddPlayer(new Player($"Player {i}"));
        }

        // Act
        game.Start();

        // Assert
        Assert.IsFalse(game.IsStarted);
    }
}
