using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Регулярное выражение.
  /// </summary>
  public partial class RegexpLiteral : Literal
  {
    private new string _value;
    private string _flags;






    public RegexpLiteral(string val, string flags = "g") : base(TT.Regexp, val)
    {
      _value = val?.ToString();
      _flags = flags?.ToString();
    }
    public RegexpLiteral(object val, string flags = "g") : base(TT.Regexp, val)
    {
      _value = val?.ToString();
      _flags = flags?.ToString();
    }








    public override object GetSelfValue()
    {
      var _val = _value.ToString();
      if (_val.StartsWith("'") && _val.EndsWith("'"))
        _val = _val.Trim(new char[] { '\'' });

      return (_value?.ToString() == "\r\n") ? _value?.ToString() : String.Join("", "/", _val, "/", _flags);
    }


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
      Formatted?.Trim('\'') == "\r\n" ? 
        new XElement(JsNS("newline")) 
        : 
        _flags != null ?
        new XElement(
          JsNS("regexp"), 
          new XAttribute("value", _value?.ToString()), 
          new XAttribute("flags", _flags)
          )
        :
        new XElement(
          JsNS("regexp"),
          new XAttribute("value", _value?.ToString())
          );

  }
}
