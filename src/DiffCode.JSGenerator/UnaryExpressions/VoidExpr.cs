using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Унарный оператор void
  /// </summary>
  public class VoidExpr : AbstractUnaryExpression
  {
    public VoidExpr(AE arg) : base(TT.Void, AbstractOperator.Void as Operator, arg) { }






    /// <summary>
    /// XML-представление объекта
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("void"), Operand.AsXml as XElement);
  }
}
