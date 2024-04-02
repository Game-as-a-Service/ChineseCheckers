namespace ChineseCheckers.Domain;

public class Game
{
    public bool IsStarted { get; private set; }
    private readonly List<Player> _players = [];

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
            // TODO: throw domain exception
        }
    }
}
