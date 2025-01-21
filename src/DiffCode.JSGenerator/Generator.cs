using System.Linq.Expressions;

using DiffCode.JSGenerator.Enums;

using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FAnon = DiffCode.JSGenerator.FunctionAnonymous;
using FB = DiffCode.JSGenerator.FnBodyBlock;
using Idd = DiffCode.JSGenerator.Identifier;
using MCall = DiffCode.JSGenerator.MethodCall;
using Memb = DiffCode.JSGenerator.MemberAccess;
using OB = DiffCode.JSGenerator.ObjectBody;
using SL = DiffCode.JSGenerator.StringLiteral;
using VarDecl = DiffCode.JSGenerator.VarDeclaration;



namespace DiffCode.JSGenerator;

/// <summary>
/// Статический класс-генератор простых/составных токенов/синтаксических конструкций.
/// </summary>
public static partial class Generator
{



  public static Expression<Func<AE, MCall>> callMethod;



  public static List<T> Chain<T>(params T[] args) where T : AT => new List<T>().Concat(args).ToList();




  /// <summary>
  /// Возвращает функцию, определяющую простой токен, не имеющий дочерних элементов.
  /// </summary>
  public static Func<TT, ST> Base => type => new ST(type);






  #region OBJ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  /// <summary>
  /// Тело объекта (контейнер свойств, перечисляемых через запятую, обернутый в фигурные скобки).
  /// </summary>
  /// <param name="items">Массив свойств объекта в формате "ключ: значение ключа".</param>
  /// <returns></returns>
  public static OB OBJ(params KVal[] items) => new OB(items);
  public static OB OBJ(IEnumerable<KVal> items) => new OB(items);
  public static OB OBJ(bool sparsed, params KVal[] items) => new OB(sparsed, items);
  public static OB OBJ(bool sparsed, IEnumerable<KVal> items) => new OB(sparsed, items);



  public static OB OBJ(bool wrapLeftBrace, bool sparsed, params KVal[] items) => new OB(wrapLeftBrace, sparsed, items);
  public static OB OBJ(bool wrapLeftBrace, bool sparsed, IEnumerable<KVal> items) => new OB(wrapLeftBrace, sparsed, items);

  #endregion








  /// <summary>
  /// Тело функции (контейнер выражений, перечисляемых через точку с запятой, обернутый в фигурные скобки).
  /// </summary>
  /// <param name="items"></param>
  /// <returns></returns>
  public static FB FnBody(params AE[] items) => new FB(items);
  public static FB FnBody(IEnumerable<AE> items) => new FB(items);






  #region Args ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  /// <summary>
  /// Блок аргументов (контейнер объектов, перечисляемых через запятую, обернутый в круглые скобки).
  /// </summary>
  /// <param name="items"></param>
  /// <returns></returns>
  public static ARG Args(params AT[] items) => new ARG(items);
  public static ARG Args(IEnumerable<AT> items) => new ARG(items);
  public static ARG Args(params string[] items) => new ARG(items);
  public static ARG Args(string str) => new ARG(str);
  public static ARG Args(double num) => new ARG(num);
  public static ARG Args(bool groupsExprs = false) => new ARG(groupsExpressions: groupsExprs);
  public static ARG Args(string str, params KVal[] props) => new ARG(Str(str), OBJ(props));

  #endregion





  public static FB Block(params AE[] items) => new FB(items);






  public static ExprStatement Expr(params AT[] items) => new ExprStatement(items);
  public static ExprStatement Expr(CommentItem comment, params AT[] items) => new ExprStatement(comment, items);
  public static ExpressionsSet EXPRS(params AE[] exprs) => new ExpressionsSet(exprs);
  public static ExpressionsSet EXPRS(IEnumerable<AE> exprs) => new ExpressionsSet(exprs);
  public static ExpressionsSet EXPRS(AE first, IEnumerable<AE> exprs) => new ExpressionsSet(first, exprs);
  public static ExpressionsSet EXPRS(IEnumerable<AE> exprs, AE second) => new ExpressionsSet(exprs, second);
  public static ExpressionsSet EXPRS(params IEnumerable<AE>[] exprs) => new ExpressionsSet(exprs);
  public static ExpressionsSet EXPRS(CommentItem comment, params IEnumerable<AE>[] exprs) => new ExpressionsSet(comment, exprs);






  public static JSRegion REGION(string name, params AE[] exprs) => new JSRegion(name, exprs);
  public static JSRegion REGION(string name, AE expr, IEnumerable<AE> exprs) => new JSRegion(name, exprs);
  public static JSRegion REGION(string name, IEnumerable<AE> exprs) => new JSRegion(name, exprs);





  public static IIFEExpression IIFE(FAnon fn, ARG args = null) => new IIFEExpression(fn, args);






  #region ASSIGN //////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static AssignExpr ASSIGN(AE left, AE right) => new AssignExpr(left, right);
  public static AssignExpr ASSIGN(string left, AE right) => new AssignExpr(Member(left), right);
  public static AssignExpr ASSIGN(string left, bool state) => new AssignExpr(Member(left), (state) ? TRUE as AE : FALSE as AE);
  public static AssignExpr ASSIGN(AE left, bool state) => new AssignExpr(left, (state) ? TRUE as AE : FALSE as AE);
  public static AssignExpr ASSIGN(string left) => new AssignExpr(Member(left), NULL);
  public static AssignExpr ASSIGN(AE left) => new AssignExpr(left, NULL);
  public static AssignExpr ASSIGN(CommentItem comment, AE left, AE right) => new AssignExpr(comment, left, right);



  public static AssignExpr ASSIGN(int gapBefore, AE left, AE right) => new AssignExpr(left, right).SetGap(gapBefore);
  public static AssignExpr ASSIGN(int gapBefore, string left, AE right) => new AssignExpr(Member(left), right).SetGap(gapBefore);
  public static AssignExpr ASSIGN(int gapBefore, string left, bool state) => new AssignExpr(Member(left), (state) ? TRUE as AE : FALSE as AE).SetGap(gapBefore);
  public static AssignExpr ASSIGN(int gapBefore, AE left, bool state) => new AssignExpr(left, (state) ? TRUE as AE : FALSE as AE).SetGap(gapBefore);
  public static AssignExpr ASSIGN(int gapBefore, string left) => new AssignExpr(Member(left), NULL).SetGap(gapBefore);
  public static AssignExpr ASSIGN(int gapBefore, AE left) => new AssignExpr(left, NULL).SetGap(gapBefore);
  public static AssignExpr ASSIGN(int gapBefore, CommentItem comment, AE left, AE right) => new AssignExpr(comment, left, right).SetGap(gapBefore);

  #endregion








  #region VAR /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static VarDecl VAR(string var) => new VarDecl(var);
  public static VarDecl VAR(AE var) => new VarDecl(var.Formatted);
  public static VarDecl VAR(Idd var) => new VarDecl(var);
  public static VarDecl VAR(string var, string stringValue) => new VarDecl(ID(var), Str(stringValue));
  public static VarDecl VAR(string var, double numValue) => new VarDecl(ID(var), Num(numValue));
  public static VarDecl VAR(string var, bool booleanValue) => new VarDecl(var, booleanValue ? TRUE.As<AE>() : FALSE.As<AE>());
  public static VarDecl VAR(string var, AE rightExpr) => new VarDecl(var, rightExpr);
  public static VarDecl VAR(CommentItem comment, string var, AE rightExpr) => new VarDecl(comment, var, rightExpr);
  public static VarDecl VAR(CommentItem comment, Idd var, AE rightExpr) => new VarDecl(comment, var, rightExpr);
  public static VarDecl VAR(AE leftExpr, AE rightExpr) => new VarDecl(leftExpr.Formatted, rightExpr);
  public static VarDecl VAR(Idd leftExpr, AE rightExpr) => new VarDecl(leftExpr, rightExpr);



  public static VarDecl VAR(int gapBefore, string var) => new VarDecl(var).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, AE var) => new VarDecl(var.Formatted).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, Idd var) => new VarDecl(var).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, string var, string stringValue) => new VarDecl(ID(var), Str(stringValue)).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, string var, double numValue) => new VarDecl(ID(var), Num(numValue)).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, string var, bool booleanValue) => new VarDecl(var, booleanValue ? TRUE.As<AE>() : FALSE.As<AE>()).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, string var, AE rightExpr) => new VarDecl(var, rightExpr).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, CommentItem comment, string var, AE rightExpr) => new VarDecl(comment, var, rightExpr).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, CommentItem comment, Idd var, AE rightExpr) => new VarDecl(comment, var, rightExpr).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, AE leftExpr, AE rightExpr) => new VarDecl(leftExpr.Formatted, rightExpr).SetGap(gapBefore);
  public static VarDecl VAR(int gapBefore, Idd leftExpr, AE rightExpr) => new VarDecl(leftExpr, rightExpr).SetGap(gapBefore);

  #endregion






  #region LET /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static LetDeclaration LET(Idd var) => new LetDeclaration(var);
  public static LetDeclaration LET(string let, AE rightExpr) => new LetDeclaration(let, rightExpr);
  public static LetDeclaration LET(AE leftExpr, AE rightExpr) => new LetDeclaration(leftExpr.Formatted, rightExpr);
  public static LetDeclaration LET(Idd leftExpr, AE rightExpr) => new LetDeclaration(leftExpr, rightExpr);



  public static LetDeclaration LET(int gapBefore, Idd var) => new LetDeclaration(var).SetGap(gapBefore);
  public static LetDeclaration LET(int gapBefore, string let, AE rightExpr) => new LetDeclaration(let, rightExpr).SetGap(gapBefore);
  public static LetDeclaration LET(int gapBefore, AE leftExpr, AE rightExpr) => new LetDeclaration(leftExpr.Formatted, rightExpr).SetGap(gapBefore);
  public static LetDeclaration LET(int gapBefore, Idd leftExpr, AE rightExpr) => new LetDeclaration(leftExpr, rightExpr).SetGap(gapBefore);

  #endregion






  #region CONST ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static ConstDeclaration CONST(string _const, AE rightExpr) => new ConstDeclaration(_const, rightExpr);
  public static ConstDeclaration CONST(AE leftExpr, AE rightExpr) => new ConstDeclaration(leftExpr.Formatted, rightExpr);
  public static ConstDeclaration CONST(CommentItem comment, string _const, AE rightExpr) => new ConstDeclaration(comment, _const, rightExpr);


  public static ConstDeclaration CONST(int gapBefore, string _const, AE rightExpr) => new ConstDeclaration(_const, rightExpr);
  public static ConstDeclaration CONST(int gapBefore, AE leftExpr, AE rightExpr) => new ConstDeclaration(leftExpr.Formatted, rightExpr);
  public static ConstDeclaration CONST(int gapBefore, CommentItem comment, string _const, AE rightExpr) => new ConstDeclaration(comment, _const, rightExpr);

  #endregion







  public static TrueExpression TRUE => new TrueExpression();
  public static FalseExpression FALSE => new FalseExpression();
  public static NullExpression NULL => new NullExpression();
  public static UndefExpression UNDEF => new UndefExpression();
  public static NaNExpression NAN => new NaNExpression();
  public static BreakExpression BREAK => new BreakExpression();
  public static ContinueExpression CONTINUE => new ContinueExpression();




  #region RETURN //////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static ReturnExpression RETURN() => new ReturnExpression();
  public static ReturnExpression RETURN(string ret) => new ReturnExpression(ret);
  public static ReturnExpression RETURN(double num) => new ReturnExpression(num);
  public static ReturnExpression RETURN(SL htmlString) => new ReturnExpression(htmlString as AE);
  public static ReturnExpression RETURN(bool ret) => new ReturnExpression(ret);
  public static ReturnExpression RETURN(AE expr) => new ReturnExpression(expr);

  #endregion




  #region TRY, CATCH, FINALLY /////////////////////////////////////////////////////////////////////////////////////////////////

  public static TryExpr TRY(params AE[] exprs) => new TryExpr(exprs);
  public static TryExpr TRY(IEnumerable<AE> exprs) => new TryExpr(exprs);
  public static CatchExpr CATCH(params AE[] exprs) => new CatchExpr(exprs);
  public static CatchExpr CATCH(IEnumerable<AE> exprs) => new CatchExpr(exprs);
  public static CatchExpr CATCH(ARG args, params AE[] exprs) => new CatchExpr(args, exprs);
  public static CatchExpr CATCH(ARG args, IEnumerable<AE> exprs) => new CatchExpr(args, exprs);
  public static FinallyExpr FINALLY(params AE[] exprs) => new FinallyExpr(exprs);
  public static FinallyExpr FINALLY(IEnumerable<AE> exprs) => new FinallyExpr(exprs);
  public static TryCatchFinallyExpr TRY_CATCH(TryExpr _try, CatchExpr _catch, FinallyExpr _finally = null) => new TryCatchFinallyExpr(_try, _catch, _finally);
  public static TryCatchFinallyExpr TRY_CATCH(TryExpr _try, FinallyExpr _finally) => new TryCatchFinallyExpr(_try, _finally);

  #endregion






  //public static ForExpression FOR(AE seed, ComparisonOperator oper, AE stop, UpdateExpression updExpr, ExpressionsSet exprs) => new ForExpression(seed, oper, stop, updExpr, exprs);
  //public static ForExpression FOR(int seed, ComparisonOperator oper, AE stop, UpdateExpression updExpr, ExpressionsSet exprs) => new ForExpression(seed, oper, stop, updExpr, exprs);

  public static ForExpr FOR(string loopvar, int seed, AbstractBinaryExpression testBlock, UpdateExpression upd, ExpressionsSet exprs) => new ForExpr(loopvar, seed, testBlock, upd, exprs);
  public static ForExpr FOR(Idd loopvar, AE seed, AbstractBinaryExpression testBlock, UpdateExpression upd, ExpressionsSet exprs) => new ForExpr(loopvar, seed, testBlock, upd, exprs);






  #region IF //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  /// <summary>
  /// Синтаксическая конструкция if (test) { ifBlock } else { elseBlock }.
  /// </summary>
  /// <param name="test">Выражение, проверяемое на истинность/ложность.</param>
  /// <param name="ifBlock">Блок выражений/инструкций, выполняемых в случае истинности проверяемого выражения.</param>
  /// <param name="elseBlock">Блок выражений/инструкций, выполняемых в случае ложности проверяемого выражения.</param>
  /// <returns></returns>
  public static IfElseExpression IF(ExprStatement test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(test, ifBlock, elseBlock);
  public static IfElseExpression IF(AbstractBinaryExpression test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(test, ifBlock, elseBlock?.ChildTokens?.Cast<AE>().ToArray());
  public static IfElseExpression IF(AbstractUnaryExpression test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(test, ifBlock, elseExprs);
  public static IfElseExpression IF(AbstractBinaryExpression test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(test, ifBlock, elseExprs);
  public static IfElseExpression IF(AbstractUnaryExpression test, FB ifBlock, IEnumerable<AE> elseExprs) => new IfElseExpression(test, ifBlock, elseExprs.ToArray());
  public static IfElseExpression IF(AbstractBinaryExpression test, FB ifBlock, IEnumerable<AE> elseExprs) => new IfElseExpression(test, ifBlock, elseExprs.ToArray());
  public static IfElseExpression IF(AbstractUnaryExpression test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(test, ifBlock, elseBlock?.ChildTokens?.Cast<AE>().ToArray());
  public static IfElseExpression IF(AE test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(test, ifBlock, elseExprs);




  public static IfElseExpression IF(int gapBefore, ExprStatement test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(test, ifBlock, elseBlock).SetGap(gapBefore);
  public static IfElseExpression IF(int gapBefore, AbstractBinaryExpression test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(test, ifBlock, elseBlock?.ChildTokens?.Cast<AE>().ToArray()).SetGap(gapBefore);
  public static IfElseExpression IF(int gapBefore, AbstractUnaryExpression test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(test, ifBlock, elseExprs).SetGap(gapBefore);
  public static IfElseExpression IF(int gapBefore, AbstractBinaryExpression test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(test, ifBlock, elseExprs).SetGap(gapBefore);
  public static IfElseExpression IF(int gapBefore, AbstractUnaryExpression test, FB ifBlock, IEnumerable<AE> elseExprs) => new IfElseExpression(test, ifBlock, elseExprs.ToArray()).SetGap(gapBefore);
  public static IfElseExpression IF(int gapBefore, AbstractBinaryExpression test, FB ifBlock, IEnumerable<AE> elseExprs) => new IfElseExpression(test, ifBlock, elseExprs.ToArray()).SetGap(gapBefore);
  public static IfElseExpression IF(int gapBefore, AbstractUnaryExpression test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(test, ifBlock, elseBlock?.ChildTokens?.Cast<AE>().ToArray()).SetGap(gapBefore);
  public static IfElseExpression IF(int gapBefore, AE test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(test, ifBlock, elseExprs).SetGap(gapBefore);




  public static IfElseExpression IF(CommentItem comment, AbstractBinaryExpression test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(comment, test, ifBlock, elseBlock);
  public static IfElseExpression IF(CommentItem comment, AbstractUnaryExpression test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(comment, test, ifBlock, elseExprs);
  public static IfElseExpression IF(CommentItem comment, AbstractBinaryExpression test, FB ifBlock, params AE[] elseExprs) => new IfElseExpression(comment, test, ifBlock, elseExprs);
  public static IfElseExpression IF(CommentItem comment, AbstractUnaryExpression test, FB ifBlock, IEnumerable<AE> elseExprs) => new IfElseExpression(comment, test, ifBlock, elseExprs.ToArray());
  public static IfElseExpression IF(CommentItem comment, AbstractBinaryExpression test, FB ifBlock, IEnumerable<AE> elseExprs) => new IfElseExpression(comment, test, ifBlock, elseExprs.ToArray());
  public static IfElseExpression IF(CommentItem comment, AbstractUnaryExpression test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(comment, test, ifBlock, elseBlock);



  public static IfElseExpression IF(CommentItem comment, ExprStatement test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(comment, test, ifBlock, elseBlock);

  #endregion



  #region TEST, THEN, ELSE ////////////////////////////////////////////////////////////////////////////////////////////////////

  public static ExprStatement TEST(AT left, ComparisonOperator compType = null, AT right = null)
  {
    var ret = new ExprStatement();
    if (left != null)
    {
      if (right != null)
        ret.Add(left, compType, right);
      else
      {
        if (compType != null)
        {
          if (compType.GetTokenType() == TT.BooleanNOT || compType.GetTokenType() == TT.IsNotEqual)
            ret.Add(NOT, left);
          else
            ret.Add(left);
        }
        else
          ret.Add(left);
      };
    }
    else
      ret.Add(True);

    return ret;
  }


  public static FB THEN(params AE[] exprs) => new FB(exprs);
  public static FB THEN(IEnumerable<AE> exprs) => new FB(exprs);


  public static FB ELSE(params AE[] exprs) => new FB(exprs);
  public static FB ELSE(IEnumerable<AE> exprs) => new FB(exprs);

  #endregion





  #region TIF /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static IfElseExpression TIF(ExprStatement test, FB ifBlock, FB elseBlock = null) => new IfElseExpression(test, ifBlock, elseBlock) { AsTernary = true };
  public static IfElseExpression TIF(AbstractUnaryExpression test, AE ifTrue, AE ifFalse) => new IfElseExpression(test, ifTrue, ifFalse) { AsTernary = true };
  public static IfElseExpression TIF(AbstractBinaryExpression test, AE ifTrue, AE ifFalse) => new IfElseExpression(test, ifTrue, ifFalse) { AsTernary = true };
  public static IfElseExpression TIF(AE test, AE ifTrue, AE ifFalse) => new IfElseExpression(test, ifTrue, ifFalse) { AsTernary = true };

  #endregion





  #region SWITCH //////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static SwitchExpr SWITCH() => new SwitchExpr();
  public static SwitchExpr SWITCH(params CaseExpression[] exprs) => new SwitchExpr(exprs);
  public static SwitchExpr SWITCH(AE arg, params CaseExpression[] exprs) => new SwitchExpr(arg, exprs);
  public static SwitchExpr SWITCH(AE arg, IEnumerable<CaseExpression> cases, CaseExpression defcase) => new SwitchExpr(arg, cases, defcase);
  public static SwitchExpr SWITCH(string arg, params CaseExpression[] exprs) => new SwitchExpr(arg, exprs);



  public static SwitchExpr SWITCH(int gapBefore) => new SwitchExpr().SetGap(gapBefore);
  public static SwitchExpr SWITCH(int gapBefore, params CaseExpression[] exprs) => new SwitchExpr(exprs).SetGap(gapBefore);
  public static SwitchExpr SWITCH(int gapBefore, AE arg, params CaseExpression[] exprs) => new SwitchExpr(arg, exprs).SetGap(gapBefore);
  public static SwitchExpr SWITCH(int gapBefore, AE arg, IEnumerable<CaseExpression> cases, CaseExpression defcase) => new SwitchExpr(arg, cases, defcase).SetGap(gapBefore);
  public static SwitchExpr SWITCH(int gapBefore, string arg, params CaseExpression[] exprs) => new SwitchExpr(arg, exprs).SetGap(gapBefore);

  #endregion





  #region CASE ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static CaseExpression CASE() => new CaseExpression();
  public static CaseExpression CASE(AT expr) => new CaseExpression(expr);
  public static CaseExpression CASE(params AE[] exprs) => new CaseExpression(exprs);
  public static CaseExpression CASE(IEnumerable<AE> exprs) => new CaseExpression(exprs);
  public static CaseExpression CASE(int caseVal, params AE[] exprs) => new CaseExpression(caseVal, exprs: exprs);
  public static CaseExpression CASE(string caseVal, params AE[] exprs) => new CaseExpression(caseVal, exprs: exprs);
  public static CaseExpression CASE(Literal caseVal, params AE[] exprs) => new CaseExpression(caseVal, exprs: exprs);

  #endregion









  /// <summary>
  /// Выражение this.
  /// </summary>
  public static ThisExpression THIS => new ThisExpression();
  /// <summary>
  /// Строка, заключенная в апострофы/кавычки.
  /// </summary>
  /// <param name="val">Объект val, значение которого будет преобразовано в строку.</param>
  /// <returns></returns>
  public static SL Str(object val) => new SL(val);
  /// <summary>
  /// Числовое значение.
  /// </summary>
  /// <param name="val">Объект val, значение которого будет преобразовано в число.</param>
  /// <returns></returns>
  public static NumberLiteral Num(object val) => new NumberLiteral(val);
  public static SimpleTypeLiteral Simple(IFormatted val) => new SimpleTypeLiteral(val);
  public static RegexpLiteral REGEXP(object val, string flags = "g") => new RegexpLiteral(val, flags);





  /// <summary>
  /// Идентификатор (имя переменной, метода, свойства, функции...) - строка БЕЗ апострофов/кавычек.
  /// </summary>
  /// <param name="name">Имя идентификатора.</param>
  /// <returns></returns>
  public static Idd ID(object name) => new Idd(name?.ToString());






  #region MEMB ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static Memb Member(params string[] segments) => new Memb(segments);
  public static Memb Member(string segments) => new Memb(segments);
  public static Memb Member(params AE[] members) => new Memb(members);




  public static Memb MEMB(AE expr, IEnumerable<AE> exprs) => new Memb(expr, exprs);
  public static Memb MEMB(Idd expr, params string[] exprs) => new Memb(expr, exprs.Select(s => ID(s)));
  public static Memb MEMB(params AE[] exprs) => new Memb(exprs);
  public static Memb MEMB(params string[] idents) => new Memb(idents);
  public static Memb MEMB(params Idd[] idents) => new Memb(idents);
  public static Memb MEMB(IEnumerable<string> idents) => new Memb(idents);
  public static Memb MEMB(IEnumerable<Idd> idents) => new Memb(idents);
  public static Memb MEMB(string ident, CallExpr callExpr) => new Memb(ident, callExpr);
  public static Memb MEMB(AE expr, params string[] idents) => new Memb(expr, idents);




  public static Memb MEMB(int gapBefore, AE expr, IEnumerable<AE> exprs) => new Memb(expr, exprs).SetGap(gapBefore);
  public static Memb MEMB(int gapBefore, Idd expr, params string[] exprs) => new Memb(expr, exprs.Select(s => ID(s))).SetGap(gapBefore);
  public static Memb MEMB(int gapBefore, params AE[] exprs) => new Memb(exprs).SetGap(gapBefore);
  public static Memb MEMB(int gapBefore, params string[] idents) => new Memb(idents).SetGap(gapBefore);
  public static Memb MEMB(int gapBefore, params Idd[] idents) => new Memb(idents).SetGap(gapBefore);
  public static Memb MEMB(int gapBefore, IEnumerable<string> idents) => new Memb(idents).SetGap(gapBefore);
  public static Memb MEMB(int gapBefore, IEnumerable<Idd> idents) => new Memb(idents).SetGap(gapBefore);

  #endregion





  #region KV, METHOD //////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static KVal KV(Literal key, AE val) => new KVal(key, val);
  public static KVal KV(Literal key, string val) => new KVal(key, val);
  public static KVal KV(Literal key, double val) => new KVal(key, val);
  public static KVal KV(string key, AE val) => new KVal(key, val);
  public static KVal KV(CommentItem comment, string key, AE val) => new KVal(comment, key, val);
  public static KVal KV(string key, string val) => new KVal(key, val);
  public static KVal KV(CommentItem comment, string key, string val) => new KVal(comment, key, val);
  public static KVal KV(string key, object val) => new KVal(key, val);
  public static KVal KV(string key, double val) => new KVal(key, val);
  public static KVal KV(string key, bool val) => new KVal(key, val);
  public static KVal KV(string key, params KVal[] props) => new KVal(key, props);




  public static KVal KV(int before, Literal key, AE val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, Literal key, string val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, Literal key, double val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, string key, AE val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, CommentItem comment, string key, AE val) => new KVal(comment, key, val).SetGap(before);
  public static KVal KV(int before, string key, string val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, CommentItem comment, string key, string val) => new KVal(comment, key, val).SetGap(before);
  public static KVal KV(int before, string key, object val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, string key, double val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, string key, bool val) => new KVal(key, val).SetGap(before);
  public static KVal KV(int before, string key, params KVal[] props) => new KVal(key, props).SetGap(before);




  public static Method METHOD(string key, FAnon func) => new Method(key, func);
  public static Method METHOD(string key, params AE[] exprs) => new Method(key, exprs);
  public static Method METHOD(string key, ARG argsBlock, params AE[] exprs) => new Method(key, argsBlock, exprs);


  #endregion







  #region CALL ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static CallExpr CALL() => new CallExpr();
  public static CallExpr CALL(ARG args) => new CallExpr(args);
  public static CallExpr CALL(string ident, ARG args = null) => new CallExpr(ident, args);
  public static CallExpr CALL(Idd ident, ARG args = null) => new CallExpr(ident, args);



  public static MCall Call(Memb dotted, ARG arguments) => new MCall(dotted, arguments);
  public static MCall Call(CommentItem comment, Memb dotted, ARG arguments) => new MCall(comment, dotted, arguments);
  public static MCall Call(string dotted, ARG arguments) => new MCall(dotted, arguments);
  public static MCall Call(CommentItem comment, string dotted, ARG arguments) => new MCall(comment, dotted, arguments);
  public static MCall Call(MCall other, Memb dotted, ARG arguments) => new MCall(other, dotted, arguments);
  public static MCall Call(MCall other, string dotted, ARG arguments) => new MCall(other, dotted, arguments);
  public static MCall Call(Memb dotted) => new MCall(dotted);
  public static MCall Call(string dotted) => new MCall(dotted);
  public static MCall Call(CommentItem comment, string dotted) => new MCall(comment, dotted);
  public static MCall Call(MCall other, Memb dotted) => new MCall(other, dotted);
  public static MCall Call(MCall other, string dotted) => new MCall(other, dotted);
  public static MCall Call(string dotted, bool arg) => new MCall(dotted, Args(arg ? TRUE as AE : FALSE as AE));
  public static MCall Call(Idd obj, ARG args) => new MCall(obj, args);

  #endregion





  #region Arr /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static ArrayDefinition Arr(params AT[] items) => new ArrayDefinition(items);
  public static ArrayDefinition Arr(IEnumerable<AT> items) => new ArrayDefinition(items);
  public static ArrayDefinition Arr(params OB[] items) => new ArrayDefinition(items);
  public static ArrayDefinition Arr(IEnumerable<OB> items) => new ArrayDefinition(items);
  public static ArrayDefinition Arr() => new ArrayDefinition();
  public static ArrayDefinition Arr<T>(params T[] items) where T : AT => new ArrayDefinition(items?.Cast<T>());
  public static ArrayDefinition Arr<T>(IEnumerable<T> items) where T : AT => new ArrayDefinition(items?.Cast<T>());
  public static ArrayDefinition Arr(bool sparsed, params AT[] items) => new ArrayDefinition(sparsed, items);
  public static ArrayDefinition Arr(bool sparsed, IEnumerable<AT> items) => new ArrayDefinition(sparsed, items);




  public static ArrayDefinition Arr(bool wrapLeft, bool sparsed, params AT[] items) => new ArrayDefinition(wrapLeft, sparsed, items);
  public static ArrayDefinition Arr(bool wrapLeft, bool sparsed, IEnumerable<AT> items) => new ArrayDefinition(wrapLeft, sparsed, items);
  public static ArrayDefinition Arr(bool wrapLeft, bool sparsed, params OB[] items) => new ArrayDefinition(wrapLeft, sparsed, items);
  public static ArrayDefinition Arr(bool wrapLeft, bool sparsed, IEnumerable<OB> items) => new ArrayDefinition(wrapLeft, sparsed, items);
  public static ArrayDefinition Arr(bool wrapLeft, bool sparsed) => new ArrayDefinition(wrapLeft, sparsed);
  public static ArrayDefinition Arr<T>(bool wrapLeft, bool sparsed, params T[] items) where T : AT => new ArrayDefinition(wrapLeft, sparsed, items?.Cast<T>());
  public static ArrayDefinition Arr<T>(bool wrapLeft, bool sparsed, IEnumerable<T> items) where T : AT => new ArrayDefinition(wrapLeft, sparsed, items?.Cast<T>());

  #endregion





  #region ArrIndex ////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static ArrayIndex ArrIndex(int index) => new ArrayIndex(index);
  public static ArrayIndex ArrIndex(string index) => new ArrayIndex(index);
  public static ArrayIndex ArrIndex(AT index) => new ArrayIndex(index);

  #endregion





  #region FN, FNlambda ////////////////////////////////////////////////////////////////////////////////////////////////////////

  /// <summary>
  /// Возвращает пустое определение анонимной функции, без аргументов и выражений/исполняемых инструкций.
  /// </summary>
  /// <returns></returns>
  public static FAnon FN() => new FAnon();
  /// <summary>
  /// Возвращает определение анонимной функции, не имеющей аргументов, с указанным набором выражений/исполняемых инструкций.
  /// </summary>
  /// <param name="codeBlock">Блок кода, содержащий набор выражений/исполняемых инструкций, которые необходимо выполнить при вызове функции.</param>
  /// <returns></returns>
  public static FAnon FN(FB codeBlock) => new FAnon(codeBlock);
  /// <summary>
  /// Возвращает определение анонимной функции, не имеющей аргументов, с указанным набором выражений/исполняемых инструкций.
  /// </summary>
  /// <param name="exprs">Набор выражений/исполняемых инструкций, которые необходимо выполнить при вызове функции.</param>
  /// <returns></returns>
  public static FAnon FN(params AE[] exprs) => new FAnon(false, exprs);
  public static FAnon FN(IEnumerable<AE> exprs) => new FAnon(false, exprs?.ToArray());
  public static FAnon FN(ARG args, ExpressionsSet exprSet) => new FAnon(args, false, exprSet.ChildTokens.Cast<AE>().ToArray());
  public static FAnon FN(ExpressionsSet exprSet) => new FAnon(false, exprSet?.ChildTokens?.Cast<AE>().ToArray());
  /// <summary>
  /// Возвращает определение анонимной функции, принимающей указанный набор аргументов, с пустым кодовым блоком.
  /// </summary>
  /// <param name="arguments">Набор аргументов функции.</param>
  /// <returns></returns>
  public static FAnon FN(ARG arguments) => new FAnon(arguments);
  public static FAnon FN(ARG arguments, FB codeBlock) => new FAnon(arguments, codeBlock);
  public static FAnon FN(ARG arguments, params AE[] exprs) => new FAnon(arguments, false, exprs);
  public static FAnon FN(ARG arguments, IEnumerable<AE> exprs) => new FAnon(arguments, false, exprs.ToArray());


  public static FAnon FNlambda(ARG arguments, params AE[] exprs) => new FAnon(arguments, true, exprs);
  public static FAnon FNlambda(ARG arguments, IEnumerable<AE> exprs) => new FAnon(arguments, true, exprs.ToArray());





  public static FunctionNamed FN(string name) => new FunctionNamed(name);
  public static FunctionNamed FN(string name, FB codeBlock) => new FunctionNamed(name, codeBlock);
  public static FunctionNamed FN(string name, params AE[] exprs) => new FunctionNamed(name, exprs);
  public static FunctionNamed FN(string name, ARG arguments) => new FunctionNamed(name, arguments);
  public static FunctionNamed FN(string name, ARG arguments, FB codeBlock) => new FunctionNamed(name, arguments, codeBlock);
  public static FunctionNamed FN(string name, ARG arguments, params AE[] exprs) => new FunctionNamed(name, arguments, exprs);



  public static FunctionNamed FN(int gapBefore, string name) => new FunctionNamed(name).SetGap(gapBefore);
  public static FunctionNamed FN(int gapBefore, string name, FB codeBlock) => new FunctionNamed(name, codeBlock).SetGap(gapBefore);
  public static FunctionNamed FN(int gapBefore, string name, params AE[] exprs) => new FunctionNamed(name, exprs).SetGap(gapBefore);
  public static FunctionNamed FN(int gapBefore, string name, ARG arguments) => new FunctionNamed(name, arguments).SetGap(gapBefore);
  public static FunctionNamed FN(int gapBefore, string name, ARG arguments, FB codeBlock) => new FunctionNamed(name, arguments, codeBlock).SetGap(gapBefore);
  public static FunctionNamed FN(int gapBefore, string name, ARG arguments, params AE[] exprs) => new FunctionNamed(name, arguments, exprs).SetGap(gapBefore);

  #endregion









  /// <summary>
  /// Возвращает набор выражений/исполняемых инструкций, обернутый в проверку объекта obj на значение undefined.
  /// Для проверки объекта на значение undefined используется вызов именованной функции <see cref="LogXAction.IsNotUndef"/>.
  /// </summary>
  /// <param name="obj">Абстрактное выражение-объект, для которого необходимо производить проверку на undefined.</param>
  /// <param name="exprs">Набор выражений/исполняемых инструкций, выполняемых в случае если значение obj не равно undefined.</param>
  /// <returns></returns>
  public static IfElseExpression IfDefined(AE obj, ExpressionsSet exprs) => IF(TEST(Call(Member("IsNotUndef"), Args(obj))), THEN(exprs.ChildTokens.Cast<AE>().ToArray()), null);







  #region SimpleTokens ////////////////////////////////////////////////////////////////////////////////////////////////////////

  /// <summary>
  /// Пустой токен, не имеющий содержимого/возвращаемого значения.
  /// </summary>
  public static ST NONE => Base(TT.NONE);
  /// <summary>
  /// Ключевое слово new
  /// </summary>
  public static ST New => Base(TT.New);
  /// <summary>
  /// Ключевое слово delete
  /// </summary>
  public static ST Delete => Base(TT.Delete);
  /// <summary>
  /// Ключевое слово var
  /// </summary>
  public static ST Var => Base(TT.Var);
  /// <summary>
  /// Ключевое слово let
  /// </summary>
  public static ST Let => Base(TT.Let);
  /// <summary>
  /// Ключевое слово const
  /// </summary>
  public static ST Const => Base(TT.Const);
  /// <summary>
  /// Знак равенства =
  /// </summary>
  public static ST Assign => Base(TT.Assign);
  /// <summary>
  /// Знак точки .
  /// </summary>
  public static ST Dot => Base(TT.Dot);
  /// <summary>
  /// Ключевое слово this
  /// </summary>
  public static ST This => Base(TT.This);
  /// <summary>
  /// Знак запятой ,
  /// </summary>
  public static ST Comma => Base(TT.Comma);
  /// <summary>
  /// Знак двоеточия :
  /// </summary>
  public static ST Colon => Base(TT.Colon);
  /// <summary>
  /// Знак точки с запятой ;
  /// </summary>
  public static ST Semicolon => Base(TT.Semicolon);
  /// <summary>
  /// Открывающая фигурная скобка {
  /// </summary>
  public static ST BraceOpen => Base(TT.BraceOpen);
  /// <summary>
  /// Закрывающая фигурная скобка }
  /// </summary>
  public static ST BraceClose => Base(TT.BraceClose);
  /// <summary>
  /// Открывающая круглая скобка (
  /// </summary>
  public static ST BracketOpen => Base(TT.BracketOpen);
  /// <summary>
  /// Закрывающая круглая скобка )
  /// </summary>
  public static ST BracketClose => Base(TT.BracketClose);
  /// <summary>
  /// Открывающая квадратная скобка [
  /// </summary>
  public static ST SqbracketOpen => Base(TT.SqbracketOpen);
  /// <summary>
  /// Закрывающая квадратная скобка ]
  /// </summary>
  public static ST SqbracketClose => Base(TT.SqbracketClose);
  /// <summary>
  /// Ключевое слово function
  /// </summary>
  public static ST Function => Base(TT.Function);
  /// <summary>
  /// Тернарный оператор ?
  /// </summary>
  public static ST TernaryIf => Base(TT.TernaryIf);
  /// <summary>
  /// Ключевое слово if
  /// </summary>
  public static ST If => Base(TT.If);
  /// <summary>
  /// Ключевое слово else
  /// </summary>
  public static ST Else => Base(TT.Else);
  /// <summary>
  /// Ключевое слово return
  /// </summary>
  public static ST Return => Base(TT.Return);
  /// <summary>
  /// Ключевое слово for
  /// </summary>
  public static ST For => Base(TT.For);

  public static ST Switch => Base(TT.Switch);
  public static ST Case => Base(TT.Case);
  public static ST Default => Base(TT.Default);
  public static ST Break => Base(TT.Break);
  public static ST Continue => Base(TT.Continue);
  public static ST Try => Base(TT.Try);
  public static ST Catch => Base(TT.Catch);
  public static ST Finally => Base(TT.Finally);
  public static ST InstanceOf => Base(TT.InstanceOf);
  public static ST TypeOf => Base(TT.Typeof);
  public static ST Do => Base(TT.Do);
  public static ST While => Base(TT.While);



  public static ST Class => Base(TT.Class);
  public static ST Constructor => Base(TT.Constructor);
  public static ST Get => Base(TT.Get);
  public static ST Set => Base(TT.Set);
  public static ST Static => Base(TT.Static);
  public static ST Extends => Base(TT.Extends);
  public static ST Super => Base(TT.Super);
  public static ST Export => Base(TT.Export);
  public static ST Import => Base(TT.Import);





  /// <summary>
  /// Ключевое слово <see langword="null"/>
  /// </summary>
  public static ST Null => Base(TT.Null);
  /// <summary>
  /// Ключевое слово undefined
  /// </summary>
  public static ST Undefined => Base(TT.Undefined);
  public static ST NaN => Base(TT.NaN);
  /// <summary>
  /// Ключевое слово <see langword="true"/>
  /// </summary>
  public static ST True => Base(TT.True);
  /// <summary>
  /// Ключевое слово <see langword="false"/>
  /// </summary>
  public static ST False => Base(TT.False);
  /// <summary>
  /// Знак !
  /// </summary>
  public static ST Not => Base(TT.Not);
  /// <summary>
  /// Знак &
  /// </summary>
  public static ST And => Base(TT.And);
  /// <summary>
  /// Знак |
  /// </summary>
  public static ST Or => Base(TT.Or);
  /// <summary>
  /// Знак >
  /// </summary>
  public static ST MoreThan => Base(TT.MoreThan);
  /// <summary>
  /// Знак <
  /// </summary>
  public static ST LessThan => Base(TT.LessThan);
  /// <summary>
  /// Пробел
  /// </summary>
  public static ST Space => Base(TT.Space);
  /// <summary>
  /// Перевод строки
  /// </summary>
  public static ST CRLF => Base(TT.CRLF);
  /// <summary>
  /// Оператор +
  /// </summary>
  public static ST PLUS => Base(TT.Plus);
  /// <summary>
  /// Оператор -
  /// </summary>
  public static ST MINUS => Base(TT.Minus);

  #endregion










  public static AE NEWLINE => Str(Environment.NewLine);
  public static AE NEWLINES(int amount)
  {
    var ret = string.Join("", Enumerable.Repeat(Environment.NewLine, amount));
    return Str(ret);
  }







  #region ComparisonOperators /////////////////////////////////////////////////////////////////////////////////////////////////

  /// <summary>
  /// Оператор сравнения СТРОГО РАВНО ===
  /// </summary>
  public static ComparisonOperator IsEq => new ComparisonOperator(TT.IsEqual);
  /// <summary>
  /// Оператор сравнения СТРОГО НЕ РАВНО !==
  /// </summary>
  public static ComparisonOperator IsNe => new ComparisonOperator(TT.IsNotEqual);
  /// <summary>
  /// Оператор сравнения БОЛЬШЕ >
  /// </summary>
  public static ComparisonOperator IsGt => new ComparisonOperator(TT.IsGreater);
  /// <summary>
  /// Оператор сравнения БОЛЬШЕ, ИЛИ РАВНО >=
  /// </summary>
  public static ComparisonOperator IsGtEq => new ComparisonOperator(TT.IsGreaterOrEqual);
  /// <summary>
  /// Оператор сравнения МЕНЬШЕ <
  /// </summary>
  public static ComparisonOperator IsLt => new ComparisonOperator(TT.IsLesser);
  /// <summary>
  /// Оператор сравнения МЕНЬШЕ ИЛИ РАВНО <=
  /// </summary>
  public static ComparisonOperator IsLtEq => new ComparisonOperator(TT.IsLesserOrEqual);
  /// <summary>
  /// Оператор логического И &&
  /// </summary>
  public static ComparisonOperator AND => new ComparisonOperator(TT.BooleanAND);
  /// <summary>
  /// Оператор логического ИЛИ ||
  /// </summary>
  public static ComparisonOperator OR => new ComparisonOperator(TT.BooleanOR);
  /// <summary>
  /// Оператор логического НЕ !
  /// </summary>
  public static ComparisonOperator NOT => new ComparisonOperator(TT.BooleanNOT);
  /// <summary>
  /// Оператор instanceof
  /// </summary>
  public static ComparisonOperator InstOf => new ComparisonOperator(TT.InstanceOf);
  /// <summary>
  /// Оператор приращения на единицу (++)
  /// </summary>
  public static UpdateOperator Incr => new UpdateOperator(TT.IncreaseOper);
  /// <summary>
  /// Оператор уменьшения на единицу (--)
  /// </summary>
  public static UpdateOperator Decr => new UpdateOperator(TT.DecreaseOper);
  /// <summary>
  /// Лямбда-оператор =>
  /// </summary>
  public static LambdaOperator Lambda => new LambdaOperator();

  #endregion






  #region CommentItems ////////////////////////////////////////////////////////////////////////////////////////////////////////

  /// <summary>
  /// Первая строка (начало) многострочного комментария - /**
  /// </summary>
  public static CommentItem CommentStart => new CommentItem(TT.CommentStart);
  /// <summary>
  /// Последняя строка (конец) многострочного комментария - */
  /// </summary>
  public static CommentItem CommentEnd => new CommentItem(TT.CommentEnd);
  /// <summary>
  /// Строка многострочного комментария с текстовым содержимым - *...
  /// </summary>
  /// <param name="comment">Текстовое содержимое строки комментария</param>
  /// <returns></returns>
  public static CommentItem CommentLine(string comment) => new CommentItem(TT.CommentLine, comment);
  public static CommentItem CommentLine(bool inline, string comment) => new CommentItem(inline, TT.CommentLine, comment);
  /// <summary>
  /// Одиночный (однострочный) комментарий - //
  /// </summary>
  /// <param name="comment">Текстовое содержимое строки комментария</param>
  /// <returns></returns>
  public static CommentItem CommentSingle(string comment) => new CommentItem(TT.CommentSingle, comment);
  /// <summary>
  /// Блок комментариев (многострочный комментарий), начинающийся с CommentStart и оканчивающийся на CommentEnd,
  /// в который можно добавить необходимое количество строк с комментариями
  /// </summary>
  /// <returns></returns>
  public static CommentBlock CommentBlock() => new CommentBlock();
  /// <summary>
  /// Блок комментариев (многострочный комментарий), начинающийся с CommentStart и оканчивающийся на CommentEnd,
  /// в который можно добавить необходимое количество строк с комментариями.
  /// Блок комментариев после создания будет содержать указанную строку.
  /// </summary>
  /// <param name="comment">Текстовое содержимое строки комментария</param>
  /// <returns></returns>
  public static CommentBlock CommentBlock(string comment) => new CommentBlock(comment);
  /// <summary>
  /// Блок комментариев (многострочный комментарий), начинающийся с CommentStart и оканчивающийся на CommentEnd,
  /// в который можно добавить необходимое количество строк с комментариями.
  /// Блок комментариев после создания будет содержать указанные строки.
  /// </summary>
  /// <param name="comments">Массив строк, текстовое содержимое которых будет добавлено в блок комментариев после его создания</param>
  /// <returns></returns>
  public static CommentBlock CommentBlock(params string[] comments) => new CommentBlock(comments);

  #endregion








  #region GLOBAL //////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public static GlobalObject GLOBAL(params AE[] exprs) => new GlobalObject(exprs);
  public static GlobalObject GLOBAL(IEnumerable<AE> exprs) => new GlobalObject(exprs.ToArray());
  public static GlobalObject GLOBAL(string name, IEnumerable<AE> exprs) => new GlobalObject(name, exprs.ToArray());
  public static GlobalObject GLOBAL(CommentBlock cblock, params AE[] exprs) => new GlobalObject(cblock, exprs);
  public static GlobalObject GLOBAL(CommentBlock cblock, string name, IEnumerable<AE> exprs) => new GlobalObject(cblock, name, exprs.ToArray());

  #endregion







  public static ExtJSClassExpression ExtDefine(string name, params KVal[] props) => new ExtJSClassExpression(name, false, props);
  public static ExtJSClassExpression ExtDefine(string name, OB config) => new ExtJSClassExpression(name, config, false);
  public static ExtJSClassExpression ExtDefine(Memb asmember) => new ExtJSClassExpression(asmember, false);












  public static SL HTMLAttr(HtmlAttr attr, string val) => Str(attr.ToString().DecapitalizeFirstChar() + "=\"" + val + "\"");
  public static SL HTMLTag(HtmlTag tag, bool selfClosing = false, params SL[] attrs)
  {
    var ret = new StringBuilder("<");
    ret.Append(tag.ToString().DecapitalizeFirstChar());
    if (attrs.Count() > 0)
      foreach (var at in attrs)
      {
        ret.Append(" ");
        ret.Append(at.Formatted.Trim('\''));
      };
    if (selfClosing)
      ret.Append("/>");
    else
    {
      ret.Append("></");
      ret.Append(tag.ToString().DecapitalizeFirstChar());
      ret.Append(">");
    }
    return Str(ret.ToString());
  }


}
