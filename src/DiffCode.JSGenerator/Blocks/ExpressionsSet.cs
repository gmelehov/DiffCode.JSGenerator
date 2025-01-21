using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Набор выражений/исполняемых инструкций JavaScript.
/// </summary>
public class ExpressionsSet : CBlock
{
  public ExpressionsSet() : base(TT.SetOfExpressions) { }
  public ExpressionsSet(params AE[] exprs) : base(TT.SetOfExpressions)
  {
    Add(exprs);
  }
  public ExpressionsSet(IEnumerable<AE> exprs) : base(TT.SetOfExpressions) => AddExpressions(exprs.ToArray());
  public ExpressionsSet(ExpressionsSet other) : base(TT.SetOfExpressions) => Add(other.ChildTokens);
  public ExpressionsSet(AE first, IEnumerable<AE> exprs) : base(TT.SetOfExpressions)
  {
    Add(first);
    Add(exprs);
  }
  public ExpressionsSet(IEnumerable<AE> exprs, AE second) : base(TT.SetOfExpressions)
  {
    Add(exprs);
    Add(second);
  }
  public ExpressionsSet(params IEnumerable<AE>[] exprs) : base(TT.SetOfExpressions)
  {
    foreach (var ex in exprs)
    {
      AddExpressions(ex.ToArray());
    };
  }
  public ExpressionsSet(CommentItem comment, params IEnumerable<AE>[] exprs) : base(TT.SetOfExpressions)
  {
    Comment = comment;
    foreach (var ex in exprs)
    {
      AddExpressions(ex.ToArray());
    };
  }








  public ExpressionsSet AddExpression(AE expr)
  {
    Add(expr);
    return this;
  }

  public ExpressionsSet AddExpressions(params AE[] exprs)
  {
    Add(exprs);
    return this;
  }

  public ExpressionsSet AddExpressions(IEnumerable<AE> exprs)
  {
    Add(exprs);
    return this;
  }

  public ExpressionsSet MergeExpressionsSets(ExpressionsSet other)
  {
    Add(other?.ChildTokens);
    return this;
  }






  /// <summary>
  /// Разделитель дочерних элементов - перевод строки / точка с запятой.
  /// </summary>
  public override string ItemsDivider => HasParent() ? ";\r\n" : ";";

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
        if (Comment is CommentBlock)
        {
          foreach (var c in Comment.ChildTokens)
          {
            sb.Append(c.Formatted + IndentString);
          };
        }
        else
        {
          sb.Append(Comment.Formatted + IndentString);
        };
      };
      if (_childTokens.Count > 0) // Если в этом объекте присутствуют дочерние элементы (выражения, исполняемые инструкции и т.д.)
      {
        foreach (var it in _childTokens.Where(IsNotNull)) // для всех дочерних элементов, не равных null
        {
          sb.Append(it.IndentString); // добавляем отступ дочернего элемента
          sb.Append(it.Formatted); // добавляем форматированное строковое представление дочернего элемента
          if (it.Formatted.Trim(new char[] { '\r', '\n', '\t', ' ', '\'' }).IsUseful()) sb.Append(ItemsDivider); // добавляем разделитель после каждого дочернего элемента, включая последний
          sb.AppendLine(); // добавляем новую строку
        };
      };
      return sb.ToString();
    }
  }





  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("region"), _childTokens.Select(s => s.AsXml));










  public static ExpressionsSet operator &(ExpressionsSet first, ExpressionsSet second) => first.MergeExpressionsSets(second);
  public static ExpressionsSet operator +(ExpressionsSet first, ExpressionsSet second) => first.MergeExpressionsSets(second);
  public static ExpressionsSet operator +(ExpressionsSet first, AE second) => first.AddExpression(second);
  public static ExpressionsSet operator +(ExpressionsSet first, AE[] exprs) => first.AddExpressions(exprs);
  public static ExpressionsSet operator +(ExpressionsSet first, IEnumerable<AE> exprs)
  {
    //var ret = EXPRS();
    var ret = EXPRS(first.ChildTokens.Cast<AE>().ToArray());
    //ret.Add(first.ChildTokens);
    ret.Add(exprs);
    return ret;
  }

}
