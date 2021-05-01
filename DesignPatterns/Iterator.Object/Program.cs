using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Iterator.Object
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T>
    {
        private readonly Node<T> root;
        public Node<T> Current;
        private bool yieldedStart;

        public InOrderIterator(Node<T> root)
        {
            this.root = root;
            Current = root;

            while (Current.Left != null)
                Current = Current.Left;
            
            //    1  <- root
            //  /  \
            //  2  3
            //  ^ Current
        }

        public bool MoveNext()
        {
            if (!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }

            if (Current.Right != null)
            {
                Current = Current.Right;
                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }
            else
            {
                var p = Current.Parent;
                while (p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }

                Current = p;
                return Current != null;
            }
        }

        public void Reset()
        {
            Current = root;
            yieldedStart = false;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            //    1 
            //  /  \
            //  2  3
            // in-order: 213

            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));

            var it = new InOrderIterator<int>(root);
            while (it.MoveNext())
            {
                Console.Write(it.Current.Value);
                Console.Write(",");
            }

            Console.WriteLine();
        }
    }
}