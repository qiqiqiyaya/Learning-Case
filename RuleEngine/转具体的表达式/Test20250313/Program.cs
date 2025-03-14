using System.Dynamic;
using System.Linq.Dynamic.Core.Parser;
using System.Linq.Expressions;

namespace Test20250313
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            var aa = new ExpandoObject();

            var type = typeof(int);
            var param = Expression.Parameter(type, "count");
            var paramList = new List<ParameterExpression>() { param };
            var values = new List<object>() { 1 };

            var ep = new ExpressionParser(paramList.ToArray(), "count<3", Array.Empty<object>(), null);
            var body = ep.Parse(typeof(bool));

            //var lambda = Expression.Lambda<Func<int, bool>>(body, param);
            //var func = lambda.Compile();
            //var result = func.Invoke(1);

            var wrappedExpression = WrapExpression<bool>(new List<Expression>() { body },
                paramList.ToArray(),
                new ParameterExpression[] { });

            var func = wrappedExpression.Compile();
            var a = func(values.ToArray());

            Console.Read();
        }


        private static Expression<Func<object[], T>> WrapExpression<T>(List<Expression> expressionList, ParameterExpression[] parameters, ParameterExpression[] variables)
        {
            var argExp = Expression.Parameter(typeof(object[]), "args");
            var paramExps = parameters.Select((c, i) =>
            {
                var arg = Expression.ArrayAccess(argExp, Expression.Constant(i));
                return (Expression)Expression.Assign(c, Expression.Convert(arg, c.Type));
            });
            var blockExpSteps = paramExps.Concat(expressionList);
            var blockExp = Expression.Block(parameters.Concat(variables), blockExpSteps);
            return Expression.Lambda<Func<object[], T>>(blockExp, argExp);
        }
    }
}
