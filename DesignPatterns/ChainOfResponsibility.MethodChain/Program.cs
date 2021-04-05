using System;
using System.Reflection.Metadata;

namespace ChainOfResponsibility.MethodChain
{
    public class Creature
    {
        public string Name;
        public int Attact, Defense;

        public Creature(string name, int attact, int defense)
        {
            Name = name;
            Attact = attact;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attact)}: {Attact}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature;
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null) next.Add(cm);
            else next = cm;
        }

        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttactModifier : CreatureModifier
    {
        public DoubleAttactModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attact *= 2;
            base.Handle();
        }
    }

    public class IncresedDefenseModifier : CreatureModifier
    {
        public IncresedDefenseModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defense.");
            creature.Defense += 3;
            base.Handle();
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            //
        }
    }

    class Program
    { 
        static void Main(string[] args)
        {
            var goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);

            var root = new CreatureModifier(goblin);
            
            Console.WriteLine("Let's double the goblin's attack");
            root.Add(new DoubleAttactModifier(goblin));

            root.Add(new NoBonusesModifier(goblin));
            
            Console.WriteLine("Let's increase the goblin's defense.");
            root.Add(new IncresedDefenseModifier(goblin));
            
            root.Handle();
            Console.WriteLine(goblin);
        }
    }
}