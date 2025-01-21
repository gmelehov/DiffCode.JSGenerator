using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Упорядоченный набор выражений/синтаксических конструкций Javascript,
/// представленный в виде сворачиваемого именованного списка #region ... #endregion.
/// </summary>
public class JSRegion : CBlock
{
  protected string _name;





  public JSRegion(string name = null, params AE[] exprs) : base(TT.CodeRegion)
  {
    _name = name;
    Add(exprs);
  }
  public JSRegion(string name = null, IEnumerable<AE> exprs = null) : base(TT.CodeRegion)
  {
    _name = name;
    Add(exprs);
  }
  public JSRegion(string name = null, AE expr = null, IEnumerable<AE> exprs = null) : base(TT.CodeRegion)
  {
    _name = name;
    Add(expr, exprs);
  }







  /// <summary>
  /// Метод для добавления блока комментариев к объекту.
  /// Комментарий добавляется внутрь сворачиваемого региона, непосредственно перед его первым дочерним элементом.
  /// </summary>
  /// <param name="comment">Добавляемый блок комментариев.</param>
  /// <returns>Возвращает исходный сворачиваемый регион с добавленным к нему блоком комментариев.</returns>
  public new JSRegion SetComment(CommentBlock comment)
  {
    Add(NEWLINE.AddComment(comment));
    return this;
  }

  /// <summary>
  /// Метод для изменения имени сворачиваемого региона.
  /// </summary>
  /// <param name="name">Новое имя сворачиваемого региона.</param>
  /// <returns>Измененный объект <see cref="JSRegion"/></returns>
  public JSRegion SetName(string name)
  {
    _name = name;
    return this;
  }

  /// <summary>
  /// Метод, возвращающий собственное наименование сворачиваемого региона.
  /// </summary>
  /// <returns>Собственное наименование сворачиваемого региона.</returns>
  public string GetSelfName() => _name;





  /// <summary>
  /// Разделитель дочерних элементов.
  /// Для вложенного (зависимого) элемента <see cref="JSRegion"/> разделителем дочерних элементов будет являться родительский разделитель.
  /// Для одиночного (независимого) элемента <see cref="JSRegion"/> разделителем дочерних элементов будет являться точка с запятой.
  /// </summary>
  public override string ItemsDivider => HasParent() ? Parent.ItemsDivider : ";";

  /// <summary>
  /// Форматированное строковое представление объекта.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");
      sb.Append(Environment.NewLine.Repeat(JSRegionHorizontalIndent.top));
      if (IsNotNull(Comment))
      {
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
      sb.Append("//#region "); // добавляем открывающий тег сворачиваемого региона
      sb.Append(_name ?? ""); // добавляем имя сворачиваемого региона, либо пустую строку, если имя не указано
      sb.AppendLine(); // добавляем новую строку
      sb.AppendLine();

      if (_childTokens.Count > 0) // Если в этом объекте присутствуют дочерние элементы (выражения, исполняемые инструкции и т.д.)
      {
        foreach (var it in _childTokens.Where(IsNotNull)) // для всех дочерних элементов, не равных null
        {
          sb.Append(it.IndentString); // добавляем отступ дочернего элемента
          sb.Append(it.Formatted); // добавляем форматированное строковое представление дочернего элемента
          if (it.Formatted.Trim(new char[] { '\r', '\n', '\t', ' ', '\'' }).IsUseful() && !(it is StringLiteral)) sb.Append(ItemsDivider); // добавляем разделитель после каждого непустого дочернего элемента, включая последний
          sb.AppendLine(); // добавляем новую строку
        };
      };
      sb.AppendLine();
      sb.Append(IndentString); // добавляем собственный отступ элемента
      sb.Append("//#endregion"); // добавляем закрывающий тег сворачиваемого региона
      sb.Append(Environment.NewLine.Repeat(JSRegionHorizontalIndent.bottom));
      return sb.ToString();
    }
  }






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("region"), new XAttribute("name", _name ?? ""), _childTokens.Select(s => s.AsXml));

}
