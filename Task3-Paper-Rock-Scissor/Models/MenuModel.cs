using Task3_Paper_Rock_Scissor.Types;

namespace Task3_Paper_Rock_Scissor.Models;

public sealed record MenuModel
{
    public required string Message { get; init; }
    public required string OrderIndex { get; init; }
    public required MenuType MenuType { get; init; }

    public static MenuModel Undefined => new MenuModel
    {
        OrderIndex = "",
        Message = "",
        MenuType = MenuType.UndefinedMessage,
    };
}