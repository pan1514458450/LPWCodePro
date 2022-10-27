using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpCode.Exp
{
    internal class ParsingExp: ExpressionVisitor
    {
        Stack<object> _stack = new Stack<object>();
        [return: NotNullIfNotNull("node")]
        public override sealed Expression? Visit(Expression? node)
        {
            return base.Visit(node);
        }
        /// <summary>
        /// 二元表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return node;
        }
        /// <summary>
        /// 常量表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            _stack.Push(node.Value);
            return node;
        }
        /// <summary>
        /// 成员表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            _stack.Push(node.Member.Name);
            return node;
        }
        /// <summary>
        /// 方法表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
           Visit(node.Object);
            Visit(node.Arguments);
            return node;
        }

    }
}
