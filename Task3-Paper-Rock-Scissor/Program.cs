using Task3_Paper_Rock_Scissor;

var ruleResult = GameRule.GameRuleFactory(args);

if (ruleResult.IsT1)
{
    Console.WriteLine(ruleResult.AsT1.Message);
    return;
}

var rule = ruleResult.AsT0 ?? throw new Exception("rule is null");
var menuConsole = new GameMenuConsole(args);
var tableGenerator = new TableGenerator(args, rule.GetMoveResult);
using var hmacGenerator = HmacGenerator.HmacGeneratorFactory();

var gameManager = new GameManager(rule, hmacGenerator, menuConsole, tableGenerator);
await gameManager.RunGameLoop();
gameManager.PrintHmacKeyMessage();
