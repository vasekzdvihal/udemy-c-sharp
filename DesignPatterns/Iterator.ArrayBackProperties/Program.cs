using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Iterator.ArrayBackProperties
{
    public class Creature : IEnumerable<int>
    {
        private int[] stats = new int[3];
        private const int strenght = 0;

        public int Strength
        {
            get => stats[strenght];
            set => stats[strenght] = value;
        }

        public int Agility { get; set; }
        public int Intelligence { get; set; }

        public double AverageStat => stats.Average();


        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int this[int index]
        {
            get { return stats[index]; }
            set { stats[index] = value; }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}