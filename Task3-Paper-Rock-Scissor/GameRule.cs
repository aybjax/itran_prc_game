using Task3_Paper_Rock_Scissor.Exceptions;
using Task3_Paper_Rock_Scissor.Types;

namespace Task3_Paper_Rock_Scissor;
using OneOf;
public sealed class GameRule
{
    private int ElementCount { get; init; }
    private string[] GameElements { get; init; }
    private Dictionary<string, int> ElementToIndex { get; init; }
    
    private GameRule(string[] gameElements, Dictionary<string, int> elementToIndex)
    {
        GameElements = gameElements;
        ElementCount = gameElements.Length;
        ElementToIndex = elementToIndex;
    }

    public static OneOf<GameRule, Exception> GameRuleFactory(string[] elements)
    {
        var exception = ValidateArgs(elements);
        if (exception is not null) return exception;
        
        var elementIndexes = elements.Select((name, index) => new {name, index })
            .ToDictionary(el => el.name, el => el.index);

        return new GameRule(elements, elementIndexes);
    }

    public string GetMove()
    {
        return GameElements[new Random().Next(ElementCount)];
    }

    public MoveResult GetMoveResult(string actorMoveName, string otherMoveName)
    {
        if (!ElementToIndex.TryGetValue(actorMoveName, out var actorMoveIndex))
        {
            throw new Exception("GameRule::MoveResult: actor move not found");
        }
        
        if (!ElementToIndex.TryGetValue(otherMoveName, out var otherMoveIndex))
        {
            throw new Exception("GameRule::MoveResult: other move not found");
        }
        
        return GetDistance(actorMoveIndex, otherMoveIndex) switch
        {
            0 => MoveResult.Draw,
            < 0 => MoveResult.Win,
            > 0 => MoveResult.Lose,
        };
    }

    private int GetDistance(int actorMoveIndex, int otherMoveIndex)
    {
        var clockwiseDistance = (otherMoveIndex < actorMoveIndex ? otherMoveIndex + ElementCount : otherMoveIndex) - actorMoveIndex;
        var anticlockwiseDistance = (otherMoveIndex > actorMoveIndex ? otherMoveIndex - ElementCount : otherMoveIndex) - actorMoveIndex;

        return Math.Abs(clockwiseDistance) < Math.Abs(anticlockwiseDistance)
            ? clockwiseDistance
            : anticlockwiseDistance;
    }

    private static Exception? ValidateArgs(string[] args)
    {
        if (args.Length < 3 || args.Length % 2 == 0)
        {
            return new OddMoreThanThreeException();
        }

        return args.GroupBy(s => s).Any(g => g.Count() > 1) ? new RepeatingArgumentsGivenException() : null;
    }
}