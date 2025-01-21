using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

  /// <summary>
  /// Выражение <see langword="null"/>.
  /// </summary>
  public class NullExpression : AE
{
	public NullExpression() : base(TT.NullExpression) => Add(Null);







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
			sb.Append(Null.Formatted.Trim('\'', '"', ' ').ToLower());
			return sb.ToString();
		}
	}



    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("null"));

  }
