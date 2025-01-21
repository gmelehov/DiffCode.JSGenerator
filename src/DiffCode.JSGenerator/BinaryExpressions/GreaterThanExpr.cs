namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки БОЛЬШЕ, ЧЕМ: <code>first > second</code>.
/// </summary>
public class GreaterThanExpr : AbstractBinaryExpression
{
  public GreaterThanExpr(AE left, AE right) : base(left, AOp.Gt, right) { }
  public GreaterThanExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Gt, right) { }
}
