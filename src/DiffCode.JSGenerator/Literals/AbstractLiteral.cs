namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Абстрактный нетипизированный литерал.
  /// </summary>
  public abstract partial class AbstractLiteral : AE
  {
		protected AbstractLiteral() : base() { }
		protected AbstractLiteral(object val)
		{
			var type = val.GetType();
			switch (Type.GetTypeCode(type))
			{
        case TypeCode.Object: _value = new StringLiteral(val).GetSelfValue(); break;
        case TypeCode.Char:
				case TypeCode.String: _value = new StringLiteral(val.ToString()).GetSelfValue(); break;
				case TypeCode.Boolean: _value = new BooleanLiteral(val).GetSelfValue(); break;
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Decimal:
				case TypeCode.Double: _value = new NumberLiteral(val).GetSelfValue(); break;

				default: break;
			};
		}


		/// <summary>
		/// Форматированное строковое представление объекта.
		/// </summary>
		public override string Formatted => _value?.ToString();

	}







	/// <summary>
	/// Абстрактный строго типизированный литерал.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract partial class AbstractLiteral<T> : AbstractLiteral where T : IEquatable<T>
	{
		protected new T _value;
		protected AbstractLiteral() : base() { }
		protected AbstractLiteral(T val) : base() { _value = val; }





		/// <summary>
		/// Метод для установки значения объекта.
		/// Возвращает исходный литерал с измененным значением.
		/// </summary>
		/// <param name="val">Новое значение литерала.</param>
		/// <returns></returns>
		protected AbstractLiteral<T> SetValue(T val)
		{
			_value = val;
			return this;
		}

	}

}
