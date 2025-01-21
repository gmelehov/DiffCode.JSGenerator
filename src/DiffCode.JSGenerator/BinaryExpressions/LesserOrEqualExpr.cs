namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки МЕНЬШЕ ИЛИ РАВНО.
/// </summary>
public class LesserOrEqualExpr : AbstractBinaryExpression
{
  public LesserOrEqualExpr(AE left, AE right) : base(left, AOp.LtEq, right) { }
  public LesserOrEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.LtEq, right) { }
}
