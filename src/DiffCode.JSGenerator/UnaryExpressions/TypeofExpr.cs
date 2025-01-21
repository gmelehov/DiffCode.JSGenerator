using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Унарное выражение typeof ...
  /// </summary>
  public partial class TypeofExpr : AbstractUnaryExpression
  {
    public TypeofExpr(AE arg) : base(TT.Typeof, AbstractOperator.TypeOf as Operator, arg) { }







    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("typeOf"), Operand.AsXml as XElement);
  }
}
