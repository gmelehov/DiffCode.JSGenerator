using System.Linq.Expressions;





namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Методы-маркеры, возвращающие признаки наличия возможности упрощения JS-синтаксиса.
  /// </summary>
  public static partial class XmlParse
  {


    public static Func<XElement, bool> isOR => xelem => xelem != null && xelem.Name.LocalName == "or";
    public static Func<XElement, bool> isAND => xelem => xelem != null && xelem.Name.LocalName == "and";
    public static Func<XElement, bool> isPLUS => xelem => xelem != null && xelem.Name.LocalName == "plus";
    public static Func<XElement, bool> isMINUS => xelem => xelem != null && xelem.Name.LocalName == "minus";
    public static Func<XElement, bool> isMULTIPLY => xelem => xelem != null && xelem.Name.LocalName == "multiply";



    public static Func<XElement, bool> hasOR => xelem => xelem != null && xelem.HasElement(JsNS("or"));
    public static Func<XElement, bool> hasAND => xelem => xelem != null && xelem.HasElement(JsNS("and"));
    public static Func<XElement, bool> hasPLUS => xelem => xelem != null && xelem.HasElement(JsNS("plus"));
    public static Func<XElement, bool> hasMINUS => xelem => xelem != null && xelem.HasElement(JsNS("minus"));
    public static Func<XElement, bool> hasMULTIPLY => xelem => xelem != null && xelem.HasElement(JsNS("multiply"));




    public static Func<XElement, bool> NestedOR => xelem => isOR(xelem) && hasOR(xelem);
    public static Func<XElement, bool> NestedAND => xelem => isAND(xelem) && hasAND(xelem);
    public static Func<XElement, bool> NestedPLUS => xelem => isPLUS(xelem) && hasPLUS(xelem);
    public static Func<XElement, bool> NestedMINUS => xelem => isMINUS(xelem) && hasMINUS(xelem);
    public static Func<XElement, bool> NestedMULTIPLY => xelem => isMULTIPLY(xelem) && hasMULTIPLY(xelem);


    public static Func<XElement, IEnumerable<XElement>> GetNestedOR => xelem => xelem.Element(JsNS("or")).Elements();
    public static Func<XElement, IEnumerable<XElement>> GetNestedAND => xelem => xelem.Element(JsNS("and")).Elements();
    public static Func<XElement, IEnumerable<XElement>> GetNestedPLUS => xelem => xelem.Element(JsNS("plus")).Elements();
    public static Func<XElement, IEnumerable<XElement>> GetNestedMINUS => xelem => xelem.Element(JsNS("minus")).Elements();
    public static Func<XElement, IEnumerable<XElement>> GetNestedMULTIPLY => xelem => xelem.Element(JsNS("multiply")).Elements();




    public static Expression<Func<XElement, bool>> CanBeSimplified => xelem => NestedOR(xelem) || NestedAND(xelem) || NestedPLUS(xelem) || NestedMINUS(xelem) || NestedMULTIPLY(xelem);

    public static Func<XElement, IEnumerable<XElement>> GetSimplifiableElements => xelem =>
    NestedOR(xelem) ?
    GetNestedOR(xelem)
    :
    NestedAND(xelem) ?
    GetNestedAND(xelem)
    :
    NestedPLUS(xelem) ?
    GetNestedPLUS(xelem)
    :
    NestedMINUS(xelem) ?
    GetNestedMINUS(xelem)
    :
    NestedMULTIPLY(xelem) ?
    GetNestedMULTIPLY(xelem)
    :
    null;

  }
}
