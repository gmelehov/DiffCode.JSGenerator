using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FB = DiffCode.JSGenerator.FnBodyBlock;
using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Выражение FOR IN
/// </summary>
public class ForInExpr : AE
{
  protected InExpr _inexpr;
  protected AbstractExpression _left;
  protected AbstractExpression _right;
  protected ARG loopDef;
  protected FB loopBody;





  public ForInExpr(InExpr inexpr, ExpressionsSet exprs) : base(TT.ForInExpression)
  {
    _type = TT.ForInExpression;
    var argsBlock = Args(true);
    var bodyBlock = FnBody(exprs.ChildTokens.Cast<AE>());
    _inexpr = inexpr;
    argsBlock.Add(inexpr);
    loopBody = bodyBlock;
    loopDef = argsBlock;
    Add(argsBlock, bodyBlock);
  }








  /// <summary>
		/// Определение цикла for-in: (var key in obj)
		/// </summary>
		public ARG LoopDefinition => loopDef;

  public InExpr InExpr => _inexpr;

  /// <summary>
  /// Блок выражений/исполняемых инструкций, выполняемых в цикле for-in: { ... }
  /// </summary>
  public FB LoopBody => loopBody;





  /// <summary>
		/// Разделитель дочерних элементов - пробел
		/// </summary>
		public override string ItemsDivider => " ";

  /// <summary>
  /// Форматированное строковое представление объекта
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
      sb.Append(For.Formatted);
      sb.Append(ItemsDivider);
      sb.Append(LoopDefinition?.Formatted);
      sb.Append(LoopBody?.Formatted);
      return sb.ToString();
    }
  }







  public override XObject AsXml =>
    new XElement(
      JsNS("for-in"),
      InExpr.AsXml,
      LoopBody.ChildTokens.Select(s => s.AsXml)
      );
}
