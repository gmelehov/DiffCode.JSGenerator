using Idd = DiffCode.JSGenerator.Identifier;
using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение постфиксного увеличения на единицу.
  /// </summary>
  public partial class PostIncrementExpression : UpdateExpression
	{
		public PostIncrementExpression(Idd arg) : base(TT.PostIncrementExpr, arg, AbstractOperator.DblPlus) { }
		public PostIncrementExpression(string arg) : base(TT.PostIncrementExpr, ID(arg), AbstractOperator.DblPlus) { }
		public PostIncrementExpression(MemberAccess arg) : base(TT.PostIncrementExpr, arg, AbstractOperator.DblPlus) { }
    public PostIncrementExpression(AbstractExpression arg) : base(TT.PostIncrementExpr, arg, AbstractOperator.DblPlus) { }







    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("dblPlus"), new XAttribute("pos", "postfix"), Operand.AsXml as XElement);
  }
}
