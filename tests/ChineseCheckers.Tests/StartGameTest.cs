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

    }


    [Description("""
        Given: 1, 5 or 7 player
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

    [Description("""
        Given: 3 players
        When: Start game
        Then: Board is set up with correct pieces
        """)]
    [TestMethod]
    public void SetUpBoardWithThreePlayers()
    {
        // Arrange
        var game = new Game();
        game.AddPlayer(new Player("Player 1"));
        game.AddPlayer(new Player("Player 2"));
        game.AddPlayer(new Player("Player 3"));

        (int x, int y)[] expectedCoordinatesWithRed = [
                (4,0),(5,1),(6,2),(7,3),(8,4),
                (4,1),(5,2),(6,3),(7,4),
                (4,2),(5,3),(6,4),
                (4,3),(5,4),
                (4, 4),
        ];
        (int x, int y)[] expectedCoordinatesWithYellow = [
            (4,8), (5, 9), (6, 10), (7, 11), (8, 12),
            (4, 9), (5, 10), (6, 11), (7, 12),
            (4, 10), (5, 11), (6, 12),
            (4, 11), (5, 12),
            (4, 12),
        ];
        (int x, int y)[] expectedCoordinatesWithGreen = [
            (12,8),(13, 9),  (14, 10), (15, 11), (16, 12),
            (12,9),(13, 10), (14, 11), (15, 12),
            (12,10),(13, 11), (14, 12),
            (12,11),(13, 12),
            (12,12),
        ];

        // Act
        game.Start();

        // Assert
        Assert.IsTrue(IsBoardWithCorrectColors(game.Board, expectedCoordinatesWithRed, PieceColor.Red));
        Assert.IsTrue(IsBoardWithCorrectColors(game.Board, expectedCoordinatesWithYellow, PieceColor.Yellow));
        Assert.IsTrue(IsBoardWithCorrectColors(game.Board, expectedCoordinatesWithGreen, PieceColor.Green));
    }

    [Description("""
        Given: 2 players
        When: Start game
        Then: Board is set up with correct pieces
        """)]
    [TestMethod]
    public void StartGameWillSetUpBoard()
    {
        // Arrange
        var game = new Game();
        game.AddPlayer(new Player("Player 1"));
        game.AddPlayer(new Player("Player 2"));

        var expectedCoordinatesWithRed = new (int x, int y)[]
        {
            (0, 4), (1, 4), (2, 4), (3, 4), (4, 4),
            (1, 5), (2, 5), (3, 5), (4, 5),
            (2, 6), (3, 6), (4, 6),
            (3, 7), (4, 7),
            (4, 8),
        };

        var expectedCoordinatesWithOrange = new (int x, int y)[]
        {
            (12, 4), (11, 4), (10, 4), (9, 4), (8, 4),
            (12, 5), (11, 5), (10, 5), (9, 5),
            (12, 6), (11, 6), (10, 6),
            (12, 7), (11, 7),
            (12, 8),
        };

        var expectedCoordinatesWithNone = new (int x, int y)[]
        {
            (4, 12), (5, 12), (6, 12), (7, 12), (8, 12),
            (4, 11), (5, 11), (6, 11), (7, 11),
            (4, 10), (5, 10), (6, 10),
            (4, 9), (5, 9),
            (4, 8),
        };

        // Act
        game.Start();

        // Assert
        Assert.IsTrue(IsBoardWithCorrectColors(game.Board, expectedCoordinatesWithRed, PieceColor.Red));
        Assert.IsTrue(IsBoardWithCorrectColors(game.Board, expectedCoordinatesWithOrange, PieceColor.Orange));
    }

    public static bool IsBoardWithCorrectColors(Board board, (int x, int y)[] coordinates, PieceColor color)
    {
        foreach (var (x, y) in coordinates)
        {
            if (board[x, y] != color)
            {
                Console.WriteLine($"Expected {color} at ({x}, {y}) but got {board[x, y]}");
                return false;
            }
        }
        return true;
    }
}
