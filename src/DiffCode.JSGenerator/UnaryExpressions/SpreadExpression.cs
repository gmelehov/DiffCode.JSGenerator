using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  public partial class SpreadExpression : AbstractUnaryExpression
  {
    public SpreadExpression(Identifier id) : base(TT.Spread, AbstractOperator.Spread as Operator, id) { }
    public SpreadExpression(string id) : base(TT.Spread, AbstractOperator.Spread as Operator, new Identifier(id)) { }






    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("spread"), Operand.AsXml as XElement);
  }
}
