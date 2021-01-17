using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Exercise
{
    class CodeField
    {
        public string Type, Name;
        public List<CodeField> Fields = new List<CodeField>();
        private const int indentSize = 2;

        public CodeField()
        {
            
        }

        public CodeField(string type, string name)
        {
            Type = type;
            Name = name;
        }

        private string ToStringImp(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);

            sb.AppendLine($"{i}public class {Name}");
            sb.AppendLine($"{i}{{");

            foreach (var f in Fields)
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine($"public {f.Type} {f.Name};");
            }

            sb.AppendLine($"{i}}}");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImp(0);
        }
    }

    class CodeBuilder
    {
        private readonly string rootname;
        private CodeField root = new CodeField();

        public CodeBuilder(string rootname)
        {
            this.rootname = rootname;
            root.Name = rootname;
        }

        public CodeBuilder AddField(string name, string type)
        {
            var f = new CodeField(type, name);
            root.Fields.Add(f);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new CodeField {Name = rootname};
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
