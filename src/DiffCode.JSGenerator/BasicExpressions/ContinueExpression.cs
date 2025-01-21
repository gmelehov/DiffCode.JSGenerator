using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

public class ContinueExpression : AE
{
  public ContinueExpression() : base(TT.ContinueExpression) => Add(Continue);







  /// <summary>
  /// Разделитель дочерних элементов - пробел.
  /// </summary>
  public override string ItemsDivider => " ";



  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("continue"));
}
