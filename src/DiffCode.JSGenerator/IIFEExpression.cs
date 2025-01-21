using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение самовызывающейся функции IIFE
/// Immediately Invoked Function Expression
/// </summary>
public class IIFEExpression : AE
{
  public IIFEExpression(FunctionAnonymous func, ArgumentsBlock args = null) : base(TT.IIFExpression)
  {
    Add(func, args?? Args());
  }







  public FunctionAnonymous Function => _childTokens.FirstOrDefault()?.As<FunctionAnonymous>();

  public ArgumentsBlock Arguments => Function?.Next?.As<ArgumentsBlock>();






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

      sb.Append(";(");
      sb.Append(Function?.Formatted);
      sb.Append(Arguments);
      sb.Append(")");

      return sb.ToString();
    }
  }







  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("iife"), _childTokens.Select(s => s.AsXml));

}
