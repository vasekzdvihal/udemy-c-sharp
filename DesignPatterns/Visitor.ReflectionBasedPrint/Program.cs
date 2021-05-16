using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.ReflectionBasedPrint
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>; 
    
    public abstract class Expression
    {
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            this.Value = value;
        }
    }
    
    public class AdditionExpression : Expression
    {
        public Expression Left;
        public Expression Right;

        public AdditionExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }
    }

    public static class ExpressionPrinter
    {
        // public static void Print(Expression e, StringBuilder sb)
        // {
        //     if (e is DoubleExpression de)
        //     {
        //         sb.Append(de.Value);
        //     } else if (e is AdditionExpression ae)
        //     {
        //         sb.Append("(");
        //         Print(ae.Left, sb);
        //         sb.Append("+");
        //         Print(ae.Right, sb);
        //         sb.Append(")");
        //     }
        // }

        private static DictType actions = new DictType
        {
            [typeof(DoubleExpression)] = (e, sb) =>
            {
                var de = (DoubleExpression) e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExpression)] = (e, sb) =>
            {
                var ae = (AdditionExpression) e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };

        public static void Print(Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var e = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)));

            var sb = new StringBuilder();
            //e.Print(sb);
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb);
        }
    }
}