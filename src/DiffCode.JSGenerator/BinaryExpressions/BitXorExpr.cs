namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция BitXor: <code>first ^ second</code>.
/// </summary>
public class BitXorExpr : AbstractBinaryExpression
{
  public BitXorExpr(AE left, AE right) : base(left, AOp.BitXor, right) { }
  public BitXorExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.BitXor, right) { }
  public BitXorExpr(AE first, AE second, AE third) : base(first, AOp.BitXor, second, AOp.BitXor, third) { }
  public BitXorExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.BitXor, second, AOp.BitXor, third, AOp.BitXor, fourth) { }
  public BitXorExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.BitXor, second, AOp.BitXor, third, AOp.BitXor, fourth, AOp.BitXor, fifth) { }
  public BitXorExpr(params AE[] exprs) : base(AOp.BitXor, exprs) { }
}
