using System.Diagnostics;

using FB = DiffCode.JSGenerator.FnBodyBlock;



namespace DiffCode.JSGenerator;

/// <summary>
/// Абстрактное выражение.
/// </summary>
public abstract partial class AbstractExpression : AT
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected int _gapBefore;





  protected AbstractExpression() : base(TT.ExpressionEval) { }
  protected AbstractExpression(params AE[] exprs) : base(TT.ExpressionEval) => Add(exprs);
  protected AbstractExpression(TT type) : base(type) { }
  protected AbstractExpression(CommentItem comment) : base(TT.ExpressionEval) => Comment = comment;
  protected AbstractExpression(CommentItem comment, params AE[] exprs) : base(TT.ExpressionEval) { Comment = comment; Add(exprs); }
  protected AbstractExpression(CommentItem comment, TT type) : base(type) => Comment = comment;







  /// <summary>
  /// Изменяет количество отступов (пустых строк) перед элементом.
  /// </summary>
  /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
  public AbstractExpression SetGap(int gapbefore)
  {
    this._gapBefore = gapbefore;
    return this;
  }








  /// <summary>
  /// Метод для добавления комментария к выражению.
  /// </summary>
  /// <param name="comment">Добавляемый комментарий.</param>
  /// <returns>Возвращает исходное выражение с добавленным к нему комментарием.</returns>
  public AE AddComment(CommentItem comment)
  {
    Comment = comment;
    return this;
  }

  /// <summary>
  /// Метод для добавления комментария к выражению.
  /// </summary>
  /// <param name="comment">Добавляемый комментарий.</param>
  /// <returns>Возвращает исходное выражение с добавленным к нему комментарием.</returns>
  public AE AddComment(string comment)
  {
    Comment = CommentSingle(comment);
    return this;
  }

  /// <summary>
  /// Возвращает собственное текстовое значение.
  /// Переопределение базового метода GetSelfValue(), возвращает пустую строку.
  /// Текстовое содержимое объекта формируется из содержимого его дочерних элементов.
  /// </summary>
  /// <returns>Возвращает пустую строку.</returns>
  public override object GetSelfValue() => "";






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
          }
        }
        else
          sb.Append(Comment.Formatted);
      };
      sb.Append(string.Join(ItemsDivider, _childTokens.Where(IsNotNull).Select(ss => ss.Formatted)));
      return sb.ToString();
    }
  }



  protected void AppendLineAndIndent(ref StringBuilder sb, int amount = 1)
  {
    if (amount > 0)
    {
      for (var i = 1; i <= amount; i++)
      {
        sb.AppendLine();
        sb.Append(IndentString);
      };
    };
  }






  public override bool Equals(AT other) => other != null && _type == other.GetTokenType() && _childTokens.Count == other.ChildTokens.Count() && (other.ChildTokens != null ? ChildTokens.SequenceEqual(other.ChildTokens) : true);
  public override bool Equals(AT x, AT y) => x.Equals(y);
  public override int GetHashCode(AT obj) => obj.GetHashCode();





  /// <summary>
  /// Индексатор для добавления нового идентификатора к выражению доступа к свойству/методу объекта.
  /// Пример: <code>new Memb("obj.array")[ID("length")]</code>.
  /// Форматированное представление этого примера вернет следующий код Javascript: <code>obj.array.length</code>.
  /// </summary>
  /// <param name="ident">Добавляемый идентификатор свойства/метода.</param>
  /// <returns>Возвращает выражение доступа к новому свойству/методу объекта.</returns>
  public virtual MemberAccess this[Identifier ident] => MEMB(this, ident);

  /// <summary>
  /// Индексатор для выражения доступа к элементу массива по его индексу, заданному в виде выражения Javascript.
  /// </summary>
  /// <param name="expr">Выражение, задающее индекс массива.</param>
  /// <returns>Возвращает выражение доступа к элементу массива.</returns>
  public virtual MemberAccess this[AE expr] => MEMB(this, ArrIndex(expr));

  /// <summary>
  /// Индексатор для выражения доступа к элементу массива по его индексу, заданному числовым параметром.
  /// Пример: <code>new Memb("obj.array")[3]</code>. 
  /// Форматированное представление этого примера вернет следующий код Javascript: <code>obj.array[3]</code>.
  /// </summary>
  /// <param name="indx">Число - индекс искомого элемента массива.</param>
  /// <returns>Возвращает выражение доступа к элементу массива.</returns>
  public virtual MemberAccess this[int indx] => MEMB(this, ArrIndex(Num(indx) as AT));

  /// <summary>
  /// Индексатор для выражения доступа к элементу массива по его индексу, заданному строковым параметром.
  /// Пример: <code>new Memb("obj.array")["indx"]</code>. 
  /// Форматированное представление этого примера вернет следующий код Javascript: <code>obj.array["indx"]</code>.
  /// </summary>
  /// <param name="indx">Число - индекс искомого элемента массива.</param>
  /// <returns>Возвращает выражение доступа к элементу массива.</returns>
  public virtual MemberAccess this[string key] => MEMB(this, ArrIndex(Str(key) as AT));









  public static FB operator +(FB first, AE second) => first.AddExpression(second);
  public static ExpressionsSet operator &(AE first, AE second) => EXPRS(first, second);
  public static ExpressionsSet operator &(AE first, ExpressionsSet second) => EXPRS(first, second.ChildTokens.Cast<AE>());
  public static ExpressionsSet operator &(ExpressionsSet first, AE second) => first.AddExpression(second);
  public static ExpressionsSet operator ~(AE expr) => EXPRS(expr);
  public static JSRegion operator +(AE first, JSRegion second) => REGION(second.GetSelfName(), first, second.ChildTokens.Cast<AE>());
  public static JSRegion operator +(JSRegion first, AE second) => first.Add(second) as JSRegion;


  public static IEnumerable<AE> operator +(AE first, AE second) => new List<AE> { first, second };
  public static IEnumerable<AE> operator +(AE first, IEnumerable<AE> second) => new List<AE> { first }.Concat(second);
  public static IEnumerable<AE> operator +(IEnumerable<AE> first, AE second) => first.Append(second);



  public static ExprStatement operator !(AE first) => TEST(first, NOT);
}
