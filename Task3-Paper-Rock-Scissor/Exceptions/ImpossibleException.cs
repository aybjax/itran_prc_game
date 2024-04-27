using System.Diagnostics.CodeAnalysis;
using Task3_Paper_Rock_Scissor.Types;

namespace Task3_Paper_Rock_Scissor.Exceptions;

public class ImpossibleException(string msg): Exception(msg)
{
    public static ImpossibleException MoveResultChoiceException() =>
        new ($"unknown {nameof(MoveResult)} variant is chosen");

    public static ImpossibleException MenuItemCannotBeNullException() =>
        new("menu item is not found");
    
    public static ImpossibleException StrategyDelegateShouldBeOneException() =>
        new("Strategy delegate should contain only 1 method");
}