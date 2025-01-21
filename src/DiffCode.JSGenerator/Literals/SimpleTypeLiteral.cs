namespace DiffCode.JSGenerator
{
  /// <summary>
  /// 
  /// </summary>
  public partial class SimpleTypeLiteral : Literal
	{
		private new string _value;
		private IFormatted _object;





		public SimpleTypeLiteral(IFormatted val) : base(TT.String, val)
		{
			_object = val;
			_value = val?.Formatted;

			bool result = Double.TryParse(val.Formatted, out double dbl);
			if ((result && !val.Formatted.Contains(" ")))
			{
				Add(Num(dbl));
				_type = TT.Number;
			}
			else
			{
				Add(Str(val.Formatted));
				_type = TT.String;
			};
		}






		public IFormatted SimpleTypeObject => _object;


		/// <summary>
		/// Форматированное строковое представление объекта.
		/// </summary>
		public override string Formatted
		{
			get
			{
				var sb = new StringBuilder("");
				bool result = Double.TryParse(SimpleTypeObject.Formatted, out double dbl);
				var _val =
					(HasItems())
					?
					(result && !SimpleTypeObject.Formatted.Contains(" ")) ? SimpleTypeObject.Formatted : "'" + SimpleTypeObject.Formatted + "'"
					:
					GetSelfValue().ToString();
				sb.Append(_val);
				return sb.ToString();
			}
		}




    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement("str", new XAttribute("value", Formatted.Trim('\'')));

  }
}
