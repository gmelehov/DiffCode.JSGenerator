namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение try { ... }
  /// </summary>
  public class TryExpr : AbstractBodiedExpression
  {
    public TryExpr(params AE[] exprs) : base(TT.Try, null, exprs) { }
    public TryExpr(IEnumerable<AE> exprs) : base(TT.Try, null, exprs.ToArray()) { }
  }
}
