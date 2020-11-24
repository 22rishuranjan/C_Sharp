using System;
using System.Collections.Generic;

namespace OpenClosedPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("OpenClosedPrinciple Demo!");

            var apple = new Product("Apple", Color.Green, Size.small);
            var mango = new Product("Mango", Color.Yellow, Size.medium);
            var House = new Product("House", Color.Red, Size.Large);

            Product[] products = { apple, mango, House };
            var pf = new ProductFilter();
            Console.WriteLine($"Green Products(old)");

            foreach(var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($"-{p.Name} is green.");
            }

            BetterFilter bf = new BetterFilter();
            Console.WriteLine($"Green Products(new)");

            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"-{p.Name} is green.");
            }

            Console.WriteLine("large red item");

            foreach (var p in bf.Filter
                             (products,
                              new AndSpecification<Product>
                                   (new ColorSpecification(Color.Red),
                                   new SizeSpecification(Size.Large)
                                   )))

            {
                Console.WriteLine($"-{p.Name} is large and red.");

            }
        }
       
    }

    public enum Color
    {
        Red, Blue, Green, Yellow
    }

    public enum Size
    {
        small, medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {

            foreach (var p in products)
            {
                if (p.Size == size)
                {
                    yield return p;
                }
            }

        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {

            foreach (var p in products)
            {
                if (p.Color == color)
                {
                    yield return p;
                }
            }

        }

        public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
        {

            foreach (var p in products)
            {
                if (p.Color == color && p.Size == size)
                {
                    yield return p;
                }
            }

        }
    }
     #region "openclosed implementation"

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }
        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color color;

            public ColorSpecification(Color color)
            {
                this.color = color;
            }
            public bool IsSatisfied(Product t)
            {
                return t.Color == color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }
            public bool IsSatisfied(Product t)
            {
                return t.Size == size;
            }
        }


    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> spec1, spec2;

        public AndSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
        {
            //if(spec1 == null)
            //{
            //    throw new ArgumentNullException(paramName: nameof(spec1));
            //}
            //if (spec2 == null)
            //{
            //    throw new ArgumentNullException(paramName: nameof(spec2));
            //}

            this.spec1 = spec1?? throw new ArgumentNullException(paramName: nameof(spec1));
            this.spec2 = spec2?? throw new ArgumentNullException(paramName: nameof(spec1));
        }
        

        public bool IsSatisfied(T t)
        {
            return spec1.IsSatisfied(t) && spec2.IsSatisfied(t);
        }
    }


    public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
               foreach(var i in items)
                {
                    if (spec.IsSatisfied(i))
                    {
                        yield return i;
                    }
                }
            }
        }



      #endregion


    }

