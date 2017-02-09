using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SP.Utils
{
    public class Log
    {
        public static void Fatal(params object[] values)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("SPARKY:    ");
            foreach (object obj in values)
            {
                Console.Write(obj);
            }
            Console.Write("\n");
            Console.ResetColor();
        }

        public static void Error(params object[] values)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("SPARKY:    ");
            foreach (object obj in values)
            {
                Console.Write(obj);
            }
            Console.Write("\n");
            Console.ResetColor();
        }

        public static void Warn(params object[] values)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SPARKY:    ");
            foreach (object obj in values)
            {
                Console.Write(obj);
            }
            Console.Write("\n");
            Console.ResetColor();
        }

        public static void Info(params object[] values)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("SPARKY:    ");
            foreach (object obj in values)
            {
                Console.Write(obj);
            }
            Console.Write("\n");
            Console.ResetColor();
        }

        public static void Assert(Expression<Func<bool>> x, params object[] values)
        {
            StackFrame stack = new StackFrame(1, true);
            if (!(x.Compile().Invoke()))
            {
                Fatal("*************************");
                Fatal("    ASSERTION FAILED!    ");
                Fatal("*************************");
                Fatal(stack.GetFileName(), ": ", stack.GetFileLineNumber());
                Fatal("Condition: ", LambdaToString(x));
                Fatal(values);
                //throw new Exception("Assertion Failed!");
            }
        }

        public static string LambdaToString(Expression<Func<bool>> expression)
        {

            var replacements = new Dictionary<string, string>();
            WalkExpression(replacements, expression);


            string body = expression.Body.ToString();

            foreach (var parm in expression.Parameters)
            {
                var parmName = parm.Name;
                var parmTypeName = parm.Type.Name;
                body = body.Replace(parmName + ".", parmTypeName + ".");
            }

            foreach (var replacement in replacements)
            {
                body = body.Replace(replacement.Key, replacement.Value);
            }

            return body;
        }

        private static void WalkExpression(Dictionary<string, string> replacements, Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.MemberAccess:
                    string replacementExpression = expression.ToString();
                    if (replacementExpression.Contains("value(") || replacementExpression.Contains("Convert("))
                    {
                        string replacementValue = Expression.Lambda(expression).Compile().DynamicInvoke().ToString();
                        if (!replacements.ContainsKey(replacementExpression))
                        {
                            replacements.Add(replacementExpression, replacementValue.ToString());
                        }
                    }
                    break;

                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.OrElse:
                case ExpressionType.AndAlso:
                case ExpressionType.Equal:
                    var bexp = expression as BinaryExpression;
                    WalkExpression(replacements, bexp.Left);
                    WalkExpression(replacements, bexp.Right);
                    break;

                case ExpressionType.Call:
                    var mcexp = expression as MethodCallExpression;
                    foreach (var argument in mcexp.Arguments)
                    {
                        WalkExpression(replacements, argument);
                    }
                    break;

                case ExpressionType.Lambda:
                    var lexp = expression as LambdaExpression;
                    WalkExpression(replacements, lexp.Body);
                    break;

                case ExpressionType.Constant:
                    //do nothing
                    break;

                default:
                    Trace.WriteLine("Unknown type");
                    break;
            }
        }
    }
}
