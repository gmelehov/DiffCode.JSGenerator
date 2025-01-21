using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FB = DiffCode.JSGenerator.FnBodyBlock;


namespace DiffCode.JSGenerator;

/// <summary>
/// Выражение FOR(...)
/// </summary>
public class ForExpression : AE
{
  public ForExpression(AE seed, ComparisonOperator oper, AE stop, UpdateExpression updExpr, ExpressionsSet exprs) : base(TT.ForExpression)
  {
    _type = TT.ForExpression;
    var argsBlock = Args(true);
    var loopvar = updExpr.Operand;
    var initBlock = ASSIGN(loopvar, seed);
    var testBlock = TEST(updExpr.Operand, oper, stop);
    var bodyBlock = FnBody(exprs.ChildTokens.Cast<AE>());
    argsBlock.Add(initBlock, testBlock, updExpr);
    Add(argsBlock, bodyBlock);
  }
  public ForExpression(int seed, ComparisonOperator oper, AE stop, UpdateExpression updExpr, ExpressionsSet exprs) : base(TT.ForExpression)
  {
    _type = TT.ForExpression;
    var argsBlock = Args(true);
    var loopvar = updExpr.Operand;
    var initBlock = ASSIGN(loopvar, Num(seed));
    var testBlock = TEST(updExpr.Operand, oper, stop);
    var bodyBlock = FnBody(exprs.ChildTokens.Cast<AE>());
    argsBlock.Add(initBlock, testBlock, updExpr);
    Add(argsBlock, bodyBlock);
  }








  /// <summary>
  /// Переменная цикла for: (i=...
  /// </summary>
  public AE LoopVar => LoopDefinition?.FindOne<AssignExpr>(t => t is AssignExpr)?.Left;

  /// <summary>
  /// Начальное значение переменной цикла for: (i=100; ...
  /// </summary>
  public AE LoopSeed => LoopDefinition?.FindOne<AssignExpr>(t => t is AssignExpr)?.Right as AE;

  /// <summary>
  /// Оператор сравнения переменной цикла for: (i=100; i>...
  /// </summary>
  public ComparisonOperator LoopTestOper => LoopTest?.ComparisonOper;

  /// <summary>
  /// Конечное значение переменной цикла for: (i=100; i>0; ...
  /// </summary>
  public AE LoopStop => LoopTest?.RightPart as AE;

  /// <summary>
  /// Выражение, задающее приращение/уменьшение счетчика цикла for: (i=100; i>0; i--...
  /// </summary>
  public UpdateExpression LoopVarUpdate => LoopDefinition?.FindOne<UpdateExpression>(t => t is UpdateExpression);

  /// <summary>
  /// Выражение проверки переменной цикла: i > 0
  /// </summary>
  public ExprStatement LoopTest => LoopDefinition?.FindOne<ExprStatement>(t => t is ExprStatement);

  /// <summary>
  /// Определение цикла for: (i=100; i>0; i--)
  /// </summary>
  public ARG LoopDefinition => this.FindOne<ARG>(t => t is ARG);

  /// <summary>
  /// Блок выражений/исполняемых инструкций, выполнямых в цикле for: { ... }
  /// </summary>
  public FB LoopBody => this.FindOne<FB>(t => t is FB);




  /// <summary>
  /// Разделитель дочерних элементов - пробел
  /// </summary>
  public override string ItemsDivider => " ";

  /// <summary>
  /// Форматированное строковое представление объекта
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      if (IsNotNull(Comment))
      {
        if (!(Order == 0 && (HasParent() && Parent is CBlock)))
        {
          sb.AppendLine();
          sb.Append(IndentString);
        };
        if (Comment is CommentBlock)
        {
          foreach (var c in Comment.ChildTokens)
          {
            sb.Append(c.Formatted);
            sb.Append(IndentString);
          };
        }
        else
        {
          sb.Append(Comment.Formatted);
          sb.Append(IndentString);
        };
      };
      sb.Append(For.Formatted);
      sb.Append(ItemsDivider);
      sb.Append(LoopDefinition?.Formatted);
      sb.Append(LoopBody?.Formatted);
      return sb.ToString();
    }
  }






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => null;

}
