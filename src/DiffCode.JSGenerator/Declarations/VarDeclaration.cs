namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Объявление переменной при помощи ключевого слова var: <code>var my = expr</code>.
  /// </summary>
  public class VarDeclaration : AbstractDeclaration
	{
		public VarDeclaration(string variable, AE expr = null) : base(Var, variable, expr) { }
		public VarDeclaration(Identifier variable, AE expr = null) : base(Var, variable, expr) { }
		public VarDeclaration(CommentItem comment, string variable, AE expr = null) : base(comment, Var, variable, expr) { }
		public VarDeclaration(CommentItem comment, Identifier variable, AE expr = null) : base(comment, Var, variable, expr) { }






    /// <summary>
    /// Изменяет количество отступов (пустых строк) перед элементом.
    /// </summary>
    /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
    public VarDeclaration SetGap(int gapbefore)
    {
      this._gapBefore = gapbefore;
      return this;
    }

  }
}
