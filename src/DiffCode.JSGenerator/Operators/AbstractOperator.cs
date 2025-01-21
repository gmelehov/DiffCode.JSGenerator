namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Абстрактный оператор.
  /// </summary>
  public abstract partial class AbstractOperator : AbstractLiteral<string>
  {
    protected string _name;





    protected AbstractOperator(string oper) : base() => SetValue(oper.Trim('\'', '"'));





    protected AOp Add(string oper) => SetValue(oper.Trim('\'', '"')).As<AOp>();



    public override object GetSelfValue() => _value;




    public override string Formatted => base.Formatted;
    public override string ToString() => Formatted;
    public override XObject AsXml => new XAttribute("op", _name);






    public static AOp Assign => new Operator("=") { _name = "assign" };
    public static AOp Equal => new Operator("==") { _name = "equal" };
    public static AOp StrictEqual => new Operator("===") { _name = "strictEqual" };
    public static AOp NotEqual => new Operator("!=") { _name = "notEqual" };
    public static AOp StrictNotEqual => new Operator("!==") { _name = "strictNotEqual" };
    public static AOp Or => new Operator("||") { _name = "or" };
    public static AOp And => new Operator("&&") { _name = "and" };
    public static AOp Not => new Operator("!") { _name = "not" };
    public static AOp DblNot => new Operator("!!") { _name = "dblNot" };
    public static AOp Gt => new Operator(">") { _name = "gt" };
    public static AOp Lt => new Operator("<") { _name = "lt" };
    public static AOp GtEq => new Operator(">=") { _name = "gtEq" };
    public static AOp LtEq => new Operator("<=") { _name = "ltEq" };
    public static AOp Mod => new Operator("%") { _name = "mod" };
    public static AOp Plus => new Operator("+") { _name = "plus" };
    public static AOp DblPlus => new Operator("++") { _name = "dblPlus" };
    public static AOp Minus => new Operator("-") { _name = "minus" };
    public static AOp DblMinus => new Operator("--") { _name = "dblMinus" };
    public static AOp Divide => new Operator("/") { _name = "divide" };
    public static AOp Multiply => new Operator("*") { _name = "multiply" };
    public static AOp Neg => new Operator("-") { _name = "neg" };
    public static AOp Pos => new Operator("+") { _name = "pos" };
    public static AOp Delete => new Operator("delete") { _name = "delete" };
    public static AOp Void => new Operator("void") { _name = "void" };
    public static AOp TypeOf => new Operator("typeof") { _name = "typeOf" };
    public static AOp InstOf => new Operator("instanceof") { _name = "instOf" };
    public static AOp Lambda => new Operator("=>") { _name = "lambda" };
    public static AOp TernaryIf => new Operator("?") { _name = "ternaryIf" };
    public static AOp Spread => new Operator("...") { _name = "spread" };
    public static AOp PlusEqual => new Operator("+=") { _name = "plusEq" };
    public static AOp MinusEqual => new Operator("-=") { _name = "minusEq" };
    public static AOp DivideEqual => new Operator("/=") { _name = "divideEq" };
    public static AOp MultiplyEqual => new Operator("*=") { _name = "multiplyEq" };
    public static AOp AndEqual => new Operator("&=") { _name = "andEq" };
    public static AOp OrEqual => new Operator("|=") { _name = "orEq" };
    public static AOp In => new Operator("in") { _name = "in" };
    public static AOp BitNot => new Operator("~") { _name = "bitNot" };
    public static AOp BitAnd => new Operator("&") { _name = "bitAnd" };
    public static AOp BitOr => new Operator("|") { _name = "bitOr" };
    public static AOp BitXor => new Operator("^") { _name = "bitXor" };
    public static AOp ShiftLeft => new Operator("<<") { _name = "shiftLeft" };
    public static AOp ShiftRight => new Operator(">>") { _name = "shiftRight" };
    public static AOp ShiftRightFill => new Operator(">>>") { _name = "shiftRightFill" };

  }
}
