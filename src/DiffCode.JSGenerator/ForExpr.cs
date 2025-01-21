using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FB = DiffCode.JSGenerator.FnBodyBlock;
using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Выражение FOR
/// </summary>
public class ForExpr : AE
{
  protected AbstractExpression _init;
  protected AbstractBinaryExpression _test;
  protected UpdateExpression _upd;
  protected ARG loopDef;
  protected FB loopBody;





  public ForExpr(string loopvar, int seed, AbstractBinaryExpression testBlock, UpdateExpression upd, ExpressionsSet exprs) : base(TT.ForExpression)
  {
    _type = TT.ForExpression;
    var argsBlock = Args(true);
    var initBlock = ASSIGN(ID(loopvar), Num(seed));
    var bodyBlock = FnBody(exprs.ChildTokens.Cast<AE>());
    loopBody = bodyBlock;
    argsBlock.Add(initBlock, testBlock, upd);
    loopDef = argsBlock;
    Add(argsBlock, bodyBlock);
  }
  public ForExpr(Identifier loopvar, AE seed, AbstractBinaryExpression testBlock, UpdateExpression upd, ExpressionsSet exprs) : base(TT.ForExpression)
  {
    _type = TT.ForExpression;
    var argsBlock = Args(true);
    var initBlock = ASSIGN(loopvar, seed);
    var bodyBlock = FnBody(exprs.ChildTokens.Cast<AE>());
    loopBody = bodyBlock;
    argsBlock.Add(initBlock, testBlock, upd);
    loopDef = argsBlock;
    Add(argsBlock, bodyBlock);
  }
  public ForExpr(AbstractDeclaration init, AbstractBinaryExpression test, UpdateExpression upd, ExpressionsSet exprs) : base(TT.ForExpression)
  {
    _type = TT.ForExpression;
    var argsBlock = Args(true);
    var bodyBlock = FnBody(exprs.ChildTokens.Cast<AE>());
    _init = init;
    _test = test;
    _upd = upd;
    argsBlock.Add(init, test, upd);
    loopBody = bodyBlock;
    loopDef = argsBlock;
    Add(argsBlock, bodyBlock);
  }
  public ForExpr(AssignExpr init, AbstractBinaryExpression test, UpdateExpression upd, ExpressionsSet exprs) : base(TT.ForExpression)
  {
    _type = TT.ForExpression;
    var argsBlock = Args(true);
    var bodyBlock = FnBody(exprs.ChildTokens.Cast<AE>());
    _init = init;
    _test = test;
    _upd = upd;
    argsBlock.Add(init, test, upd);
    loopBody = bodyBlock;
    loopDef = argsBlock;
    Add(argsBlock, bodyBlock);
  }







  /// <summary>
		/// Определение цикла for: (i=100; i>0; i--)
		/// </summary>
		public ARG LoopDefinition => loopDef;

  /// <summary>
  /// Блок выражений/исполняемых инструкций, выполняемых в цикле for: { ... }
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
      JsNS("for"), 

      _init is AbstractDeclaration 
        ? 
        _init.As<AbstractDeclaration>().AsXml
        :
        _init.As<AssignExpr>().AsXml,

      //_init.AsXml, 
      _test.AsXml, 
      new XElement(JsNS("upd"), _upd), 
      LoopBody.ChildTokens.Select(s => s.AsXml)
      );

}
