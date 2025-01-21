using System.Diagnostics;

using static DiffCode.JSGenerator.XmlParse;





namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Строковый литерал (строка).
  /// </summary>
  public partial class StringLiteral : Literal
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private new string _value;






    public StringLiteral(string val) : base(TT.String, val) => _value = val?.ToString();
    public StringLiteral(object val) : base(TT.String, val) => _value = val?.ToString();







    /// <summary>
    /// Форматированное строковое представление объекта.
    /// </summary>
    public override string Formatted
    {
      get
      {
        var sb = new StringBuilder("");
        if (IsNotNull(Comment))
        {
          if (Comment is CommentBlock)
          {
            foreach (var c in Comment.ChildTokens)
            {
              sb.Append(c.Formatted + IndentString);
            };
          }
          else
          {
            sb.Append(Comment.Formatted + IndentString);
          };
        };
        sb.Append(
          (HasItems() == true ?
          string.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted))
          :
          GetSelfValue().ToString())
          );
        return sb.ToString();
      }
    }






    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml =>
      (Formatted.Trim('\'') == "\r\n" || Formatted.Trim(new char[] { '\r', '\n' }) == "") && Formatted == ""
      ?
      new XElement(JsNS("newline"))
      :
      new XElement(JsNS("str"), new XAttribute("value", Formatted.Trim('\'')));

  }
}
