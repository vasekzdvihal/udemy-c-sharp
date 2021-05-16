// using System;
// using System.ComponentModel;
// using System.Runtime.CompilerServices;
// using Observer.Collections.Annotations;
//
// namespace Observer.Collections
// {
//     public class Market : INotifyPropertyChanged
//     {
//         private float valatility;
//
//         public float Valatility
//         {
//             get => valatility;
//             set
//             {
//                 if (value.Equals(valatility)) return;
//                 valatility = value;
//                 OnPropertyChanged();
//             }
//         }
//
//         public event PropertyChangedEventHandler PropertyChanged;
//
//         [NotifyPropertyChangedInvocator]
//         protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//         {
//             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//         }
//     }
//     
//     static class Program
//     {
//         public static void Main(string[] args)
//         {
//             var market = new Market();
//             market.PropertyChanged += (sender, eventArgs) =>
//             {
//                 if (eventArgs.PropertyName == "Volatility")
//                 {
//                     Console.WriteLine("Volatility was changed");
//                 }
//             };
//         }
//     }
// }