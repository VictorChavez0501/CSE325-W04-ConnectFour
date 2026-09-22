using ConnectFour;

var checks = 0;

void Check(bool condition, string name)
{
    if (!condition)
        throw new Exception($"FAILED: {name}");

    Console.WriteLine($"PASS: {name}");
    checks++;
}

var game = new GameState();
Check(game.Moves.Count == 0 && game.PlayerTurn == 1, "initial state");

var first = game.PlayPiece(0);
Check(first == new GameMove(1, 1, 1, 6), "first move records player, column, and landing row");
Check(game.PieceAt(5, 0) == 1 && game.PlayerTurn == 2, "piece drops and turn alternates");

game.Reset();
foreach (var column in new[] { 0, 1, 0, 1, 0, 1, 0 }) game.PlayPiece(column);
Check(game.Winner == 1 && game.IsOver, "vertical win");
Check(game.Moves.Count == 7, "all moves retained in history");
try { game.PlayPiece(2); throw new Exception("Post-win move was allowed"); }
catch (InvalidOperationException) { Check(true, "post-win move rejected"); }

game.Reset();
Check(game.Moves.Count == 0 && game.Winner == 0 && game.PlayerTurn == 1, "reset clears game and history");
foreach (var column in new[] { 0, 0, 1, 1, 2, 2, 3 }) game.PlayPiece(column);
Check(game.Winner == 1, "horizontal win");

game.Reset();
foreach (var column in new[] { 0, 0, 0, 0, 0, 0 }) game.PlayPiece(column);
Check(game.IsColumnFull(0), "full column detected");
try { game.PlayPiece(0); throw new Exception("Full-column move was allowed"); }
catch (InvalidOperationException) { Check(game.Moves.Count == 6, "full column does not add history"); }

game.Reset();
foreach (var column in new[] { 0, 1, 1, 2, 3, 2, 2, 3, 4, 3, 3 }) game.PlayPiece(column);
Check(game.Winner == 1, "diagonal win");

Console.WriteLine($"{checks} checks passed.");
