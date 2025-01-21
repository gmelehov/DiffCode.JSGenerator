namespace DiffCode.JSGenerator;

/// <summary>
/// 
/// </summary>
public abstract class ClassBaseMethod : ClassMember
{
  protected ClassBaseMethod(TT type, bool isStatic = false, ArgumentsBlock args = null, string name = null, params AE[] exprs) : base(type)
  {
    if(args == null)
    {
      args = Args();
    };

    var bodyBlock = FnBody(exprs);


    switch (type)
    {

      case TT.ClassConstructor:
        base.Add(isStatic ? Static : null, Constructor, args, bodyBlock);
        break;


      case TT.ClassMethod:
        base.Add(isStatic ? Static : null, Str(name), args, bodyBlock);
        break;


      case TT.ClassGetter:
        base.Add(isStatic ? Static : null, Get, Str(name), Args(), bodyBlock);
        break;


      case TT.ClassSetter:
        base.Add(isStatic ? Static : null, Set, Str(name), args, bodyBlock);
        break;



      default: break;
    };


  }









}
