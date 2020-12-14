using System;
using System.IO;
using System.Collections.Generic;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }

        static void Part1()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;
            int earliest;
            line = sr.ReadLine();
            earliest = int.Parse(line);
            line = sr.ReadLine();
            List<int> busses = new List<int>();
            var tokens = line.Split(',');
            foreach(string s in tokens)
            {
                if (s != "x")
                    busses.Add(int.Parse(s));
            }

            int minTime = int.MaxValue;
            int busID = 0;
            foreach(int b in busses)
            {
                if (b - (earliest % b) < minTime)
                {
                    minTime = b - (earliest % b);
                    busID = b;
                }
            }

            Console.WriteLine(minTime * busID);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;
            line = sr.ReadLine();
            line = sr.ReadLine();
            Dictionary<int, int> busses = new Dictionary<int, int>();
            var tokens = line.Split(',');
            long offset = 0;
            foreach (string s in tokens)
            {
                if (s != "x")
                    busses.Add((int)offset, int.Parse(s));

                offset++;
            }

            long multiple = 1;
            long jump = 1;
            offset = 0;
            bool found = false;
            long time = 0;

            foreach (KeyValuePair<int, int> kvp in busses)
            {
                found = false;
                multiple = 1;
                while (!found)
                {
                    time = jump * multiple + offset;
                    if ((kvp.Value - (time % kvp.Value)) % kvp.Value == kvp.Key)
                    {
                        found = true;
                        offset = time;
                        jump = jump * kvp.Value;
                    }
                    else
                    {
                        multiple++;
                    }
                }
            }

            //while (!found)
            //{
            //    time = busses[0] * multiple;
            //    found = true;
            //    foreach (KeyValuePair<int, int> kvp in busses)
            //    {
            //        if ((kvp.Value - (time % kvp.Value)) % kvp.Value != kvp.Key)
            //        {
            //            found = false;
            //            break;
            //        }
            //    }
            //    multiple++;
            //}

            Console.WriteLine(time);
        }
    }
}
