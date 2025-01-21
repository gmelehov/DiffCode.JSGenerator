namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция -=: <code>first -= second</code>.
/// </summary>
public class MinusEqualExpr : AbstractBinaryExpression
{
  public MinusEqualExpr(AE left, AE right) : base(left, AOp.MinusEqual, right) { }
  public MinusEqualExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.MinusEqual, right) { }
}

