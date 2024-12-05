namespace Neolution.Utilities.Linq
{
    using System.Linq.Expressions;

    /// <summary>
    /// Some expressions utilities.
    /// </summary>
    public static class ExpressionUtils
    {
        /// <summary>
        /// Gets the path of an expression.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="expression">The path.</param>
        /// <returns>A string with all properties separated by a dot.</returns>
        public static string PathOf<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Stack<string> memberNames = new Stack<string>();

            MemberExpression memberExp = GetMemberExpression(expression.Body);

            while (memberExp != null)
            {
                memberNames.Push(memberExp.Member.Name);
                memberExp = GetMemberExpression(memberExp.Expression);
            }

            return string.Join(".", memberNames);
        }

        /// <summary>
        /// Gets the full path of an expression.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="expression">The path.</param>
        /// <returns>A string with all properties separated by a dot.</returns>
        public static string FullPathOf<T>(Expression<Func<T, object>> expression)
        {
            return $"{typeof(T).Name}.{PathOf(expression)}";
        }

        /// <summary>
        /// Gets the member expression.
        /// </summary>
        /// <param name="toUnwrap">The expression to unwrap.</param>
        /// <returns>The member expression.</returns>
        public static MemberExpression GetMemberExpression(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression unaryExpression)
            {
                return unaryExpression.Operand as MemberExpression;
            }

            return toUnwrap as MemberExpression;
        }

        /// <summary>
        /// Connects two expressions by an or condition.
        /// </summary>
        /// <typeparam name="T">The of object.</typeparam>
        /// <param name="expr1">The expr1.</param>
        /// <param name="expr2">The expr2.</param>
        /// <returns>Th expression.</returns>
        /// <exception cref="ArgumentNullException">
        /// expr1
        /// or
        /// expr2.
        /// </exception>
        public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                throw new ArgumentNullException(nameof(expr1));
            }

            if (expr2 == null)
            {
                throw new ArgumentNullException(nameof(expr2));
            }

            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.Or(left, right), parameter);
        }

        /// <summary>
        /// Connects two expressions by an and condition.
        /// </summary>
        /// <typeparam name="T">The of object.</typeparam>
        /// <param name="expr1">The expr1.</param>
        /// <param name="expr2">The expr2.</param>
        /// <returns>Th expression.</returns>
        /// <exception cref="ArgumentNullException">
        /// expr1
        /// and
        /// expr2.
        /// </exception>
        public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                throw new ArgumentNullException(nameof(expr1));
            }

            if (expr2 == null)
            {
                throw new ArgumentNullException(nameof(expr2));
            }

            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.And(left, right), parameter);
        }

        /// <summary>
        /// Evaluates the comparison.
        /// </summary>
        /// <typeparam name="T">Type of arguments.</typeparam>
        /// <param name="a">The first argument.</param>
        /// <param name="b">The second argument.</param>
        /// <param name="sign">The sign.</param>
        /// <returns>True or false of comparison</returns>
        public static bool EvaluateComparison<T>(T a, T b, string sign)
        {
            var arg1 = Expression.Parameter(typeof(T));
            var arg2 = Expression.Parameter(typeof(T));

            var comparison = sign switch
            {
                "<" => Expression.LessThan(arg1, arg2),
                "<=" => Expression.LessThanOrEqual(arg1, arg2),
                ">" => Expression.GreaterThan(arg1, arg2),
                ">=" => Expression.GreaterThanOrEqual(arg1, arg2),
                _ => throw new NotSupportedException("Operation is unknown"),
            };

            var func = Expression.Lambda<Func<T, T, bool>>(comparison, arg1, arg2).Compile();

            return func(a, b);
        }
    }
}
