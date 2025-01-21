namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки на неравенство: <code>first != second</code>.
/// </summary>
public class NotEqualExpr : AbstractBinaryExpression
{
  public NotEqualExpr(AE left, AE right) : base(left, AOp.NotEqual, right) { }
  public NotEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.NotEqual, right) { }
}
