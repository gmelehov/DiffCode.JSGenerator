using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FB = DiffCode.JSGenerator.FnBodyBlock;
using SL = DiffCode.JSGenerator.StringLiteral;
using static DiffCode.JSGenerator.XmlParse;



namespace DiffCode.JSGenerator;

  /// <summary>
  /// Выражение switch.
  /// </summary>
  public class SwitchExpr : AE
{
	public SwitchExpr() : base(TT.SwitchExpression)
	{
		_type = TT.SwitchExpression;
		var _args = Args();
		var _body = FnBody();
		Add(Switch, _args, _body);
	}
	public SwitchExpr(CaseExpression expr) : base(TT.SwitchExpression)
	{
		_type = TT.SwitchExpression;
		var _args = Args();
		var _body = FnBody(expr);
		Add(Switch, _args, _body);
	}
	public SwitchExpr(params CaseExpression[] exprs) : base(TT.SwitchExpression)
	{
		_type = TT.SwitchExpression;
		var _args = Args();
		var _body = FnBody(exprs);
		Add(Switch, _args, _body);
	}
	public SwitchExpr(AE arg, params CaseExpression[] exprs) : base(TT.SwitchExpression)
	{
		_type = TT.SwitchExpression;
		var _args = Args(arg);
		var _body = FnBody(exprs);
		var _arg = (arg is SL) ?
			ID(arg.Formatted)
			:
			arg;
		Add(Switch, _args, _body);
	}
	public SwitchExpr(string arg, params CaseExpression[] exprs) : base(TT.SwitchExpression)
	{
		_type = TT.SwitchExpression;
		var _args = Args(ID(arg));
		var _body = FnBody(exprs);
		Add(Switch, _args, _body);
	}
    public SwitchExpr(AE arg, IEnumerable<CaseExpression> cases, CaseExpression defcase) : base(TT.SwitchExpression)
    {
      _type = TT.SwitchExpression;
      var _args = Args(arg);
      var _body = FnBody(cases.Append(defcase));
      var _arg = (arg is SL) ?
        ID(arg.Formatted)
        :
        arg;
      Add(Switch, _args, _body);
    }







    /// <summary>
    /// Изменяет количество отступов (пустых строк) перед элементом.
    /// </summary>
    /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
    public new SwitchExpr SetGap(int gapbefore)
    {
      this._gapBefore = gapbefore;
      return this;
    }





    /// <summary>
    /// Тестируемый параметр.
    /// </summary>
    public ARG Arguments => _childTokens?.First()?.Next?.As<ARG>();
	
	/// <summary>
	/// Блок кода с выражениями case.
	/// </summary>
	public FB Body => Arguments?.Next?.As<FB>();





	/// <summary>
	/// Форматированное строковое представление объекта.
	/// </summary>
	public override string Formatted
	{
		get
		{
			var sb = new StringBuilder("");
        AppendLineAndIndent(ref sb, _gapBefore);
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
			sb.Append(Switch.Formatted);
			sb.Append(" ");
			sb.Append(Arguments?.Formatted);
			sb.Append(Body?.Formatted);
			return sb.ToString();
		}
	}




    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => 
      _gapBefore > 0
      ?
      new XElement(JsNS("switch"), new XAttribute("gap", _gapBefore), Arguments.ChildTokens.FirstOrDefault().AsXml, (Body?.AsXml as XElement).Elements())
      :
      new XElement(JsNS("switch"), Arguments.ChildTokens.FirstOrDefault().AsXml, (Body?.AsXml as XElement).Elements());

  }
