namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция BitOr: <code>first | second</code>.
/// </summary>
public class BitOrExpr : AbstractBinaryExpression
{
  public BitOrExpr(AE left, AE right) : base(left, AOp.BitOr, right) { }
  public BitOrExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.BitOr, right) { }
  public BitOrExpr(AE first, AE second, AE third) : base(first, AOp.BitOr, second, AOp.BitOr, third) { }
  public BitOrExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.BitOr, second, AOp.BitOr, third, AOp.BitOr, fourth) { }
  public BitOrExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.BitOr, second, AOp.BitOr, third, AOp.BitOr, fourth, AOp.BitOr, fifth) { }
  public BitOrExpr(params AE[] exprs) : base(AOp.BitOr, exprs) { }
}
