using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Унарное выражение delete
  /// </summary>
  public class DeleteExpr : AbstractUnaryExpression
  {
    public DeleteExpr(AE arg) : base(TT.Delete, AbstractOperator.Delete as Operator, arg) { }





    /// <summary>
    /// XML-представление объекта
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("delete"), Operand.AsXml as XElement);
  }
}
