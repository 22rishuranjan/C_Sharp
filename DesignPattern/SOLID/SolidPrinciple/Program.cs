using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SolidPrinciple
{
    class Program
    {
        static int Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            Journal j = new Journal();
            j.AddEntry("I am R2");
            j.AddEntry("I serve master Luke");

            Console.WriteLine(j);
            Persistence p = new Persistence();
            var fileName = @"D:\myjournal1.txt";
            p.SaveToFile(j, fileName, true);
            Console.ReadLine();

           // Process.Start(fileName);

            return 0;
        }
    }

    internal class Journal
    {
        public readonly List<string> entries = new List<string>();
        private static int count;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}:{text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine,entries);
        }

        //voilates Single Responsibility Principle

        //public void save(String fileName)
        //{
        //    File.WriteAllText(fileName, ToString());
        //}

        //public void Load(String fileName)
        //{

        //}

    }

    internal class Persistence
    {
        public void SaveToFile(Journal j, String fileName,  Boolean Overwrite = false)
        {
            if(Overwrite || !File.Exists(fileName))
            {
                File.WriteAllText(fileName, ToString());
            }

            
        }
    }
}
