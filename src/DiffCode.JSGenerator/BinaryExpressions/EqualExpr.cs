namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки на равенство: <code>first == second</code>
/// </summary>
public class EqualExpr : AbstractBinaryExpression
{
  public EqualExpr(AE left, AE right) : base(left, AOp.Equal, right) { }
  public EqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Equal, right) { }
}
