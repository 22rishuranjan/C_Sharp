using System;

namespace FluentBuilderInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fluent Builder Inheritance!");

            var me = Person
                .New()
                .Called("Rishu")
                .WorksAsA("Programmer")
                .Build();
            Console.WriteLine(me);

                
        }
    }
    public class Builder : PersonJobBuilder<Builder>
    {

    }


    public class Person
    {
        public string Name, Position;

        public override string ToString()
        {
            return $"{nameof(Name)}:{Name}, {nameof(Position)}:{Position}";
        }

        public static Builder New() => new Builder();

    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<self>
        :PersonBuilder
        where self :PersonInfoBuilder<self>
    {
        
        public self Called(string name)
        {
            person.Name = name;
            return (self) this;
        }
    }

    public class PersonJobBuilder<self>
        :PersonInfoBuilder<PersonJobBuilder<self>>
        where self :PersonJobBuilder<self>
    {
        

        public self WorksAsA(string position)
        {
            person.Position = position;
            return (self) this;
        }
    }
}


