using System.Diagnostics;


namespace DiffCode.JSGenerator;

/// <summary>
/// Абстрактный класс, описывающий объект "JS-токен".
/// </summary>
public abstract class AbstractToken : IEquatable<AT>, IEqualityComparer<AT>, IFormatted, IHasSelfValue<object>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	protected object _value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	protected List<AT> _childTokens;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	protected TT _type;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	protected string _extraIndent = "";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	protected object _node_;








	protected AbstractToken() => _childTokens = new List<AT>();
	protected AbstractToken(TT type)
    {
      _childTokens = new List<AT>();
      _type = type;
    }








    public void SetNodeObject(object node) => _node_ = node;




	public AT Add(IEnumerable<AT> list)
	{
      if(list != null && list.Where(IsNotNull).Count() > 0)
      {
        _childTokens.AddRange(list.Where(IsNotNull));
        RefreshAll();
      };      
		return this;
	}
	public AT Add(AT tok, IEnumerable<AT> list)
	{
		if (IsNotNull(tok)) _childTokens.Add(tok);
      if (list != null && list.Where(IsNotNull).Count() > 0)
      {
        _childTokens.AddRange(list?.Where(IsNotNull));
        RefreshAll();
      };
      return this;
	}
	public AT Add(params AT[] list)
	{
      if (list != null && list.Where(IsNotNull).Count() > 0) _childTokens?.AddRange(list?.Where(IsNotNull));
		RefreshAll();
		return this;
	}
	public AT Add(AT tok, params AT[] list)
	{
		if (IsNotNull(tok)) _childTokens.Add(tok);
      if (list != null && list.Where(IsNotNull).Count() > 0) _childTokens?.AddRange(list?.Where(IsNotNull));
		RefreshAll();
		return this;
	}
	public AT Add(AT tok1, AT tok2, params AT[] list)
	{
		if (IsNotNull(tok1)) _childTokens.Add(tok1);
		if (IsNotNull(tok2)) _childTokens.Add(tok2);
      if (list != null && list.Where(IsNotNull).Count() > 0) _childTokens?.AddRange(list?.Where(IsNotNull));
		RefreshAll();
		return this;
	}
	public AT Prepend(AT tok)
	{
		if (IsNotNull(tok))
		{
			tok.Parent = this;
			_childTokens = _childTokens.Prepend(tok).ToList();
			RefreshAll();
		};
		return this;
	}




	/// <summary>
	/// Метод для привязки собственных вложенных дочерних элементов.
	/// </summary>
	public void ReBindItems()
	{
		_childTokens?.ForEach(it =>
		{
			it.Parent = this;
			it.ReBindItems();
		});
	}
	
	/// <summary>
	/// Метод для удаления вложенных дочерних элементов, равных <see langword="null"/>.
	/// </summary>
	public void RemoveNullItems()
	{
		if (_childTokens != null) _childTokens.RemoveAll(r => IsNull(r));
	}
	
	/// <summary>
	/// Метод для проведения полной реорганизации коллекции собственных дочерних элементов.
	/// Выполняет повторную привязку всех собственных дочерних элементов к самому себе,
	/// производит их перенумерацию.
	/// </summary>
	public void RefreshAll()
	{
		ReBindItems();
		_childTokens?.ReOrder();
	}




	/// <summary>
	/// Возвращает тип токена.
	/// </summary>
	/// <returns></returns>
	public TT GetTokenType() => _type;
	
	/// <summary>
	/// Возвращает глубину вложенности токена по цепочке своих родительских элементов.
	/// </summary>
	/// <returns></returns>
	public int GetDepth() =>
		(Parent != null &&
			(Parent.GetTokenType() == TT.CodeBlock || Parent.GetTokenType() == TT.ArrayDefinition || Parent.GetTokenType() == TT.ObjectBodyBlock || Parent.GetTokenType() == TT.FnBodyBlock)) ?
				Parent.GetDepth() + 1
				:
				(Parent != null) ? Parent.GetDepth() : 0;
	
	/// <summary>
	/// Возвращает собственное значение.
	/// </summary>
	/// <returns></returns>
	public virtual object GetSelfValue() => _value;





    
    public int GetTotalLength() => Formatted.Length;
    public int GetTotalLines() => Formatted.Count(c => c == '\n');
    public int GetLastLineSeparatorIndex() => Formatted.LastIndexOf('\n');
    public string GetLastLine() => Formatted.Substring(GetLastLineSeparatorIndex() + 1);
    public int GetLastLineLength() => GetLastLine().Length;







	/// <summary>
	/// Родительский токен.
	/// </summary>
	public AT Parent { get; set; }
	
	/// <summary>
	/// Порядок следования токена в списке дочерних элементов своего родительского токена.
	/// </summary>
	public int Order { get; set; }
    
	/// <summary>
    /// Дополнительный отступ.
    /// </summary>
    public string ExtraIndent => _extraIndent;
	
	/// <summary>
	/// Строка отступа, используемая при расчете форматированного строкового представления токена.
	/// По умолчанию единичный отступ равен двум пробелам.
	/// </summary>
	public string IndentString => ((GetDepth() > 0) ? string.Join("", Enumerable.Range(1, GetDepth() * IndentSymbols).Select(s => IndentChar.ToString())) : "") + _extraIndent;
	
	/// <summary>
	/// Список дочерних элементов токена.
	/// </summary>
	public IEnumerable<AT> ChildTokens => _childTokens;





	/// <summary>
	/// Первый токен в списке дочерних элементов родительского токена.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public AT First => Parent?._childTokens?.First();

	/// <summary>
	/// Последний токен в списке дочерних элементов родительского токена.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public AT Last => Parent?._childTokens?.Last();

	/// <summary>
	/// Предыдущий (непосредственно перед текущим) токен в списке дочерних элементов родительского токена.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public AT Prev => Parent?._childTokens?.PrevToken(this);

	/// <summary>
	/// Следующий (непосредственно после текущего) токен в списке дочерних элементов родительского токена.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public AT Next => Parent?._childTokens?.NextToken(this);






    public bool IsTopHost() => !HasParent() && Order == 0;
	
	/// <summary>
	/// <see langword="true"/>, если токен имеет токена-родителя (вложен в его список дочерних элементов).
	/// </summary>
	public bool HasParent() => Parent != null && Parent.HasItems();
	
	/// <summary>
	/// <see langword="true"/>, если токен имеет вложенные дочерние элементы.
	/// </summary>
	public bool HasItems() => _childTokens != null && _childTokens?.Count > 0;




	/// <summary>
	/// <see langword="true"/>, если токен является первым в списке дочерних элементов своего родительского токена.
	/// </summary>
	public bool IsFirst() => First != null && Equals(First);
	
	/// <summary>
	/// <see langword="true"/>, если токен является последним в списке дочерних элементов своего родительского токена.
	/// </summary>
	public bool IsLast() => Last != null && Equals(Last);





	/// <summary>
	/// Разделитель дочерних элементов, используемый при расчете форматированного строкового представления токена.
	/// По умолчанию - пустая строка ("").
	/// </summary>
	public virtual string ItemsDivider => "";
	
	/// <summary>
	/// Комментарий к токену, используемый при расчете форматированного строкового представления токена.
	/// </summary>
	public virtual CommentItem Comment { get; set; }
	
	/// <summary>
	/// Форматированное строковое представление токена.
	/// </summary>
	public abstract string Formatted { get; }
    
	/// <summary>
    /// Реализация интерфейса IAsXml.
    /// XML-представление объекта.
    /// </summary>
    public abstract XObject AsXml { get; }







	public abstract bool Equals(AT other);
	public abstract bool Equals(AT x, AT y);
	public abstract int GetHashCode(AT obj);










	public override string ToString() => this.Formatted;











    /// <summary>
    /// Символ для строки отступа.
    /// </summary>
    public static char IndentChar = '\t';
	
	/// <summary>
	/// Количество символов в единичном отступе.
	/// </summary>
	public static int IndentSymbols = 1;
	
	/// <summary>
	/// Установить схему отступа "2 пробела".
	/// </summary>
	public static void SetDoubleSpaceIndent()
	{
		IndentChar = ' ';
		IndentSymbols = 2;
	}
	
	/// <summary>
	/// Установить схему отступа "1 табуляция".
	/// </summary>
	public static void SetTabIndent()
	{
		IndentChar = '\t';
		IndentSymbols = 1;
	}


	/// <summary>
	/// Размеры отступа перед определением ExtJS-класса и после него.
	/// Задается в виде кортежа из 2 чисел, например: (2, 1) - отступ 2 строки перед определениеи и 1 строка после него.
	/// Значение по умолчанию = (2, 2) - 2 пустые строки перед определением и 2 пустые строки после определения.
	/// </summary>
	public static (int top, int bottom) ClassDefHorizontalIndent => (2, 0);
	
	/// <summary>
	/// Размеры отступа перед определением сворачиваемого региона и после него.
	/// Задается в виде кортежа из 2 чисел, например: (2, 1) - отступ 2 строки перед определениеи и 1 строка после него.
	/// Значение по умолчанию = (1, 1) - 1 пустая строка перед определением и 1 пустая строка после определения.
	/// </summary>
	public static (int top, int bottom) JSRegionHorizontalIndent => (0, 0);

  }
