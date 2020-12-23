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

            Dictionary<int, List<string>> rules = new Dictionary<int, List<string>>();

            string line;
            bool donerules = false;
            string[] tokens;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(':'))
                {
                    tokens = line.Split(':');
                    rules.Add(int.Parse(tokens[0]), new List<string>());
                    rules[int.Parse(tokens[0])].Add(tokens[1].Trim());
                }
                else
                {
                    if (!donerules)
                    {
                        donerules = true;
                        for (int i = rules.Count - 1; i >= 0; i--)
                        {
                            if (rules[i][0].Contains('"'))
                            {
                                string letters = rules[i][0].Substring(1, 1);
                                rules[i].Add(letters);
                            }
                            else
                            {
                                int index = 1;
                                rules[i].Add("");
                                for (int j = 0; j < rules[i].Count; j++)
                                {
                                    tokens = rules[i][0].Split();

                                    foreach(string s in tokens)
                                    {
                                        if (s == "|")
                                        {
                                            index++;
                                            rules[i].Add("");
                                        }
                                        else
                                        {
                                            int num = int.Parse(tokens[j]);
                                            for (int k = 0; k < rules[num].Count; k++)
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
        }
    }
}
