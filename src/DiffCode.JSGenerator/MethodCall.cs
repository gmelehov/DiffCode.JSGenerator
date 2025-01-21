using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using MCall = DiffCode.JSGenerator.MethodCall;
using Memb = DiffCode.JSGenerator.MemberAccess;
using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

  /// <summary>
  /// Определение выражения для вызова метода (функции).
  /// </summary>
  public class MethodCall : AE
{
	public MethodCall(Memb member, ARG args) : base(TT.MethodCall) => Add(member, args);
	public MethodCall(CommentItem comment, Memb member, ARG args) : base(TT.MethodCall)
	{
		Comment = comment;
		Add(member, args);
	}
	public MethodCall(string member, ARG args) : base(TT.MethodCall) => Add(Member(member.Split('.')), args);
	public MethodCall(CommentItem comment, string member, ARG args) : base(TT.MethodCall)
	{
		Comment = comment;
		Add(Member(member.Split('.')), args);
	}
	public MethodCall(MCall other, Memb next, ARG args) : base(TT.MethodCall) => Add(Member(other, next), args);
	public MethodCall(MCall other, string next, ARG args) : base(TT.MethodCall) => Add(Member(other, Member(next.Split('.'))), args);
	public MethodCall(Memb member) : base(TT.MethodCall) => Add(member, Args());
	public MethodCall(string member) : base(TT.MethodCall) => Add(Member(member.Split('.')), Args());
	public MethodCall(CommentItem comment, string member) : base(TT.MethodCall)
	{
		Comment = comment;
		Add(Member(member.Split('.')), Args());
	}
	public MethodCall(MCall other, Memb next) : base(TT.MethodCall) => Add(Member(other, next), Args());
	public MethodCall(MCall other, string next) : base(TT.MethodCall) => Add(Member(other, Member(next.Split('.'))), Args());
	public MethodCall(Identifier obj, ARG args) : base(TT.MethodCall) => Add(Member(obj), args);







	/// <summary>
	/// Простое/составное имя свойства/метода объекта ДО круглых скобок.
	/// </summary>
	public Memb Member => _childTokens?.First().As<Memb>();

	/// <summary>
	/// Список аргументов, передаваемых при вызове (параметры метода в круглых скобках).
	/// </summary>
	public AE[] Arguments => Member?.Next?.ChildTokens?.Where(IsNotNull).Cast<AE>().ToArray();



	/// <summary>
	/// Форматированное строковое представление объекта.
	/// </summary>
	public override string Formatted
	{
		get
		{
			var sb = new StringBuilder("");
			if (IsNotNull(Comment))
			{
				if (!(Order == 0 && (HasParent() && Parent is CBlock)))
				{
					sb.AppendLine();
					sb.Append(IndentString);
				};
				if (Comment is CommentBlock)
				{
					foreach (var c in Comment.ChildTokens)
					{
						sb.Append(c.Formatted);
						sb.Append(IndentString);
					};
				}
				else
				{
					sb.Append(Comment.Formatted);
					sb.Append(IndentString);
				};
			};
			sb.Append(string.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted)));
			return sb.ToString();
		}
	}







    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml =>
      new XElement(JsNS("call"),
        new XAttribute("method", _childTokens.ExceptLast().LastOrDefault().Formatted.Trim('\'')),
        (_childTokens.LastOrDefault().AsXml as XElement).Elements()
        );

  }
