namespace DiffCode.JSGenerator;

/// <summary>
/// Бинарная операция %
/// </summary>
public class ModExpr : AbstractBinaryExpression
{
  public ModExpr(AE left, AE right) : base(left, AOp.Mod, right) { }
  public ModExpr(CommentItem comment, AE left, AE right) : base(comment, left, AOp.Mod, right) { }
}
