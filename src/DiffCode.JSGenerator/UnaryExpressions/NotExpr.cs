using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Унарное выражение отрицания !
  /// </summary>
  public partial class NotExpr : AbstractUnaryExpression
	{
		public NotExpr(AE arg) : base(TT.BooleanNOT, AbstractOperator.Not as Operator, arg) { }






    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("not"), Operand.AsXml as XElement);
  }
}
