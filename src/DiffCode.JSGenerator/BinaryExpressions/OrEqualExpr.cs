namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция ИЛИ=: <code>first |= second</code>.
/// </summary>
public class OrEqualExpr : AbstractBinaryExpression
{
  public OrEqualExpr(AE left, AE right) : base(left, AOp.OrEqual, right) { }
  public OrEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.OrEqual, right) { }
}
	
