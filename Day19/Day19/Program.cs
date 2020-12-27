using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day19
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
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            Dictionary<int, string> input = new Dictionary<int, string>();

            string line;
            bool donerules = false;
            string[] tokens;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(':'))
                {
                    tokens = line.Split(':');
                    input.Add(int.Parse(tokens[0]), "");
                    input[int.Parse(tokens[0])] += tokens[1].Trim();
                }
                else
                {
                    if (!donerules)
                    {
                        donerules = true;
                        List<string> rule0 = BuildRules(new List<string>(), input, 0, 0, 0);
                    }
                }
            }
        }

        static List<string> BuildRules(List<string> rules, Dictionary<int, string> input, int rule, int low, int high)
        {
            if (input[rule].Contains('"'))
            {
                for (int i = low; i < high; i++)
                {
                    rules[i] = input[rule][1].ToString();
                }
                return rules;
            }
            else
            {

            }

            return rules;
        }
    }
}
