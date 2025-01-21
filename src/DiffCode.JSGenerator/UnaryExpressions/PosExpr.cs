using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Унарное выражение смены знака на положительный: +value
  /// </summary>
  public partial class PosExpr : AbstractUnaryExpression
  {
    public PosExpr(AE arg) : base(TT.PositiveSign, AbstractOperator.Pos as Operator, arg) { }







    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("pos"), Operand.AsXml as XElement);
  }
}
