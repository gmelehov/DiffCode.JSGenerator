using Idd = DiffCode.JSGenerator.Identifier;
using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение префиксного увеличения на единицу.
  /// </summary>
  public partial class PreIncrementExpression : UpdateExpression
	{
		public PreIncrementExpression(Idd arg) : base(TT.PreIncrementExpr, AbstractOperator.DblPlus, arg) { }
		public PreIncrementExpression(string arg) : base(TT.PreIncrementExpr, AbstractOperator.DblPlus, ID(arg)) { }
		public PreIncrementExpression(MemberAccess arg) : base(TT.PreIncrementExpr, AbstractOperator.DblPlus, arg) { }
    public PreIncrementExpression(AbstractExpression arg) : base(TT.PreIncrementExpr, AbstractOperator.DblPlus, arg) { }








    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("dblPlus"), new XAttribute("pos", "prefix"), Operand.AsXml as XElement);
  }
}
