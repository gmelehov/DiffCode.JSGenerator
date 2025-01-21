using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение new ...
  /// </summary>
  public class NewExpr : AE
  {
    public NewExpr(MemberAccess expr) : base(TT.NewExpression) => Add(New, expr);





    public AE Expression => _childTokens?.FirstOrDefault()?.Next?.As<AE>();



    public override string ItemsDivider => " ";



    public override XObject AsXml => new XElement(JsNS("new"), Expression.AsXml);
  }
}
