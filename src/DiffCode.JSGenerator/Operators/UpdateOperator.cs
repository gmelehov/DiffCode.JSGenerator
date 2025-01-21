namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Определение оператора приращения (++) / уменьшения (--)
  /// </summary>
  public partial class UpdateOperator : AT
	{
		public UpdateOperator(TT type)
		{
			_type = type;
			var items = ComputeItems(type);
			Add(items);
		}







		/// <summary>
		/// Вычисляет список необходимых дочерних элементов на основании указанного типа токена.
		/// </summary>
		/// <param name="type">Тип токена, для которого необходимо вычислить список соответствующих дочерних элементов</param>
		/// <returns></returns>
		public List<ST> ComputeItems(TT type)
		{
			switch (type)
			{
				case TT.IncreaseOper: return Chain(PLUS, PLUS).Cast<ST>().ToList();
				case TT.DecreaseOper: return Chain(MINUS, MINUS).Cast<ST>().ToList();
				default: return null;
			};
		}



		/// <summary>
		/// Форматированное строковое представление объекта.
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
    public override XObject AsXml => null;





    public override bool Equals(AT other) => _type == other.GetTokenType() && GetSelfValue() != null && other.GetSelfValue() != null && GetSelfValue().Equals(other.GetSelfValue());
		public override bool Equals(AT x, AT y) => x.Equals(y);
		public override int GetHashCode(AT obj) => obj.GetHashCode();

	}
}
