using OB = DiffCode.JSGenerator.ObjectBody;
using static DiffCode.JSGenerator.XmlParse;
using System.Diagnostics;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение тела объекта 
/// (набора ключей-значений объекта, разделенных запятыми, обернутого фигурными скобками).
/// </summary>
public class ObjectBody : CBlock
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _sparsed = true;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _wrapLeftBrace = true;






  #region CONSTRUCTORS ///////////////////////////////////////////////////////////////////////////////////////////////////

  public ObjectBody() : base(TT.ObjectBodyBlock) { }
  public ObjectBody(params KVal[] keyvals) : base(TT.ObjectBodyBlock) => Add(keyvals);
  public ObjectBody(IEnumerable<KVal> keyvals) : base(TT.ObjectBodyBlock) => Add(keyvals.ToArray());
  public ObjectBody(bool sparsed, params KVal[] keyvals) : base(TT.ObjectBodyBlock)
  {
    _sparsed = sparsed;
    Add(keyvals);
  }
  public ObjectBody(bool sparsed, IEnumerable<KVal> keyvals) : base(TT.ObjectBodyBlock)
  {
    _sparsed = sparsed;
    Add(keyvals.ToArray());
  }
  public ObjectBody(bool wrapLeftBrace, bool sparsed, params KVal[] keyvals) : base(TT.ObjectBodyBlock)
  {
    _wrapLeftBrace = wrapLeftBrace;
    _sparsed = sparsed;
    Add(keyvals);
  }
  public ObjectBody(bool wrapLeftBrace, bool sparsed, IEnumerable<KVal> keyvals) : base(TT.ObjectBodyBlock)
  {
    _wrapLeftBrace = wrapLeftBrace;
    _sparsed = sparsed;
    Add(keyvals.ToArray());
  }

  #endregion








  /// <summary>
  /// Индексатор для быстрого доступа к дочернему элементу <see cref="KeyValue"/> по указанному ключу.
  /// </summary>
  /// <param name="key">Ключ дочернего элемента.</param>
  /// <returns></returns>
  public new KVal this[string key] => Props?.FirstOrDefault(f => f.Key.GetSelfValue().ToString() == key);








  /// <summary>
  /// Метод для добавления нового свойства в объект.
  /// Возвращает измененный объект.
  /// </summary>
  /// <param name="prop">Добавляемое свойство.</param>
  /// <returns></returns>
  public OB AddProperty(KVal prop)
  {
    Add(prop);
    return this;
  }

  public OB AddProperties(params KVal[] props)
  {
    Add(props);
    return this;
  }

  public OB AddProperties(IEnumerable<KVal> props)
  {
    Add(props?.ToArray());
    return this;
  }







  /// <summary>
  /// Коллекция свойств объекта (комбинаций "ключ: значение").
  /// </summary>
  public IEnumerable<KVal> Props => _childTokens?.Cast<KVal>();

  /// <summary>
  /// Разделитель дочерних элементов (набора ключей-значений объекта) - запятая.
  /// </summary>
  public override string ItemsDivider => ",";

  /// <summary>
  /// Форматированное строковое представление.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      if (HasParent() && Parent.GetTokenType().Equals(TT.ArrayDefinition))
      {
        if (Parent.As<ArrayDefinition>().Sparsed)
        {
          sb.Append(IndentString);
        };
        WrapAllToNewLine(ref sb);
      }
      else if (_wrapLeftBrace && (Parent is VarDeclaration || Parent is ArgumentsBlock || Parent is KeyValue))
      {
        WrapAllToNewLine(ref sb);
      }
      else if (Parent is ReturnExpression || (_wrapLeftBrace == false && (Parent is VarDeclaration || Parent is ArgumentsBlock || Parent is KeyValue)))
      {
        WrapObjectToNewLine(ref sb);
      }
      else
      {
        if (_wrapLeftBrace)
          WrapAllToNewLine(ref sb);
        else
          WrapObjectToNewLine(ref sb);
      };

      return sb.ToString();
    }
  }





  protected void WrapAllToNewLine(ref StringBuilder sb)
  {
    sb.Append("{");
    if (_childTokens.Any(IsKeyFuncValue) || _sparsed)
    {
      sb.AppendLine();
      foreach (var it in _childTokens.Where(IsNotNull))
      {
        sb.Append(it.IndentString);
        sb.Append(it.Formatted);
        if (it != it.Last) sb.Append(ItemsDivider);
        sb.AppendLine();
      };
      sb.Append(IndentString);
    }
    else
    {
      sb.Append(" ");
      foreach (var it in _childTokens.Where(IsNotNull))
      {
        sb.Append(it.Formatted);
        if (it != it.Last) sb.Append(ItemsDivider);
        sb.Append(" ");
      };
    };
    sb.Append("}");
  }
  protected void WrapObjectToNewLine(ref StringBuilder sb)
  {
    sb.Append("{");
    if (_childTokens.Any(IsKeyFuncValue) || _sparsed)
    {
      sb.AppendLine();
      foreach (var it in _childTokens.Where(IsNotNull))
      {
        sb.Append(it.IndentString);
        sb.Append(it.Formatted);
        if (it != it.Last) sb.Append(ItemsDivider);
        sb.AppendLine();
      };
      sb.Append(IndentString);
    }
    else
    {
      sb.Append(" ");
      foreach (var it in _childTokens.Where(IsNotNull))
      {
        sb.Append(it.Formatted);
        if (it != it.Last) sb.Append(ItemsDivider);
        sb.Append(" ");
      };
    };
    sb.Append("}");
  }




  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml =>
    _wrapLeftBrace == false
    ?
    new XElement(JsNS("obj"), new XAttribute("wrapleft", false), Props.Select(p => p.AsXml))
    :
    new XElement(JsNS("obj"), Props.Select(p => p.AsXml));










  #region ПЕРЕГРУЗКА ОПЕРАТОРОВ //////////////////////////////////////////////////////////////////////////////////////////

  public static OB operator +(OB first, KVal second) => first.AddProperty(second);
  public static OB operator +(OB first, KVal[] second) => first.AddProperties(second);
  public static OB operator +(OB first, IEnumerable<KVal> second) => first.AddProperties(second);

  #endregion

}
