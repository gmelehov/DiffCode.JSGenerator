using static DiffCode.JSGenerator.XmlParse;
using System.Diagnostics;


namespace DiffCode.JSGenerator;

/// <summary>
/// Блок аргументов, перечисленных через запятую, заключенный в круглые скобки.
/// </summary>
public class ArgumentsBlock : CBlock
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool _groupsExpressions = false;






  public ArgumentsBlock(bool groupsExpressions = false) : base(TT.Arguments) => _groupsExpressions = groupsExpressions;
  public ArgumentsBlock(string arg) : base(TT.Arguments) => Add(Str(arg));
  public ArgumentsBlock(double arg) : base(TT.Arguments) => Add(Num(arg));
  public ArgumentsBlock(params string[] args) : base(TT.Arguments) => Add(args.Select(ss => ID(ss)));
  public ArgumentsBlock(params AT[] exprs) : base(TT.Arguments) { var _exprs = exprs.Where(IsNotNull).Select(ex => (ex is AE) ? ex as AE : ex); Add(_exprs); }
  public ArgumentsBlock(IEnumerable<AT> exprs) : base(TT.Arguments) { var _exprs = exprs.Where(IsNotNull).Select(ex => (ex is AE) ? ex as AE : ex); Add(_exprs); }






  /// <summary>
  /// Разделитель дочерних элементов: 
  /// точка с запятой, если блок аргументов используется в выражениях типа for(i = 10; i > 0; i--)... ,
  /// пробел, если блок аргументов используется в выражениях for(key in obj)...,
  /// запятая во всех иных случаях.
  /// </summary>
  public override string ItemsDivider => ComputeItemsDivider();

  /// <summary>
  /// Форматированное строковое представление объекта.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      sb.Append("(");
      foreach (var it in _childTokens.Where(IsNotNull))
      {
        sb.Append(it.Formatted);
        if (!it.IsLast())
        {
          sb.Append(ItemsDivider + " ");
        };
      };
      sb.Append(")");
      return sb.ToString();
    }
  }





  protected string ComputeItemsDivider()
  {
    var ret = ",";

    if (Parent is ForExpr && _groupsExpressions)
      ret = ";";

    if (Parent is ForInExpr && _groupsExpressions)
      ret = " in ";

    return ret;
  }






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("args"), _childTokens.Where(IsNotNull).Select(s => s.AsXml));

}
