using Idd = DiffCode.JSGenerator.Identifier;
using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение двойного отрицания !!
  /// </summary>
  public partial class DoubleNotExpression : AbstractUnaryExpression
  {
    public DoubleNotExpression(Idd arg) : base(TT.DoubleNot, AbstractOperator.DblNot as Operator, arg) { }
    public DoubleNotExpression(string arg) : base(TT.DoubleNot, AbstractOperator.DblNot as Operator, ID(arg)) { }
    public DoubleNotExpression(MemberAccess arg) : base(TT.DoubleNot, AbstractOperator.DblNot as Operator, arg) { }
    public DoubleNotExpression(AbstractExpression arg) : base(TT.DoubleNot, AbstractOperator.DblNot as Operator, arg) { }






    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("dblNot"), Operand.AsXml as XElement);
  }
}
