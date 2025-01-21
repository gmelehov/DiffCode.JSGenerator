using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// 
/// </summary>
public class EmptyExpression : AE
{
  public EmptyExpression() : base() { }






  public override XObject AsXml => new XElement(JsNS("empty"));
}
