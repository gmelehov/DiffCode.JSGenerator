namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция строгой проверки на равенство: <code>first === second</code>.
/// </summary>
public class StrictEqualExpr : AbstractBinaryExpression
{
  public StrictEqualExpr(AE left, AE right) : base(left, AOp.StrictEqual, right) { }
  public StrictEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.StrictEqual, right) { }
}
