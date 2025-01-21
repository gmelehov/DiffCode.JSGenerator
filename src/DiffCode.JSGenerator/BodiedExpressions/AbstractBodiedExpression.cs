using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class AbstractBodiedExpression : AE
  {
    public bool _hasArgsBlock = false;





    protected AbstractBodiedExpression(TT type, ArgumentsBlock catchArgs = null, params AE[] exprs)
    {
      if (catchArgs == null)
        catchArgs = Args(ID("E"));

      var bodyBlock = FnBody(exprs);

      switch (type)
      {
        case TT.Try:
          base.Add(Try, bodyBlock);
          break;
        case TT.Catch:
          base.Add(Catch, catchArgs, bodyBlock);
          _hasArgsBlock = true;
          break;
        case TT.Finally:
          base.Add(Finally, bodyBlock);
          break;

        case TT.While:
          base.Add(While, catchArgs, bodyBlock);
          _hasArgsBlock = true;
          break;
        case TT.DoWhileExpression:
          base.Add(Do, bodyBlock, While, catchArgs);
          _hasArgsBlock = true;
          break;


        default:
          break;
      };
    }





    public AT KeyWord => _childTokens?.FirstOrDefault();
    public ArgumentsBlock Arguments => _hasArgsBlock ? KeyWord?.Next?.As<ArgumentsBlock>() : null;
    public FnBodyBlock Body => _childTokens?.LastOrDefault().As<FnBodyBlock>();






    /// <summary>
		/// Разделитель дочерних элементов - пробел.
		/// </summary>
		public override string ItemsDivider => " ";

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

        sb.Append(KeyWord.Formatted);
        if (Arguments != null)
          sb.Append(Arguments?.Formatted);
        sb.Append(Body?.Formatted);
        return sb.ToString();
      }
    }






    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml =>
      _hasArgsBlock ?
        new XElement(JsNS(KeyWord.Formatted), Arguments.AsXml, Body.ChildTokens.Select(s => s.AsXml))
        :
        new XElement(JsNS(KeyWord.Formatted), Body.ChildTokens.Select(s => s.AsXml));

  }
}
