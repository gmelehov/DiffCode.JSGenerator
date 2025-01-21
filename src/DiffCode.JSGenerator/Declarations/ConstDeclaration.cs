namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Объявление переменной при помощи ключевого слова let: <code>let my = expr</code>.
  /// </summary>
  public class ConstDeclaration : AbstractDeclaration
  {
    public ConstDeclaration(string variable, AE expr = null) : base(Const, variable, expr) { }
    public ConstDeclaration(Identifier variable, AE expr = null) : base(Const, variable, expr) { }
    public ConstDeclaration(CommentItem comment, string variable, AE expr = null) : base(comment, Const, variable, expr) { }
    public ConstDeclaration(CommentItem comment, Identifier variable, AE expr = null) : base(comment, Const, variable, expr) { }






    /// <summary>
    /// Изменяет количество отступов (пустых строк) перед элементом.
    /// </summary>
    /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
    public ConstDeclaration SetGap(int gapbefore)
    {
      this._gapBefore = gapbefore;
      return this;
    }

  }
}
