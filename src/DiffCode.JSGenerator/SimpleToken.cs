using System.Diagnostics;



namespace DiffCode.JSGenerator;

/// <summary>
/// Простейшие элементы языка (ключевые слова, комментарии)
/// </summary>
public class SimpleToken : AT
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private object _val;





  public SimpleToken() => _type = TT.NONE;
  public SimpleToken(object val)
  {
    _type = TT.NONE;
    _val = val;
  }
  public SimpleToken(TT type) => _type = type;






  public override object GetSelfValue() => (TB.Dict.ContainsKey(_type)) ? TB.Dict[_type].Value : "";




  /// <summary>
  /// Форматированное строковое представление объекта.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      var _val = (HasItems()) ? string.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted)) : GetSelfValue().ToString();
      sb.Append(_val);
      return sb.ToString();
    }
  }



  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => null;




  public override bool Equals(AT other) => _type == other.GetTokenType() && GetSelfValue().Equals(other.GetSelfValue());
  public override bool Equals(AT x, AT y) => x.Equals(y);
  public override int GetHashCode(AT obj) => obj.GetHashCode();
}
