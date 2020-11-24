using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Builder Pattern!");

            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Hello");
            builder.AddChild("li", "World!");
            builder.ToString();
            Console.WriteLine(builder);


            //fluent builder
            var fluentBuilder = new HtmlBuilder("ul");
            fluentBuilder
                .AddChild("li", "Hello")
                .AddChild("li", "World!")
                .AddChild("li", "Fluent Builder.")
                .ToString();
            Console.WriteLine(fluentBuilder);
        }
    }

    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();

        public const int indentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        private string ToStringImp(int indent)
        {
            StringBuilder sb = new StringBuilder();
            var i = new string(' ', indent * indentSize);
            sb.AppendLine($"{i} <{Name}");
            if(!String.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new String(' ', (indent + 1) * indentSize));
                sb.AppendLine(Text);
            }

            foreach(var elem in Elements)
            {
                sb.Append(elem.ToStringImp(indent + 1));
            }
            sb.AppendLine($"{i} </{Name}");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImp(0);

        }


    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        //public void AddChild(string childName, string childText)
        //{
        //    var e = new HtmlElement(childName, childText);
        //    root.Elements.Add(e);
        //}

        

        //Fluent Builder
        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }



        public override string ToString()
        {
            return root.ToString(); 
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }
}
