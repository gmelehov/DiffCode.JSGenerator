namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Блок комментариев к коду (многострочный комментарий).
  /// </summary>
  public class CommentBlock : CommentItem
  {
    static CommentBlock()
    {
      TopIndent = 2;
    }
    public CommentBlock() : base(TT.CommentBlock)
    {
      var start = CommentStart;
      var end = CommentEnd;
      var line = CommentLine("");
      Add(start, line, end);
    }
    public CommentBlock(string comment) : base(TT.CommentBlock)
    {
      var start = CommentStart.SetInline(true);
      var end = CommentEnd.SetInline(true);
      var line = CommentLine(true, comment);
      Add(start, line, end);
    }
    public CommentBlock(params string[] comments) : base(TT.CommentBlock)
    {
      var amount = comments.Count();
      var start = amount == 1 ? CommentStart.SetInline(true) : CommentStart;
      var end = amount == 1 ? CommentEnd.SetInline(true) : CommentEnd;
      var lines = Chain(start).Concat(comments.Select(ss => CommentLine(amount == 1, ss)).ToArray()).Append(end).ToArray();
      Add(lines);
    }






    public void AddLine(string comment)
    {
      var line = CommentLine(comment);
      AddToItemsBefore(this, line, IsCommentEnd);
      RefreshAll();
    }

    public void AddLines(params string[] comments)
    {
      foreach (var c in comments)
      {
        var line = CommentLine(c);
        AddToItemsBefore(this, line, IsCommentEnd);
      };
      RefreshAll();
    }






    /// <summary>
    /// Строка с началом многострочного комментария - /**
    /// </summary>
    public CommentItem Start => _childTokens.First() as CommentItem;

    /// <summary>
    /// Коллекция дочерних элементов - однострочных комментариев - * ...
    /// </summary>
    public List<CommentItem> Lines => _childTokens.Where(w => !w.IsFirst() && !w.IsLast()).Cast<CommentItem>().ToList();

    /// <summary>
    /// Строка с окончанием многострочного комментария - */
    /// </summary>
    public CommentItem End => _childTokens.Last() as CommentItem;


    /// <summary>
    /// Форматированное строковое представление объекта.
    /// </summary>
    public override string Formatted
    {
      get
      {
        var sb = new StringBuilder("");
        var parentIndent = HasParent() ? Parent.IndentString : IndentString;

        if (IsNotNull(Start) && IsNotNull(End) && Lines != null && Lines.Count > 0)
        {
          sb.Append(parentIndent + Start?.Formatted);
          foreach (var it in Lines)
          {
            //sb.Append(parentIndent + it?.Formatted);
            sb.Append(it?.Formatted);
          };
            
          sb.Append(Lines.Count == 1 ? "" : parentIndent + End?.Formatted);
        };
        return sb.ToString();
      }
    }






    /// <summary>
    /// Количество пустых строк, выводимых перед началом каждого блока комментариев.
    /// По умолчанию - 2 строки.
    /// </summary>
    public static int TopIndent { get; set; }

  }
}
