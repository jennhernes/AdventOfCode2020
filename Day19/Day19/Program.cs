using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            string rule0 = "";
            int numValid = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(':'))
                {
                    tokens = line.Split(':');
                    input.Add(int.Parse(tokens[0]), "");
                    input[int.Parse(tokens[0])] += tokens[1].Trim();
                }
                else if (line == "")
                {
                    donerules = true;
                    var ruleNos = new List<int>();
                    foreach (KeyValuePair<int, string> kvp in input)
                    {
                        ruleNos.Add(kvp.Key);
                    }
                    ruleNos.Sort((x, y) => y.CompareTo(x));
                    rule0 = "";
                    foreach (int i in ruleNos)
                    {
                        var thisRule = "";
                        if (input[i].Contains('"'))
                        {
                            thisRule = input[i][1].ToString();
                        }
                        else
                        {
                            tokens = input[i].Split();

                            if (i == 0)
                                thisRule += "^";
                            thisRule += "(";
                            for (int j = 0; j < tokens.Length; j++)
                            {
                                string s = tokens[j];
                                if (s == "|")
                                    thisRule += s;
                                else
                                {
                                    thisRule += input[int.Parse(s)];
                                }
                            }
                            thisRule += "){1}";
                            if (i == 0)
                                thisRule += "$";
                        }
                        input[i] = thisRule;
                    }
                    rule0 = input[0];
                    Console.WriteLine(input[0]);
                }
                else
                {
                    if(IsValidMessage(line, rule0))
                    {
                        numValid++;
                    }
                }
            }

            Console.WriteLine(numValid);
        }

        static bool IsValidMessage(string message, string pattern)
        {
            return Regex.IsMatch(message, pattern);
        }
    }
}
