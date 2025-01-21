using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение индекса массива.
/// </summary>
public class ArrayIndex : CBlock
{
	public ArrayIndex() : base(TT.ArrayIndexDef) { }
	public ArrayIndex(int index) : base(TT.ArrayIndexDef) => Add(Num(index));
	public ArrayIndex(string index) : base(TT.ArrayIndexDef) => Add(Str(index));
	public ArrayIndex(AT index) : base(TT.ArrayIndexDef) => Add(index);







	/// <summary>
	/// Разделитель дочерних элементов - запятая.
	/// </summary>
	public override string ItemsDivider => ",";

	/// <summary>
	/// Форматированное строковое представление объекта.
	/// </summary>
	public override string Formatted
	{
		get
		{
			var sb = new StringBuilder("");
			sb.Append("[");
			sb.Append(String.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted)));
			sb.Append("]");
			return sb.ToString();
		}
	}






    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => new XElement(JsNS("index"), _childTokens.FirstOrDefault()?.AsXml);

  }
