using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Параметр строки-шаблона ${...}
/// </summary>
public class StringTemplateParameter : AbstractExpression
{
  public StringTemplateParameter(AbstractExpression expr) : base(TT.StringTemplateParameter) => base.Add(expr);





  public AbstractExpression Expression => _childTokens?.FirstOrDefault()?.As<AbstractExpression>();







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
      sb.Append("${");
      sb.Append(Expression.Formatted);
      sb.Append("}");
      return sb.ToString().Trim('\'');
    }
  }






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("prm"), Expression.AsXml as XElement);
}
