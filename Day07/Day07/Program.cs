using System;
using System.IO;
using System.Collections.Generic;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            // Part2();
        }

        static void Part1()
        {
            string filename = "../../../../test.txt";
            StreamReader sr = new StreamReader(filename);

            List<Bag> rules = new List<Bag>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split();
                string colour = "";
                int i = 0;
                for (i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i] == "bags")
                        i += 2;
                        break;

                    colour += tokens[i];
                }

                int index = rules.FindIndex(x => x.colour == colour);
                if (index == -1)
                {
                    rules.Add(new Bag(colour));
                    index = rules.Count - 1;
                }

                if (tokens[i] == "no")
                    continue;


            }
        }
    }

    class Bag : IComparable
    {
        public string colour;
        public List<string> inside;

        public Bag(string c)
        {
            colour = c;
            inside = new List<string>();
        }

        public int CompareTo(object obj)
        {
            Bag b = obj as Bag;

            return String.Compare(this.colour, b.colour, StringComparison.OrdinalIgnoreCase);
        }
    }
}
