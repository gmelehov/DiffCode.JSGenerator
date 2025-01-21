namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция BitAnd: <code>first & second</code>.
/// </summary>
public class BitAndExpr : AbstractBinaryExpression
{
  public BitAndExpr(AE left, AE right) : base(left, AOp.BitAnd, right) { }
  public BitAndExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.BitAnd, right) { }
  public BitAndExpr(AE first, AE second, AE third) : base(first, AOp.BitAnd, second, AOp.BitAnd, third) { }
  public BitAndExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.BitAnd, second, AOp.BitAnd, third, AOp.BitAnd, fourth) { }
  public BitAndExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.BitAnd, second, AOp.BitAnd, third, AOp.BitAnd, fourth, AOp.BitAnd, fifth) { }
  public BitAndExpr(params AE[] exprs) : base(AOp.BitAnd, exprs) { }
}
