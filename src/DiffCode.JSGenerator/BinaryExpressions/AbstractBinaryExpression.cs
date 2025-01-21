using XE = System.Xml.Linq.XElement;
using static DiffCode.JSGenerator.XmlParse;
using System.Diagnostics;


namespace DiffCode.JSGenerator;

/// <summary>
/// Абстрактное бинарное выражение.
/// </summary>
public abstract class AbstractBinaryExpression : AE
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected AOp _operator;





  protected AbstractBinaryExpression(AE left, AOp oper, AE right) : base(left, oper, right) => _operator = oper;
  protected AbstractBinaryExpression(CommentItem comment, AE left, AOp oper, AE right) : base(comment, left, oper, right) => _operator = oper;
  protected AbstractBinaryExpression(AE first, AOp oper1, AE second, AOp oper2, AE third) : base(first, oper1, second, oper2, third) => _operator = oper1;
  protected AbstractBinaryExpression(AE first, AOp oper1, AE second, AOp oper2, AE third, AOp oper3, AE fourth) : base(first, oper1, second, oper2, third, oper3, fourth) => _operator = oper1;
  protected AbstractBinaryExpression(AE first, AOp oper1, AE second, AOp oper2, AE third, AOp oper3, AE fourth, AOp oper4, AE fifth) : base(first, oper1, second, oper2, third, oper3, fourth, oper4, fifth) => _operator = oper1;
  protected AbstractBinaryExpression(AE first, AOp oper1, AE second, AOp oper2, AE third, AOp oper3, AE fourth, AOp oper4, AE fifth, AOp oper5, AE sixth) : base(first, oper1, second, oper2, third, oper3, fourth, oper4, fifth, oper5, sixth) => _operator = oper1;
  protected AbstractBinaryExpression(AOp oper, params AE[] exprs)
  {
    _operator = oper;
    var amount = exprs.Count();
    var items = new List<AE>
    {
      exprs[0]
    };
    for (var i = 1; i < amount; i++)
    {
      items.Add(oper);
      items.Add(exprs[i]);
    };
    base.Add(items);
  }







  /// <summary>
  /// Левая часть выражения.
  /// </summary>
  public AE Left => _childTokens?.FirstOrDefault()?.As<AE>();

  /// <summary>
  /// Оператор выражения.
  /// </summary>
  public AOp Operator => _operator;

  /// <summary>
  /// Правая часть выражения.
  /// </summary>
  public AE Right => Operator?.Next?.As<AE>();

  /// <summary>
  /// Последний операнд выражения.
  /// </summary>
  public AE LastOperand => _childTokens?.LastOrDefault()?.As<AE>();





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
      AppendLineAndIndent(ref sb, _gapBefore);
      if (Comment != null)
      {
        if (!(Order == 0 && (HasParent() && Parent is CBlock)))
        {
          sb.AppendLine();
          sb.Append(IndentString);
        };
        if (Comment is CommentBlock)
        {
          sb.AppendLine();
          sb.Append(IndentString);

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

      bool isItemOfIF = HasParent() && Parent is IfElseExpression;
      if (isItemOfIF)
        sb.Append("(");
      foreach (var c in _childTokens)
      {
        if (c is AOp)
          WrapIfOperator(c.As<AOp>(), ref sb);
        else
          WrapIfBinary(c.As<AE>(), ref sb);
      };
      if (isItemOfIF)
        sb.Append(")");
      return sb.ToString();
    }
  }





  protected void WrapIfBinary(AE expr, ref StringBuilder sbuilder)
  {
    if (ToBeWrapped(this, expr))
      sbuilder.Append("(");
    sbuilder.Append(expr?.Formatted);
    if (ToBeWrapped(this, expr))
      sbuilder.Append(")");
  }
  protected void WrapIfOperator(AOp oper, ref StringBuilder sbuilder)
  {
    sbuilder.Append(ItemsDivider + oper.Formatted + ItemsDivider);
  }





  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml =>
    Operator == AOp.Assign && _gapBefore > 0
    ?
    new XE(JsNS(TypeXNameDict[Operator?.Formatted]), new XAttribute("gap", _gapBefore), _childTokens?.Where(r => !(r is AOp))?.Select(c => c.AsXml)?.Cast<XE>())
    :
    new XE(JsNS(TypeXNameDict[Operator?.Formatted]), _childTokens?.Where(r => !(r is AOp))?.Select(c => c.AsXml)?.Cast<XE>());





  public static Func<AbstractBinaryExpression, AE, bool> NeedsWrapping => (bin, expr) =>
  {
    bool noNeed =
    (bin is PlusExpr
        &&
        (expr is PlusExpr
        ||
        expr is MinusExpr
        )
        )
    ||
    (bin is MinusExpr
        &&
        (expr is PlusExpr
        ||
        expr is MinusExpr
        )
        )
    ||
    (bin is AndExpr && expr is AndExpr)
    ||
    (bin is OrExpr && expr is OrExpr)
    ||
    (bin is MultiplyExpr && expr is MultiplyExpr)
    ||
    (expr is NotExpr)
    ||
    (expr is NegExpr || expr is PosExpr)
    ||
    (expr is StringLiteral)
    ||
    (bin is AssignExpr)
    ||
    !(expr is AbstractBinaryExpression || expr is AbstractUnaryExpression)
    ||
    (bin is OrExpr && (!(expr is OrExpr) || !(expr is AndExpr)))
    ||
    (bin is AndExpr && (!(expr is OrExpr) || !(expr is AndExpr)))
    ||
    (bin is EqualExpr || bin is StrictEqualExpr || bin is NotEqualExpr || bin is StrictNotEqualExpr)
    ;


    return !noNeed;
  };


  public static Func<AbstractBinaryExpression, AE, bool> ToBeWrapped => (bin, expr) =>
  {
    bool ret =
    (bin is OrExpr && expr is AndExpr)
    ||
    (bin is AndExpr && expr is OrExpr)
    ||
    (
      (bin is PlusExpr
      ||
      bin is MinusExpr)
      &&
      (expr is PreIncrementExpression || expr is PreDecrementExpression || expr is OrExpr)
    )
    ||
    (
      (bin is ModExpr || bin is MultiplyExpr || bin is DivideExpr)
      &&
      (expr is PlusExpr || expr is MinusExpr || expr is ModExpr)
    )
    ||
    (
      (bin is EqualExpr
      ||
      bin is NotEqualExpr
      ||
      bin is StrictEqualExpr
      ||
      bin is StrictNotEqualExpr)
      &&
      (expr is AndExpr || expr is OrExpr)
    )
    ;


    return ret;
  };










  public static AbstractBinaryExpression operator &(AbstractBinaryExpression first, AbstractBinaryExpression second) => new AndExpr(first, second);
  public static AbstractBinaryExpression operator |(AbstractBinaryExpression first, AbstractBinaryExpression second) => new OrExpr(first, second);
  public static AbstractBinaryExpression operator &(AbstractBinaryExpression first, AbstractUnaryExpression second) => new AndExpr(first, second);
  public static AbstractBinaryExpression operator |(AbstractBinaryExpression first, AbstractUnaryExpression second) => new OrExpr(first, second);
  public static AbstractBinaryExpression operator &(AbstractUnaryExpression first, AbstractBinaryExpression second) => new AndExpr(first, second);
  public static AbstractBinaryExpression operator |(AbstractUnaryExpression first, AbstractBinaryExpression second) => new OrExpr(first, second);
  public static AbstractUnaryExpression operator !(AbstractBinaryExpression expr) => new NotExpr(expr);







  private static Dictionary<string, string> TypeXNameDict => new Dictionary<string, string>
  {
    ["&&"] = "and",
    ["&="] = "andEq",
    ["="] = "assign",
    ["|"] = "bitOr",
    ["^"] = "bitXor",
    ["&"] = "bitAnd",
    ["/"] = "divide",
    ["/="] = "divideEq",
    ["=="] = "equal",
    [">"] = "gt",
    [">="] = "gtEq",
    ["in"] = "in",
    ["instanceof"] = "instOf",
    ["<"] = "lt",
    ["<="] = "ltEq",
    ["-"] = "minus",
    ["-="] = "minusEq",
    ["%"] = "mod",
    ["*"] = "multiply",
    ["*="] = "multiplyEq",
    ["!="] = "notEqual",
    ["||"] = "or",
    ["|="] = "orEq",
    ["+"] = "plus",
    ["+="] = "plusEq",
    ["==="] = "strictEqual",
    ["!=="] = "strictNotEqual",
  };

}
