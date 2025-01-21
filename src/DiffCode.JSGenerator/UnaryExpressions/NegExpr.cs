using static DiffCode.JSGenerator.XmlParse;



namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Унарное выражение смены знака на отрицательный: -value
  /// </summary>
  public class NegExpr : AbstractUnaryExpression
  {
    public NegExpr(AE arg) : base(TT.NegativeSign, AbstractOperator.Neg as Operator, arg) { }





    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("neg"), Operand.AsXml as XElement);
  }
}
