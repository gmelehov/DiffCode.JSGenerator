namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция деления: <code>first / second</code>
/// </summary>
public class DivideExpr : AbstractBinaryExpression
{
  public DivideExpr(AE left, AE right) : base(left, AOp.Divide, right) { }
  public DivideExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Divide, right) { }
}
