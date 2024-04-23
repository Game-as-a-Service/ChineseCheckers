
using System.Runtime.Serialization;

namespace ChineseCheckers.Domain;

public class Game
{
    public bool IsStarted { get; private set; }
    private readonly Board _board = new();
    private readonly List<Player> _players = [];

    public Board Board => _board;
    public Game()
    {
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    public void Start()
    {
        // 在這裡實作遊戲開始的邏輯
        // 例如，初始化遊戲板、發放棋子等等
        IsStarted = _players.Count is 2 or 3 or 4 or 6;

        if (IsStarted == false)
        {
            return;
        }

        _board.Initialize(_players.Count);
    }

    public void MoveChess(int sx, int sy, int dx, int dy)
    {
        if (_board.IsValidMove(sx, sy, dx, dy) == false)
        {
            throw new InvalidMoveException("Invalid Move");
        }

        var tmp = _board.GetPieceColor(sx, sy);
        _board.SetPieceColor(sx, sy, _board.GetPieceColor(dx, dy));
        _board.SetPieceColor(dx, dy, tmp);
    }
}

public class InvalidMoveException(string message) : Exception(message)
{
}
