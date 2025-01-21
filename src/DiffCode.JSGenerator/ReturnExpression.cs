using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Выражение return.
/// </summary>
public class ReturnExpression : AE
{
  public ReturnExpression() : base(TT.ReturnExpression) => Add(Return);
  public ReturnExpression(AE expr) : base(TT.ReturnExpression) => Add(Return, expr);
  public ReturnExpression(bool expr) : base(TT.ReturnExpression) => Add(Return, expr ? TRUE as AE : FALSE as AE);
  public ReturnExpression(ST tok) : base(TT.ReturnExpression) => Add(Return, tok);
  public ReturnExpression(string str) : base(TT.ReturnExpression) => Add(Return, ((str != null) ? Str(str) : Null as AT));
  public ReturnExpression(double num) : base(TT.ReturnExpression) => Add(Return, Num(num));






  public virtual AT ReturnExpr => _childTokens?.FirstOrDefault()?.Next ?? null;





  /// <summary>
  /// Разделитель дочерних элементов - пробел.
  /// </summary>
  public override string ItemsDivider => " ";






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => ReturnExpr != null ? new XElement(JsNS("ret"), ReturnExpr.AsXml) : new XElement(JsNS("ret"));

}
