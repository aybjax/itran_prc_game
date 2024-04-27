using System.Text;
using Task3_Paper_Rock_Scissor.Exceptions;
using Task3_Paper_Rock_Scissor.Messages;
using Task3_Paper_Rock_Scissor.Models;
using Task3_Paper_Rock_Scissor.Types;

namespace Task3_Paper_Rock_Scissor;

public sealed class GameManager(GameRule rule, HmacGenerator hmacGenerator, GameMenuConsole menuConsole, TableGenerator tableGenerator)
{
    public void PrintHmacKeyMessage()
    {
        var builder = new StringBuilder();
        builder.Append(ConsoleMessages.HmacKeyLineStart);
        builder.Append(hmacGenerator.GetKey());
        GameMenuConsole.PrintMessage(builder);
    }

    private void ShowMenuMessage(string computerMove)
    {
        var hmac = hmacGenerator.GetHmac(computerMove);
        var builder = new StringBuilder();
        builder.Append(ConsoleMessages.HmacLineStart);
        builder.AppendLine(hmac);
        builder = menuConsole.GetMenuMessage(builder);
        GameMenuConsole.PrintMessage(builder);
    }
    public async Task RunGameLoop()
    {
        var computerMove = rule.GetMove();
        ShowMenuMessage(computerMove);
        var menuItem = await menuConsole.GetUserMovePrompt();
        var gameEnded = ProcessChoice(menuItem, computerMove);

        if (gameEnded)
        {
            return;
        }
        
        await RunGameLoop();
    }

    private bool ProcessChoice(MenuModel menuItem, string computerMove)
    {
        return menuItem.MenuType switch
        {
            MenuType.Move => ProcessMove(menuItem, computerMove),
            MenuType.Help => ProcessHelp(),
            MenuType.Exit => ProcessExit(),
            MenuType.UndefinedMessage => ProcessUndefinedChoice(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static bool ProcessUndefinedChoice()
    {
        GameMenuConsole.PrintMessage(ConsoleMessages.ResponseToUnknownMove);

        return false;
    }

    private bool ProcessExit()
    {
        GameMenuConsole.PrintMessage(ConsoleMessages.ResponseToExit);
            
        return true;
    }

    private bool ProcessHelp()
    {
        var build = new StringBuilder();
        build.AppendLine(ConsoleMessages.ResponseTitleToTableRequest);
        build.AppendLine(tableGenerator.Table);
        GameMenuConsole.PrintMessage(build);

        return false;
    }

    private bool ProcessMove(MenuModel menuItem, string computerMove)
    {
        var result = rule.GetMoveResult(menuItem.Message, computerMove);
        var (builder, gameEnded) = ProcessGameResult(result, menuItem.Message, computerMove);
        GameMenuConsole.PrintMessage(builder);

        return gameEnded;
    }

    private (StringBuilder builder, bool gameEnded) ProcessGameResult(MoveResult result, string playerMove, string programMove)
    {
        var builder = new StringBuilder();
        builder.Append(ConsoleMessages.PlayerMoveLineStart);
        builder.AppendLine(playerMove);
        builder.Append(ConsoleMessages.ProgramMoveLineStart);
        builder.AppendLine(programMove);
        
        return result switch
        {
            MoveResult.Win => ProcessPlayerWin(builder, playerMove, programMove),
            MoveResult.Draw => ProcessDraw(builder, playerMove),
            MoveResult.Lose => ProcessLost(builder, playerMove, programMove),
            _ => throw ImpossibleException.MoveResultChoiceException(),
        };
    }

    private static (StringBuilder builder, bool gameEnded) ProcessLost(StringBuilder builder, string playerMove, string programMove)
    {
        builder.AppendLine(ConsoleMessages.ResponseToLost);
        
        return (builder, true);
    }

    private static (StringBuilder builder, bool gameEnded) ProcessDraw(StringBuilder builder, string playerMove)
    {
        builder.AppendLine(ConsoleMessages.ResponseToDraw);

        return (builder, false);
    }

    private static (StringBuilder builder, bool gameEnded) ProcessPlayerWin(StringBuilder builder, string playerMove, string programMove)
    {
        builder.AppendLine(ConsoleMessages.ResponseToWin);
        
        return (builder, true);
    }
}