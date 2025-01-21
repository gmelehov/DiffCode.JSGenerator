using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Унарное выражение побитового НЕ ~
  /// </summary>
  public partial class BitNotExpr : AbstractUnaryExpression
  {
    public BitNotExpr(AE arg) : base(TT.BitNot, AbstractOperator.BitNot as Operator, arg) { }




    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("bitNot"), Operand.AsXml as XElement);
  }
}
