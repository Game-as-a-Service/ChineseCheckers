using ChineseCheckers.Domain;
using FluentAssertions;

namespace ChineseCheckers.Tests;

public class GameBuilder
{
    private readonly List<string> _players = [];
    private readonly List<(int x, int y, PieceColor color)> _pieces = [];
    private bool _isStarted;

    public GameBuilder()
    {
    }

    public GameBuilder WithPlayers(params string[] players)
    {
        _players.AddRange(players);
        return this;
    }

    public GameBuilder Started(bool isStarted)
    {
        _isStarted = isStarted;
        return this;
    }

    public GameBuilder WithBoard(int x, int y, PieceColor color)
    {
        _pieces.Add((x, y, color));
        return this;
    }

    public Game Build()
    {
        var game = new Game();
        foreach (var name in _players)
        {
            game.AddPlayer(new Player(name));
        }

        if (_isStarted)
        {
            game.Start();
        }

        foreach (var (x, y, color) in _pieces)
        {
            game.Board.SetPieceColor(x, y, color);
        }

        return game;
    }
}



[TestClass]
public class MoveChessTest
{
    [Description("""
        Given: (4, 0) is red, (5, 1) is none
        When: (4, 0) move to (5, 1)
        Then: (4, 0) is none, (5, 1) is red
        """)]
    [TestMethod]
    public void CanMoveChessButtomRight()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(4, 0, PieceColor.Red)
            .WithBoard(5, 1, PieceColor.None)
            .Started(true)
            .Build();

        // Act
        game.MoveChess([new Point(4, 0), new Point(5, 1)]);

        // Assert
        game.Board.GetPieceColor(4, 0).Should().Be(PieceColor.None);
        game.Board.GetPieceColor(5, 1).Should().Be(PieceColor.Red);
    }

    [Description("""
        Given: (4, 0) is red, (4, 1) is none
        When: (4, 0) move to (4, 1)
        Then: (4, 0) is none, (4, 1) is red
        """)]
    [TestMethod]
    public void CanMoveChessButtomLeft()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(4, 0, PieceColor.Red)
            .WithBoard(4, 1, PieceColor.None)
            .Started(true)
            .Build();

        // Act
        game.MoveChess([new Point(4, 0), new Point(4, 1)]);

        // Assert
        game.Board.GetPieceColor(4, 0).Should().Be(PieceColor.None);
        game.Board.GetPieceColor(4, 1).Should().Be(PieceColor.Red);
    }

    [Description("""
        Given: (4, 0) is red, (5, 1) is red, (6, 2) is none
        When: (4, 0) jump to (6, 2)
        Then: Success
        """)]
    [TestMethod]
    public void CanJumpChessButtomRight()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(4, 0, PieceColor.Red)
            .WithBoard(5, 1, PieceColor.Red)
            .WithBoard(6, 2, PieceColor.None)
            .Started(true)
            .Build();

        // Act
        game.MoveChess([new Point(4, 0), new Point(6, 2)]);

        // Assert
        game.Board.GetPieceColor(4, 0).Should().Be(PieceColor.None);
        game.Board.GetPieceColor(5, 1).Should().Be(PieceColor.Red);
        game.Board.GetPieceColor(6, 2).Should().Be(PieceColor.Red);
    }

    [Description("""
        Given: (4, 0) is red, (5, 1) is none, (6, 2) is none
        When: (4, 0) jump to (6, 2)
        Then: InvalidMoveException
        """)]
    [TestMethod]
    public void CanNotJumpChessButtomRightWhenMiddleNone()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(4, 0, PieceColor.Red)
            .WithBoard(5, 1, PieceColor.None)
            .WithBoard(6, 2, PieceColor.None)
            .Started(true)
            .Build();

        // Act
        var act = () => game.MoveChess([new Point(4, 0), new Point(6, 2)]);

        // Assert
        act.Should().Throw<InvalidMoveException>();
    }

    [Description("""
        Given: (4, 0) is red
        When: (4, 0) move to (5, 2)
        Then: InvalidMoveException
        """)]
    [TestMethod]
    public void CanNotMoveChessButtom()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(4, 0, PieceColor.Red)
            .Started(true)
            .Build();

        // Act
        var act = () => game.MoveChess([new Point(4, 0), new Point(5, 2)]);

        // Assert
        act.Should().Throw<InvalidMoveException>();
    }

    [Description("""
        Given: (4, 3) is red, (5, 2) is none
        When: (4, 3) move to (5, 2)
        Then: InvalidMoveException
        """)]
    [TestMethod]
    public void CanNotMoveChessTopRightRight()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(4, 3, PieceColor.Red)
            .WithBoard(5, 2, PieceColor.None)
            .Started(true)
            .Build();

        // Act
        var act = () => game.MoveChess([new Point(4, 3), new Point(5, 2)]);

        // Assert
        act.Should().Throw<InvalidMoveException>();
    }

    [Description("""
        Given: (7, 3) is red
        When: (7, 3) move to (5, 2)
        Then: InvalidMoveException
        """)]
    [TestMethod]
    public void CanNotMoveChessTopLeftLeft()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(7, 3, PieceColor.Red)
            .Started(true)
            .Build();

        // Act
        var act = () => game.MoveChess([new Point(7, 3), new Point(5, 2)]);

        // Assert
        act.Should().Throw<InvalidMoveException>();
    }

    [Description("""
        Given: (3, 4) is red, (4, 3) is red
        When: (3, 4) jump to (5, 2)
        Then: InvalidMoveException
        """)]
    [TestMethod]
    public void CanNotJumpChessTopRightRight()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(3, 4, PieceColor.Red)
            .WithBoard(4, 3, PieceColor.Red)
            .Started(true)
            .Build();

        // Act
        var act = () => game.MoveChess([new Point(3, 4), new Point(5, 2)]);

        // Assert
        act.Should().Throw<InvalidMoveException>();
    }

    [Description("""
        Given: (4, 0) is red, (5, 1) is red, (6, 2) is none, (4, 0) jump to (6, 2)
        When: (6, 2) move to (6, 3)
        Then: InvalidMoveException
        """)]
    [TestMethod]
    public void CanNotMoveChessButtomLeftAfterJumpChess()
    {
        // Arrange
        var game = new GameBuilder()
            .WithPlayers("Player 1", "Player 2")
            .WithBoard(4, 0, PieceColor.Red)
            .WithBoard(5, 1, PieceColor.Red)
            .WithBoard(6, 2, PieceColor.None)
            .Started(true)
            .Build();

        // Act
        var act = () => game.MoveChess([new Point(4, 0), new Point(6, 2), new Point(6, 3)]);

        // Assert
        act.Should().Throw<InvalidMoveException>();
    }

}
