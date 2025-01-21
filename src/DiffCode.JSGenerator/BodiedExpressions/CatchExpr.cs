namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение catch(E) { ... }
  /// </summary>
  public class CatchExpr : AbstractBodiedExpression
  {
    public CatchExpr(params AE[] exprs) : base(TT.Catch, Args(ID("E")), exprs) { }
    public CatchExpr(IEnumerable<AE> exprs) : base(TT.Catch, Args(ID("E")), exprs.ToArray()) { }
    public CatchExpr(ArgumentsBlock args, params AE[] exprs) : base(TT.Catch, args, exprs) { }
    public CatchExpr(ArgumentsBlock args, IEnumerable<AE> exprs) : base(TT.Catch, args, exprs.ToArray()) { }
  }
}
