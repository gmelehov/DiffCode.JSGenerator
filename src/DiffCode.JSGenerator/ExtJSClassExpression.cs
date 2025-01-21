using static DiffCode.JSGenerator.XmlParse;

using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using OB = DiffCode.JSGenerator.ObjectBody;
using SL = DiffCode.JSGenerator.StringLiteral;
using XE = System.Xml.Linq.XElement;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение или создание ExtJS-класса (Ext.define, Ext.create).
/// </summary>
public class ExtJSClassExpression : AE
{
  protected SL _classname;
  protected MemberAccess _ext;
  protected ARG _arguments;
  protected OB _config;



  public ExtJSClassExpression(string name, bool create = false) : base(TT.ClassDefinition)
  {
    _type = (create) ? TT.ClassCreation : TT.ClassDefinition;
    var body = OBJ(false, true);
    _classname = Str(name);
    _config = body;
    _arguments = Args(_classname, _config);
    var content = (create) ? MEMB(ID("Ext"), ID("create"), CALL(_arguments)) : MEMB(ID("Ext"), ID("define"), CALL(_arguments));
    Add(content);
  }
  public ExtJSClassExpression(string name, bool create = false, params KVal[] props) : base(TT.ClassDefinition)
  {
    _type = (create) ? TT.ClassCreation : TT.ClassDefinition;
    var body = OBJ(false, true, props);
    _classname = Str(name);
    _config = body;
    _arguments = Args(_classname, _config);
    var content = (create) ? MEMB(ID("Ext"), ID("create"), CALL(_arguments)) : MEMB(ID("Ext"), ID("define"), CALL(_arguments));
    Add(content);
  }
  public ExtJSClassExpression(string name, OB config, bool create = false) : base(TT.ClassDefinition)
  {
    _type = (create) ? TT.ClassCreation : TT.ClassDefinition;
    _classname = Str(name);
    _config = config;
    _arguments = Args(_classname, _config);
    var content = (create) ? MEMB(ID("Ext"), ID("create"), CALL(_arguments)) : MEMB(ID("Ext"), ID("define"), CALL(_arguments));
    Add(content);
  }
  public ExtJSClassExpression(MemberAccess asmember, bool create = false) : base(TT.ClassDefinition)
  {
    _classname = asmember?.FindFirst<CallExpr>()?.FindFirst<SL>();
    _config = asmember?.FindFirst<CallExpr>()?.FindFirst<OB>();
    _type = (create) ? TT.ClassCreation : TT.ClassDefinition;

    if(_classname != null && _config != null)
    {
      _arguments = Args(_classname, _config);
      var content = (create) ? MEMB(ID("Ext"), ID("create"), CALL(_arguments)) : MEMB(ID("Ext"), ID("define"), CALL(_arguments));
      Add(content);
    };
  }






  /// <summary>
  /// Метод для задания блока комментариев к объекту.
  /// </summary>
  /// <param name="comment"></param>
  public void SetComment(CommentBlock comment) => Comment = comment;
  public ExtJSClassExpression AddCommentBlock(CommentBlock comment)
  {
    Comment = comment;
    return this;
  }
  public ExtJSClassExpression AddCommentBlock(string comment)
  {
    if (comment != null)
      Comment = CommentBlock(comment);

    return this;
  }
  public ExtJSClassExpression AddCommentBlock(params string[] comments)
  {
    if(comments != null && comments.Length > 0)
      Comment = CommentBlock(comments);

    return this;
  }



  /// <summary>
  /// Изменяет количество отступов (пустых строк) перед элементом.
  /// </summary>
  /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
  public new ExtJSClassExpression SetGap(int gapbefore)
  {
    this._gapBefore = gapbefore;
    return this;
  }









  /// <summary>
  /// Ищет первое по счету свойство в объекте-конфигураторе класса,
  /// устанавливает его отступ сверху = 1 строке.
  /// </summary>
  public ExtJSClassExpression SetFirstPropertyGap()
  {
    Config?.Props.FirstOrDefault()?.SetGap(1);
    return this;
  }

  /// <summary>
  /// Ищет в объекте-конфигураторе класса все объявленные методы,
  /// устанавливает для них отступ сверху = 2 строкам.
  /// </summary>
  public ExtJSClassExpression SetMethodsGap()
  {
    var methods = Config.Props.Where(w => w.Val.GetTokenType() == TT.FuncAnonymous);
    foreach(var m in methods)
    {
      m.SetGap(2);
    };
    return this;
  }

  /// <summary>
  /// Ищет в объекте-конфигураторе класса свойства 
  /// items, dockedItems, buttons, columns, fields,
  /// устанавливает для них отступ сверху = 1 строке.
  /// </summary>
  public ExtJSClassExpression SetArrayPropertyGap()
  {
    var propKeys = new string[] { "items", "dockedItems", "buttons", "columns", "fields" };
    var props = Config?.Props.Where(w => propKeys.Contains(w.Key.GetSelfValue()) && w.Val.GetTokenType() == TT.ArrayDefinition);
    foreach(var p in props)
    {
      p.SetGap(1);
    };
    return this;
  }












  /// <summary>
  /// Вызываемый статический ExtJS-объект (Ext.define или Ext.create).
  /// </summary>
  public MemberAccess Ext => _ext;
  /// <summary>
  /// Аргументы вызываемого ExtJS-метода (Ext.define('...', {...}) или Ext.create('...', {...})).
  /// </summary>
  public ARG Arguments => _arguments;
  /// <summary>
  /// Имя класса, заданное при создании экземпляра объекта.
  /// </summary>
  public SL ClassName => _classname;
  /// <summary>
  /// Объект-конфигуратор класса.
  /// </summary>
  public OB Config => _config;
  /// <summary>
  /// Имя класса-родителя, заданное при создании экземпляра объекта.
  /// </summary>
  public SL Extend => (Config?.Props?.FirstOrDefault(d => d.Key.Formatted == "extend")?.Val as SL) ?? null;
  /// <summary>
  /// Псевдоним класса, заданный при создании экземпляра объекта.
  /// </summary>
  public SL Alias => (Config?.Props?.FirstOrDefault(d => d.Key.Formatted == "alias")?.Val as SL) ?? null;
  /// <summary>
  /// Краткий псевдоним (xtype) класса, вычисленный из значения Alias.
  /// </summary>
  public SL XType => (Alias != null) ? Str(string.Join(".", Alias?.ToString().Split('.').Skip(1))) : null;




  /// <summary>
  /// Разделитель дочерних элементов (названия класса и его объекта-конфигуратора) - новая строка.
  /// </summary>
  public override string ItemsDivider => "\r\n";
  /// <summary>
  /// Форматированное строковое представление.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      AppendLineAndIndent(ref sb, _gapBefore);
      if ((_type == TT.ClassDefinition || _type == TT.ClassCreation) && Comment != null)
      {
        if (Comment is CommentBlock)
        {
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
      sb.Append(string.Join(ItemsDivider, _childTokens.Where(w => w != null).Select(ss => ss.Formatted)));
      return sb.ToString();
    }
  }





  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml =>
    XmlParse.Member2ExtDefine(
    new XE(
      JsNS("member"),
      new XAttribute("gap", _gapBefore),
      new XAttribute("name", "Ext"),
      new XE(
        JsNS("call"),
        new XAttribute("method", _type == TT.ClassDefinition ? "define" : "create"),
        ClassName.AsXml,
        Arguments.AsXml
        )));








  public static ExtJSClassExpression[] operator +(ExtJSClassExpression first, ExtJSClassExpression second) => new ExtJSClassExpression[] { first, second };
  public static ExtJSClassExpression[] operator +(ExtJSClassExpression[] array, ExtJSClassExpression item) => array.Append(item).ToArray();
  public static ExtJSClassExpression[] operator +(ExtJSClassExpression item, ExtJSClassExpression[] array) =>
    item != null ?
      (array != null ?
        new ExtJSClassExpression[] { item }.Concat(array).ToArray()
        :
        new ExtJSClassExpression[] { item })
      :
      array;

}
