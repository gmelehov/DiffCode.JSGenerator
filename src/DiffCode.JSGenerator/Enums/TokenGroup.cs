

namespace DiffCode.JSGenerator.Enums
{
  /// <summary>
  /// Группы JavaScript-токенов.
  /// </summary>
  public enum TokenGroup
  {
    /// <summary>
    /// Наименование группы отсутствует/не установлено/не определено
    /// </summary>
    NONE,

    /// <summary>
    /// Группа ключевых слов
    /// </summary>
    Keywords,

    /// <summary>
    /// Группа математических операторов
    /// </summary>
    MathOpers,

    /// <summary>
    /// Группа булевых операторов
    /// </summary>
    BooleanOpers,

    /// <summary>
    /// Прочее
    /// </summary>
    Other,

    /// <summary>
    /// Группа открывающих и закрывающих скобок
    /// </summary>
    BracesBrackets,
    
    /// <summary>
    /// Группа разделителей-знаков препинания
    /// </summary>
    Separators,
    
    /// <summary>
    /// Группа кавычек-ограничителей строк
    /// </summary>
    Quotes,
    
    /// <summary>
    /// Группа пробельных символов
    /// </summary>
    Spaces,
    
    /// <summary>
    /// Группа идентификаторов, строковых и числовых значений
    /// </summary>
    Literals,
    
    /// <summary>
    /// Группа фрагментов (типовых последовательностей токенов)
    /// </summary>
    Fragments,
    
    /// <summary>
    /// Группа глобальных объектов
    /// </summary>
    GLOBAL,
    
    /// <summary>
    /// Группа комментариев
    /// </summary>
    CommentItems,
    
    /// <summary>
    /// Группа блоков комментариев
    /// </summary>
    CommentBlock,

  }
}
