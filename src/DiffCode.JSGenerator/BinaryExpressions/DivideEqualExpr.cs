namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция /=: <code>first /= second</code>.
/// </summary>
public class DivideEqualExpr : AbstractBinaryExpression
{
  public DivideEqualExpr(AE left, AE right) : base(left, AOp.DivideEqual, right) { }
  public DivideEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.DivideEqual, right) { }
}
	
