using static DiffCode.JSGenerator.XmlParse;
using System.Globalization;






namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Числовой литерал (число).
  /// </summary>
  public partial class NumberLiteral : Literal
	{
		protected double? _number;
    protected object _hexval;
    protected int? _hexresult;





		public NumberLiteral(object val) : base(TT.Number, val)
		{
			bool result = double.TryParse(val.ToString(), out double dbl);
      bool hexresult = int.TryParse(val.ToString(), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out int hexres);

			_value = result ? dbl : val;
      _hexval = hexresult ? val : null;
      _hexresult = hexresult ? hexres as int? : null;
			_number = result ? dbl as double? : null;
		}
		public NumberLiteral(double val) : base(TT.Number, val)
		{
			bool result = double.TryParse(val.ToString(), out double dbl);
			_value = result ? dbl : val;
			_number = result ? dbl as double? : null;
		}





		public double? Number => _number;
    public int? HexNumber => _hexresult;




		public override object GetSelfValue() => _value;
		
		/// <summary>
		/// Форматированное строковое представление объекта
		/// </summary>
		public override string Formatted
		{
			get
			{
				var sb = new StringBuilder("");
				var _val = (HasItems()) ? string.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted)) : GetSelfValue().ToString();
				sb.Append(_val.Replace(",", ".").Trim(new char[] { '\'' }));
				return sb.ToString();
			}
		}



    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("num"), new XAttribute("value", Formatted.Trim('\'')));

  }
}
