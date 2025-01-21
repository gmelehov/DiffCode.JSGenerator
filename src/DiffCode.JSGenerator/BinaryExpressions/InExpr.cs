namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция проверки существования свойства у объекта: <code>key in data</code>.
/// </summary>
public class InExpr : AbstractBinaryExpression
{
  public InExpr(AE left, AE right) : base(left, AOp.In, right) { }
  public InExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.In, right) { }
}
