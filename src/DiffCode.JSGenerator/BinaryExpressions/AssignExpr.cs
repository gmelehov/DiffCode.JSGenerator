namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция присваивания: <code>first = second</code>
/// </summary>
public class AssignExpr : AbstractBinaryExpression
{
  public AssignExpr(AE left, AE right) : base(left, AOp.Assign, right) { }
  public AssignExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Assign, right) { }






  /// <summary>
  /// Изменяет количество отступов (пустых строк) перед элементом.
  /// </summary>
  /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
  public new AssignExpr SetGap(int gapbefore)
  {
    this._gapBefore = gapbefore;
    return this;
  }

}
