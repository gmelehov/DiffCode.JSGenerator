using Idd = DiffCode.JSGenerator.Identifier;
using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение префиксного уменьшения на единицу.
  /// </summary>
  public partial class PreDecrementExpression : UpdateExpression
	{
		public PreDecrementExpression(Idd arg) : base(TT.PreDecrementExpr, AbstractOperator.DblMinus, arg) { }
		public PreDecrementExpression(string arg) : base(TT.PreDecrementExpr, AbstractOperator.DblMinus, ID(arg)) { }
		public PreDecrementExpression(MemberAccess arg) : base(TT.PreDecrementExpr, AbstractOperator.DblMinus, arg) { }
    public PreDecrementExpression(AbstractExpression arg) : base(TT.PreDecrementExpr, AbstractOperator.DblMinus, arg) { }








    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("dblMinus"), new XAttribute("pos", "prefix"), Operand.AsXml as XElement);
  }
}
