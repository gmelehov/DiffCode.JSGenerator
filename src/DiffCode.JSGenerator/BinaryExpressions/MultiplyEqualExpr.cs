namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция *=: <code>first *= second</code>.
/// </summary>
public class MultiplyEqualExpr : AbstractBinaryExpression
{
  public MultiplyEqualExpr(AE left, AE right) : base(left, AOp.MultiplyEqual, right) { }
  public MultiplyEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.MultiplyEqual, right) { }
}

