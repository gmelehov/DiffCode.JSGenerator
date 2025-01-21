using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FB = DiffCode.JSGenerator.FnBodyBlock;
using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение анонимной функции.
/// </summary>
public class FunctionAnonymous : AE
{
  protected bool useLambda = false;





  public FunctionAnonymous(bool uselambda = false) : base(TT.FuncAnonymous)
  {
    useLambda = uselambda;
    var _args = Args();
    var _body = FnBody();
    Add(_args, _body);
  }
  public FunctionAnonymous(ARG args, bool uselambda = false) : base(TT.FuncAnonymous)
  {
    useLambda = uselambda;
    var _body = FnBody();
    Add(args, _body);
  }
  public FunctionAnonymous(FB fnBody) : base(TT.FuncAnonymous)
  {
    var _args = Args();
    Add(_args, fnBody);
  }
  public FunctionAnonymous(ARG args, FB fnBody) : base(TT.FuncAnonymous) => Add(args, fnBody);
  public FunctionAnonymous(ARG args, bool uselambda = false, params AE[] exprs) : base(TT.FuncAnonymous)
  {
    useLambda = uselambda;
    var _body = FnBody(exprs);
    Add(args, _body);
  }
  public FunctionAnonymous(bool uselambda = false, params AE[] exprs) : base(TT.FuncAnonymous)
  {
    useLambda = uselambda;
    var _args = Args();
    var _body = FnBody(exprs);
    Add(_args, _body);
  }








  /// <summary>
  /// Добавить аргумент в список аргументов функции.
  /// </summary>
  /// <param name="arg">Добавляемый аргумент.</param>
  public void AddArgument(AT arg) => Arguments?.Add(arg);

  /// <summary>
  /// Добавить выражение (инструкцию) в блок кода функции.
  /// </summary>
  /// <param name="expr">Добавляемое выражение(инструкция).</param>
  public void AddExpr(AE expr) => Body?.AddExpression(expr);





  /// <summary>
  /// Аргументы функции.
  /// </summary>
  public virtual ARG Arguments => _childTokens?.First() as ARG;

  /// <summary>
  /// Тело функции (блок кода).
  /// </summary>
  public virtual FB Body => Arguments.Next as FB;




  /// <summary>
  /// Разделитель дочерних элементов - пробел.
  /// </summary>
  public override string ItemsDivider => " ";

  /// <summary>
  /// Форматированное строковое представление объекта.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      if (!useLambda)
      {
        sb.Append(Function.Formatted);
        sb.Append(ItemsDivider);
      };
      sb.Append(Arguments?.Formatted);
      if (useLambda)
      {
        sb.Append(ItemsDivider);
        sb.Append(Lambda.Formatted);
      }
      sb.Append(Body?.Formatted);
      return sb.ToString();
    }
  }







  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(useLambda ? JsNS("lambda") : JsNS("fn"), Arguments.AsXml, Body.ChildTokens.Select(s => s.AsXml));

}
