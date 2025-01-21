using OB = DiffCode.JSGenerator.ObjectBody;
using static DiffCode.JSGenerator.XmlParse;



namespace DiffCode.JSGenerator;

/// <summary>
/// JS-выражение, представляющее свойство объекта ("ключ: значение").
/// </summary>
public class KeyValue : AE
{
  public KeyValue(Literal key, AT val) : base(TT.KeyValueAssign)
  {
    if (IsNotNull(key) && IsNotNull(val))
    {
      Add(key, val);
    };
  }
  public KeyValue(Literal key, string val) : base(TT.KeyValueAssign)
  {
    if (IsNotNull(key) && val.IsUseful())
    {
      bool result = Double.TryParse(val, out double dbl);
      Add(
        key,
        (result) ?
        Num(dbl) as Literal
        :
        val.AsBool() != null ?
        (val.AsBool() == true ? TRUE as AE : FALSE as AE)
        :
        Str(val) as Literal);
    };
  }
  public KeyValue(Literal key, double val) : base(TT.KeyValueAssign)
  {
    if (IsNotNull(key))
    {
      Add(key, Num(val));
    };
  }
  public KeyValue(string key, AT val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful() && IsNotNull(val))
    {
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key), val);
    };
  }
  public KeyValue(string key, string val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful() && val != null)
    {
      bool result = Double.TryParse(val, out double dbl);
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key) as Literal, (result && !val.Contains(" ")) ? Num(dbl) as Literal : Str(val) as Literal);
    };
  }
  public KeyValue(string key, bool val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful())
    {
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key), val ? TRUE as AE : FALSE as AE);
    };
  }
  public KeyValue(string key, params KVal[] props) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful())
    {
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key), OBJ(props));
    };
  }
  public KeyValue(CommentItem comment, string key, string val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful() && val.IsUseful())
    {
      bool result = Double.TryParse(val, out double dbl);
      Comment = comment;
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key) as Literal, (result) ? Num(dbl) as Literal : Str(val) as Literal);
    };
  }
  public KeyValue(string key, double val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful())
    {
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key) as Literal, Num(val));
    };
  }
  public KeyValue(string key, int val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful())
    {
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key) as Literal, Num(val));
    };
  }
  public KeyValue(string key, object val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful() && val != null)
    {
      bool result = Double.TryParse(val.ToString(), out double dbl);
      bool iskeyword = IsNotNull(TB.TryConvertToKeyword(val.ToString().DecapitalizeFirstChar()));
      Add(
        key.Contains(' ') ? Str(key) as Literal : ID(key) as Literal,

        (result && !val.ToString().Contains(" ")) ?
        Num(dbl)
        :
        (iskeyword) ?
          TB.TryConvertToKeyword(val.ToString().DecapitalizeFirstChar()) as AT
          :
          Str(val)
          );
    };
  }
  public KeyValue(string key, SimpleTypeLiteral val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful())
    {
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key) as Literal, val);
    }
  }
  public KeyValue(CommentItem comment, string key, AT val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful() && IsNotNull(val))
    {
      Comment = comment;
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key), val);
    };
  }
  public KeyValue(string key, AE val) : base(TT.KeyValueAssign)
  {
    if (key.IsUseful() && val != null)
    {
      Add(key.Contains(' ') ? Str(key) as Literal : ID(key), val);
    };
  }








  /// <summary>
  /// Изменяет значение "ключа".
  /// </summary>
  /// <param name="key"></param>
  public void SetKey(string key)
  {
    if (key.IsUseful())
    {
      _childTokens?.RemoveAt(0);
      var newKey = key.Contains(' ') ? Str(key) as Literal : ID(key);
      Prepend(newKey);
    };
  }

  /// <summary>
  /// Изменяет значение "значения".
  /// </summary>
  /// <param name="val"></param>
  public void SetValue(AT val)
  {
    if (val != null)
    {
      _childTokens?.RemoveAt(1);
      Add(val);
    };
  }


  public void SetValue(IFormatted val)
  {
    if (val != null)
    {
      _childTokens?.RemoveAt(1);
      Add(Simple(val));
    };
  }

  /// <summary>
  /// Изменяет количество отступов (пустых строк) перед элементом.
  /// </summary>
  /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
  public new KeyValue SetGap(int gapbefore)
  {
    this._gapBefore = gapbefore;
    return this;
  }







  /// <summary>
  /// Ключ (наименование свойства объекта).
  /// </summary>
  public Literal Key => _childTokens?.FirstOrDefault() as Literal;

  /// <summary>
  /// Значение ключа (свойства) объекта.
  /// </summary>
  public AT Val => Key?.Next;


  /// <summary>
  /// Разделитель дочерних элементов - двоеточие.
  /// </summary>
  public override string ItemsDivider => ":";

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
        if (Comment is CommentBlock)
        {
          sb.AppendLine();
          sb.Append(IndentString);

          //sb.Append(Comment.As<CommentBlock>().Start.Formatted);
          foreach (var c in Comment.ChildTokens)
          {
            sb.Append(c.Formatted);
            //sb.Append(IndentString);
          };

          sb.Append(IndentString);
        }
        else
        {
          sb.Append(Comment.Formatted);
          sb.Append(IndentString);
        };
      };
      if (KeyFormatted() != null && Val?.Formatted != null)
      {
        sb.Append(KeyFormatted());
        sb.Append(ItemsDivider);
        sb.Append(" ");
        sb.Append(Val?.Formatted);
      };

      return sb.ToString();
    }
  }





  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml
  {
    get
    {
      if (Key != null && Val != null)
      {
        XElement ret;

        if (Val is StringLiteral || Val is NumberLiteral || Val is RegexpLiteral)
        {
          var selfvalue = Val is StringLiteral ? Val.GetSelfValue().ToString().Trim('\'') : Val.GetSelfValue();
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", selfvalue));
        }
        else if (Val is BooleanLiteral || Val.GetSelfValue().ToString() == "true" || Val.GetSelfValue().ToString() == "false")
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", Val.GetSelfValue()));
        }
        else if (Val is TrueExpression)
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", true));
        }
        else if (Val is FalseExpression)
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", false));
        }
        else if (Val is ThisExpression)
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", "THIS"));
        }
        else if (Val is NullExpression)
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", "NULL"));
        }
        else if (Val is UndefExpression || Val.GetSelfValue().ToString() == "undefined")
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", "UNDEF"));
        }
        else if (Val is ArrayDefinition && !Val.As<ArrayDefinition>().HasItems())
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", "[]"));
        }
        else if (Val is OB && !Val.As<OB>().HasItems())
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", "{}"));
        }
        else if (Val is FunctionAnonymous && Val.As<FunctionAnonymous>().Arguments?.HasItems() == false && Val.As<FunctionAnonymous>().Body?.HasItems() == false)
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", "()"));
        }
        else if (Val is Identifier)
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), new XAttribute("val", "{$" + Val.GetSelfValue() + "}"));
        }
        else
        {
          ret = new XElement(JsNS("kv"), new XAttribute("key", Key.Formatted.Trim('\'')), Val.AsXml);
        };

        if (_gapBefore > 0)
          ret.Add(new XAttribute("gap", _gapBefore));

        return ret;
      }
      else
      {
        return null;
      };
    }
  }




  protected string KeyFormatted()
  {
    if (Key != null)
    {
      bool isIdent = Key.Formatted.IsJSIdentifier();
      return isIdent ? Key.Formatted : "'" + Key.Formatted.Trim(new char[] { '\'', '"' }) + "'";
    }
    else
      return null;
  }








  public static OB operator &(KVal first, KVal second) => OBJ(first, second);
  public static OB operator &(KVal[] first, KVal second) => OBJ(first.Append(second));
  public static OB operator &(KVal first, KVal[] second) => OBJ(first).AddProperties(second);
  public static OB operator &(IEnumerable<KVal> first, KVal second) => OBJ(first.Append(second));
  public static OB operator &(KVal first, IEnumerable<KVal> second) => OBJ(first).AddProperties(second);
  public static OB operator &(OB first, KVal second) => first.AddProperty(second);
  public static OB operator &(KVal first, OB second) => OBJ(first).AddProperties(second.Props);
  public static OB operator ~(KVal prop) => OBJ(prop);



  public static KVal[] operator +(KVal first, KVal second) => new KVal[] { first, second };
  public static KVal[] operator +(KVal first, KVal[] second) => second != null ? new KVal[] { first }.Concat(second).ToArray() : new KVal[] { first };
  public static KVal[] operator +(KVal[] first, KVal second) => first.Append(second).ToArray();

}
