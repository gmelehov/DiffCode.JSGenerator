using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

  /// <summary>
  /// Выражение this.
  /// </summary>
  public class ThisExpression : AE
{
	public ThisExpression() : base(TT.ThisExpression) => Add(This);






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
			sb.Append(This.Formatted);
			return sb.ToString();
		}
	}




    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("this"));

  }
