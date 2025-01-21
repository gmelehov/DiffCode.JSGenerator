using Idd = DiffCode.JSGenerator.Identifier;
using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение постфиксного уменьшения на единицу
  /// </summary>
  public partial class PostDecrementExpression : UpdateExpression
	{
		public PostDecrementExpression(Idd arg) : base(TT.PostDecrementExpr, arg, AbstractOperator.DblMinus) { }
		public PostDecrementExpression(string arg) : base(TT.PostDecrementExpr, ID(arg), AbstractOperator.DblMinus) { }
		public PostDecrementExpression(MemberAccess arg) : base(TT.PostDecrementExpr, arg, AbstractOperator.DblMinus) { }
    public PostDecrementExpression(AbstractExpression arg) : base(TT.PostDecrementExpr, arg, AbstractOperator.DblMinus) { }







    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("dblMinus"), new XAttribute("pos", "postfix"), Operand.AsXml as XElement);
  }
}
