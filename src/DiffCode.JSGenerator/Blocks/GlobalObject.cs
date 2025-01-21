using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Глобальный объект JavaScript (документ),
/// содержащий набор выражений, определений и исполняемых инструкций,
/// разделенных точками с запятой
/// </summary>
public class GlobalObject : CBlock
{
  public GlobalObject() : base(TT.GLOBAL) { }
  public GlobalObject(params AE[] items) : base(TT.GLOBAL) => Add(items);
  public GlobalObject(CommentBlock cblock, params AE[] items) : base(TT.GLOBAL) { Add(items); SetComment(cblock); }
  public GlobalObject(string name, params AE[] items) : base(TT.GLOBAL) { Add(items); Name = name; }
  public GlobalObject(CommentBlock cblock, string name, params AE[] items) : base(TT.GLOBAL) { Add(items); Name = name; SetComment(cblock); }







  /// <summary>
  /// Метод для добавления одиночного выражения/исполняемой инструкции в список дочерних элементов.
  /// Выражение/исполняемая инструкция добавляются в конец списка.
  /// </summary>
  /// <param name="expr"></param>
  public void AddExpression(AE expr) => Add(expr);





  /// <summary>
  /// Ищет все определения MemberAccess, подходящие для конвертации в ExtJSClassExpression,
  /// конвертирует найденные в определения классов ExtJS.
  /// </summary>
  public GlobalObject ProcessExtDefines()
  {
    var membs =
      GetExpressions()
      .Where(
        w => w.FindFirst<Identifier>()?.GetSelfValue().ToString() == "Ext"
        &&
        w.FindByOrder<Identifier>(2)?.GetSelfValue().ToString() == "define"
        &&
        w.FindFirst<CallExpr>()?.FindFirst<StringLiteral>() != null
        )
        .Cast<MemberAccess>()
        .ToList();
    membs.ForEach(f => f.ToExtDefine());
    return this;
  }





  /// <summary>
  /// Возвращает все выражения, содержащиеся в документе.
  /// </summary>
  /// <returns></returns>
  public IEnumerable<AE> GetExpressions() => ChildTokens.Cast<AE>();





  /// <summary>
  /// Имя документа
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Автоматически сгенерированная сводка по содержимому модуля,
  /// выводимая в блоке комментариев в начале модуля
  /// </summary>
  public CommentBlock ModuleSummary
  {
    get
    {
      var ret = new CommentBlock("");
      ret.AddLine("ДОКУМЕНТ: " + Name);
      ret.AddLine("ДИНАМИЧЕСКАЯ СБОРКА: " + DateTime.Today.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString());
      ret.AddLine("");
      //ret.AddLine("NAMED FUNCTIONS: " + String.Join(", ", FuncsDefinitions.Select(ss => ss.Name.Formatted)));
      //ret.AddLine("GLOBAL VARS: " + String.Join(", ", GlobalVars.Select(ss => ss.DeepFindOne<Idd>(is_Identifier).Formatted)));
      ret.AddLine("");
      var commentLines = (Comment as CommentBlock)?.Lines?.Select(s => s.Formatted.Substring(3));
      if (commentLines != null) ret.AddLines(commentLines.ToArray());
      return ret;
    }
  }






  /// <summary>
  /// Разделитель дочерних элементов-выражений - точка с запятой
  /// </summary>
  public override string ItemsDivider => HasParent() ? "\r\n" : ";";

  /// <summary>
  /// Форматированное строковое представление объекта
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      //if (IsNotNull(ModuleSummary)) sb.Append(ModuleSummary.Formatted);
      sb.AppendLine();
      sb.AppendLine();
      sb.AppendLine();
      if (_childTokens.Count > 0) // Если в этом объекте присутствуют дочерние элементы (выражения, исполняемые инструкции и т.д.)...
      {
        sb.AppendLine(); // добавляем новую строку...
        foreach (var it in _childTokens.Where(IsNotNull)) // для всех дочерних элементов, не равных null...
        {
          sb.Append(it.IndentString); // добавляем отступ дочернего элемента...
          sb.Append(it.Formatted); // добавляем форматированное строковое представление дочернего элемента...
          sb.Append(ItemsDivider); // добавляем разделитель после каждого дочернего элемента, включая последний...
          sb.AppendLine(); // добавляем новую строку...
          sb.AppendLine();
          sb.AppendLine();
        };
      };
      return sb.ToString();
    }
  }







  /// <summary>
  /// XML-представление документа.
  /// </summary>
  public override XObject AsXml =>
    Name.IsUseful() ?
    new XElement(JsNS("global"), new XAttribute("name", Name), ChildTokens?.Select(s => s.AsXml))
    :
    new XElement(JsNS("global"), ChildTokens?.Select(s => s.AsXml))
    ;

}
