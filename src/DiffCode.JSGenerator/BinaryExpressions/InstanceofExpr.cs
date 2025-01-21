namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки принадлежности объекта к типу: <code>first instanceof second</code>.
/// </summary>
public class InstanceofExpr : AbstractBinaryExpression
{
  public InstanceofExpr(AE left, AE right) : base(left, AOp.InstOf, right) { }
  public InstanceofExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.InstOf, right) { }
}
