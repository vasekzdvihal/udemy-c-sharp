using System;
using System.Text;

namespace Visitor.DynamicVisitor
{
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

    public class ExpressionPrinter
    {
        public void Print(AdditionExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)ae.Left, sb);
            sb.Append("+");
            Print((dynamic)ae.Right, sb);
            sb.Append(")");
        }
        
        public void Print(DoubleExpression de, StringBuilder sb)
        {
            sb.Append(de.Value);
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

            var ep = new ExpressionPrinter();
            var sb = new StringBuilder();
            
            ep.Print((dynamic)e, sb);

            Console.WriteLine(sb.ToString());
        }
    }
}