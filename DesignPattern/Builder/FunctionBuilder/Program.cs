using System;
using System.Collections.Generic;

namespace FunctionBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Person
    {
        public string Name, Position;
    }

    public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();
    
        private PersonBuilder AddAction(Action<Person> action)
        {
            action.Add(p => { action(p); return p; });
            return this;
        }
    } 
}
