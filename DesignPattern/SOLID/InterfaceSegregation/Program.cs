using System;

namespace InterfaceSegregation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Interface Segregation Demo!");
        }
    }


    public class Document
    {

    }

    #region "without inteface segregation

    public interface IMachine
    {
        public void Print(Document d);
        public void Scan(Document d);
        public void Fax(Document d);

    }

    public class MutifunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //used
        }

        public void Print(Document d)
        {
            //used
        }

        public void Scan(Document d)
        {
            //used
        }
    }


    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }

        public void Print(Document d)
        {
            //used
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }


    #endregion

    #region "with inteface segregation
    //interface should have single functionality

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class PhotoCopier : IScanner, IPrinter
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice: IPrinter, IScanner
    {

    }
    #endregion


    public class MultiFunctionalDevice: IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionalDevice(IPrinter printer, IScanner scanner)
        {

            this.printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }

}
