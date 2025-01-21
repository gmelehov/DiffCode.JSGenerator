using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FB = DiffCode.JSGenerator.FnBodyBlock;
using Idd = DiffCode.JSGenerator.Identifier;
using static DiffCode.JSGenerator.XmlParse;


namespace DiffCode.JSGenerator;

/// <summary>
/// Определение именованной функции.
/// </summary>
public class FunctionNamed : AE
{
  public FunctionNamed(string name) : base(TT.FuncDefinition)
  {
    var _args = Args();
    var _body = FnBody();
    Add(Function, ID(name), _args, _body);
  }
  public FunctionNamed(string name, ARG args) : base(TT.FuncDefinition)
  {
    var _body = FnBody();
    Add(Function, ID(name), args, _body);
  }
  public FunctionNamed(string name, FB fnBody) : base(TT.FuncDefinition)
  {
    var _args = Args();
    Add(Function, ID(name), _args, fnBody);
  }
  public FunctionNamed(string name, ARG args, FB fnBody) : base(TT.FuncDefinition) => Add(Function, ID(name), args, fnBody);
  public FunctionNamed(string name, ARG args, params AE[] exprs) : base(TT.FuncDefinition)
  {
    var _body = FnBody(exprs);
    Add(Function, ID(name), args, _body);
  }
  public FunctionNamed(string name, params AE[] exprs) : base(TT.FuncDefinition)
  {
    var _args = Args();
    var _body = FnBody(exprs);
    Add(Function, ID(name), _args, _body);
  }







  /// <summary>
  /// Добавить аргумент в список аргументов функции.
  /// </summary>
  /// <param name="arg">Добавляемый аргумент.</param>
  public void AddArgument(AT arg)
  {
    Arguments?.Add(arg);
    Arguments?.RefreshAll();
  }

  /// <summary>
  /// Добавить выражение (инструкцию) в блок кода функции.
  /// </summary>
  /// <param name="expr">Добавляемое выражение(инструкция).</param>
  public void AddExpr(AE expr) => Body?.AddExpression(expr);

  /// <summary>
  /// Добавить блок комментариев к определению функции.
  /// </summary>
  /// <param name="comment"></param>
  /// <returns></returns>
  public virtual FunctionNamed AddCommentBlock(string comment)
  {
    Comment = CommentBlock(comment);
    return this;
  }

  /// <summary>
  /// Добавить блок комментариев к определению функции.
  /// </summary>
  /// <param name="comment"></param>
  /// <returns></returns>
  public virtual FunctionNamed AddCommentBlock(params string[] comments)
  {
    Comment = CommentBlock(comments);
    return this;
  }

  /// <summary>
  /// Изменяет количество отступов (пустых строк) перед элементом.
  /// </summary>
  /// <param name="gapbefore">Количество пустых строк перед элементом.</param>
  public new FunctionNamed SetGap(int gapbefore)
  {
    this._gapBefore = gapbefore;
    return this;
  }







  /// <summary>
  /// Имя функции (идентификатор).
  /// </summary>
  public Idd Name => _childTokens?.FirstOrDefault()?.Next as Idd;

  /// <summary>
  /// Аргументы функции.
  /// </summary>
  public ARG Arguments => Name?.Next as ARG;

  /// <summary>
  /// Тело функции (блок кода).
  /// </summary>
  public FB Body => Arguments?.Next as FB ?? _childTokens?.LastOrDefault() as FB;

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
      sb.Append(Function.Formatted + " " + Name.Formatted + " " + Arguments?.Formatted + Body?.Formatted);
      return sb.ToString();
    }
  }






  /// <summary>
  /// XML-представление объекта.
  /// </summary>
  public override XObject AsXml =>
    _gapBefore > 0
    ?
    new XElement(JsNS("fn"), new XAttribute("name", Name.Formatted.Trim('\'')), new XAttribute("gap", _gapBefore), Arguments.AsXml, Body.ChildTokens.Select(s => s.AsXml))
    :
    new XElement(JsNS("fn"), new XAttribute("name", Name.Formatted.Trim('\'')), Arguments.AsXml, Body.ChildTokens.Select(s => s.AsXml));

}
