namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция ИЛИ: <code>first || second</code>.
/// </summary>
public class OrExpr : AbstractBinaryExpression
{
  public OrExpr(AE left, AE right) : base(left, AOp.Or, right) { }
  public OrExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Or, right) { }
  public OrExpr(AE first, AE second, AE third) : base(first, AOp.Or, second, AOp.Or, third) { }
  public OrExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.Or, second, AOp.Or, third, AOp.Or, fourth) { }
  public OrExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.Or, second, AOp.Or, third, AOp.Or, fourth, AOp.Or, fifth) { }
  public OrExpr(params AE[] exprs) : base(AOp.Or, exprs) { }
}
