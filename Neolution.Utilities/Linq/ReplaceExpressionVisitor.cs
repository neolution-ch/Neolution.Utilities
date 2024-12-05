namespace Neolution.Utilities.Linq
{
    using System.Linq.Expressions;

    /// <summary>
    /// Internal helper class.
    /// </summary>
    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        /// <summary>
        /// The old value.
        /// </summary>
        private readonly Expression oldValue;

        /// <summary>
        /// The new value.
        /// </summary>
        private readonly Expression newValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceExpressionVisitor"/> class.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        /// <summary>
        /// Dispatches the expression to one of the more specialized visit methods in this class.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        public override Expression Visit(Expression node)
        {
            if (node == oldValue)
            {
                return newValue;
            }

            return base.Visit(node);
        }
    }
}
