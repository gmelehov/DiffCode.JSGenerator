namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция сложения: <code>first + second</code>
/// </summary>
public class PlusExpr : AbstractBinaryExpression
{
  public PlusExpr(AE left, AE right) : base(left, AOp.Plus, right) { }
  public PlusExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Plus, right) { }
  public PlusExpr(AE first, AE second, AE third) : base(first, AOp.Plus, second, AOp.Plus, third) { }
  public PlusExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.Plus, second, AOp.Plus, third, AOp.Plus, fourth) { }
  public PlusExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.Plus, second, AOp.Plus, third, AOp.Plus, fourth, AOp.Plus, fifth) { }
  public PlusExpr(params AE[] exprs) : base(AOp.Plus, exprs) { }
}
