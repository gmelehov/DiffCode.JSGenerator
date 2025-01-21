using static DiffCode.JSGenerator.XmlParse;




namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Абстрактная операция объявления переменной.
  /// </summary>
  public abstract class AbstractDeclaration : AE
  {
    protected string _var;





    protected AbstractDeclaration(ST kind, string variable, AE expr = null) : base()
    {
      if ((kind.Formatted == "var" || kind.Formatted == "let" || kind.Formatted == "const") && variable.IsUseful() && !IsKeyword(variable))
      {
        base.Add(kind, ID(variable));
        if (expr != null)
          base.Add(AbstractOperator.Assign, expr);
        _var = variable;
      };
    }
    protected AbstractDeclaration(ST kind, Identifier variable, AE expr = null) : base()
    {
      if ((kind.Formatted == "var" || kind.Formatted == "let" || kind.Formatted == "const"))
      {
        base.Add(kind, variable);
        if (expr != null)
          base.Add(AbstractOperator.Assign, expr);
        _var = variable.Formatted.Trim('\'');
      };
    }
    protected AbstractDeclaration(CommentItem comment, ST kind, string variable, AE expr = null) : base(comment)
    {
      if ((kind.Formatted == "var" || kind.Formatted == "let" || kind.Formatted == "const") && variable.IsUseful() && !IsKeyword(variable))
      {
        base.Add(kind, ID(variable));
        if (expr != null)
          base.Add(AbstractOperator.Assign, expr);
        _var = variable;
      };
    }
    protected AbstractDeclaration(CommentItem comment, ST kind, Identifier variable, AE expr = null) : base(comment)
    {
      if ((kind.Formatted == "var" || kind.Formatted == "let" || kind.Formatted == "const"))
      {
        base.Add(kind, variable);
        if (expr != null)
          base.Add(AbstractOperator.Assign, expr);
        _var = variable.Formatted.Trim('\'');
      };
    }







    /// <summary>
    /// Ключевое слово присваивания.
    /// </summary>
    public ST Kind => _childTokens?.FirstOrDefault()?.As<ST>();
    
    /// <summary>
    /// Идентификатор (имя) переменной.
    /// </summary>
    public Identifier Variable => Kind?.Next?.As<Identifier>();
    
    /// <summary>
    /// Оператор присваивания.
    /// </summary>
    public AbstractOperator AssignOperator => Variable?.Next?.As<AbstractOperator>();
    
    /// <summary>
    /// Выражение присваивания.
    /// </summary>
    public AE Expression => _childTokens?.Last()?.As<AE>();





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
        AppendLineAndIndent(ref sb, _gapBefore);
        if (Comment != null)
        {
          if (Comment is CommentBlock)
          {
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.Append(IndentString);

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
        sb.Append(Kind?.Formatted);
        sb.Append(ItemsDivider);
        sb.Append(Variable?.Formatted);
        if (AssignOperator != null)
        {
          sb.Append(ItemsDivider);
          sb.Append(AssignOperator?.Formatted);
          sb.Append(ItemsDivider);
          sb.Append(Expression?.Formatted);
        };
        return sb.ToString();
      }
    }





    /// <summary>
    /// XML-представление объекта.
    /// </summary>
    public override XObject AsXml => _childTokens.Count <= 2 ?
      new XElement(JsNS(Kind.Formatted.Trim('\'')), new XAttribute("name", _var))
      :
      new XElement(JsNS(Kind.Formatted.Trim('\'')), new XAttribute("name", _var), Expression?.AsXml);

  }
}
