namespace ConnectFour;

public sealed class GameState
{
    public const int Rows = 6;
    public const int Columns = 7;

    private readonly int[,] board = new int[Rows, Columns];
    private readonly List<GameMove> moves = [];

    public int PlayerTurn { get; private set; } = 1;
    public int Winner { get; private set; }
    public bool IsDraw { get; private set; }
    public bool IsOver => Winner != 0 || IsDraw;
    public IReadOnlyList<GameMove> Moves => moves;

    public int PieceAt(int row, int column) => board[row, column];
    public bool IsColumnFull(int column) => board[0, column] != 0;

    public GameMove PlayPiece(int column)
    {
        if (column is < 0 or >= Columns)
            throw new ArgumentOutOfRangeException(nameof(column));
        if (IsOver)
            throw new InvalidOperationException("The game is over. Start a new game to continue.");

        for (var row = Rows - 1; row >= 0; row--)
        {
            if (board[row, column] != 0)
                continue;

            var player = PlayerTurn;
            board[row, column] = player;
            var move = new GameMove(moves.Count + 1, player, column + 1, row + 1);
            moves.Add(move);

            if (HasFourFrom(row, column, player))
                Winner = player;
            else if (moves.Count == Rows * Columns)
                IsDraw = true;
            else
                PlayerTurn = player == 1 ? 2 : 1;

            return move;
        }

        throw new InvalidOperationException("That column is full. Choose another column.");
    }

    public void Reset()
    {
        Array.Clear(board);
        moves.Clear();
        PlayerTurn = 1;
        Winner = 0;
        IsDraw = false;
    }

    private bool HasFourFrom(int row, int column, int player)
    {
        return CountLine(row, column, 0, 1, player) >= 4
            || CountLine(row, column, 1, 0, player) >= 4
            || CountLine(row, column, 1, 1, player) >= 4
            || CountLine(row, column, 1, -1, player) >= 4;
    }

    private int CountLine(int row, int column, int deltaRow, int deltaColumn, int player)
    {
        return 1 + CountDirection(row, column, deltaRow, deltaColumn, player)
            + CountDirection(row, column, -deltaRow, -deltaColumn, player);
    }

    private int CountDirection(int row, int column, int deltaRow, int deltaColumn, int player)
    {
        var count = 0;
        row += deltaRow;
        column += deltaColumn;

        while (row >= 0 && row < Rows && column >= 0 && column < Columns
            && board[row, column] == player)
        {
            count++;
            row += deltaRow;
            column += deltaColumn;
        }

        return count;
    }
}

public sealed record GameMove(int Number, int Player, int Column, int Row);
