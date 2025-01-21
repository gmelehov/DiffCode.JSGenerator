namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Абстрактное выражение приращения/уменьшения на единицу
  /// </summary>
  public abstract partial class UpdateExpression : AbstractUnaryExpression
	{
    protected UpdateExpression(TT type, AE arg, AOp oper) : base(type, arg, oper as Operator) { }
    protected UpdateExpression(TT type, AOp oper, AE arg) : base(type, oper as Operator, arg) { }
	}
}
