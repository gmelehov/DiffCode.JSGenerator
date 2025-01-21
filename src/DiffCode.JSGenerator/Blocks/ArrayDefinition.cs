using OB = DiffCode.JSGenerator.ObjectBody;
using static DiffCode.JSGenerator.XmlParse;
using System.Diagnostics;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение массива.
/// </summary>
public class ArrayDefinition : CBlock
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _sparsed = true;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _wrapLeftBrace = true;





  public ArrayDefinition() : base(TT.ArrayDefinition) { }
  public ArrayDefinition(bool wrapLeft, bool sparsed) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
  }
  public ArrayDefinition(params AT[] items) : base(TT.ArrayDefinition) => Add(items);
  public ArrayDefinition(params OB[] items) : base(TT.ArrayDefinition) => Add(items);
  public ArrayDefinition(params string[] items) : base(TT.ArrayDefinition) => Add(items.Select(ss => Str(ss.ToString())));
  public ArrayDefinition(params double[] items) : base(TT.ArrayDefinition) => Add(items.Select(ss => Num(ss)));
  public ArrayDefinition(IEnumerable<AT> items) : base(TT.ArrayDefinition) => Add(items);
  public ArrayDefinition(IEnumerable<OB> items) : base(TT.ArrayDefinition) => Add(items);
  public ArrayDefinition(IEnumerable<string> items) : base(TT.ArrayDefinition) => Add(items.Select(ss => Str(ss.ToString())));
  public ArrayDefinition(IEnumerable<double> items) : base(TT.ArrayDefinition) => Add(items.Select(ss => Num(ss)));
  public ArrayDefinition(bool sparsed, params AT[] items) : base(TT.ArrayDefinition)
  {
    _sparsed = sparsed;
    Add(items);
  }
  public ArrayDefinition(bool sparsed, IEnumerable<AT> items) : base(TT.ArrayDefinition)
  {
    _sparsed = sparsed;
    Add(items);
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, params AT[] items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items);
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, params OB[] items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items);
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, params string[] items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items.Select(ss => Str(ss.ToString())));
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, params double[] items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items.Select(ss => Num(ss)));
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, IEnumerable<AT> items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items);
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, IEnumerable<OB> items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items);
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, IEnumerable<string> items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items.Select(ss => Str(ss.ToString())));
  }
  public ArrayDefinition(bool wrapLeft, bool sparsed, IEnumerable<double> items) : base(TT.ArrayDefinition)
  {
    _wrapLeftBrace = wrapLeft;
    _sparsed = sparsed;
    Add(items.Select(ss => Num(ss)));
  }








  /// <summary>
  /// Разделитель дочерних элементов - запятая.
  /// </summary>
  public override string ItemsDivider => ",";

  /// <summary>
  /// Форматированное строковое представление объекта.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      if (_childTokens.Count > 0)
      {
        AppendSpace(ref sb, "", false);
        AppendIndent(ref sb, IndentString, false);
      };
      sb.Append("[");
      if (_childTokens.Count > 0)
      {
        AppendSpace(ref sb, "", true);////
        foreach (var it in _childTokens.Where(IsNotNull))
        {
          if (it is Literal || it is MemberAccess || it is FunctionAnonymous || it is NewExpr || it is ExtJSClassExpression || it is AbstractBinaryExpression || it is AbstractUnaryExpression)
          {
            AppendIndent(ref sb, it.IndentString);
          };
          sb.Append(it.Formatted);
          if (it != it.Last)
          {
            sb.Append(ItemsDivider);
            AppendSpace(ref sb, " ");
          };
        };
        AppendSpace(ref sb, "");
        AppendIndent(ref sb, IndentString);
      };
      sb.Append("]");
      return sb.ToString();
    }
  }




  public bool MakeInline
  {
    get
    {
      var totalItems = _childTokens.Count;
      if (totalItems < 1 || (/*HasOnlyShortItems ||*/ HasOnlyLiterals))
        return true;
      else
        return false;
    }
  }

  public bool HasOnlyLiterals => _childTokens.All(a => a is Literal);






  protected void AppendSpace(ref StringBuilder sbuilder, string space, bool ignoreWrapLeft = true)
  {
    if (!_sparsed || MakeInline || (_wrapLeftBrace == false && ignoreWrapLeft == false))
    {
      sbuilder.Append(space);
    }
    else
    {
      sbuilder.AppendLine();
    };
  }


  protected void AppendIndent(ref StringBuilder sbuilder, string indent, bool ignoreWrapLeft = true)
  {
    if (!_sparsed || MakeInline || (_wrapLeftBrace == false && ignoreWrapLeft == false))
    {
      //sbuilder.Append("");
    }
    else
    {
      sbuilder.Append(indent);
    };
  }



  public bool Sparsed => _sparsed;
  public bool WrapLeftBrace => _wrapLeftBrace;






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml =>
    _wrapLeftBrace == false
    ?
    new XElement(JsNS("arr"), new XAttribute("wrapleft", false), _childTokens.Select(p => p.AsXml))
    :
    new XElement(JsNS("arr"), _childTokens.Select(p => p.AsXml));

}
