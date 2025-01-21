namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Абстрактное унарное выражение.
  /// </summary>
  public abstract partial class AbstractUnaryExpression : AE
  {
    protected bool _isPrefixOperator;





    protected AbstractUnaryExpression(TT type, AE left, Operator oper) : base(type) { _isPrefixOperator = false; base.Add(left, oper); }
    protected AbstractUnaryExpression(CommentItem comment, TT type, AE left, Operator oper) : base(type) { _isPrefixOperator = false; base.AddComment(comment); base.Add(left, oper); }
    protected AbstractUnaryExpression(TT type, Operator oper, AE left) : base(type) { _isPrefixOperator = true; base.Add(oper, left); }
    protected AbstractUnaryExpression(CommentItem comment, TT type, Operator oper, AE left) : base(type) { _isPrefixOperator = true; base.AddComment(comment); base.Add(oper, left); }







    /// <summary>
    /// Операнд выражения.
    /// </summary>
    public AE Operand => IsPrefix ? _childTokens?.Last()?.As<AE>() : _childTokens?.First()?.As<AE>();
    
    /// <summary>
    /// Оператор выражения.
    /// </summary>
    public Operator Operator => IsPrefix ? _childTokens?.First()?.As<Operator>() : _childTokens?.Last()?.As<Operator>();
    
    /// <summary>
    /// <see langword="true"/>, если оператор является префиксным (находится СЛЕВА от операнда),
    /// <see langword="false"/>, если оператор является постфиксным (находится СПРАВА от операнда).
    /// </summary>
    public virtual bool IsPrefix => _isPrefixOperator;

    /// <summary>
    /// Разделитель дочерних элементов - пробел (для выражения typeof ...), 
    /// либо пустая строка (для всех остальных унарных выражений).
    /// </summary>
    public override string ItemsDivider => Operator.Formatted == "typeof" || Operator.Formatted == "delete" || Operator.Formatted == "void" ? " " : "";

    /// <summary>
    /// Форматированное строковое представление объекта.
    /// </summary>
    public override string Formatted
    {
      get
      {
        var sb = new StringBuilder("");
        if (Comment != null)
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
        bool isItemOfIF = HasParent() && Parent is IfElseExpression && Parent.As<IfElseExpression>().AsTernary == false;
        if (isItemOfIF)
          sb.Append("(");
        if (IsPrefix)
        {
          sb.Append(Operator.Formatted);
          sb.Append(ItemsDivider);
          WrapIfUpdateExpr(Operand, ref sb);
        }
        else
        {
          WrapIfUpdateExpr(Operand, ref sb);
          sb.Append(ItemsDivider);
          sb.Append(Operator.Formatted);
        };
        if (isItemOfIF)
          sb.Append(")");
        return sb.ToString();
      }
    }







    protected void WrapIfUpdateExpr(AE expr, ref StringBuilder sbuilder)
    {
      if (expr is AbstractBinaryExpression || expr is UpdateExpression)
        sbuilder.Append("(");
      sbuilder.Append(expr?.Formatted);
      if (expr is AbstractBinaryExpression || expr is UpdateExpression)
        sbuilder.Append(")");
    }

    protected void WrapIfOperator(AOp oper, ref StringBuilder sbuilder)
    {
      sbuilder.Append(ItemsDivider);
      sbuilder.Append(oper.Formatted);
      sbuilder.Append(ItemsDivider);
    }





    /// <summary>
    /// Переопределение базового метода ToString().
    /// </summary>
    /// <returns></returns>
    public override string ToString() => Formatted;










    public static AbstractBinaryExpression operator &(AbstractUnaryExpression first, AbstractUnaryExpression second) => new AndExpr(first, second);
    public static AbstractBinaryExpression operator |(AbstractUnaryExpression first, AbstractUnaryExpression second) => new OrExpr(first, second);
    public static AbstractUnaryExpression operator !(AbstractUnaryExpression expr) => new NotExpr(expr);

  }
}
