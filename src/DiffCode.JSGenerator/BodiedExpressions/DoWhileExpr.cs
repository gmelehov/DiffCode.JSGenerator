using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  public class DoWhileExpr : AbstractBodiedExpression
  {
    public DoWhileExpr(ArgumentsBlock args, params AE[] exprs) : base(TT.DoWhileExpression, args, exprs) { }
    public DoWhileExpr(ArgumentsBlock args, IEnumerable<AE> exprs) : base(TT.DoWhileExpression, args, exprs.ToArray()) { }





    public SimpleToken Do => _childTokens[0].As<SimpleToken>();
    public FnBodyBlock BodyStatement => _childTokens[1].As<FnBodyBlock>();
    public SimpleToken While => _childTokens[2].As<SimpleToken>();
    public ArgumentsBlock ArgumentsBlock => _childTokens[3].As<ArgumentsBlock>();






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

        sb.Append(Do.Formatted);
        sb.Append(BodyStatement.Formatted);
        sb.AppendLine();
        sb.Append(IndentString);
        sb.Append(While.Formatted);
        sb.Append(" ");
        if (ArgumentsBlock != null)
          sb.Append(ArgumentsBlock.Formatted);

        return sb.ToString();
      }
    }




    public override XObject AsXml => new XElement(JsNS("do-while"), BodyStatement.ChildTokens.Select(s=>s.AsXml), ArgumentsBlock.AsXml);
  }
}
