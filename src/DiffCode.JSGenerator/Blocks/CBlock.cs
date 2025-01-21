namespace DiffCode.JSGenerator;

/// <summary>
/// Абстрактный блок кода 
/// (блок выражений и исполняемых инструкций, разделенных точками с запятой).
/// </summary>
public abstract class CBlock : AE
{
  public CBlock(TT type) : base(type) { }






  /// <summary>
  /// Метод для задания блока комментариев, выводимых непосредственно перед объектом.
  /// </summary>
  /// <param name="comment">Добавляемый блок комментариев.</param>
  public void SetComment(CommentBlock comment) => Comment = comment;


  /// <summary>
  /// Метод для добавления одиночной строки комментария в блок многострочных комментариев объекта.
  /// Новая строка комментария добавляется в конец блока, непосредственно перед последней строкой, замыкающей блок комментариев.
  /// </summary>
  /// <param name="item">Добавляемая строка комментариев.</param>
  public void NewComment(CommentItem item)
  {
    if (IsCommentLine(item))
    {
      if (!IsNotNull(Comment) || !IsCommentBlock(Comment))
      {
        SetComment(CommentBlock());
      }
      else
      {
        var lastItem = Comment.ChildTokens?.Last();
        var lastItemIndex = Comment.ChildTokens.ToList().IndexOf(lastItem);
        Comment.ChildTokens.ToList().Insert(lastItemIndex, item);
      };
    };
  }
}
