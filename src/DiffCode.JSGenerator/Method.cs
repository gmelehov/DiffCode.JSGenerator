namespace DiffCode.JSGenerator;

/// <summary>
/// 
/// </summary>
public class Method : KVal
{
  public Method(string key, FunctionAnonymous func) : base(key, func)
  {

  }
  public Method(string key, params AE[] exprs) : this(key, FN(exprs))
  {

  }
  public Method(string key, ArgumentsBlock argsBlock, params AE[] exprs) : this(key, FN(argsBlock, exprs))
  {

  }

}
