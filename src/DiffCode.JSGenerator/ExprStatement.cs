namespace DiffCode.JSGenerator;

public class ExprStatement : AE
{
  public ExprStatement()
  {

  }
  public ExprStatement(params AT[] items) => Add(items);
  public ExprStatement(CommentItem comment, params AT[] items)
  {
    Comment = comment;
    Add(items);
  }







  /// <summary>
  /// Ключевое слово var, let или const, если оно использовалось при составлении выражения.
  /// </summary>
  public ST VariableKeyword => this.FindOne<ST>(tt => IsOfType(tt, TT.Var) || IsOfType(tt, TT.Let) || IsOfType(tt, TT.Const));

  /// <summary>
  /// Левая часть выражения (за исключением ключевого слова присваивания и знаков присваивания/сравнения).
  /// </summary>
  public IEnumerable<AT> LeftPart => HasAssignment ?
    _childTokens.Skip(HasAssignment ? 1 : 0).TakeWhile(t => !IsAssignOperator(t))
    :
    _childTokens.TakeWhile(t => !IsComparisonOperator(t));

  /// <summary>
  /// Правая часть выражения (после знаков присваивания/сравнения).
  /// </summary>
  public AT RightPart => HasComparison ? _childTokens.Last() : _childTokens.Skip(VariableKeyword != null ? LeftPart.Count() + 2 : LeftPart.Count() + 1).FirstOrDefault();

  /// <summary>
  /// Оператор сравнения (если присутствует).
  /// </summary>
  public ComparisonOperator ComparisonOper => this.FindOne<ComparisonOperator>(is_ComparisonOper);

  /// <summary>
  /// Проверка на присутствие в выражении оператора присваивания.
  /// </summary>
  public bool HasAssignment => this.FindOne<ST>(r => IsAssignOperator(r) && !IsComparisonOperator(r.Parent)) != null;

  /// <summary>
  /// Проверка на присутствие в выражении оператора сравнения.
  /// </summary>
  public bool HasComparison => ComparisonOper != null;




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
      bool isInnerExpr = Parent?.GetTokenType() == TT.ExpressionEval || Parent?.GetTokenType() == TT.IfElseStatement;
      bool isSpaced = _childTokens?.FirstOrDefault()?.GetTokenType() != TT.Not;
      var divider = (isSpaced) ? ItemsDivider : "";

      var sb = new StringBuilder("");
      if (IsNotNull(Comment))
      {
        sb.AppendLine();
        sb.Append(IndentString);
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
      if (isInnerExpr) sb.Append("(");
      sb.Append(String.Join(divider, _childTokens?.Where(IsNotNull)?.Select(ss => ss.Formatted)));
      if (isInnerExpr) sb.Append(")");
      return sb.ToString();
    }
  }








  public override XObject AsXml => null;








  public static ExprStatement operator &(ExprStatement first, ExprStatement second) => new ExprStatement(first, AND, second);
  public static ExprStatement operator |(ExprStatement first, ExprStatement second) => new ExprStatement(first, OR, second);
  public static ExprStatement operator !(ExprStatement first) => new ExprStatement(first, NOT);

}
