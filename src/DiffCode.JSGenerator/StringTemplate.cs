using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Строка-шаблон `...`
/// </summary>
public class StringTemplate : AE
{
  protected List<StringTemplateParameter> _prms = new List<StringTemplateParameter>();

  public StringTemplate(params AE[] args) : base(TT.StringTemplateExpression)
  {
    var _args = args.Where(w => w is StringLiteral || w is StringTemplateParameter).ToList();
    _prms.AddRange(_args.OfType<StringTemplateParameter>());
    base.Add(_args);
  }
  public StringTemplate(IEnumerable<AE> args) : base(TT.StringTemplateExpression)
  {
    var _args = args.Where(w => w is StringLiteral || w is StringTemplateParameter).ToList();
    _prms.AddRange(_args.OfType<StringTemplateParameter>());
    base.Add(_args);
  }






  public List<StringTemplateParameter> Parameters => _prms;




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
      sb.Append('`');
      sb.Append((HasItems() == true ?
        string.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted.Trim('\'')))
        :
        GetSelfValue().ToString().Trim('\'')
        ));
      sb.Append('`');
      return sb.ToString();
    }
  }







  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("str-template"), _childTokens.Select(c => c.AsXml).Cast<XElement>());
}
