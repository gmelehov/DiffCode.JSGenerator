namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Определение лямбда-оператора =>
  /// </summary>
  public partial class LambdaOperator : AT
	{
		public LambdaOperator()
		{
			_type = TT.LambdaOper;
			Add(Assign, MoreThan);
		}






		/// <summary>
		/// Форматированное строковое представление объекта
		/// </summary>
		public override string Formatted
		{
			get
			{
				var sb = new StringBuilder("");
				foreach (var it in _childTokens.Where(IsNotNull))
				{
					sb.Append(it.Formatted);
					sb.Append(ItemsDivider);
				};
				return sb.ToString();
			}
		}





    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XAttribute("op", "lambda");







    public override bool Equals(AT other) => _type == other.GetTokenType() && GetSelfValue() != null && other.GetSelfValue() != null && GetSelfValue().Equals(other.GetSelfValue());
		public override bool Equals(AT x, AT y) => x.Equals(y);
		public override int GetHashCode(AT obj) => obj.GetHashCode();
	}
}
