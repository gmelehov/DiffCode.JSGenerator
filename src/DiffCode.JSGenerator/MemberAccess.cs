using static DiffCode.JSGenerator.XmlParse;
using System.Diagnostics;



namespace DiffCode.JSGenerator;

/// <summary>
/// Определение доступа к свойству/индексатору/методу объекта "через точку".
/// </summary>
public class MemberAccess : AE
{
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="obj">Строка идентификатора объекта.</param>
  /// <param name="prop">Строка идентификатора свойства объекта.</param>
  public MemberAccess(string obj, string prop) : base(TT.Member) => Add(ID(obj), ID(prop));
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="obj">Идентификатор объекта.</param>
  /// <param name="prop">Идентификатор свойства.</param>
  public MemberAccess(Identifier obj, Identifier prop) : base(TT.Member) => Add(obj.As<Identifier>(), prop.As<Identifier>());
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="idents">Набор строк идентификаторов, первый из которых будет использоваться в качестве идентификатора объекта, 
  /// оставшиеся - в качестве идентификаторов свойств объекта.</param>
  public MemberAccess(params string[] idents) : base(TT.Member)
  {
    foreach(var id in idents)
    {
      Add(id.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(s => ID(s)));
    };
  }
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="idents">Набор идентификаторов, первый из которых будет использоваться в качестве идентификатора объекта, 
  /// оставшиеся - в качестве идентификаторов свойств объекта.</param>
  public MemberAccess(params Identifier[] idents) : base(TT.Member) => Add(idents);
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="idents">Коллекция идентификаторов, первый из которых будет использоваться в качестве идентификатора объекта, 
  /// оставшиеся - в качестве идентификаторов свойств объекта.</param>
  public MemberAccess(IEnumerable<Identifier> idents) : base(TT.Member) => Add(idents);
  /// <summary>
  /// Конструктор класса.
  /// </summary>
  /// <param name="idents">Коллекция строк идентификаторов, первый из которых будет использоваться в качестве идентификатора объекта, 
  /// оставшиеся - в качестве идентификаторов свойств объекта.</param>
  public MemberAccess(IEnumerable<string> idents) : base(TT.Member) => Add(idents.Select(s => ID(s)));
  public MemberAccess(AE expr, IEnumerable<AE> idents) : base(TT.Member) => Add(expr, idents);
  public MemberAccess(AE expr, params string[] idents) : base(TT.Member)
  {
    Add(expr);
    foreach (var id in idents)
    {
      Add(id.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(s => ID(s)));
    };
  }
  public MemberAccess(string ident, CallExpr call) : base(TT.Member)
  {
    var idents = ident.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(s => ID(s));
    Add(idents);
    Add(call);
  }
  public MemberAccess(Identifier ident, CallExpr call) : base(TT.Member) => Add(ident, call);
  public MemberAccess(params AE[] exprs) : base(TT.Member)
  {
    var first = exprs[0];
    var rest = exprs.ExceptFirst();
    if (first is AE || first is Identifier || first is ThisExpression || first is NewExpr || first is StringLiteral || first is RegexpLiteral || first is ArrayDefinition || first is MemberAccess)
    {
      Add(first);
      foreach (var ex in rest)
      {
        Add(ex);
      };
    };
  }
  public MemberAccess() : base(TT.Member)
  {

  }








  /// <summary>
  /// Выполняет слияние двух определений. 
  /// Идентификаторы и прочее содержимое второго определения (other) добавляются в конец 
  /// списка содержимого этого определения. Возвращает исходное определение с обновленным содержимым.
  /// </summary>
  /// <param name="other">Объект с определением, содержимое которого должно быть добавлено к текущему определению.</param>
  /// <returns></returns>
  public MemberAccess Merge(MemberAccess other)
  {
    Add(other.ChildTokens);
    return this;
  }

  public MemberAccess AddIndex(int indx)
  {
    Add(ArrIndex(indx));
    return this;
  }
  
  public MemberAccess AddIndex(string indx)
  {
    Add(ArrIndex(indx));
    return this;
  }
  
  public MemberAccess AddExpr(AE expr)
  {
    Add(expr);
    return this;
  }





  public new MemberAccess this[int indx] => AddIndex(indx);
  public new MemberAccess this[string indx] => AddIndex(indx);
  public new MemberAccess this[AE expr] => AddExpr(expr);





  /// <summary>
  /// Выполняет попытку конвертации в определение класса ExtJS.
  /// Возвращает определение класса ExtJS при удачной конвертации,
  /// возвращает исходный объект (собственный инстанс), если конвертация не удалась.
  /// </summary>
  /// <returns></returns>
  public AE ToExtDefine()
  {
    var extdefine = new ExtJSClassExpression(this, false);
    return extdefine.ClassName != null ? extdefine as AE : this;
  }





  /// <summary>
  /// Изменяет количество отступов (пустых строк) перед элементом.
  /// </summary>
  /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
  public new MemberAccess SetGap(int gapbefore)
  {
    this._gapBefore = gapbefore;
    return this;
  }





  /// <summary>
  /// Ссылка на объект, к свойствам/индексаторам/методам которого определяется доступ.
  /// </summary>
  public AE Obj => _childTokens?.FirstOrDefault()?.As<AE>();


  /// <summary>
		/// Разделитель дочерних элементов - точка.
		/// </summary>
		public override string ItemsDivider => ".";

  /// <summary>
		/// Форматированное строковое представление объекта.
		/// </summary>
		public override string Formatted
  {
    get
    {
      var hasArrayIndex = IsArrayIndex(_childTokens.LastOrDefault()); // true, если последний дочерний элемент объекта - определение индекса массива
      var sb = new StringBuilder("");
      AppendLineAndIndent(ref sb, _gapBefore);
      if (IsNotNull(Comment))
      {
        if (!(Order == 0 && (HasParent() && Parent is CBlock)))
        {
          sb.AppendLine();
          sb.Append(IndentString);
        }
        else
        {
          sb.Append(IndentString);
        };
        if (Comment is CommentBlock)
        {
          sb.AppendLine();
          sb.Append(IndentString);

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

      foreach (var it in _childTokens.Where(IsNotNull))
      {
        bool needsWrap = it == it.First && FirstItemNeedsWrapping == true;
        if (needsWrap)
        {
          var str = WrapItem(it.Formatted);
          sb.Append(str);
        }
        else
        {
          var str = it.Formatted;
          sb.Append(str);
        };
        

        // если обрабатываемый дочерний элемент не является последним, 
        // ... И следующий за ним по очереди дочерний элемент не является определением индекса массива
        if (it != it.Last && !(IsArrayIndex(it.Next)) && !(it.Next is ArgumentsBlock) && !(it.Next is CallExpr && it.Next.As<CallExpr>().Identifier == null))
          sb.Append(ItemsDivider); // ... то добавляем разделитель дочерних элементов - точку
      };
      if (!HasParent()) sb.Append(";");
      return sb.ToString();
    }
  }



  public bool FirstItemNeedsWrapping
  {
    get
    {
      var it = _childTokens.FirstOrDefault();
      return (it is AbstractBinaryExpression || it is AbstractUnaryExpression || it is NumberLiteral || (it is IfElseExpression && it.As<IfElseExpression>()?.AsTernary == true));
    }
  }


  private string WrapItem(string formatted)
  {
    if (formatted.IsUseful() && !(formatted.StartsWith("(") && formatted.EndsWith(")")))
      return "(" + formatted + ")";
    else
      return formatted;
  }


  public bool HasOnlyIdentifiers => _childTokens.All(IsIdentifier) || (IsTHISExpression(_childTokens.FirstOrDefault()) && _childTokens.ExceptFirst().All(IsIdentifier));


  public bool WrapsMemberCall => _childTokens.Count == 2 && IsMember(_childTokens.FirstOrDefault()) && IsMethodCall(_childTokens.LastOrDefault());
  public bool WrapsMemberAndID => _childTokens.Count == 2 && IsMember(_childTokens.FirstOrDefault()) && IsIdentifier(_childTokens.LastOrDefault());







  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => HasOnlyIdentifiers ? IfHasOnlyIdentifiers : IfHasMixedContent;







  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private XElement IfHasOnlyIdentifiers =>
    _gapBefore > 0
    ?
    new XElement(JsNS("member"), new XAttribute("name", string.Join(" ", _childTokens.Select(c => c?.Formatted))), new XAttribute("gap", _gapBefore))
    :
    new XElement(JsNS("member"), new XAttribute("name", string.Join(" ", _childTokens.Select(c => c?.Formatted))))
    ;




  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private XElement IfWrapsMemberCall
  {
    get
    {
      var ret = new XElement(_childTokens.FirstOrDefault().AsXml as XElement);
      if (_gapBefore > 0) ret.Add(new XAttribute("gap", _gapBefore));
      ret.Add(_childTokens.LastOrDefault().AsXml as XElement);
      return ret;
    }
  }


  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private XElement IfWrapsMemberAndID
  {
    get
    {
      var ret = new XElement(_childTokens.FirstOrDefault().AsXml as XElement);
      if (_gapBefore > 0) ret.Add(new XAttribute("gap", _gapBefore));
      var nameattr = $"{string.Join(" ", _childTokens.FirstOrDefault()?.ChildTokens.Select(c => c?.Formatted))} {_childTokens.LastOrDefault()?.Formatted}";
      ret.SetAttributeValue("name", nameattr);
      return ret;
    }
  }


  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private XElement IfHasMixedContent => 
    _gapBefore > 0
    ?
    new XElement(JsNS("member"), new XAttribute("gap", _gapBefore), _childTokens.Select(e => e?.AsXml))
    :
    new XElement(JsNS("member"), _childTokens.Select(e => e?.AsXml))
    ;

}
