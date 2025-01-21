namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Объявление переменной при помощи ключевого слова let: <code>let my = expr</code>.
  /// </summary>
  public class LetDeclaration : AbstractDeclaration
  {
    public LetDeclaration(string variable, AE expr = null) : base(Let, variable, expr) { }
    public LetDeclaration(Identifier variable, AE expr = null) : base(Let, variable, expr) { }
    public LetDeclaration(CommentItem comment, string variable, AE expr = null) : base(comment, Let, variable, expr) { }
    public LetDeclaration(CommentItem comment, Identifier variable, AE expr = null) : base(comment, Let, variable, expr) { }







    /// <summary>
    /// Изменяет количество отступов (пустых строк) перед элементом.
    /// </summary>
    /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
    public LetDeclaration SetGap(int gapbefore)
    {
      this._gapBefore = gapbefore;
      return this;
    }

  }
}
