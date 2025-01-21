namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция строгой проверки на неравенство: <code>first !== second</code>.
/// </summary>
public class StrictNotEqualExpr : AbstractBinaryExpression
{
  public StrictNotEqualExpr(AE left, AE right) : base(left, AOp.StrictNotEqual, right) { }
  public StrictNotEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.StrictNotEqual, right) { }
}
