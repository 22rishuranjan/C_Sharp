using System;

namespace LiskovSubstitution
{
    class Program
    { 
        public static int Area(Rectangle r) => r.Height * r.Width;
        public static int Area(LiskovRectangle r) => r.Height * r.Width;

        static void Main(string[] args)
        {
            Console.WriteLine("Liskov Substitution demo!");

            Rectangle rec = new Rectangle(2, 3);
            Console.WriteLine($"{rec} has area : {Area(rec)}.");

            Square sq = new Square();
            sq.Width = 2;
            Console.WriteLine($"{sq} has area : {Area(sq)}.");


            Rectangle sq1 = new Square();
            sq1.Width = 3;
            Console.WriteLine($"{sq1} has area : {Area(sq1)}.");

            LiskovRectangle sq2 = new LiskovSquare();
            sq2.Width = 3;
            Console.WriteLine($"{sq2.ToString()} has area : {Area(sq2)}.");


        }
    }

    #region "without Liskov substitution"
    public class Rectangle
    {

        private int height;
        private int width;
        public int Height { get; set; }
        public int Width {
            get { return width; }
            set
            {
                //Width = value; // infinite loop
                width = value;
            }
        }

        public Rectangle()
        {
        }
        public Rectangle(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}:{Height}";
        }
    }

    public class Square :Rectangle
    {
        public new int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

        public new int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

    }

    #endregion


    #region "with Liskov substitution"
    public class LiskovRectangle
    {
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }

        public LiskovRectangle()
        {
        }
        public LiskovRectangle(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}:{Height}";
        }
    }

    public class LiskovSquare : LiskovRectangle
    {
        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

    }

    #endregion
}
