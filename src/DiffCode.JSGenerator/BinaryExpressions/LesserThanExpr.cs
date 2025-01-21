namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки МЕНЬШЕ, ЧЕМ.
/// </summary>
public class LesserThanExpr : AbstractBinaryExpression
{
  public LesserThanExpr(AE left, AE right) : base(left, AOp.Lt, right) { }
  public LesserThanExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Lt, right) { }
}
