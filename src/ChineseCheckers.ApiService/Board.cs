using System.Text;

namespace ChineseCheckers.ApiService;

// Q1: 棋子要怎麼表示? 
// 已知 棋子 有 6 種顏色，分別是 紅、橙、黃、綠、藍、紫
// 棋子的位置是由 2 個座標表示，分別是 x, y
// 棋子的座標範圍是 0 ~ 16


public enum PieceColor
{
    Block = -1,
    None = 0,
    Red = 1,
    Orange = 2,
    Yellow = 3,
    Green = 4,
    Blue = 5,
    Purple = 6,
}

public class Board
{
    private readonly int[,] _board;

    // 棋盤 Area
    public (int x, int y)[]
        top = [
            // x-y<=4, y<4, x>=4
            (4,0),(5,1),(6,2),(7,3),
            (4,1),(5,2),(6,3),
            (4,2),(5,3),
            (4,3),
        ],
        topRight = [
            // x-y>4, y<=12, x>=4
            (9,4),(10,4),(11,4),(12,4),
                  (10,5),(11,5),(12,5),
                         (11,6),(12,6),
                                (12,7), 
        ],
        topLeft = [
            // x-y>=-4, y>=4, x<4
            (0,4),(1,4),(2,4),(3,4),
                  (1,5),(2,5),(3,5),
                        (2,6),(3,6),
                              (3,7),
        ],
        bottom = [
            // x-y>=-4, y>12, x<=12
            (9, 13),(10, 13),(11, 13),(12, 13),
                    (10, 14),(11, 14),(12, 14),
                             (11, 15),(12, 15),
                                      (12, 16),
        ],
        bottomRight = [
            // x-y<=4, y<=12, x>12
            (13, 9),  (14, 10), (15, 11), (16, 12),
            (13, 10), (14, 11), (15, 12),
            (13, 11), (14, 12),
            (13, 12),
        ],
        bottomLeft = [
            // x-y<-4, y<=12, x>=4
            (4, 9), (5, 10), (6, 11), (7, 12),
            (4, 10), (5, 11), (6, 12),
            (4, 11), (5, 12),
            (4, 12),
        ],
        center =
        [
            // x>=4, x<=12, y>=4, y<=12, x-y>=-4, x-y<=4
            (4,4),(5,4),(6,4), (7,4), (8,4),
            (4,5),(5,5),(6,5), (7,5), (8,5), (9,5),
            (4,6),(5,6),(6,6), (7,6), (8,6), (9,6), (10,6),
            (4,7),(5,7),(6,7), (7,7), (8,7), (9,7), (10,7), (11,7),
            (4,8),(5,8),(6,8), (7,8), (8,8), (9,8), (10,8), (11,8), (12,8),
                  (5,9),(6,9), (7,9), (8,9), (9,9), (10,9), (11,9), (12,9),
                        (6,10),(7,10),(8,10),(9,10),(10,10),(11,10),(12,10),
                               (7,11),(8,11),(9,11),(10,11),(11,11),(12,11),
                                      (8,12),(9,12),(10,12),(11,12),(12,12),
        ]
        ;

    public Board(int players)
    {
        // 初始化棋盤
        _board = new int[17, 17];

        Initialize(players);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int x = 0; x < 17; x++)
        {
            sb.Append(new string(' ', 16 - x));
            for (int y = 0; y < 17; y++)
            {
                sb.Append((PieceColor)_board[x, y]).Append(' ');
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private string GetPieceSymbol(PieceColor pieceColor)
    {
        return "*";
    }

    private void Initialize(int players)
    {
        // 初始化棋盤
        FillAll(PieceColor.Block);

        // 中心區域
        FillArea(center, PieceColor.None);

        // 玩家數量
        switch (players)
        {
            case 2:
                FillArea(topLeft, PieceColor.Red);
                FillArea(topRight, PieceColor.Orange);
                FillArea(bottomLeft, PieceColor.None);
                FillArea(bottomRight, PieceColor.None);
                FillArea(top, PieceColor.None);
                FillArea(bottom, PieceColor.None);
                break;
            case 3:
                FillArea(topLeft, PieceColor.Red);
                FillArea(topRight, PieceColor.Orange);
                FillArea(bottomLeft, PieceColor.None);
                FillArea(bottomRight, PieceColor.None);
                FillArea(top, PieceColor.None);
                FillArea(bottom, PieceColor.Purple);
                break;
            case 4:
                FillArea(topLeft, PieceColor.Red);
                FillArea(topRight, PieceColor.Orange);
                FillArea(bottomLeft, PieceColor.Yellow);
                FillArea(bottomRight, PieceColor.Green);
                FillArea(top, PieceColor.None);
                FillArea(bottom, PieceColor.None);
                break;
            case 6:
                FillArea(topLeft, PieceColor.Red);
                FillArea(topRight, PieceColor.Orange);
                FillArea(bottomLeft, PieceColor.Yellow);
                FillArea(bottomRight, PieceColor.Green);
                FillArea(top, PieceColor.Blue);
                FillArea(bottom, PieceColor.Purple);
                break;
            default:
                throw new ArgumentException("Invalid players");
        }
    }


    private void FillArea((int x, int y)[] area, PieceColor color)
    {
        foreach (var (x, y) in area)
        {
            _board[x, y] = (int)color;
        }
    }

    private void FillAll(PieceColor block)
    {
        for (int i = 0; i < 17; i++)
        {
            for (int j = 0; j < 17; j++)
            {
                _board[i, j] = (int)block;
            }
        }
    }

    public PieceColor GetPieceColor(int x, int y)
    {
        return (PieceColor)_board[x, y];
    }

    public void SetPieceColor(int x, int y, PieceColor color)
    {
        _board[x, y] = (int)color;
    }

    public bool IsValidMove(int fromX, int fromY, int toX, int toY)
    {
        // 檢查是否超出絕對邊界
        if (toX < 0 || toX > 16 || toY < 0 || toY > 16)
        {
            return false;
        }

        // From 必須要有棋子
        if (_board[fromX, fromY] <= (int)PieceColor.None)
        {
            return false;
        }

        // To 必須是空位
        if (_board[toX, toY] != (int)PieceColor.None)
        {
            return false;
        }

        // 檢查是否為一步移動
        if (Math.Abs(fromX - toX) is 0 or 1 && Math.Abs(fromY - toY) is 0 or 1)
        {
            return true;
        }

        // 檢查是否為跳躍移動
        if (Math.Abs(fromX - toX) is 0 or 2 && Math.Abs(fromY - toY) is 0 or 2)
        {
            int middleX = (fromX + toX) / 2;
            int middleY = (fromY + toY) / 2;

            if (_board[middleX, middleY] > (int)PieceColor.None)
            {
                return true;
            }
        }

        return false;
    }
}

