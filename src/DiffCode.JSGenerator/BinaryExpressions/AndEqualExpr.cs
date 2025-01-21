namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция И=: <code>first &= second</code>.
/// </summary>
public class AndEqualExpr : AbstractBinaryExpression
{
  public AndEqualExpr(AE left, AE right) : base(left, AOp.AndEqual, right) { }
  public AndEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.AndEqual, right) { }
}
	
