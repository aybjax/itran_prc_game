using System.Collections.Immutable;
using System.Text;
using Sharprompt;
using Task3_Paper_Rock_Scissor.Exceptions;
using Task3_Paper_Rock_Scissor.Messages;
using Task3_Paper_Rock_Scissor.Models;
using Task3_Paper_Rock_Scissor.Types;

namespace Task3_Paper_Rock_Scissor;

public sealed class GameMenuConsole
{
    private string _menuItemsAsString = string.Empty;

    private string MenuItemsAsString
    {
        get
        {
            if (!string.IsNullOrEmpty(_menuItemsAsString))
            {
                return _menuItemsAsString;
            }
            
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(ConsoleMessages.AvailableMoveLineStart);
            
            foreach (var item in MenuList)
            {
                stringBuilder.AppendLine($"{item.OrderIndex} - {item.Message}");
            }

            _menuItemsAsString = stringBuilder.ToString().TrimEnd();

            return _menuItemsAsString;
        }
    }
    private ImmutableArray<MenuModel> MenuList { get; init; }
    private ImmutableDictionary<string, MenuModel> MenuLookUp { get; init; }
    
    public GameMenuConsole(string[] gameElements)
    {
        var menuList = gameElements.Select((el, index) => new MenuModel
        {
            OrderIndex = (1 + index).ToString(),
            Message = el,
            MenuType = MenuType.Move,
        }).ToList();
        menuList.Add(new MenuModel
        {
            OrderIndex = ConsoleMessages.MenuItemMessages.ExitIndex,
            Message = ConsoleMessages.MenuItemMessages.ExitMessage,
            MenuType = MenuType.Exit,
        });
        menuList.Add(new MenuModel
        {
            OrderIndex = ConsoleMessages.MenuItemMessages.HelpIndex,
            Message = ConsoleMessages.MenuItemMessages.HelpMessage,
            MenuType = MenuType.Help,
        });

        MenuList = menuList.ToImmutableArray();
        MenuLookUp = menuList.ToImmutableDictionary(el => el.OrderIndex, el => el);
    }

    public StringBuilder GetMenuMessage(StringBuilder? stringBuilder = null)
    {
        stringBuilder ??= new StringBuilder();

        stringBuilder.AppendLine(MenuItemsAsString);

        return stringBuilder;
    }

    public static void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public static void PrintMessage(StringBuilder message)
    {
        Console.WriteLine(message.ToString().TrimEnd());
    }

    public async Task<MenuModel> GetUserMovePrompt()
    {
        TaskCompletionSource<MenuModel> taskCompletionSource = new();

        await Task.Run(() =>
        {
            var orderIndex = Prompt.Input<string>(ConsoleMessages.EnterMovePrompt);

            if (!MenuLookUp.TryGetValue(orderIndex, out var menuModel))
            {
                menuModel = MenuModel.Undefined;
            }
            
            taskCompletionSource.SetResult(menuModel
                                           ?? throw ImpossibleException.MenuItemCannotBeNullException());
        });
        
        return await taskCompletionSource.Task;
    }
}