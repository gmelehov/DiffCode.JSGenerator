using static DiffCode.JSGenerator.XmlParse;





namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Элемент блока комментариев к коду.
  /// </summary>
  public class CommentItem : AbstractExpression
	{
    public bool _inlined = false;





		public CommentItem(TT type) : base(type) => _type = type;
		public CommentItem(TT type, object val) : base(type)
		{
			_type = type;
			_value = val;
		}
    public CommentItem(bool inline, TT type, object val) : base(type)
    {
      _inlined = inline;
      _type = type;
      _value = val;
    }








    public CommentItem SetInline(bool inline)
    {
      this._inlined = inline;
      return this;
    }







    /// <summary>
    /// Переопределение базового метода GetSelfValue().
    /// </summary>
    /// <returns></returns>
    public override object GetSelfValue()
		{
			switch (GetTokenType())
			{
        case TT.CommentStart: return $"/**{StringEnding}";

        case TT.CommentLine: return $"{StringStart}{_value?.ToString()}{StringEnding}";

				case TT.CommentEnd: return $" */{StringEnding}";

				case TT.CommentSingle: return $"// {_value?.ToString()}{StringEnding}";


				default: return "";
			};
		}

		/// <summary>
		/// Форматированное строковое представление объекта.
		/// </summary>
		public override string Formatted
		{
			get
			{
				var sb = new StringBuilder("");
        //var parentIndent = HasParent() ? Parent.IndentString : "";
        
        //if (GetTokenType().Equals(TT.CommentStart))
        //  parentIndent = "\r\n\r\n" + parentIndent;

        //sb.Append(parentIndent + GetSelfValue()?.ToString());
        sb.Append(IndentBefore + GetSelfValue()?.ToString());

        return sb.ToString();
			}
		}





    private string StringStart => _inlined && GetTokenType() == TT.CommentLine ? " " : " * ";
    private string StringEnding => _inlined && GetTokenType() != TT.CommentEnd ? "" : "\r\n";
    private string IndentBefore => HasParent() && (GetTokenType() == TT.CommentStart || GetTokenType() == TT.CommentSingle) ? Parent.IndentString : "";







    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => 
      new XElement(JsNS("info"),
        new XAttribute("text", _value.ToString())
        );

  }
}
