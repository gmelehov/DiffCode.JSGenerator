using XE = System.Xml.Linq.XElement;
using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Комплексное выражение try {...} catch(E) {...} finally {...}
  /// </summary>
  public class TryCatchFinallyExpr : AE
  {
    public TryCatchFinallyExpr(TryExpr tryExpr, CatchExpr catchExpr, FinallyExpr finallyExpr = null) : base(tryExpr, catchExpr, finallyExpr) { }
    public TryCatchFinallyExpr(TryExpr tryExpr, FinallyExpr finallyExpr) : base(tryExpr, finallyExpr) { }






    public TryExpr Try => _childTokens?.FirstOrDefault()?.As<TryExpr>();
    public CatchExpr Catch => (Try?.Next is CatchExpr) ? Try?.Next?.As<CatchExpr>() : null;
    public FinallyExpr Finally => (_childTokens?.LastOrDefault() is FinallyExpr) ? _childTokens?.LastOrDefault()?.As<FinallyExpr>() : null;






    /// <summary>
		/// Разделитель дочерних элементов - пробел.
		/// </summary>
		public override string ItemsDivider => " ";

    /// <summary>
    /// Форматированное строковое представление объекта.
    /// </summary>
    public override string Formatted
    {
      get
      {
        var sb = new StringBuilder("");
        AppendLineAndIndent(ref sb, _gapBefore);
        if (IsNotNull(Comment))
        {
          if (!(Order == 0 && (HasParent() && Parent is CBlock)))
          {
            sb.AppendLine();
            sb.Append(IndentString);
          };
          if (Comment is CommentBlock)
          {
            foreach (var c in Comment.ChildTokens)
            {
              sb.Append(c.Formatted);
              sb.Append(IndentString);
            };
          }
          else
          {
            sb.Append(Comment.Formatted);
            sb.Append(IndentString);
          };
        };

        sb.Append(Try.Formatted);
        
        if (Catch != null)
        {
          sb.AppendLine();
          sb.Append(IndentString);
          sb.Append(Catch?.Formatted);
        };         

        if (Finally != null)
        {
          sb.AppendLine();
          sb.Append(IndentString);
          sb.Append(Finally?.Formatted);
        };
        return sb.ToString();
      }
    }
    
    
    
    
    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml =>
      Catch == null ?
        new XE(JsNS("try-catch"), Try.AsXml, Finally.AsXml)
        :
        Finally == null ?
          new XE(JsNS("try-catch"), Try.AsXml, Catch.AsXml)
          :
          new XE(JsNS("try-catch"), Try.AsXml, Catch.AsXml, Finally.AsXml);
  }
}
