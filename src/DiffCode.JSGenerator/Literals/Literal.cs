namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Абстрактный литерал (число, строка, регулярное выражение, идентификатор).
  /// </summary>
  public abstract partial class Literal : AE
	{
		public Literal(TT type, object val)
		{
      if (type == TT.Identifier || type == TT.String || type == TT.Number || type == TT.Regexp || type == TT.NONE || type == TT.Newline)
      {
        _type = type;
        _value = val;
      };
    }



		public override object GetSelfValue()
		{
			switch (GetTokenType())
			{
        //case TT.Identifier: return _value?.ToString();
        //case TT.Number: return _value.ToString().Replace(",", ".").Trim(new char[] { '\'' });
        //case TT.String: return (_value?.ToString() == "\r\n") ? _value?.ToString() : String.Join("", "'", _value?.ToString().Trim(new char[] { '\'' }), "'");
        //case TT.Regexp: return (_value?.ToString() == "\r\n") ? _value?.ToString() : String.Join("", "/", _value?.ToString().Trim(new char[] { '\'' }), "/");
        //case TT.NONE: return (_value?.ToString() == "\r\n") ? _value?.ToString() : _value?.ToString().Trim(new char[] { '\'' });

        case TT.Identifier: return _value?.ToString();

        case TT.Number: return _value.ToString().Replace(",", ".").Trim(new char[] { '\'' });

        case TT.String: return (_value?.ToString() == "\r\n" || _value?.ToString().Trim(new char[] { '\r', '\n' }) == "") ? _value?.ToString() : String.Join("", "'", _value?.ToString().Trim(new char[] { '\'' }), "'");

        case TT.Regexp: return (_value?.ToString() == "\r\n") ? _value?.ToString() : String.Join("", "/", _value?.ToString().Trim(new char[] { '\'' }), "/");

        case TT.NONE: return (_value?.ToString() == "\r\n") ? _value?.ToString() : _value?.ToString().Trim(new char[] { '\'' });



        default: return _value?.ToString();
			};
		}



    
		public override bool Equals(AT other) => _type == other.GetTokenType() && GetSelfValue().Equals(other?.GetSelfValue());
		public override bool Equals(AT x, AT y) => x.Equals(y);
		public override int GetHashCode(AT obj) => obj.GetHashCode();

	}
}
