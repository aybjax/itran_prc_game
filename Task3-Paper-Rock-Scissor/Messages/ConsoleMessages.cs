namespace Task3_Paper_Rock_Scissor.Messages;

public static class ConsoleMessages
{
    public const string HmacKeyLineStart = "HMAC key: ";
    public const string HmacLineStart = "HMAC: ";
    public const string PlayerMoveLineStart = "Your move: ";
    public const string ProgramMoveLineStart = "Computer move: ";
    public const string AvailableMoveLineStart = "Available moves:";
    public const string ResponseToUnknownMove = "Please select from menu above. Unknown move detected!!!";
    public const string ResponseToExit = "Ending the game as requested...";
    public const string ResponseTitleToTableRequest = "Table of game results:";
    public const string ResponseToLost = "You lose!";
    public const string ResponseToDraw = "Draw! Try another move";
    public const string ResponseToWin = "You win!";
    public const string EnterMovePrompt = "Enter your move";
    public const string TableTopLeftCell = @"v PC\User >";

    public static class MenuItemMessages
    {
        public const string ExitIndex = "0";
        public const string ExitMessage = "exit";
        public const string HelpIndex = "?";
        public const string HelpMessage = "help";
    }
}