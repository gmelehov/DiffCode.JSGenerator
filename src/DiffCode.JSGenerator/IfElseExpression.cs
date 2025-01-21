using static DiffCode.JSGenerator.XmlParse;

using FB = DiffCode.JSGenerator.FnBodyBlock;



namespace DiffCode.JSGenerator;

/// <summary>
/// Выражение IF...ELSE.
/// </summary>
public class IfElseExpression : AE
{
  public IfElseExpression(ExprStatement test, FB ifBlock) : base(TT.IfElseStatement) => Add(If, test, ifBlock);
  public IfElseExpression(ExprStatement test, params AE[] ifExprs) : base(TT.IfElseStatement)
  {
    var fnbody = FnBody(ifExprs);
    Add(If, test, fnbody);
  }
  public IfElseExpression(AbstractBinaryExpression test, params AE[] ifExprs) : base(TT.IfElseStatement)
  {
    var fnbody = FnBody(ifExprs);
    Add(If, test, fnbody);
  }
  public IfElseExpression(AbstractUnaryExpression test, params AE[] ifExprs) : base(TT.IfElseStatement)
  {
    var fnbody = FnBody(ifExprs);
    Add(If, test, fnbody);
  }
  public IfElseExpression(CommentItem comment, AbstractBinaryExpression test, params AE[] ifExprs) : base(TT.IfElseStatement)
  {
    Comment = comment;
    var fnbody = FnBody(ifExprs);
    Add(If, test, fnbody);
  }
  public IfElseExpression(CommentItem comment, AbstractUnaryExpression test, params AE[] ifExprs) : base(TT.IfElseStatement)
  {
    Comment = comment;
    var fnbody = FnBody(ifExprs);
    Add(If, test, fnbody);
  }
  public IfElseExpression(ExprStatement test, FB ifBlock, FB elseBlock) : base(TT.IfElseStatement) => Add(If, test, ifBlock, Else, elseBlock);
  public IfElseExpression(AE test, FB ifBlock, params AE[] elseExprs) : base(TT.IfElseStatement) => Add(If, test, ifBlock, Else, (elseExprs != null && elseExprs.Count() > 0) ? FnBody(elseExprs) : null);
  public IfElseExpression(CommentItem comment, ExprStatement test, FB ifBlock, FB elseBlock) : base(TT.IfElseStatement)
  {
    Comment = comment;
    Add(If, test, ifBlock, Else, elseBlock);
  }
  public IfElseExpression(ExprStatement test, FB ifBlock, params AE[] elseExprs) : base(TT.IfElseStatement)
  {
    var fnbody = FnBody(elseExprs);
    Add(If, test, ifBlock, Else, fnbody);
  }
  public IfElseExpression(AbstractBinaryExpression test, FB ifBlock, params AE[] elseExprs) : base(TT.IfElseStatement)
  {
    var fnbody = FnBody(elseExprs);
    Add(If, test, ifBlock, Else, fnbody);
  }
  public IfElseExpression(AbstractUnaryExpression test, FB ifBlock, params AE[] elseExprs) : base(TT.IfElseStatement)
  {
    var fnbody = FnBody(elseExprs);
    Add(If, test, ifBlock, Else, fnbody);
  }
  public IfElseExpression(AbstractUnaryExpression test, AE trueExpr, AE falseExpr) : base(TT.IfElseStatement) => Add(If, test, FnBody(trueExpr), Else, FnBody(falseExpr));
  public IfElseExpression(AbstractBinaryExpression test, AE trueExpr, AE falseExpr) : base(TT.IfElseStatement) => Add(If, test, FnBody(trueExpr), Else, FnBody(falseExpr));
  public IfElseExpression(AbstractExpression test, AE trueExpr, AE falseExpr) : base(TT.IfElseStatement) => Add(If, test, FnBody(trueExpr), Else, FnBody(falseExpr));

  public IfElseExpression(CommentItem comment, AbstractBinaryExpression test, FB ifBlock, params AE[] elseExprs) : base(TT.IfElseStatement)
  {
    Comment = comment;
    var fnbody = FnBody(elseExprs);
    Add(If, test, ifBlock, Else, fnbody);
  }
  public IfElseExpression(CommentItem comment, AbstractUnaryExpression test, FB ifBlock, params AE[] elseExprs) : base(TT.IfElseStatement)
  {
    Comment = comment;
    var fnbody = FnBody(elseExprs);
    Add(If, test, ifBlock, Else, fnbody);
  }







  /// <summary>
  /// Изменяет количество отступов (пустых строк) перед элементом.
  /// </summary>
  /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
  public new IfElseExpression SetGap(int gapbefore)
  {
    _gapBefore = gapbefore;
    return this;
  }





  /// <summary>
  /// Блок проверки значения выражения.
  /// </summary>
  public AE test => (_childTokens?.First()?.Next ?? null) as AE;

  /// <summary>
  /// Блок команд, выполняемых в случае истинности проверяемого выражения.
  /// </summary>
  public FB ifBlock => (test?.Next ?? null) as FB;

  /// <summary>
  /// Блок команд, выполняемых в случае ложности проверяемого выражения.
  /// </summary>
  public FB elseBlock => (ifBlock?.Next?.Next ?? null) as FB;





  /// <summary>
  /// В случае <see langword="true"/> выражение будет представлено 
  /// в виде тернарной операции: TEST ? IfTrue : IfFalse.
  /// </summary>
  public bool AsTernary { get; set; }

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

      if (!AsTernary)
        FormatAsUsual(ref sb);
      else
        if (elseBlock != null)
      {
        sb.Append(test?.Formatted);
        sb.Append(" ");
        sb.Append(TernaryIf.Formatted);
        sb.Append(" ");
        //sb.AppendLine();
        //sb.Append("  " + IndentString);
        sb.Append(ifBlock.ChildTokens.FirstOrDefault()?.Formatted);
        //sb.AppendLine();
        //sb.Append(IndentString);
        sb.Append(" ");
        sb.Append(Colon.Formatted);
        //sb.AppendLine();
        //sb.Append("  " + IndentString);
        sb.Append(" ");
        sb.Append(elseBlock.ChildTokens.FirstOrDefault()?.Formatted);
      }
      else
        FormatAsUsual(ref sb);

      return sb.ToString();
    }
  }

  private void FormatAsUsual(ref StringBuilder sb)
  {
    sb.Append(_childTokens?.First()?.Formatted);
    sb.Append(" ");

    if (test is Identifier || test is MemberAccess)
      sb.Append("(");

    sb.Append(test?.Formatted);

    if (test is Identifier || test is MemberAccess)
      sb.Append(")");

    sb.Append(ifBlock?.Formatted);

    if (elseBlock != null && elseBlock.ChildTokens.Count() > 0)
    {
      sb.AppendLine();
      sb.Append(IndentString);
      sb.Append(Else.Formatted);

      if (elseBlock.ChildTokens.Count() == 1 && elseBlock.ChildTokens.First() is IfElseExpression)
      {
        sb.AppendLine();
        sb.Append(elseBlock.ChildTokens.First().IndentString);
        sb.Append(elseBlock.ChildTokens.First().Formatted);
      }
      else
      {
        sb.Append(elseBlock?.Formatted);
      };
    };
  }



  private bool IsInsideCase => this.Closest<CaseExpression>() != null;







  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml =>
    AsTernary == true ?
    new XElement(JsNS("tif"),
      test?.AsXml,
      ifBlock.ChildTokens.First().AsXml,
      elseBlock.ChildTokens.First().AsXml
      )
    :
    _gapBefore > 0
    ?
    new XElement(JsNS("if"), new XAttribute("gap", _gapBefore),
      test?.AsXml,
      new XElement(JsNS("then"), ifBlock?.ChildTokens.Select(s => s.AsXml)),
      elseBlock != null ?
      new XElement(JsNS("else"), elseBlock?.ChildTokens.Select(s => s.AsXml))
      :
      null
      )
    :
    new XElement(JsNS("if"),
      test?.AsXml,
      new XElement(JsNS("then"), ifBlock?.ChildTokens.Select(s => s.AsXml)),
      elseBlock != null ?
      new XElement(JsNS("else"), elseBlock?.ChildTokens.Select(s => s.AsXml))
      :
      null
      );

}
