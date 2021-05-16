using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Observer.Bidirectional.Annotations;

namespace Observer.Bidirectional
{
    class Product : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{nameof(name)}: {name}, {nameof(Name)}: {Name}";
        }
    }

    public class Window : INotifyPropertyChanged
    {
        private string productName;

        public string ProductName
        {
            get => productName;
            set
            {
                if (value == productName) return;
                productName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{nameof(productName)}: {productName}, {nameof(ProductName)}: {ProductName}";
        }
    }

    public sealed class BidirectionalBinding : IDisposable
    {
        private bool disposed;
        
         // first second
         // firstProp, secondProp

         public BidirectionalBinding(INotifyPropertyChanged first, Expression<Func<object>> firstProperty, INotifyPropertyChanged second, Expression<Func<object>> secondProperty)
         {
             // xxxProperty is MemberExpression
             // Member ^^^ PropertyInfo
             if (firstProperty.Body is MemberExpression firstExpr && secondProperty.Body is MemberExpression secondExpr)
             {
                 if (firstExpr.Member is PropertyInfo firstProp && secondExpr.Member is PropertyInfo secondProp)
                 {
                     first.PropertyChanged += (sender, args) =>
                     {
                         if (!disposed)
                             secondProp.SetValue(second, firstProp.GetValue(first));
                     };

                     second.PropertyChanged += (sender, args) =>
                     {
                         if (!disposed)
                             firstProp.SetValue(first, secondProp.GetValue(second));
                     };
                 }
             }
         } 
         
        public void Dispose()
        {
            
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var product = new Product{ Name = "Book" };
            var window = new Window{ ProductName = "Book" };

            // product.PropertyChanged += (sender, eventArgs) =>
            // {
            //     if (eventArgs.PropertyName == "Name")
            //     {
            //         Console.WriteLine("Name changed id Product");
            //         window.ProductName = product.Name;
            //     }
            // };
            //
            // window.PropertyChanged += (sender, eventArgs) =>
            // {
            //     if (eventArgs.PropertyName == "ProductName")
            //     {
            //         Console.WriteLine("Name is changed in Window");
            //         product.Name = window.ProductName;
            //     }
            // }

            using var binding = new BidirectionalBinding(
                product,
                () => product.Name,
                window,
                () => window.ProductName
            );

            product.Name = "Smart Book";
            Console.WriteLine(product);
            Console.WriteLine(window);
            
            product.Name = "Really Smart Book";
            Console.WriteLine(product);
            Console.WriteLine(window);
        }
    }
}