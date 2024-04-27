using FluentAssertions;
using Task3_Paper_Rock_Scissor.Types;

namespace Task3_Paper_Rock_Scissor.UnitTests;

public class GameRule_GetMoveResult_Tests
{
    private readonly GameRule _sut;
    private readonly string[] _args = new []{"rock", "spock", "paper", "lizard", "scissors"};

    public GameRule_GetMoveResult_Tests()
    {
        _sut = GameRule.GameRuleFactory(_args).AsT0;
    }
    
    [Fact]
    public void GetMoveResult_ShouldGiveDraw_WhenMovesAreEqual()
    {
        for (int i = 0; i < _args.Length; i++)
        {
            //Arrange
            var actorMove = _args[i];
            var computerMove = _args[i];
                
            //Act
            var result = _sut.GetMoveResult(actorMove, computerMove);
                
            //Assert
            result.Should().Be(MoveResult.Draw);
        }
    }
    
    [Fact]
    public void GetMoveResult_ShouldGiveLose_WhenMovesAreHalfOfNext()
    {
        for (int i = 0; i < _args.Length; i++)
        {
            for (int j = 0; j < _args.Length/2; j++)
            {
                //Arrange
                var actorMove = _args[i];
                var computerMove = _args[(i + 1 + j) % _args.Length];
                
                //Act
                var result = _sut.GetMoveResult(actorMove, computerMove);
                
                //Assert
                result.Should().Be(MoveResult.Lose);
            }
        }
    }
    
    [Fact]
    public void GetMoveResult_ShouldGiveWin_WhenMovesAreHalfOfPrev()
    {
        for (int i = 0; i < _args.Length; i++)
        {
            for (int j = 0; j < _args.Length/2; j++)
            {
                //Arrange
                var actorMove = _args[i];
                var computerMove = _args[(i + _args.Length/2 + 1 + j) % _args.Length];
                
                //Act
                var result = _sut.GetMoveResult(actorMove, computerMove);
                
                //Assert
                result.Should().Be(MoveResult.Win);
            }
        }
    }
}