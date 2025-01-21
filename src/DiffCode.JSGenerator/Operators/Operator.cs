namespace DiffCode.JSGenerator
{
  /// <summary>
  /// 
  /// </summary>
  public partial class Operator : AOp
  {
    public Operator(string oper) : base(oper) { }






    public override object GetSelfValue() => _value;




    public override string Formatted => GetSelfValue().ToString();
    public override string ToString() => Formatted;

  }
}
