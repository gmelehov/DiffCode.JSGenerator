namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция +=: <code>first += second</code>.
/// </summary>
public class PlusEqualExpr : AbstractBinaryExpression
{
  public PlusEqualExpr(AE left, AE right) : base(left, AOp.PlusEqual, right) { }
  public PlusEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.PlusEqual, right) { }
}

