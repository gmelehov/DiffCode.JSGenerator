namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Булевый литерал.
  /// </summary>
  public partial class BooleanLiteral : AbstractLiteral<bool>
	{
    protected bool _val_;




		public BooleanLiteral(bool val) : base(val) { _val_ = val; }
		public BooleanLiteral(object val)
		{
			bool.TryParse(val.ToString().Trim(new char[] { '\'', '"', ' ' }), out bool result);
			base.SetValue(result);
      _val_ = result;
		}






		/// <summary>
		/// Форматированное строковое представление объекта.
		/// </summary>
		public override string Formatted => _value.ToString();


    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => _val_ ? TRUE.AsXml : FALSE.AsXml;

  }
}
