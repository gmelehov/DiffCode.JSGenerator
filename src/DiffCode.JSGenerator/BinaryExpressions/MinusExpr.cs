namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция -
/// </summary>
public class MinusExpr : AbstractBinaryExpression
{
  public MinusExpr(AE left, AE right) : base(left, AOp.Minus, right) { }
  public MinusExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Minus, right) { }
  public MinusExpr(AE first, AE second, AE third) : base(first, AOp.Minus, second, AOp.Minus, third) { }
  public MinusExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.Minus, second, AOp.Minus, third, AOp.Minus, fourth) { }
  public MinusExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.Minus, second, AOp.Minus, third, AOp.Minus, fourth, AOp.Minus, fifth) { }
  public MinusExpr(params AE[] exprs) : base(AOp.Minus, exprs) { }
}
