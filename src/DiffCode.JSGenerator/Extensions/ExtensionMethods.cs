using System.Linq.Expressions;






namespace DiffCode.JSGenerator.Extensions
{
  /// <summary>
  /// Методы расширения.
  /// </summary>
  public static class ExtensionMethods
  {


    public static TOut As<TOut>(this AT elem) where TOut : AT => (elem is TOut && (elem as TOut) != null) ? elem as TOut : null;
    public static TOut Convert<TOut>(this AT tok) where TOut : AT => tok as TOut;



    public static IEnumerable<AT> Find(this AT token, Expression<Func<AT, bool>> predicate)
    {
      var ret = new List<AT>();
      if (predicate.Compile()(token))
        ret.Add(token);

      ret.AddRange(token.ChildTokens.SelectMany(ss => ss.Find(predicate)));
      return ret;
    }
    public static IEnumerable<TOut> Find<TOut>(this AT token, Expression<Func<AT, bool>> predicate)
    {
      var ret = new List<AT>();
      if (predicate.Compile().Invoke(token))
        ret.Add(token);

      ret.AddRange(token.ChildTokens.SelectMany(ss => ss.Find(predicate)));
      return ret.Cast<TOut>();
    }
    public static IEnumerable<AT> Find(this AT token, Expression<Func<AT, bool>> predicate, Expression<Func<AT, bool>> wherePredicate)
    {
      var ret = new List<AT>();
      if (predicate.Compile()(token))
        ret.Add(token);

      ret.AddRange(token.ChildTokens.SelectMany(ss => ss.Find(predicate)));
      return ret.AsQueryable().Where(wherePredicate);
    }
    public static IEnumerable<TOut> Find<TOut>(this AT token, Expression<Func<AT, bool>> predicate, Expression<Func<TOut, bool>> wherePredicate)
    {
      var ret = new List<AT>();
      if (predicate.Compile()(token))
        ret.Add(token);

      ret.AddRange(token.ChildTokens.AsQueryable().SelectMany(ss => ss.Find(predicate)));
      return ret.AsQueryable().Cast<TOut>().Where(wherePredicate);
    }




    public static AT FindOne(this AT token, Expression<Func<AT, bool>> predicate)
    {
      var compiled = predicate.Compile();
      if (compiled(token))
        return token;
      return token.ChildTokens.FirstOrDefault(compiled) ?? token.Find(predicate).FirstOrDefault() ?? null;
    }
    public static TOut FindOne<TOut>(this AT token, Expression<Func<AT, bool>> predicate) where TOut : AT
    {
      var compiled = predicate.Compile();
      if (compiled(token))
        return token as TOut;
      return token.ChildTokens.FirstOrDefault(compiled).As<TOut>() ?? token.Find<TOut>(predicate).FirstOrDefault() ?? null;
    }


    public static TOut FindFirst<TOut>(this AT token, Expression<Func<TOut, bool>> predicate) where TOut : AT
    {
      var compiled = predicate.Compile();
      if (token?.As<TOut>() != null && compiled.Invoke(token.As<TOut>()) == true)
        return token as TOut;
      return token?.FindAll<TOut>()?.FirstOrDefault(compiled);
    }
    public static TOut FindFirst<TOut>(this AT token) where TOut : AT
    {
      if (token?.As<TOut>() != null)
        return token as TOut;
      return token?.FindAll<TOut>()?.FirstOrDefault();
    }


    public static TOut FindByOrder<TOut>(this AT token, int order) where TOut : AT
    {
      return token?.FindAll<TOut>()?.Skip(order - 1)?.FirstOrDefault();
    }




    public static IEnumerable<TOut> FindAll<TOut>(this AT token) => token.ChildTokens.SelectMany(ss => ss.Find<TOut>(t => t is TOut)).OfType<TOut>();




    public static THost Closest<THost>(this AT token) where THost : AT
    {
      var ret = token.Parent;
      if (ret != null && ret is THost)
        return ret.As<THost>();
      while (ret != null && !(ret is THost))
        ret = ret.Parent;
      return ret?.As<THost>();
    }




    public static AT PrevToken(this IEnumerable<AT> list, AT from) => (from != list.First()) ? list.Where(w => from.Order - w.Order == 1).FirstOrDefault() : null;
    public static AT NextToken(this IEnumerable<AT> list, AT from) => (from != list.Last()) ? list.Where(w => w.Order - from.Order == 1).FirstOrDefault() : null;



    public static AT FindTopHost(this AT token)
    {
      AT ret = token;
      if (token.IsTopHost())
      {
        ret = token;
      }
      else
      {
        while (!ret.IsTopHost() && ret.HasParent())
        {
          ret = ret.Parent;
        };
      };

      return ret;
    }





    public static void ReOrder(this IEnumerable<AT> list)
    {
      var order = 0;
      list.ToList().ForEach(f =>
      {
        f.Order = order;
        order++;
      });
    }


  }
}
