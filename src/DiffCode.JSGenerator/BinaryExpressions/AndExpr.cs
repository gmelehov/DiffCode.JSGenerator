namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция И: <code>first && second</code>
/// </summary>
public class AndExpr : AbstractBinaryExpression
{
  public AndExpr(AE left, AE right) : base(left, AOp.And, right) { }
  public AndExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.And, right) { }
  public AndExpr(AE first, AE second, AE third) : base(first, AOp.And, second, AOp.And, third) { }
  public AndExpr(AE first, AE second, AE third, AE fourth) : base(first, AOp.And, second, AOp.And, third, AOp.And, fourth) { }
  public AndExpr(AE first, AE second, AE third, AE fourth, AE fifth) : base(first, AOp.And, second, AOp.And, third, AOp.And, fourth, AOp.And, fifth) { }
  public AndExpr(params AE[] exprs) : base(AOp.And, exprs) { }

}

