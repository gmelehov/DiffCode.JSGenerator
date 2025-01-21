using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
	/// Выражение break.
	/// </summary>
	public partial class BreakExpression : AE
{
  public BreakExpression() : base(TT.BreakExpression) => Add(Break);






  /// <summary>
  /// Разделитель дочерних элементов - пробел.
  /// </summary>
  public override string ItemsDivider => " ";
  

  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("break"));

}
