namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение while(...) { ... }
  /// </summary>
  public class WhileExpr : AbstractBodiedExpression
  {
    public WhileExpr(ArgumentsBlock args, params AE[] exprs) : base(TT.While, args, exprs) { }
    public WhileExpr(ArgumentsBlock args, IEnumerable<AE> exprs) : base(TT.While, args, exprs.ToArray()) { }
  }
}
