using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Выражение NaN.
/// </summary>
public class NaNExpression : AE
{
  public NaNExpression() : base(TT.NaNExpression) => Add(NaN);






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
      sb.Append(NaN.Formatted);
      return sb.ToString();
    }
  }




  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("nan"));

}
