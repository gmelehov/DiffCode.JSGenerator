namespace DiffCode.JSGenerator.Enums
{
  /// <summary>
  /// 
  /// </summary>
  [Flags]
  public enum CompilationTarget
  {

    /// <summary>
    /// Не включать в компиляцию.
    /// </summary>
    none = 0,

    /// <summary>
    /// Включать в компиляцию, если цель сборки - определение класса ExtJS.
    /// </summary>
    define = 1,

    /// <summary>
    /// Включать в компиляцию, если цель сборки - определение объекта-конфигуратора для класса ExtJS.
    /// </summary>
    config = 2,

    /// <summary>
    /// Включать в компиляцию, если цель сборки - определение выражения для создания экземпляра класса ExtJS.
    /// </summary>
    create = 4,

  }
}
