namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция умножения: <code>first * second</code>
/// </summary>
public class MultiplyExpr : AbstractBinaryExpression
{
  public MultiplyExpr(AE left, AE right) : base(left, AOp.Multiply, right) { }
  public MultiplyExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Multiply, right) { }
  public MultiplyExpr(AE first, AE second, AE third) : base(first, AOp.Multiply, second, AOp.Multiply, third) { }
  public MultiplyExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.Multiply, second, AOp.Multiply, third, AOp.Multiply, fourth) { }
  public MultiplyExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.Multiply, second, AOp.Multiply, third, AOp.Multiply, fourth, AOp.Multiply, fifth) { }
  public MultiplyExpr(params AE[] exprs) : base(AOp.Multiply, exprs) { }
}
