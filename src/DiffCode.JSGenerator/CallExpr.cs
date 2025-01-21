using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение выражения для вызова метода/функции.
/// </summary>
public class CallExpr : AbstractExpression
{
  /// <summary>
  /// Конструктор класса по умолчанию.
  /// Не принимает никаких параметров, возвращает выражение, содержащее пустой список аргументов вызова.
  /// </summary>
  public CallExpr() : base(TT.MethodCall) => Add(Args());
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="args">Блок аргументов вызова.</param>
  public CallExpr(ARG args) : base(TT.MethodCall) => Add(args);
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="identifier">Строка идентификатора вызываемого метода/функции.</param>
  /// <param name="args">Блок аргументов вызова.</param>
  public CallExpr(string identifier, ARG args = null) : base(TT.MethodCall)
  {
    if (args != null)
      Add(ID(identifier), args);
    else
      Add(ID(identifier));
  }
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="identifier">Идентификатор вызываемого метода/функции.</param>
  /// <param name="args">Блок аргументов вызова.</param>
  public CallExpr(Identifier identifier, ARG args = null) : base(TT.MethodCall)
  {
    if (args != null)
      Add(ID(identifier), args);
    else
      Add(ID(identifier));
  }







  /// <summary>
  /// Добавляет набор выражений в блок аргументов вызова.
  /// Возвращает исходное выражение с обновленным списком аргументов вызова.
  /// </summary>
  /// <param name="exprs">Добавляемые выражения - аргументы вызова.</param>
  /// <returns></returns>
  public CallExpr AddArguments(params AbstractExpression[] exprs)
  {
    if (Arguments != null)
    {
      Arguments.Add(exprs);
    }
    else
    {
      Add(Args(exprs));
    };
    return this;
  }





  /// <summary>
  /// Идентификатор вызываемого метода/функции, либо <see langword="null"/>, если задан только блок аргументов вызова.
  /// </summary>
  public Identifier Identifier => (_childTokens?[0] is ARG) ? null : _childTokens[0].As<Identifier>();

  /// <summary>
  /// Ссылка на блок аргументов вызова метода/функции.
  /// </summary>
  public ARG Arguments => _childTokens?.LastOrDefault()?.As<ARG>();




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
      sb.Append(string.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted)));
      return sb.ToString();
    }
  }






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml =>
    Identifier != null ?
    new XElement(JsNS("call"), new XAttribute("method", Identifier.Formatted.Trim('\'')), (Arguments?.AsXml as XElement)?.Elements())
    :
    new XElement(JsNS("call"), (Arguments?.AsXml as XElement)?.Elements());

}
