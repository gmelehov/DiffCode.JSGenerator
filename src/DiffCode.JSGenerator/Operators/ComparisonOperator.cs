namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Определение оператора сравнения
  /// </summary>
  public partial class ComparisonOperator : AT
	{
		public ComparisonOperator(TT type)
		{
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
				case TT.IsEqual: return Chain(Assign, Assign, Assign).Cast<ST>().ToList();
				case TT.IsNotEqual: return Chain(Not, Assign, Assign).Cast<ST>().ToList();
				case TT.IsGreater: return Chain(MoreThan).Cast<ST>().ToList();
				case TT.IsGreaterOrEqual: return Chain(MoreThan, Assign).Cast<ST>().ToList();
				case TT.IsLesser: return Chain(LessThan).Cast<ST>().ToList();
				case TT.IsLesserOrEqual: return Chain(LessThan, Assign).Cast<ST>().ToList();
				case TT.BooleanAND: return Chain(And, And).Cast<ST>().ToList();
				case TT.BooleanOR: return Chain(Or, Or).Cast<ST>().ToList();

				case TT.InstanceOf: return Chain(InstanceOf).Cast<ST>().ToList();

				default: return Chain(True).Cast<ST>().ToList();
			};
		}






		public override object GetSelfValue() => ToString();

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
    public override XObject AsXml => null;







    public override bool Equals(AT other) => _type == other.GetTokenType() && GetSelfValue().Equals(other.GetSelfValue());
		public override bool Equals(AT x, AT y) => x.Equals(y);
		public override int GetHashCode(AT obj) => obj.GetHashCode();
	}
}
