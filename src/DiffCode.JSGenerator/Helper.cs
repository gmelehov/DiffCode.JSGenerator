using ARG = DiffCode.JSGenerator.ArgumentsBlock;
using FAnon = DiffCode.JSGenerator.FunctionAnonymous;
using FB = DiffCode.JSGenerator.FnBodyBlock;
using Idd = DiffCode.JSGenerator.Identifier;
using IsATexpr = System.Linq.Expressions.Expression<System.Func<DiffCode.JSGenerator.AbstractToken, bool>>;
using IsATfunc = System.Func<DiffCode.JSGenerator.AbstractToken, bool>;
using OB = DiffCode.JSGenerator.ObjectBody;
using SL = DiffCode.JSGenerator.StringLiteral;
using VarDecl = DiffCode.JSGenerator.VarDeclaration;
using System.Linq.Expressions;





namespace DiffCode.JSGenerator
{


  public delegate bool Is(AT tok);
	public delegate bool IsOf(AT tok, TT type);
	public delegate bool IsIn(AT tok, params TT[] types);
	public delegate bool Has(AT tok, IsOf func);
	public delegate bool PatternTypesFor(params TT[] types);






	public static partial class Helper
	{



		public static IEnumerable<string> Keywords => new List<string>
		{
			"break",
			"case",
			"catch",
			"const",
			"continue",
			"default",
			"delete",
			"do",
			"else",
			"false",
			"finally",
			"for",
			"function",
			"if",
			"in",
			"Infinity",
			"instanceof",
			"let",
			"NaN",
			"new",
			"null",
			"return",
			"switch",
			"this",
			"throw",
			"true",
			"try",
			"typeof",
			"undefined",
			"var",
			"while",
			"with",
		};



		public static Func<bool, SimpleToken> BoolToKeyword => val => val ? True : False;
		public static Func<string, bool> IsKeyword => str => Keywords.Contains(str);
		public static Func<object, AT> TryToKeyword => obj =>
		{
			var val_ = obj.ToString();
			switch (val_)
			{
				case "this": return This;
				case "null": return Null;
				case "undefined": return Undefined;
				case "true": return True;
				case "false": return False;
				case "[]": return Arr();
				case "{}": return OBJ();

				default:
					var result = double.TryParse(val_, out double dbl);
					if (result) return Num(dbl);
					break;
			};
			return Str(val_);
		};











		public static Is IsNull => (tok) => tok == null;
		public static IsOf IsOfType => (tok, type) => IsNotNull(tok) && tok.GetTokenType() == type;
		public static bool IsInTypes(AT tok, params TT[] types) => IsNotNull(tok) && types.Any(a => IsOfType(tok, a));




		/// <summary>
		/// Проверка существования у объекта tok дочернего элемента, удовлетворяющего условиям, заданным в предикате fn
		/// </summary>
		public static Func<AT, IsATfunc, bool> HasItem = (tok, fn) => IsNotNull(tok) && tok.ChildTokens?.Count(fn) > 0;
		/// <summary>
		/// Проверка существования у объекта tok первого по порядку дочернего элемента, удовлетворяющего условиям, заданным в предикате fn
		/// </summary>
		public static Func<AT, IsATfunc, bool> HasFirstItem = (tok, fn) => IsNotNull(tok) && IsNotNull(tok.ChildTokens?.FirstOrDefault(fn)) && tok.ChildTokens.FirstOrDefault(fn).IsFirst();
		/// <summary>
		/// Проверка существования у объекта tok последнего по порядку дочернего элемента, удовлетворяющего условиям, заданным в предикате fn
		/// </summary>
		public static Func<AT, IsATfunc, bool> HasLastItem = (tok, fn) => IsNotNull(tok) && IsNotNull(tok.ChildTokens?.LastOrDefault(fn)) && tok.ChildTokens.LastOrDefault(fn).IsLast();
		public static Func<AT, IEnumerable<AT>, IsATfunc, bool> HasItems => (tok, tokens, fn) => IsNotNull(tok) && tok.ChildTokens.Where(fn).SequenceEqual(tokens.Where(fn));



		public static Func<AT, IsATfunc, bool> HasSuchParent => (tok, fn) => IsNotNull(tok) && tok.HasParent() && fn(tok.Parent);
    public static Func<AT, IsATfunc, bool> HasSuchParents => (tok, fn) => HasSuchParent(tok, fn) || HasSuchParent(tok.Parent, fn) || HasSuchParent(tok.Parent?.Parent, fn);






    /// <summary>
    /// Проверка на null
    /// </summary>
    public static IsATfunc IsNotNull = tok => tok != null;
		/// <summary>
		/// Проверка принадлежности токена к группе Fragments
		/// </summary>
		//public static IsATfunc IsFragment => tok => IsNotNull(tok) &&  tok.Group == TG.Fragments;

		public static IsATfunc IsGlobal => tok => IsNotNull(tok) && tok is GlobalObject /*tok.Group == TG.GLOBAL*/;

		public static IsATfunc IsCodeBlock = tok => IsOfType(tok, TT.CodeBlock);
		public static IsATfunc IsFunctionBody = tok => IsOfType(tok, TT.FnBodyBlock) && tok is FB;
		public static IsATfunc IsObjectBody = tok => IsOfType(tok, TT.ObjectBodyBlock) && tok is OB;
		public static IsATfunc IsArrayDef = tok => IsOfType(tok, TT.ArrayDefinition) && tok is ArrayDefinition;
		public static IsATfunc IsArrayIndex = tok => IsOfType(tok, TT.ArrayIndexDef) && tok is ArrayIndex;
		public static IsATfunc IsFuncDef = tok => IsInTypes(tok, TT.FuncAnonymous, TT.FuncDefinition);
		public static IsATfunc IsMethodCall = tok => IsOfType(tok, TT.MethodCall);
		public static IsATfunc IsMember = tok => IsOfType(tok, TT.Member);
		public static IsATfunc IsKeyValue = tok => IsOfType(tok, TT.KeyValueAssign) && tok is KVal;


    /// <summary>
    /// Проверка принадлежности токена к типу KeyValue, значение (value) которого является анонимной функцией.
    /// key: function() {...}
    /// </summary>
    public static IsATfunc IsKeyFuncValue => tok => IsOfType(tok, TT.KeyValueAssign) && tok is KVal && IsFuncDef((tok as KVal).Val);


    public static IsATfunc IsObjectDef = tok => IsCodeBlock(tok) && tok is OB;
		public static IsATfunc IsClassInstance = tok => IsOfType(tok, TT.ClassCreation);
		public static IsATfunc IsExpression = tok => IsNotNull(tok) && tok.HasParent() && (IsCodeBlock(tok.Parent) || IsFunctionBody(tok.Parent)) && tok is AE;
		public static IsATfunc IsArgumentsBlock = tok => IsOfType(tok, TT.Arguments) && tok is ARG;

		public static IsATfunc IsIdentifier = tok => IsOfType(tok, TT.Identifier) && tok is Idd;
		public static IsATfunc IsString = tok => IsOfType(tok, TT.String) && tok is SL;
		public static IsATfunc IsNumber = tok => IsOfType(tok, TT.Number) && tok is NumberLiteral;
		public static IsATfunc IsLiteral = tok => (IsOfType(tok, TT.String) || IsOfType(tok, TT.Number)) && (tok is SL || tok is NumberLiteral);
    public static IsATfunc IsTHISExpression = tok => IsOfType(tok, TT.ThisExpression) && tok is ThisExpression;

		public static IsATfunc IsAssignOperator = tok => IsOfType(tok, TT.Assign);
		public static IsATfunc IsComparisonOperator = tok => tok is ComparisonOperator;

		public static IsATfunc IsIfExpression = tok => IsOfType(tok, TT.IfElseStatement) && tok is IfElseExpression;
		public static IsATfunc IsIf = tok => IsOfType(tok, TT.If);
		public static IsATfunc IsElse = tok => IsOfType(tok, TT.Else);
		public static IsATfunc IsSwitchExpression = tok => IsOfType(tok, TT.SwitchExpression) && tok is SwitchExpr;
    public static IsATfunc IsCaseExpression = tok => IsOfType(tok, TT.CaseExpression) && tok is CaseExpression;
    public static IsATfunc IsReturnExpression = tok => IsOfType(tok, TT.ReturnExpression) && tok is ReturnExpression;


    public static IsATfunc IsVarDefinition = tok => tok is VarDecl;
		public static IsATfunc IsFuncDefinition = tok => IsOfType(tok, TT.FuncDefinition) && tok is FunctionNamed;


		public static IsATfunc IsCommentLine = tok => IsNotNull(tok) && IsOfType(tok, TT.CommentLine) && tok is CommentItem;
		public static IsATfunc IsCommentSingle = tok => IsNotNull(tok) && IsOfType(tok, TT.CommentSingle) && tok is CommentItem;
		public static IsATfunc IsCommentStart = tok => IsNotNull(tok) && IsOfType(tok, TT.CommentStart) && tok is CommentItem;
		public static IsATfunc IsCommentEnd = tok => IsNotNull(tok) && IsOfType(tok, TT.CommentEnd) && tok is CommentItem;
		public static IsATfunc IsCommentBlock = tok => IsNotNull(tok) && IsOfType(tok, TT.CommentBlock) && tok is CommentBlock && HasFirstItem(tok, IsCommentStart) && HasLastItem(tok, IsCommentEnd);






		public static IsATexpr is_NotNull => tok => tok != null;
		public static IsATexpr is_FunctionBody => tok => IsOfType(tok, TT.FnBodyBlock) && tok is FB;
		public static IsATexpr is_ObjBody => tok => IsOfType(tok, TT.ObjectBodyBlock) && tok is OB;
		public static IsATexpr is_KeyValue => tok => tok is KVal;
		public static IsATexpr is_Identifier => tok => IsOfType(tok, TT.Identifier) && tok is Idd;
		public static IsATexpr is_String => tok => IsOfType(tok, TT.String) && tok is SL;
		public static IsATexpr is_Number => tok => IsOfType(tok, TT.Number) && tok is NumberLiteral;
		public static IsATexpr is_ArgsBlock => tok => tok is ARG;
		public static IsATexpr is_Member => tok => IsOfType(tok, TT.Member);
		public static IsATexpr is_VarDefinition => tok => tok is VarDecl;
		public static IsATexpr is_FuncAnonymous => tok => tok is FAnon;


		public static IsATexpr is_ofType_Var => tok => IsOfType(tok, TT.Var);
		public static IsATexpr is_ofType_Let => tok => IsOfType(tok, TT.Let);
		public static IsATexpr is_ofType_Const => tok => IsOfType(tok, TT.Const);
		public static IsATexpr is_ComparisonOper => tok => tok is ComparisonOperator;








		/// <summary>
		/// Добавляет токен add в список дочерних элементов токена tok, 
		/// непосредственно перед первым по порядку дочерним элементом, удовлетворяющим условиям, заданным в предикате test
		/// </summary>
		public static Action<AT, AT, IsATfunc> AddToItemsBefore => (tok, add, test) =>
		{
			var target = tok.ChildTokens.FirstOrDefault(test);
			var itemIndex = (IsNotNull(target)) ? tok.ChildTokens.ToList().IndexOf(target) : -1;
			if (itemIndex > -1)
				tok.ChildTokens.ToList().Insert(itemIndex, add);
		};
		/// <summary>
		/// Добавляет токен add в список дочерних элементов токена tok, 
		/// непосредственно после последнего по порядку дочернего элемента, удовлетворяющего условиям, заданным в предикате test
		/// </summary>
		public static Action<AT, AT, IsATfunc> AddToItemsAfter => (tok, add, test) =>
		{
			var target = tok.ChildTokens.LastOrDefault(test);
			var itemIndex = (IsNotNull(target)) ? tok.ChildTokens.ToList().IndexOf(target) : -1;
			if (itemIndex > -1)
				tok.ChildTokens.ToList().Insert(itemIndex, add);
		};




		public static KVal ToKeyVal<K, V>(this KeyValuePair<K, V> kvpair) where K : Literal where V : AE => KV(kvpair.Key, kvpair.Value);
		public static Expression<Func<Dictionary<string, object>, OB>> DictToObject => dict => OBJ(dict.Select(d => KV(d.Key, d.Value)).ToArray());


	}
}
