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

    public void MoveChess(Point[] moves)
    {
        if (_board.IsValidMoves(moves) == false)
        {
            throw new InvalidMoveException("Invalid Move");
        }

        var tmp = _board.GetPieceColor(moves[0].X, moves[0].Y);
        _board.SetPieceColor(moves[0].X, moves[0].Y, PieceColor.None);
        _board.SetPieceColor(moves[^1].X, moves[^1].Y, tmp);
    }
}

public class InvalidMoveException(string message) : Exception(message)
{
}
