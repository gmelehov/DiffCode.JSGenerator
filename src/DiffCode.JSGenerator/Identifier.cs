using static DiffCode.JSGenerator.XmlParse;



namespace DiffCode.JSGenerator;

/// <summary>
/// Идентификатор (имя переменной, свойства, название метода, функции и т.д.).
/// </summary>
public class Identifier : Literal
{
  private new string _value;





  public Identifier(string name) : base(TT.Identifier, name) => _value = name;






  /// <summary>
  /// Переопределение базового свойства Value.
  /// </summary>
  public override object GetSelfValue() => _value;

  public override string ToString() => Formatted;
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
      sb.Append(GetSelfValue()?.ToString());
      return sb.ToString();
    }
  }




  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("id"), new XAttribute("name", _value));

}
