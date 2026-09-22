# CSE 325 W04 — Connect Four with Blazor

A two-player Connect Four game built with a .NET 8 Blazor Web App, following the [Microsoft Learn module](https://learn.microsoft.com/en-us/training/modules/dotnet-connect-four/).

## Features

- Interactive 7-column × 6-row board with alternating turns.
- Horizontal, vertical, and diagonal win detection; full-board draw detection.
- Full columns and all moves after game over are disabled.
- New game button clears the board and starts with Player 1.
- Board and player colors are passed as Blazor component parameters.
- **Additional W04 feature:** a live move history lists each move's number, player, column, and landing row. New game clears the history.
- Accessible button labels, keyboard focus styling, and status announcements.

## Run

Requires the .NET 8 SDK.

```powershell
dotnet run --project src/ConnectFour/ConnectFour.csproj
```

Open the local URL printed by the command. Two people can play on the same screen.

## Verify game logic

```powershell
dotnet run --project tests/ConnectFour.Checks/ConnectFour.Checks.csproj
```

The small dependency-free test runner checks win detection, move history, full columns, and reset behavior.

## Video

See [VIDEO-GUIDE.md](VIDEO-GUIDE.md) for a short Spanish narration script and exactly what to demonstrate. The video must be recorded with the student's own voice.
