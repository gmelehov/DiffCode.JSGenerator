using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

  /// <summary>
  /// Выражение <see langword="true"/>.
  /// </summary>
  public class TrueExpression : AE
{
	public TrueExpression() : base(TT.TrueExpression) => Add(True);







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
			sb.Append(True.Formatted.Trim('\'', '"', ' ').ToLower());
			return sb.ToString();
		}
	}




    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("true"));

  }
