using FB = DiffCode.JSGenerator.FnBodyBlock;
using static DiffCode.JSGenerator.XmlParse;
using System.Diagnostics;


namespace DiffCode.JSGenerator;

/// <summary>
/// Тело функции (блок кода).
/// </summary>
public class FnBodyBlock : CBlock
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _wrapLeftBrace = true;






  public FnBodyBlock() : base(TT.FnBodyBlock)
  {
    if (IsInsideCase)
    {
      _extraIndent = "  ";
    };
  }
  public FnBodyBlock(params AE[] exprs) : this()
  {
    if (exprs.Count() == 1 && exprs[0] is FB)
    {
      Add(exprs[0].ChildTokens);
    }
    else
    {
      Add(exprs);
    };
  }
  public FnBodyBlock(IEnumerable<AE> exprs) : this()
  {
    if (exprs.Count() == 1 && exprs.FirstOrDefault() is FB)
    {
      Add(exprs.FirstOrDefault().ChildTokens);
    }
    else
    {
      Add(exprs);
    };
  }
  public FnBodyBlock(ExpressionsSet exprs) : this()
  {
    Add(exprs.ChildTokens);
    if (exprs.Comment != null)
    {
      this.ChildTokens?.FirstOrDefault()?.As<AE>()?.AddComment(exprs.Comment);
    };
  }







  /// <summary>
  /// Метод для добавления одиночного выражения/исполняемой инструкции в список дочерних элементов.
  /// Выражение/исполняемая инструкция добавляются в конец списка.
  /// </summary>
  /// <param name="expr"></param>
  public FB AddExpression(AE expr)
  {
    Add(expr);
    return this;
  }





  /// <summary>
  /// Разделитель дочерних элементов-выражений - точка с запятой.
  /// </summary>
  public override string ItemsDivider => ";";

  /// <summary>
  /// Форматированное строковое представление объекта.
  /// </summary>
  public override string Formatted
  {
    get
    {
      var sb = new StringBuilder("");

      /// Если родительский элемент - this или выражение if,
      /// И количество дочерних элементов в блоке равно -1
      /// ТАКИМ ОБРАЗОМ, ВСЕГДА РАВНО false!
      bool wrappersSkip = false;
      //bool wrappersSkip = (HasSuchParents(this, IsIfExpression) && _childTokens?.Count == -1);

      /// Если родительский элемент этого блока кода - НЕ определение массива, 
      /// И этот блок кода следует НЕ за ключевым словом else, И в этом блоке кода присутствуют дочерние элементы-выражения
      if (Parent?.GetTokenType() != TT.ArrayDefinition /*&& IsNotNull(Prev)*/ && _childTokens.Count > 0)
      {
        /// если открывающая скобка должна помещаться на новой строке,
        /// добавляем новую строку и собственный отступ
        if (_wrapLeftBrace == true)
        {
          sb.AppendLine();
          sb.Append(IndentString);
        }
        /// ...иначе добавляем один пробел
        else
        {
          sb.Append(" ");
        };
      }
      else if (IsNotNull(Prev))
      {
        sb.Append(" ");
      };


      /// добавляем открывающую фигурную скобку
      if (!wrappersSkip)
      {
        if (IsInsideCase)
        {
          sb.Append("  ");
        };
        sb.Append("{");
      };

      /// Если в этом блоке кода присутствуют дочерние элементы-выражения
      if (_childTokens.Count > 0)
      {
        /// добавляем новую строку
        if (!wrappersSkip)
        {
          sb.AppendLine();
        };



        /// для всех дочерних элементов-выражений, не равных null
        foreach (var it in _childTokens.Where(IsNotNull))
        {
          /// добавляем отступ дочернего элемента-выражения
          if (!wrappersSkip)
          {
            sb.Append(it.IndentString);
            if (IsInsideCase)
            {
              sb.Append("  ");
            };
          }

          /// добавляем форматированное строковое представление дочернего элемента-выражения
          sb.Append(it.Formatted);

          if (!wrappersSkip)
          {
            /// добавляем разделитель после каждого дочернего элемента, включая последний
            if (!it.Formatted.StartsWith("\r\n") && !(it is ExpressionsSet) && !(it is JSRegion) && !(it is CaseExpression && it.As<CaseExpression>().IsEmpty))
              sb.Append(ItemsDivider);

            /// добавляем новую строку
            sb.AppendLine();

            /// дополнительная отбивка одной пустой строкой для непустых дочерних элементов типа case
            if (it is CaseExpression && !it.As<CaseExpression>().IsEmpty)
            {
              sb.AppendLine();
            };

          };
        };



        /// добавляем отступ этого блока кода
        if (!wrappersSkip)
          sb.Append(IndentString);
      };

      /// добавляем закрывающую фигурную скобку
      if (!wrappersSkip)
      {
        if (IsInsideCase)
        {
          sb.Append("  ");
        };
        sb.Append("}");
      };


      /// возвращаем результат
      return sb.ToString();
    }
  }






  private bool IsInsideCase => this.Closest<CaseExpression>() != null;






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml => new XElement(JsNS("region"), _childTokens.Select(s => s.AsXml));

}
