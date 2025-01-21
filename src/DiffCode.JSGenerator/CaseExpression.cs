using static DiffCode.JSGenerator.XmlParse;





namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Выражение CASE (DEFAULT).
  /// </summary>
  public class CaseExpression : AE
	{
		protected List<AE> _exprs;





		public CaseExpression() : base(TT.CaseExpression) { _exprs = null; ComputeItems(); }
		public CaseExpression(Literal caseVal, bool returnsValue) : base(TT.CaseExpression) { _exprs = null; ComputeItems(); }
    public CaseExpression(IEnumerable<AE> exprs) : base(TT.CaseExpression) { _exprs = exprs.ToList(); ComputeItems(exprs); }
		public CaseExpression(AT expr) : base(TT.CaseExpression) { _exprs = new List<AE> { expr as AE }; ComputeItems(expr); }
    public CaseExpression(params AE[] exprs) : base(TT.CaseExpression) { _exprs = exprs.ToList(); ComputeItems(exprs); }
    public CaseExpression(int caseVal, params AE[] exprs) : base(TT.CaseExpression) { _exprs = exprs.ToList(); ComputeItems(Num(caseVal), exprs); }
		public CaseExpression(string caseVal, params AE[] exprs) : base(TT.CaseExpression) { _exprs = exprs.ToList(); ComputeItems(Str(caseVal), exprs); }
		public CaseExpression(Literal caseVal, params AE[] exprs) : base(TT.CaseExpression) { _exprs = exprs.ToList(); ComputeItems(caseVal, exprs); }
    







    public void ComputeItems(AT expr) => Add(Default, expr);
    public void ComputeItems(params AE[] exprs) => Add(Default, exprs);
    public void ComputeItems(IEnumerable<AE> exprs) => Add(Default, exprs);
    public void ComputeItems(Literal caseVal, params AE[] exprs) => Add(Case, caseVal, exprs);
   








    public bool IsDefault => _childTokens?.First()?.GetTokenType() == TT.Default;
		public bool ReturnsValue => _childTokens?.Last()?.GetTokenType() == TT.Return;
    public bool IsEmpty => _childTokens?.LastOrDefault() is Literal;





    /// <summary>
    /// Разделитель дочерних элементов-выражений - пустая строка (для пустых выражений case),
    /// либо точка с запятой (для обычных выражений case).
    /// </summary>
    public override string ItemsDivider => IsEmpty ? "" : ";";
		/// <summary>
		/// Форматированное строковое представление объекта.
		/// </summary>
		public override string Formatted
		{
			get
			{
				var sb = new StringBuilder("");

        if(Prev == null || (Prev != null && Prev.GetTokenType() != TT.CaseExpression))
        {
          sb.AppendLine();
          sb.Append(IndentString);
        };
        
        /// default или case
				sb.Append(_childTokens?.First()?.Formatted);
				if (!IsDefault)
				{
					sb.Append(" ");
					sb.Append(_childTokens?.First()?.Next?.Formatted);
					sb.Append(Colon.Formatted);
				}
				else
					sb.Append(Colon.Formatted);

        if (_exprs != null)
					foreach (var it in _exprs.Where(IsNotNull))
					{
            sb.AppendLine();
            sb.Append(IndentString + "  ");
            //sb.Append(" ");
						sb.Append(it.Formatted);

            if (!it.Equals(_exprs.LastOrDefault()))
            {
              sb.Append(ItemsDivider);
            };
					};

        return sb.ToString();
			}
		}



    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => IsDefault ?
      new XElement(JsNS("default"), _exprs.Select(s => s?.AsXml))
      :
      new XElement(JsNS("case"), new XAttribute("value", _childTokens.Skip(1)?.FirstOrDefault()?.Formatted), _exprs.Select(s => s?.AsXml));







    public static CaseExpression[] operator +(CaseExpression first, CaseExpression second) => new CaseExpression[] { first, second };
		public static CaseExpression[] operator +(CaseExpression[] first, CaseExpression second) => first.Append(second).ToArray();
	}
}
