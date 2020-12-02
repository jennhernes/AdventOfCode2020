using System;
using System.Collections.Generic;
using System.IO;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        public static void Part1()
        {
            string filename = "../../../../input.txt";

            StreamReader sr = new StreamReader(filename);

            List<Entry> input = new List<Entry>();

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split();
                Entry e = new Entry(Convert.ToInt32(s[0].Split('-')[0]), Convert.ToInt32(s[0].Split('-')[1]), Convert.ToChar(s[1].Substring(0, 1)), s[2]);
                input.Add(e);
            }

            int correct = 0;

            foreach (Entry e in input)
            {
                int count = 0;
                foreach (char c in e.password)
                {
                    if (c == e.letter)
                    {
                        count++;
                    }
                }

                if (count >= e.min && count <= e.max)
                {
                    correct++;
                }
            }

            Console.WriteLine(correct);
        }

        public static void Part2()
        {
            string filename = "../../../../input.txt";

            StreamReader sr = new StreamReader(filename);

            List<Entry> input = new List<Entry>();

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split();
                Entry e = new Entry(Convert.ToInt32(s[0].Split('-')[0]), Convert.ToInt32(s[0].Split('-')[1]), Convert.ToChar(s[1].Substring(0, 1)), s[2]);
                input.Add(e);
            }

            int correct = 0;

            foreach (Entry e in input)
            {
                bool count = false;

                if (e.password[e.min - 1] == e.letter) count = !count;
                if (e.password[e.max - 1] == e.letter) count = !count;
                
                if (count)
                {
                    correct++;
                }
            }

            Console.WriteLine(correct);
        }
    }

    public class Entry
    {
        public int min;
        public int max;
        public char letter;
        public string password;

        public Entry(int min, int max, char l, string p)
        {
            this.min = min;
            this.max = max;
            this.letter = l;
            this.password = p;
        }
    }
}
