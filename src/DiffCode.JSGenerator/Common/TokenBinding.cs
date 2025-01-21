using TG = DiffCode.JSGenerator.Enums.TokenGroup;





namespace DiffCode.JSGenerator.Common
{
  /// <summary>
  /// Привязка токена к группе, значению и регулярному выражению.
  /// </summary>
  public class TokenBinding
	{
		public TokenBinding(TT type, TG grp, string val, string regexp)
		{
			Type = type;
			Group = grp;
			Value = val;
			Regexp = regexp;
		}







		/// <summary>
		/// Тип токена.
		/// </summary>
		public TT Type { get; set; }

		/// <summary>
		/// Группа токенов.
		/// </summary>
		public TG Group { get; set; }

		/// <summary>
		/// Текстовое значение токена.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Регулярное выражение для парсинга токена.
		/// </summary>
		public string Regexp { get; set; }





		/// <summary>
		/// Список всех имеющихся привязок.
		/// </summary>
		public static List<TB> List { get; set; } = new List<TB>
		{
			new TB(TT.NONE, TG.NONE, "", ""),
 			new TB(TT.Break, TG.Keywords, "break", "break"),
 			new TB(TT.Case, TG.Keywords, "case", "case"),
 			new TB(TT.Catch, TG.Keywords, "catch", "catch"),
 			new TB(TT.Const, TG.Keywords, "const", "const"),
 			new TB(TT.Continue, TG.Keywords, "continue", "continue"),
 			new TB(TT.Default, TG.Keywords, "default", "default"),
 			new TB(TT.Delete, TG.Keywords, "delete", "delete"),
 			new TB(TT.Do, TG.Keywords, "do", "do"),
 			new TB(TT.Else, TG.Keywords, "else", "else"),
 			new TB(TT.False, TG.Keywords, "false", "false"),
 			new TB(TT.Finally, TG.Keywords, "finally", "finally"),
 			new TB(TT.For, TG.Keywords, "for", "for"),
 			new TB(TT.Function, TG.Keywords, "function", "function"),
 			new TB(TT.If, TG.Keywords, "if", "if"),
 			new TB(TT.In, TG.Keywords, "in", "in"),
 			new TB(TT.Infinity, TG.Keywords, "Infinity", "Infinity"),
 			new TB(TT.InstanceOf, TG.Keywords, "instanceof", "instanceof"),
 			new TB(TT.Let, TG.Keywords, "let", "let"),
 			new TB(TT.NaN, TG.Keywords, "NaN", "NaN"),
 			new TB(TT.New, TG.Keywords, "new", "new"),
 			new TB(TT.Null, TG.Keywords, "null", "null"),
 			new TB(TT.Return, TG.Keywords, "return", "return"),
 			new TB(TT.Switch, TG.Keywords, "switch", "switch"),
 			new TB(TT.This, TG.Keywords, "this", "this"),
 			new TB(TT.Throw, TG.Keywords, "throw", "throw"),
 			new TB(TT.True, TG.Keywords, "true", "true"),
 			new TB(TT.Try, TG.Keywords, "try", "try"),
 			new TB(TT.Typeof, TG.Keywords, "typeof", "typeof"),
 			new TB(TT.Undefined, TG.Keywords, "undefined", "undefined"),
 			new TB(TT.Var, TG.Keywords, "var", "var"),
 			new TB(TT.While, TG.Keywords, "while", "while"),
 			new TB(TT.With, TG.Keywords, "with", "with"),
 			new TB(TT.Minus, TG.MathOpers, "-", "\\-"),
 			new TB(TT.Mod, TG.MathOpers, "%", "%"),
 			new TB(TT.Mult, TG.MathOpers, "*", "\\*"),
 			new TB(TT.Plus, TG.MathOpers, "+", "\\+"),
 			new TB(TT.And, TG.BooleanOpers, "&", "&"),
 			new TB(TT.Assign, TG.BooleanOpers, "=", "\\="),
 			new TB(TT.LessThan, TG.BooleanOpers, "<", "\\<"),
 			new TB(TT.MoreThan, TG.BooleanOpers, ">", "\\>"),
 			new TB(TT.Not, TG.BooleanOpers, "!", "!"),
 			new TB(TT.Or, TG.BooleanOpers, "|", "\\|"),
 			new TB(TT.TernaryIf, TG.BooleanOpers, "?", "\\?"),
 			new TB(TT.LambdaOper, TG.Other, "=>", "=>"),
 			new TB(TT.BraceOpen, TG.BracesBrackets, "{", "{"),
 			new TB(TT.BraceClose, TG.BracesBrackets, "}", "}"),
 			new TB(TT.BracketOpen, TG.BracesBrackets, "(", "\\("),
 			new TB(TT.BracketClose, TG.BracesBrackets, ")", "\\)"),
 			new TB(TT.SqbracketOpen, TG.BracesBrackets, "[", "\\["),
 			new TB(TT.SqbracketClose, TG.BracesBrackets, "]", "\\]"),
 			new TB(TT.Colon, TG.Separators, ":", "\\:"),
 			new TB(TT.Comma, TG.Separators, ",", ","),
 			new TB(TT.Dot, TG.Separators, ".", "\\."),
 			new TB(TT.Semicolon, TG.Separators, ";", ";"),
 			new TB(TT.Apos, TG.Quotes, "'", "'"),
 			new TB(TT.Quote, TG.Quotes, "\"", "\""),
 			new TB(TT.CRLF, TG.Spaces, "\n", "\r\n"),
 			new TB(TT.Space, TG.Spaces, " ", "\\s+"),
 			new TB(TT.Identifier, TG.Literals, "", "[A-Za-z$_]+[0-9]?"),
 			new TB(TT.Number, TG.Literals, "", "[0-9]+(?:\\.[0-9]*)?"),
 			new TB(TT.String, TG.Literals, "", "['\"](?:.+?)?(?:\\w+)*?['\"]"),
 			new TB(TT.ThisExpression, TG.Literals, "this", "this"),
 			new TB(TT.Arguments, TG.Fragments, "", ""),
 			new TB(TT.ArrayDefinition, TG.Fragments, "", ""),
 			new TB(TT.ArrayIndexDef, TG.Fragments, "", ""),
 			new TB(TT.BooleanAND, TG.Fragments, "", ""),
 			new TB(TT.BooleanOR, TG.Fragments, "", ""),
 			new TB(TT.BooleanNOT, TG.Fragments, "", ""),
 			new TB(TT.BreakExpression, TG.Fragments, "", ""),
 			new TB(TT.CaseExpression, TG.Fragments, "", ""),
 			new TB(TT.ClassCreation, TG.Fragments, "", ""),
 			new TB(TT.ClassDefinition, TG.Fragments, "", ""),
 			new TB(TT.CodeBlock, TG.Fragments, "", ""),
 			new TB(TT.Comparison, TG.Fragments, "", ""),
 			new TB(TT.DecreaseOper, TG.Fragments, "", ""),
 			new TB(TT.ExpressionEval, TG.Fragments, "", ""),
 			new TB(TT.FnBodyBlock, TG.Fragments, "", ""),
 			new TB(TT.ForExpression, TG.Fragments, "", ""),
 			new TB(TT.FuncAnonymous, TG.Fragments, "", ""),
 			new TB(TT.FuncDefinition, TG.Fragments, "", ""),
 			new TB(TT.IfElseStatement, TG.Fragments, "", ""),
 			new TB(TT.IIFExpression, TG.Fragments, "", ""),
 			new TB(TT.IncreaseOper, TG.Fragments, "", ""),
 			new TB(TT.IsEqual, TG.Fragments, "", ""),
 			new TB(TT.IsGreater, TG.Fragments, "", ""),
 			new TB(TT.IsGreaterOrEqual, TG.Fragments, "", ""),
 			new TB(TT.IsLesser, TG.Fragments, "", ""),
 			new TB(TT.IsLesserOrEqual, TG.Fragments, "", ""),
 			new TB(TT.IsNotEqual, TG.Fragments, "", ""),
 			new TB(TT.KeyValueAssign, TG.Fragments, "", ""),
 			new TB(TT.Member, TG.Fragments, "", ""),
 			new TB(TT.MethodCall, TG.Fragments, "", ""),
 			new TB(TT.ObjectBodyBlock, TG.Fragments, "", ""),
 			new TB(TT.ReturnExpression, TG.Fragments, "", ""),
 			new TB(TT.SetOfExpressions, TG.Fragments, "", ""),
 			new TB(TT.SwitchExpression, TG.Fragments, "", ""),
 			new TB(TT.PostDecrementExpr, TG.Fragments, "", ""),
			new TB(TT.PostIncrementExpr, TG.Fragments, "", ""),
			new TB(TT.PreDecrementExpr, TG.Fragments, "", ""),
			new TB(TT.PreIncrementExpr, TG.Fragments, "", ""),
 			new TB(TT.VarAssign, TG.Fragments, "", ""),
 			new TB(TT.VarDefinition, TG.Fragments, "", ""),
 			new TB(TT.WhileExpression, TG.Fragments, "", ""),
 			new TB(TT.GLOBAL, TG.GLOBAL, "", ""),
 			new TB(TT.MODULE, TG.GLOBAL, "", ""),
 			new TB(TT.SetOfClasses, TG.GLOBAL, "", ""),
 			new TB(TT.CommentStart, TG.CommentItems, "/**\r\n", ""),
 			new TB(TT.CommentLine, TG.CommentItems, " * ", ""),
 			new TB(TT.CommentEnd, TG.CommentItems, " */\r\n", ""),
 			new TB(TT.CommentSingle, TG.CommentItems, "// ", ""),
 			new TB(TT.CommentBlock, TG.CommentBlock, "", ""),
 		};


		/// <summary>
		/// Представление списка привязок в виде словаря.
		/// </summary>
		public static Dictionary<TT, TB> Dict => List.ToDictionary(k => k.Type, v => v);
		
    /// <summary>
		/// Представление списка привязок в виде словаря, сгруппированного по группам токенов.
		/// </summary>
		public static Dictionary<TG, List<TT>> Groups => Dict.Values.GroupBy(d => d.Group).ToDictionary(k => k.Key, v => v.Select(ss => ss.Type).ToList());

		/// <summary>
		/// Список зарегистрированных ключевых слов.
		/// </summary>
		public static IEnumerable<string> Keywords => Dict.Where(d => d.Value.Group == TG.Keywords).Select(s => s.Value.Value);


		/// <summary>
		/// Возвращает <see langword="true"/>, если указанная строка является ключевым словом (keyword).
		/// </summary>
		/// <param name="str">Строка, проверяемая на совпадение с зарегистрированными ключевыми словами.</param>
		/// <returns></returns>
		public static bool IsKeyword(string str) => (Dict.Where(w => w.Value.Group == TG.Keywords && w.Value.Value == str).Count() > 0);


		public static SimpleToken TryConvertToKeyword(string str)
		{
			switch (str)
			{
				case "true": return True;
				case "false": return False;
				case "null": return Null;
				case "this": return This;
				case "undefined": return Undefined;

				default: return IsKeyword(str) ? new SimpleToken(ValueToType(str)) : null;
			}
		}


		public static TT ValueToType(string str)
		{
			return (IsKeyword(str) && (Dict.Where(w => w.Value.Group == TG.Keywords && w.Value.Value == str).FirstOrDefault().Value != null)) ?
				Dict.Where(w => w.Value.Group == TG.Keywords && w.Value.Value == str).FirstOrDefault().Value.Type
				:
				TT.Identifier;
		}

	}
}
