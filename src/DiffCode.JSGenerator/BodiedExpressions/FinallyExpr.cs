namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение finally { ... }
  /// </summary>
  public class FinallyExpr : AbstractBodiedExpression
  {
    public FinallyExpr(params AE[] exprs) : base(TT.Finally, null, exprs) { }
    public FinallyExpr(IEnumerable<AE> exprs) : base(TT.Finally, null, exprs.ToArray()) { }
  }
}
