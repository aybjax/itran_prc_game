using ConsoleTables;
using Task3_Paper_Rock_Scissor.Exceptions;
using Task3_Paper_Rock_Scissor.Messages;
using Task3_Paper_Rock_Scissor.Types;

namespace Task3_Paper_Rock_Scissor;

public class TableGenerator
{
    private readonly Func<string, string, MoveResult> _getMoveResultByActorAndProgram;
    private readonly string[] _args;

    public TableGenerator(string[] args, Func<string, string, MoveResult> getMoveResultByActorAndProgram)
    {
        if (getMoveResultByActorAndProgram.GetInvocationList().Length != 1)
        {
            throw ImpossibleException.StrategyDelegateShouldBeOneException();
        }
        
        _getMoveResultByActorAndProgram = getMoveResultByActorAndProgram;
        _args = args;
    }

    private string? _generatedTable;
    public string Table
    {
        get
        {
            if (_generatedTable is not null)
            {
                return _generatedTable;
            }
            
            var firstRow = new List<string>{ConsoleMessages.TableTopLeftCell};
            firstRow.AddRange(_args);
            var table = new ConsoleTable(firstRow.ToArray());

            foreach (var programMove in _args)
            {
                List<string> row = new () {programMove};
                row.AddRange(_args.Select(actorMove => _getMoveResultByActorAndProgram(actorMove, programMove).ToString()));

                // ReSharper disable once CoVariantArrayConversion
                table.AddRow(row.ToArray());
            }
        
            _generatedTable = table
                .Configure(o => o.EnableCount = false)
                .ToString();;

            return _generatedTable;
        }
    }
}