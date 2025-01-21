namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки БОЛЬШЕ ИЛИ РАВНО: <code>first >= second</code>.
/// </summary>
public class GreaterOrEqualExpr : AbstractBinaryExpression
{
  public GreaterOrEqualExpr(AE left, AE right) : base(left, AOp.GtEq, right) { }
  public GreaterOrEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.GtEq, right) { }
}
