namespace DiffCode.JSGenerator
{
  /// <summary>
  /// Методы-парсеры Xml => JS
  /// </summary>
  public static partial class XmlParse
  {

    public static class Settings
    {

      public static bool PreferLambdaFunctions => false;

      public static int LinesBetweenExtDefines => 4;

      public static int LinesBeforeFunctions => 2;

      public static bool ProcessComments => false;






    }












    /// <summary>
    /// Словарь сопоставлений имен XML-элементов и их методов-парсеров.
    /// </summary>
    public static Dictionary<string, Func<XElement, AE>> sourceDict => new Dictionary<string, Func<XElement, AE>>
    {
      ["assign"] = parseBINARY,
      ["bitOr"] = parseBINARY,
      ["bitXor"] = parseBINARY,
      ["bitAnd"] = parseBINARY,
      ["equal"] = parseBINARY,
      ["strictEqual"] = parseBINARY,
      ["notEqual"] = parseBINARY,
      ["strictNotEqual"] = parseBINARY,
      ["or"] = parseBINARY,
      ["and"] = parseBINARY,
      ["gt"] = parseBINARY,
      ["lt"] = parseBINARY,
      ["gtEq"] = parseBINARY,
      ["ltEq"] = parseBINARY,
      ["instOf"] = parseBINARY,
      ["multiply"] = parseBINARY,
      ["divide"] = parseBINARY,
      ["plus"] = parseBINARY,
      ["minus"] = parseBINARY,
      ["mod"] = parseBINARY,
      ["plusEq"] = parseBINARY,
      ["minusEq"] = parseBINARY,
      ["orEq"] = parseBINARY,
      ["andEq"] = parseBINARY,
      ["multiplyEq"] = parseBINARY,
      ["divideEq"] = parseBINARY,
      ["in"] = parseBINARY,

      ["neg"] = parseUNARY,
      ["pos"] = parseUNARY,
      ["not"] = parseUNARY,
      ["dblNot"] = parseUNARY,
      ["bitNot"] = parseUNARY,
      ["spread"] = parseUNARY,
      ["typeOf"] = parseUNARY,
      ["dblPlus"] = parseUNARY,
      ["dblMinus"] = parseUNARY,
      ["delete"] = parseUNARY,
      ["void"] = parseUNARY,


      ["arg"] = parseARG,
      ["args"] = parseARGS,
      ["arr"] = parseARR,
      ["break"] = parseBREAK,
      ["call"] = parseCALL,
      ["case"] = parseCASE,
      ["comments"] = parseCOMMENT,
      ["const"] = parseCONST,
      ["continue"] = parseCONTINUE,
      ["default"] = parseDEFAULT,
      ["do-while"] = parseDO_WHILE,
      ["empty"] = parseEMPTY,
      ["false"] = parseFALSE,
      ["filter"] = parseFILTER,
      ["fn"] = parseFN,
      ["for"] = parseFOR,
      ["for-in"] = parseFOR_IN,
      ["forEach"] = parseFOREACH,
      ["global"] = parseGLOBAL,
      ["html-fragment"] = parseHTML,
      ["id"] = parseID,
      ["if"] = parseIF,
      ["iife"] = parseIIFE,
      ["index"] = parseINDEX,
      ["info"] = parseCOMMENT,
      ["kv"] = parseKV,
      ["lambda"] = parseLAMBDA,
      ["let"] = parseLET,
      ["map"] = parseMAP,
      ["member"] = parseMEMBER,
      ["new"] = parseNEW,
      ["newline"] = parseNEWLINE,
      ["ns"] = parseNS,
      ["null"] = parseNULL,
      ["num"] = parseNUM,
      ["obj"] = parseOBJ,
      ["prm"] = parsePRM,
      ["regexp"] = parseREGEXP,
      ["region"] = parseREGION,
      ["ret"] = parseRET,
      ["str"] = parseSTR,
      ["str-template"] = parseSTR_TEMPLATE,
      ["switch"] = parseSWITCH,
      ["template"] = parseTEMPLATE,
      ["templates"] = parseTEMPLATES,
      ["this"] = parseTHIS,
      ["tif"] = parseTIF,
      ["true"] = parseTRUE,
      ["try-catch"] = parseTRY_CATCH,
      ["undef"] = parseUNDEF,
      ["var"] = parseVAR,
      ["while"] = parseWHILE,

      ["IsUndefined"] = parseIS_UNDEFINED,

      ["Ext.define"] = parseEXTDEFINE,
      ["Ext.create"] = parseEXTCREATE,
    };



    public static Dictionary<string, Func<IEnumerable<object>, XElement>> factoryDict => new Dictionary<string, Func<IEnumerable<object>, XElement>>
    {
      ["obj"] = items => items != null ? new XElement("obj", items) : new XElement("obj"),
      ["arr"] = items => items != null ? new XElement("arr", items) : new XElement("arr"),
      ["fn"] = items => items != null ? new XElement("fn", items) : new XElement("fn"),
      ["member"] = items => items != null ? new XElement("member", items) : new XElement("member"),
      ["var"] = items => items != null ? new XElement("var", items) : new XElement("var"),
      ["let"] = items => items != null ? new XElement("let", items) : new XElement("let"),
      ["const"] = items => items != null ? new XElement("const", items) : new XElement("const"),
      ["bin"] = items => items != null ? new XElement("bin", items) : new XElement("bin"),
      ["if"] = items => items != null ? new XElement("if", items) : new XElement("if"),
      ["un"] = items => items != null ? new XElement("un", items) : new XElement("un"),
      ["keyArray"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyArray"), items) : new XElement("kv", new XAttribute(IType, "keyArray")),
      ["keyObj"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyObj"), items) : new XElement("kv", new XAttribute(IType, "keyObj")),
      ["keyFunc"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyFunc"), items) : new XElement("kv", new XAttribute(IType, "keyFunc")),
      ["keyMember"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyMember"), items) : new XElement("kv", new XAttribute(IType, "keyMember")),
      ["keyString"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyString"), items) : new XElement("kv", new XAttribute(IType, "keyString")),
      ["keyNum"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyNum"), items) : new XElement("kv", new XAttribute(IType, "keyNum")),
      ["keyBool"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyBool"), items) : new XElement("kv", new XAttribute(IType, "keyBool")),
      ["keyId"] = items => items != null ? new XElement("kv", new XAttribute(IType, "keyId"), items) : new XElement("kv", new XAttribute(IType, "keyId")),
      ["region"] = items => items != null ? new XElement("region", items) : new XElement("region"),


    };





    public static Func<XElement, IEnumerable<XAttribute>> findParams => xelem => xelem.Attributes().Where(x => x.Value.StartsWith("${") && x.Value.EndsWith("}"));
    public static Func<XAttribute, object, XAttribute> substParam => (xattr, value) =>
    {
      xattr.SetValue(value);
      return xattr;
    };

    public static Func<XDocument, IEnumerable<AE>> parseXDocument => xdoc => xdoc?.Document.Root.DescendantsAndSelf().Where(w => w.Name.LocalName == "global")?.Select(s => parseAE(s));


    /// <summary>
    /// Диспетчер, вызывающий конкретный метод-парсер.
    /// </summary>
    public static Func<XElement, AE> parseAE => xelem =>
    {
      var name = xelem?.Name?.LocalName;
      if (name != null && sourceDict.ContainsKey(name))
        return sourceDict[name].Invoke(xelem);

      else
        return null;
    };




    public static Func<XElement, CallExpr> parseFOREACH => xelem =>
    {
      var args = Args(xelem.AttrVal("args").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => ID(s)));
      var fn = Settings.PreferLambdaFunctions ? FNlambda(args, xelem.Elements().Select(ss => parseAE(ss))) : FN(args, xelem.Elements().Select(ss => parseAE(ss)));
      var ret = new CallExpr("forEach", Args(fn));

      return ret;
    };
    public static Func<XElement, CallExpr> parseMAP => xelem =>
    {
      var args = Args(xelem.AttrVal("args").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => ID(s)));
      var fn = Settings.PreferLambdaFunctions ? FNlambda(args, xelem.Elements().Select(ss => parseAE(ss))) : FN(args, xelem.Elements().Select(ss => parseAE(ss)));
      var ret = new CallExpr("map", Args(fn));

      return ret;
    };
    public static Func<XElement, CallExpr> parseFILTER => xelem =>
    {
      var args = Args(xelem.AttrVal("args").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => ID(s)));
      var fn = Settings.PreferLambdaFunctions ? FNlambda(args, xelem.Elements().Select(ss => parseAE(ss))) : FN(args, xelem.Elements().Select(ss => parseAE(ss)));
      var ret = new CallExpr("filter", Args(fn));

      return ret;
    };





    public static Func<XElement, MemberAccess> parseIS_UNDEFINED => xelem =>
    {
      var args = xelem.Elements().Select(s => parseAE(s));
      var ret = MEMB(ID("IsUndefined"), CALL(Args(args)));
      return ret;
    };



    public static Func<XElement, XElement> Member2ExtDefine => xelem =>
    {
      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;

      var clname = xelem?.Descendants()?.FirstOrDefault(f => f.Name.LocalName == "call" && f.AttrVal("method") == "define")?.Elements()?.FirstOrDefault()?.AttrVal("value");
      var kvs = xelem?.Descendants()?.FirstOrDefault(f => f.Name.LocalName == "obj")?.Elements();

      var extend = kvs?.FirstOrDefault(k => k.AttrVal("key") == "extend")?.Elements()?.FirstOrDefault()?.AttrVal("value");
      if (extend == null)
        extend = kvs?.FirstOrDefault(k => k.AttrVal("key") == "extend").AttrVal("val");

      var alias = kvs?.FirstOrDefault(k => k.AttrVal("key") == "alias")?.Elements()?.FirstOrDefault()?.AttrVal("value");
      if (alias == null)
        alias = kvs?.FirstOrDefault(k => k.AttrVal("key") == "alias").AttrVal("val");

      var id = kvs?.FirstOrDefault(k => k.AttrVal("key") == "id")?.Elements()?.FirstOrDefault()?.AttrVal("value");
      if (id == null)
        id = kvs?.FirstOrDefault(k => k.AttrVal("key") == "id").AttrVal("val");

      var itemId = kvs?.FirstOrDefault(k => k.AttrVal("key") == "itemId")?.Elements()?.FirstOrDefault()?.AttrVal("value");
      if (itemId == null)
        itemId = kvs?.FirstOrDefault(k => k.AttrVal("key") == "itemId").AttrVal("val");


      if (clname.IsUseful() && xelem.Elements().FirstOrDefault().AttrVal("name") == "Ext")
      {
        var extdefine = new XElement(JsNS("Ext.define"), new XAttribute("name", clname));

        if (extend.IsUseful())
          extdefine.Add(new XAttribute("extend", extend));

        if (alias.IsUseful())
          extdefine.Add(new XAttribute("alias", alias));

        if (itemId.IsUseful())
          extdefine.Add(new XAttribute("itemId", itemId));

        if (id.IsUseful())
          extdefine.Add(new XAttribute("id", id));

        var others = kvs.Where(w => w.AttrVal("key") != "extend" && w.AttrVal("key") != "alias" && w.AttrVal("key") != "itemId" && w.AttrVal("key") != "id").ToList();
        extdefine.Add(others);
        if (gapbefore > 0)
          extdefine.Add(new XAttribute("gap", gapbefore));

        return extdefine;
      }
      else
        return xelem;
    };








    public static Func<XElement, IEnumerable<XElement>> FindElementsToSimplify => xelem => xelem.Descendants(CanBeSimplified);
    public static Func<XElement, XElement> SimplifyBinaryExpr => xelem =>
    {
      var elems = GetSimplifiableElements(xelem);
      if (elems != null)
      {
        elems.FirstOrDefault().Parent.Remove();
        xelem.AddFirst(elems);
      };
      return xelem;
    };






    public static Func<XElement, AE> parseTEMPLATES => xelem => REGION("", xelem.Elements().Select(s => parseTEMPLATE(s)));
    public static Func<XElement, AE> parseTEMPLATE => xelem =>
    {
      // находим внутри шаблона его первый дочерний элемент с локальным именем content
      var container = xelem?.Elements()?.FirstOrDefault(f => f.Name.LocalName == "content");

      // у найденного контейнера content определяем значение атрибута type - подстановочный тип элемента content
      var itype = container?.Attributes()?.FirstOrDefault(f => f.Name.LocalName == "type")?.Value;

      // выбираем все атрибуты найденного контейнера, кроме подстановочного атрибута type
      var containerAttrs = container?.Attributes()?.Where(f => !f.Name.Equals(IType));

      // выбираем содержимое найденного контейнера - все его дочерние элементы первого уровня вложенности
      var containerItems = container?.Elements();

      var items = new List<object>();
      if (containerAttrs != null)
        items.AddRange(containerAttrs);
      if (containerItems != null)
        items.AddRange(containerItems);

      // создаем новый XElement с именем, равным значению подстановочного типа найденного контейнера
      var elem = factoryDict.ContainsKey(itype) ? factoryDict[itype].Invoke(items) : null;

      var content = elem != null ? parseAE(elem) : null;
      return content;
    };
    public static Func<XElement, JSRegion> parseREGION => xelem => REGION(xelem.AttrVal("name"), xelem.Elements().Select(s => parseAE(s)));
    public static Func<XElement, AE> parseNS => xelem => REGION(xelem.AttrVal("id"), xelem.Elements().Select(s => parseAE(s)));


    public static Func<XElement, ExpressionsSet> parseGLOBAL => xelem => EXPRS(xelem.Elements().Select(s => parseAE(s)));
    public static Func<XElement, MemberAccess> parseEXTDEFINE => xelem =>
    {
      var fullName = xelem.AttrVal("name");
      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;

      var nss = xelem.Ancestors(JsNS("ns"))?.Reverse();
      if (nss != null)
      {
        var nss_names = string.Join(".", nss.Select(s => s.AttrVal("id")));
        fullName = nss_names.IsUseful() ? nss_names + "." + xelem.AttrVal("name") : xelem.AttrVal("name");
      };

      var obj = OBJ(false, true);

      CommentItem comment = null;
      if (xelem.HasElement(JsNS("info")))
        comment = parseCOMMENT(xelem.Element(JsNS("info")));

      if (xelem.HasElement(JsNS("comments")))
        comment = parseCOMMENT(xelem.Element(JsNS("comments")));

      if (xelem.HasAttr("extend"))
        obj &= KV("extend", Str(xelem.AttrVal("extend")) as AE);

      if (xelem.HasAttr("alias"))
        obj &= KV("alias", Str(xelem.AttrVal("alias")) as AE);

      if (xelem.HasAttr("itemId"))
        obj &= KV("itemId", Str(xelem.AttrVal("itemId")) as AE);

      if (xelem.HasAttr("id"))
        obj &= KV("id", Str(xelem.AttrVal("id")) as AE);

      obj += xelem.Elements().Where(w => w.Name.LocalName != "info" && w.Name.LocalName != "comments").Select(s => parseKV(s)).ToArray();

      var ret =
        MEMB(gapbefore,
          ID("Ext"),
          CALL(
            "define",
            Args(
              Str(fullName),
              obj
              )));


      ret.AddComment(comment);

      return ret;
    };
    public static Func<XElement, MemberAccess> parseEXTCREATE => xelem =>
    {
      var obj = OBJ();

      if (xelem.HasAttr("itemId"))
        obj &= KV("itemId", Str(xelem.AttrVal("itemId")) as AE);

      if (xelem.HasAttr("id"))
        obj &= KV("id", Str(xelem.AttrVal("id")) as AE);

      obj += xelem.Elements().Select(s => parseKV(s)).ToArray();

      var ret = MEMB(
        ID("Ext"),
        CALL(
          "create",
          Args(
            Str(xelem.AttrVal("name")),
            obj.Formatted == "{}" ? null : obj
            //KV("extend", Str(xelem.AttrVal("extend"))) & xelem.Elements().Select(s => parseKV(s))
            )));

      return ret;
    };
    public static Func<XElement, IIFEExpression> parseIIFE => xelem => IIFE(parseFNANON(xelem.Elements().FirstOrDefault()), xelem.Elements().Count() > 1 ? parseARGS(xelem.Elements().LastOrDefault()) : null);
    public static Func<XElement, ObjectBody> parseOBJ => xelem =>
    {
      var items = xelem.Elements().Select(s => parseAE(s)).Cast<KVal>();
      var wrapLeft = xelem.HasAttr("wrapleft") ? xelem.AttrVal("wrapleft") == "false" ? false : true : true;
      var itemsLength = items.Sum(s => s.Formatted.Length);
      return OBJ(wrapLeft, itemsLength > 100, items);
    };
    public static Func<XElement, KVal> parseKV => xelem =>
    {
      CommentItem comment = null;
      if (xelem.HasElement(JsNS("info")))
        comment = parseCOMMENT(xelem.Element(JsNS("info")));

      var key = xelem.AttrVal("key");
      var ret = KV(key, "''");

      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;

      if (xelem.HasAttr("val"))
      {
        var val = xelem.AttrVal("val");
        bool result = double.TryParse(val, out double dbl);

        if (result)
        {
          ret = KV(gapbefore, key, Num(dbl) as AE);
        }
        else if (val.AsBool() != null)
        {
          ret = KV(gapbefore, key, val.AsBool() == true ? TRUE as AE : FALSE as AE);
        }
        else
        {
          switch (val)
          {
            case "THIS":
              ret = KV(gapbefore, key, THIS);
              break;
            case "NULL":
              ret = KV(gapbefore, key, NULL);
              break;
            case "UNDEF":
              ret = KV(gapbefore, key, UNDEF);
              break;
            case "[]":
              ret = KV(gapbefore, key, Arr(false));
              break;
            case "{}":
              ret = KV(gapbefore, key, OBJ(false));
              break;
            case "()":
              ret = KV(gapbefore, key, FN());
              break;


            default:
              if (val.IsUseful() && val.StartsWith("{$") && val.EndsWith("}"))
              {
                ret = KV(gapbefore, key, ID(val.TrimStart('{', '$').TrimEnd('}')));
              }
              else
              {
                ret = KV(gapbefore, key, Str("'" + val + "'") as AE);
              };
              break;
          };
        };
      }
      else
      {
        ret = KV(gapbefore, key, parseAE(xelem.Elements().LastOrDefault()));
      };

      ret.AddComment(comment);

      return ret;
    };
    public static Func<XElement, ArrayDefinition> parseARR => xelem =>
    {
      var wrapLeft = xelem.HasAttr("wrapleft") ? xelem.AttrVal("wrapleft") == "false" ? false : true : true;

      return Arr(wrapLeft, true, xelem.Elements().Select(s => parseAE(s)));
    };
    public static Func<XElement, StringLiteral> parseSTR => xelem => xelem.AttrVal("value") != null ? Str("'" + xelem.AttrVal("value") + "'") : Str("''");
    public static Func<XElement, NumberLiteral> parseNUM => xelem => Num(xelem.AttrVal("value"));
    public static Func<XElement, RegexpLiteral> parseREGEXP => xelem => REGEXP(xelem.AttrVal("value"), xelem.AttrVal("flags"));
    public static Func<XElement, Identifier> parseID => xelem => ID(xelem.AttrVal("name"));
    public static Func<XElement, ThisExpression> parseTHIS => xelem => THIS;
    public static Func<XElement, TrueExpression> parseTRUE => xelem => TRUE;
    public static Func<XElement, FalseExpression> parseFALSE => xelem => FALSE;
    public static Func<XElement, NullExpression> parseNULL => xelem => NULL;
    public static Func<XElement, UndefExpression> parseUNDEF => xelem => UNDEF;
    public static Func<XElement, BreakExpression> parseBREAK => xelem => BREAK;
    public static Func<XElement, ContinueExpression> parseCONTINUE => xelem => CONTINUE;
    public static Func<XElement, FunctionAnonymous> parseLAMBDA => xelem =>
    {
      var first = xelem.Elements()?.FirstOrDefault();
      var rest = xelem.Elements()?.Skip(1);
      if (first != null && first.Name.LocalName == "args")
      {
        var args = parseARGS.Invoke(first);
        return FNlambda(args, rest.Select(s => parseAE(s))?.Cast<AE>());
      }
      else
      {
        return FNlambda(Args(), xelem.Elements().Select(s => parseAE(s))?.Cast<AE>());
      };
    };
    public static Func<XElement, FunctionAnonymous> parseFNANON => xelem =>
    {
      var first = xelem.FirstElement();
      var rest = xelem.Elements()?.Skip(1);
      if (first != null && first.Name.LocalName == "args")
      {
        var args = parseARGS.Invoke(first);
        return (args?.HasItems() == false) ? FN(rest.Select(s => parseAE(s))?.Cast<AE>()) : FN(args, rest.Select(s => parseAE(s))?.Cast<AE>());
      }
      else
      {
        return FN(xelem.Elements().Select(s => parseAE(s))?.Cast<AE>());
      };
    };
    public static Func<XElement, FunctionNamed> parseFNamed => xelem =>
    {
      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;

      var ret =
      FN(gapbefore,
      name: xelem.AttrVal("name"),
      arguments: (xelem.Elements().FirstOrDefault().Name.LocalName == "args" ? parseAE(xelem.Elements().FirstOrDefault()) as ArgumentsBlock : Args(false)),
      exprs: (xelem.Elements().FirstOrDefault().Name.LocalName != "args" ? xelem.Elements().Select(s => parseAE(s)).Cast<AE>().ToArray() : xelem.Elements().ExceptFirst().Select(s => parseAE(s)).ToArray())
      );

      return ret;
    };
    public static Func<XElement, AE> parseFN => xelem => (xelem.HasAttr("name") ? parseFNamed.Invoke(xelem) as AE : parseFNANON.Invoke(xelem) as AE);
    public static Func<XElement, ArgumentsBlock> parseARGS => xelem => Args(xelem.Elements().Select(s => parseAE(s)));
    public static Func<XElement, AE> parseARG => xelem =>
    {
      var itype = xelem.Attributes()?.FirstOrDefault(f => f.Name.LocalName == "type")?.Value;
      var value = xelem.HasAttr("value") ? xelem.AttrVal("value") : null;
      if (itype != null && sourceDict.ContainsKey(itype))
        return sourceDict[itype].Invoke(xelem);

      else
        return null;
    };
    public static Func<XElement, VarDeclaration> parseVAR => xelem =>
    {
      CommentItem comment = null;
      if (xelem.HasElement(JsNS("info")))
        comment = parseCOMMENT(xelem.Element(JsNS("info")));
      if (xelem.HasElement(JsNS("comments")))
        comment = parseCOMMENT(xelem.Element(JsNS("comments")));

      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;
      var ret =
        xelem.HasElements
          ?
          comment != null
            ?
            VAR(gapbefore, comment, xelem.AttrVal("name"), parseAE(xelem.LastElement()))
            :
            VAR(gapbefore, xelem.AttrVal("name"), parseAE(xelem.LastElement()))
          :
          comment != null
            ?
            VAR(gapbefore, comment, ID(xelem.AttrVal("name")))
            :
            VAR(gapbefore, ID(xelem.AttrVal("name")));

      return ret;
    };
    public static Func<XElement, LetDeclaration> parseLET => xelem => xelem.HasElements ? LET(xelem.AttrVal("name"), parseAE(xelem.Elements()?.FirstOrDefault())) : LET(ID(xelem.AttrVal("name")));
    public static Func<XElement, ConstDeclaration> parseCONST => xelem => CONST(xelem.AttrVal("name"), parseAE(xelem.Elements()?.FirstOrDefault()));
    public static Func<XElement, MemberAccess> parseMEMBER => xelem =>
    {
      var first = xelem.Elements()?.FirstOrDefault();
      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;
      var nameAttr = xelem.AttrVal("name");

      if (nameAttr != null && first == null)
      {
        var nameArr = nameAttr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        MemberAccess memb = new MemberAccess();

        nameArr.ForEach(n =>
        {
          if (n == "this")
            memb.AddExpr(THIS);
          if (!n.EndsWith("()") && !n.EndsWith("]"))
            memb.AddExpr(ID(n));


          if (n.EndsWith("()"))
          {
            var argums = xelem.AttrVal("args") ?? "";
            var argsList = argums.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var argsBlock = Args();

            if (n == nameArr.LastOrDefault())
            {
              argsList.ForEach(a =>
              {

                /// если строка this
                if (a == "this")
                {
                  argsBlock.Add(THIS);
                };

                /// если строка null
                if (a == "null")
                {
                  argsBlock.Add(NULL);
                };

                /// если строка НЕ заканчивается на (),
                /// И строка НЕ заканчивается на ],
                /// И строка НЕ равна this
                /// И строка не равна null
                if (!a.EndsWith("()") && !a.EndsWith("]") && a != "this" && a != "null")
                {
                  if (IsLikeIdentifier(a))
                  {
                    argsBlock.Add(ValueToIdentifier(a));
                  }
                  else if (IsLikeBooleanLiteral(a))
                  {
                    argsBlock.Add(ValueToBooleanLiteral(a));
                  }
                  else
                  {
                    argsBlock.Add(ValueToStringLiteral(a));
                  };
                };

                /// если строка заканчивается на ()
                if (a.EndsWith("()"))
                {
                  var meth = a.TrimEnd(')', '(');
                  argsBlock.Add(CALL(meth, Args()));
                };


                /// если строка похожа на индекс массива
                if (IsLikeArrayIndex(a))
                {
                  /// убираем квадратные скобки
                  var trimmed = a.TrimStart('[').TrimEnd(']');
                  bool isnumber = int.TryParse(trimmed, out int asnumber);

                  /// индекс массива - число?...
                  var check = isnumber ?
                /// memb[0]...
                ArrIndex(asnumber)
                           :
                /// если нет, то без квадратных скобок строка похожа на идентификатор ?...
                /// {$ident}...
                IsLikeIdentifier(trimmed) ?
                /// memb[ident]... ===> memb.ident
                ArrIndex(ValueToIdentifier(trimmed))
                           :
                /// если не похожа, значит это строка...
                /// memb['str']...
                ArrIndex(Str(trimmed) as AE);

                  argsBlock.Add(check);
                };
              });
            };

            var method = n.TrimEnd(')', '(');
            memb.AddExpr(CALL(method, argsBlock));
          };



          if (n.StartsWith("[") && n.EndsWith("]"))
          {
            var trimmed = n.TrimStart('[').TrimEnd(']');
            bool isnumber = int.TryParse(trimmed, out int asnumber);

            memb = isnumber ? memb[asnumber] : memb[trimmed];
          };
        });
        memb = memb.SetGap(gapbefore);
        return memb;
      }
      else
      {
        if (first?.Name.LocalName == "info")
        {
          return MEMB(gapbefore, xelem.Elements().Select(s => parseAE(s)).ToArray()).AddComment(parseCOMMENT(first)).As<MemberAccess>();
        }
        else
        {
          return MEMB(gapbefore, xelem.Elements().Select(s => parseAE(s)).ToArray());
        };
      };
    };
    public static Func<XElement, ArrayIndex> parseINDEX => xelem => ArrIndex(parseAE(xelem.Elements().FirstOrDefault()));
    public static Func<XElement, CallExpr> parseCALL => xelem =>
    {
      var elems = xelem?.Elements();
      if (xelem.HasAttr("method"))
      {
        return CALL(xelem.AttrVal("method"), (elems != null) ? Args(elems.Select(e => parseAE(e))) as ArgumentsBlock : Args());
      }
      else
      {
        return CALL((elems != null) ? Args(elems.Select(e => parseAE(e))) as ArgumentsBlock : Args());

      };
    };
    public static Func<XElement, AbstractBinaryExpression> parseBINARY => xelem =>
    {
      var xname = xelem.Name.LocalName;
      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;
      var left = xelem.Elements().FirstOrDefault();
      var right = (xelem.Elements().LastOrDefault() == xelem.Elements().FirstOrDefault()) ? null : xelem.Elements().LastOrDefault();
      switch (xname)
      {
        case "assign":
          return new AssignExpr(parseAE(left), parseAE(right)).SetGap(gapbefore);
        case "equal":
          return new EqualExpr(parseAE(left), parseAE(right));
        case "strictEqual":
          return new StrictEqualExpr(parseAE(left), parseAE(right));
        case "notEqual":
          return new NotEqualExpr(parseAE(left), parseAE(right));
        case "strictNotEqual":
          return new StrictNotEqualExpr(parseAE(left), parseAE(right));
        case "bitOr":
          return new BitOrExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "bitXor":
          return new BitXorExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "bitAnd":
          return new BitAndExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "or":
          return new OrExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "orEq":
          return new OrEqualExpr(parseAE(left), parseAE(right));
        case "and":
          return new AndExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "andEq":
          return new AndEqualExpr(parseAE(left), parseAE(right));
        case "plus":
          return new PlusExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "plusEq":
          return new PlusEqualExpr(parseAE(left), parseAE(right));
        case "minus":
          return new MinusExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "minusEq":
          return new MinusEqualExpr(parseAE(left), parseAE(right));
        case "mod":
          return new ModExpr(parseAE(left), parseAE(right));
        case "multiply":
          return new MultiplyExpr(xelem.Elements().Select(s => parseAE(s)).ToArray());
        case "multiplyEq":
          return new MultiplyEqualExpr(parseAE(left), parseAE(right));
        case "divide":
          return new DivideExpr(parseAE(left), parseAE(right));
        case "divideEq":
          return new DivideEqualExpr(parseAE(left), parseAE(right));
        case "gt":
          return new GreaterThanExpr(parseAE(left), parseAE(right));
        case "lt":
          return new LesserThanExpr(parseAE(left), parseAE(right));
        case "gtEq":
          return new GreaterOrEqualExpr(parseAE(left), parseAE(right));
        case "ltEq":
          return new LesserOrEqualExpr(parseAE(left), parseAE(right));
        case "instOf":
          return new InstanceofExpr(parseAE(left), parseAE(right));
        case "in":
          return new InExpr(parseAE(left), parseAE(right));

        default:
          return null;
      };
    };
    public static Func<XElement, AbstractUnaryExpression> parseUNARY => xelem =>
    {
      var xname = xelem?.Name?.LocalName;
      var pos = xelem?.AttrVal("pos");
      var left = xelem?.Elements().FirstOrDefault();
      switch (xname)
      {
        case "neg":
          return new NegExpr(parseAE(left));
        case "pos":
          return new PosExpr(parseAE(left));
        case "not":
          return new NotExpr(parseAE(left));
        case "dblNot":
          return new DoubleNotExpression(parseAE(left));
        case "bitNot":
          return new BitNotExpr(parseAE(left));
        case "spread":
          return new SpreadExpression(parseID(left));
        case "typeOf":
          return new TypeofExpr(parseAE(left));
        case "delete":
          return new DeleteExpr(parseAE(left));
        case "void":
          return new VoidExpr(parseAE(left));


        case "dblPlus":
          switch (pos)
          {
            case "prefix":
              return new PreIncrementExpression(parseAE(left));
            case "postfix":
              return new PostIncrementExpression(parseAE(left));
            default:
              return null;
          };
        case "dblMinus":
          switch (pos)
          {
            case "prefix":
              return new PreDecrementExpression(parseAE(left));
            case "postfix":
              return new PostDecrementExpression(parseAE(left));
            default:
              return null;
          };

        default:
          return null;
      };
    };
    public static Func<XElement, IfElseExpression> parseIF => xelem =>
    {
      var firstElem = xelem?.Elements()?.FirstOrDefault();
      var thenElems = xelem?.Elements().Skip(1).Take(1).FirstOrDefault().Elements().Select(s => parseAE(s));
      var elseElems = xelem?.Elements().Skip(2)?.Take(1).FirstOrDefault()?.Elements().Select(s => parseAE(s));
      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;
      switch (firstElem.Name.LocalName)
      {
        case "assign":
        case "equal":
        case "strictEqual":
        case "notEqual":
        case "strictNotEqual":
        case "bitOr":
        case "bitXor":
        case "bitAnd":
        case "or":
        case "orEq":
        case "and":
        case "andEq":
        case "plus":
        case "plusEq":
        case "minus":
        case "minusEq":
        case "multiply":
        case "multiplyEq":
        case "divide":
        case "divideEq":
        case "gt":
        case "lt":
        case "gtEq":
        case "ltEq":
        case "in":
        case "instOf":
          return IF(gapbefore, parseBINARY(firstElem), FnBody(thenElems.ToArray()), elseElems != null && elseElems.Count() > 0 ? elseElems?.ToArray() : new AE[] { });


        case "neg":
        case "pos":
        case "not":
        case "bitNot":
        case "dblNot":
        case "spread":
        case "typeOf":
        case "dblPlus":
        case "dblMinus":
          return IF(gapbefore, parseUNARY(firstElem), FnBody(thenElems.ToArray()), elseElems != null && elseElems.Count() > 0 ? elseElems?.ToArray() : null);

        case "id":
        case "member":
          return IF(gapbefore, parseAE(firstElem), FnBody(thenElems.ToArray()), elseElems?.ToArray());

        default:
          return null;
      };
    };
    public static Func<XElement, IfElseExpression> parseTIF => xelem =>
    {
      var firstElem = xelem.Elements().FirstOrDefault();
      var thenElems = xelem.Elements().Skip(1).Take(1).FirstOrDefault();
      var elseElems = xelem.Elements().Skip(2)?.Take(1).FirstOrDefault();
      switch (firstElem.Name.LocalName)
      {
        case "assign":
        case "equal":
        case "strictEqual":
        case "notEqual":
        case "strictNotEqual":
        case "bitOr":
        case "bitXor":
        case "bitAnd":
        case "or":
        case "orEq":
        case "and":
        case "andEq":
        case "plus":
        case "plusEq":
        case "minus":
        case "minusEq":
        case "mod":
        case "multiply":
        case "multiplyEq":
        case "divide":
        case "divideEq":
        case "gt":
        case "lt":
        case "gtEq":
        case "ltEq":
        case "in":
        case "instOf":
          return TIF(parseBINARY(firstElem), parseAE(thenElems), parseAE(elseElems));

        case "neg":
        case "pos":
        case "not":
        case "bitNot":
        case "dblNot":
        case "spread":
        case "typeOf":
        case "dblPlus":
        case "dblMinus":
          return TIF(parseUNARY(firstElem), parseAE(thenElems), parseAE(elseElems));

        case "id":
        case "member":
          return TIF(parseAE(firstElem), parseAE(thenElems), parseAE(elseElems));

        default:
          return null;
      };
    };
    public static Func<XElement, ReturnExpression> parseRET => xelem => xelem.HasElements ? RETURN(parseAE(xelem?.Elements()?.FirstOrDefault())) : RETURN();
    public static Func<XElement, CommentItem> parseCOMMENT => xelem =>
    {
      var name = xelem?.Name.LocalName;
      if (name == "comments")
      {
        var lines = xelem.Elements().Select(s => s.AttrVal("value").Trim('\''));
        return CommentBlock(lines.ToArray());
      }
      else
      {
        var itype = xelem.Attributes().FirstOrDefault(f => f.Name.LocalName == "type").Value;
        switch (itype)
        {
          case "block":
            var lines = xelem.Elements().Select(s => s.AttrVal("value").Trim('\''));
            return CommentBlock(lines.ToArray());
          case "single":
            return CommentSingle(xelem.AttrVal("value"));

          default:
            return null;
        };
      };
    };
    public static Func<XElement, AE> parseNEWLINE => xelem =>
    {
      var amountStr = xelem.AttrVal("amount") ?? "1";
      bool checkParse = int.TryParse(amountStr, out int res);
      int amount = checkParse ? res : 1;
      return NEWLINES(amount - 1);
    };
    public static Func<XElement, EmptyExpression> parseEMPTY => xelem => new EmptyExpression();
    public static Func<XElement, CaseExpression> parseCASE => xelem =>
    {
      Literal liter;

      if (xelem.HasAttr("value"))
      {
        var val = xelem.AttrVal("value");
        if (int.TryParse(val, out int res))
        {
          liter = Num(res);
        }
        else
        {
          liter = Str(val);
        };
      }
      else
      {
        liter = null;
      };
      return liter != null ? CASE(liter, xelem?.Elements()?.Select(s => parseAE(s)).ToArray()) : null;
    };
    public static Func<XElement, CaseExpression> parseDEFAULT => xelem => CASE(xelem?.Elements()?.Select(s => parseAE(s)));
    public static Func<XElement, SwitchExpr> parseSWITCH => xelem =>
    {
      var gapbefore = xelem.HasAttr("gap") ? Convert.ToInt32(xelem?.AttrVal("gap")) : 0;
      var first = xelem.Elements()?.FirstOrDefault();
      var cases = xelem.Elements()?.ExceptFirst().Select(s => parseAE(s)).Cast<CaseExpression>().ToArray();
      var ret = SWITCH(gapbefore, parseAE(first), cases);
      return ret;
    };
    public static Func<XElement, ForExpr> parseFOR => xelem =>
    {
      AE init = parseAE(xelem.Elements().FirstOrDefault()) as AE;
      AbstractBinaryExpression test = parseBINARY(xelem.Elements().Skip(1).FirstOrDefault());
      UpdateExpression upd = parseUNARY(xelem.Elements().Skip(2).FirstOrDefault().Elements().FirstOrDefault()) as UpdateExpression;
      var exprs = EXPRS(xelem.Elements().Skip(3).Select(ss => parseAE(ss)));
      var ret = init is AbstractDeclaration ? new ForExpr(init.As<AbstractDeclaration>(), test, upd, exprs) : new ForExpr(init.As<AssignExpr>(), test, upd, exprs);
      return ret;
      //return new ForExpr(init, test, upd, exprs);
    };
    public static Func<XElement, ForInExpr> parseFOR_IN => xelem =>
    {
      var inElem = xelem.Elements().FirstOrDefault();
      InExpr inExpr = parseBINARY(inElem).As<InExpr>();
      var exprs = EXPRS(xelem.Elements().Skip(1).Select(ss => parseAE(ss)));
      return new ForInExpr(inExpr, exprs);
    };
    public static Func<XElement, TryCatchFinallyExpr> parseTRY_CATCH => xelem =>
    {
      TryCatchFinallyExpr ret;

      var _try = xelem?.Elements()?.FirstOrDefault();
      var _catch = xelem?.Elements()?.FirstOrDefault(f => f.Name.LocalName == "catch");
      var _finally = xelem?.Elements()?.FirstOrDefault(f => f.Name.LocalName == "finally");

      if (_catch != null)
      {
        ret = (_finally != null) ?
        TRY_CATCH(TRY(_try.Elements().Select(s => parseAE(s))), CATCH(_catch.Elements().ExceptFirst().Select(s => parseAE(s))), FINALLY(_finally.Elements().Select(s => parseAE(s))))
        :
        TRY_CATCH(TRY(_try.Elements().Select(s => parseAE(s))), CATCH(_catch.Elements().ExceptFirst().Select(s => parseAE(s))));
      }
      else
      {
        ret = TRY_CATCH(TRY(_try.Elements().Select(s => parseAE(s))), FINALLY(_finally.Elements().Select(s => parseAE(s))));
      };
      return ret;
    };
    public static Func<XElement, WhileExpr> parseWHILE => xelem =>
    {
      var args = xelem.Elements().FirstOrDefault();
      var body = xelem.Elements().Skip(1);

      var ret = new WhileExpr(parseARGS(args), body.Select(s => parseAE(s)));
      return ret;
    };
    public static Func<XElement, DoWhileExpr> parseDO_WHILE => xelem =>
    {
      var body = xelem.Elements().ExceptLast();
      var args = xelem.Elements().LastOrDefault();

      var ret = new DoWhileExpr(parseARGS(args), body.Select(s => parseAE(s)));
      return ret;
    };
    public static Func<XElement, NewExpr> parseNEW => xelem => new NewExpr(parseMEMBER(xelem.Elements().FirstOrDefault()));
    public static Func<XElement, DeleteExpr> parseDELETE => xelem => new DeleteExpr(parseMEMBER(xelem.Elements().FirstOrDefault()));

    public static Func<XElement, StringTemplateParameter> parsePRM => xelem => new StringTemplateParameter(parseAE(xelem.Elements().FirstOrDefault()));
    public static Func<XElement, StringTemplate> parseSTR_TEMPLATE => xelem => new StringTemplate(xelem.Elements().Select(ss => parseAE(ss)));




    public static Func<XElement, StringLiteral> parseHTML => xelem =>
    {
      var ret = new StringLiteral("");




      return ret;
    };





    private static XName IType => null; //XName.Get("type", S.Default.xsdSchemaInstanceNS);

    public static Func<string, XName> JsNS => str => XName.Get(str, "JSModelSchema-1.1");










    /// <summary>
    /// Проверка строки на отсутствие внутри нее пробелов, табуляций, символов перевода строки и возврата каретки.
    /// </summary>
    public static Func<string, bool> IsWithoutSpaces => str => str == string.Join("", str.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
    /// <summary>
    /// Проверка строки на соответствие шаблону "Идентификатор".
    /// </summary>
    public static Func<string, bool> IsLikeIdentifier => str => str.IsUseful() && IsWithoutSpaces(str) && str.StartsWith("{$") && str.EndsWith("}");
    /// <summary>
    /// Проверка строки на соответствие шаблону "true/false".
    /// </summary>
    public static Func<string, bool> IsLikeBooleanLiteral => str => str.IsUseful() && IsWithoutSpaces(str) && (str.ToLower() == "true" || str.ToLower() == "false");
    /// <summary>
    /// Проверка строки на соответствие шаблону "Индекс массива".
    /// </summary>
    public static Func<string, bool> IsLikeArrayIndex => str => str.IsUseful() && IsWithoutSpaces(str) && str.StartsWith("[") && str.EndsWith("]");






    /// <summary>
    /// Конвертация строкового значения атрибута в идентификатор.
    /// </summary>
    public static Func<string, Identifier> ValueToIdentifier => str => ID(str.TrimStart('\'', '{', '$').TrimEnd('\'', '}'));
    /// <summary>
    /// Конвертация строкового значения атрибута в строковый литерал.
    /// </summary>
    public static Func<string, StringLiteral> ValueToStringLiteral => str => Str("'" + str + "'");
    /// <summary>
    /// Конвертация строкового значения атрибута в литерал true/false.
    /// </summary>
    public static Func<string, AE> ValueToBooleanLiteral => str => str.AsBool() == true ? TRUE as AE : FALSE as AE;



  }
}
