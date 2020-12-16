using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day16
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

            var rules = new Dictionary<string, List<int>>();
            var section = Notes.fields;
            var sum = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "" && (line = sr.ReadLine()) != null)
                    section = (Notes)(((int)section + 1) % 3);
                else
                {

                    if (section == Notes.fields)
                    {
                        var tokens = line.Split();
                        int i = 0;
                        string field = "";
                        for (; i < tokens.Length; i++)
                        {
                            field += tokens[i] + " ";
                            if (tokens[i].Contains(":"))
                            {
                                field = field.Substring(0, field.Length - 2);
                                i++;
                                break;
                            }
                        }

                        var validNums = new List<int>();
                        rules.Add(field, validNums);
                        for (; i < tokens.Length; i++)
                        {
                            if (tokens[i] != "or")
                            {
                                int low = int.Parse(tokens[i].Split('-')[0]);
                                int high = int.Parse(tokens[i].Split('-')[1]);
                                for (int j = low; j < high + 1; j++)
                                {
                                    validNums.Add(j);
                                }
                            }
                        }
                    }
                    else if (section == Notes.othertickets)
                    {
                        var values = line.Split(',');
                        foreach (string s in values)
                        {
                            var v = int.Parse(s);
                            bool valid = false;
                            foreach (KeyValuePair<string, List<int>> kvp in rules)
                            {
                                if (kvp.Value.Contains(v))
                                {
                                    valid = true;
                                    break;
                                }
                            }
                            if (!valid)
                                sum += v;
                        }
                    }
                }
            }
            Console.WriteLine(sum);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;

            var rules = new Dictionary<string, List<int>>();
            var fieldOrder = new Dictionary<int, List<string>>();
            var mine = new List<int>();
            var section = Notes.fields;
            var product = 1L;
            int index = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "" && (line = sr.ReadLine()) != null)
                    section = (Notes)(((int)section + 1) % 3);
                else
                {

                    if (section == Notes.fields)
                    {
                        fieldOrder.Add(index, new List<string>());
                        index++;
                        var tokens = line.Split();
                        int i = 0;
                        string field = "";
                        for (; i < tokens.Length; i++)
                        {
                            field += tokens[i] + " ";
                            if (tokens[i].Contains(":"))
                            {
                                field = field.Substring(0, field.Length - 2);
                                i++;
                                break;
                            }
                        }

                        var validNums = new List<int>();
                        rules.Add(field, validNums);
                        for (; i < tokens.Length; i++)
                        {
                            if (tokens[i] != "or")
                            {
                                int low = int.Parse(tokens[i].Split('-')[0]);
                                int high = int.Parse(tokens[i].Split('-')[1]);
                                for (int j = low; j < high + 1; j++)
                                {
                                    validNums.Add(j);
                                }
                            }
                        }
                    }
                    else if (section == Notes.myticket)
                    {
                        var temp = line.Split(',');
                        foreach (string s in temp)
                        {
                            mine.Add(int.Parse(s));
                        }

                        foreach (KeyValuePair<string, List<int>> kvp in rules)
                        {
                            for (int j = 0; j < fieldOrder.Count; j++)
                            {
                                fieldOrder[j].Add(kvp.Key);
                            }
                        }

                    }
                    else if (section == Notes.othertickets)
                    {
                        var values = line.Split(',');
                        if (IsValidTicket(rules, values))
                        {
                            for (int j = 0; j < values.Length; j++)
                            {
                                var v = int.Parse(values[j]);
                                foreach (KeyValuePair<string, List<int>> kvp in rules)
                                {
                                    if (!kvp.Value.Contains(v))
                                    {
                                        fieldOrder[j].Remove(kvp.Key);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            while (fieldOrder.Count(x => x.Value.Count > 1) > 1)
            {
                for (int i = 0; i < fieldOrder.Count; i++)
                {
                    var list = fieldOrder[i];
                    if (list.Count == 1)
                    {
                        for (int j = 0; j < fieldOrder.Count; j++)
                        {
                            if (i != j)
                                fieldOrder[j].Remove(list[0]);
                        }
                    }
                }
            }

            for (int i = 0; i < fieldOrder.Count; i++)
            {
                if (fieldOrder[i][0].Contains("departure"))
                    product *= mine[i];
            }
            Console.WriteLine(product);
        }

        static bool IsValidTicket(Dictionary<string, List<int>> rules, string[] values)
        {
            bool valid = false;
            foreach (string s in values)
            {
                var v = int.Parse(s);
                valid = false;
                foreach (KeyValuePair<string, List<int>> kvp in rules)
                {
                    if (kvp.Value.Contains(v))
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid) break;
            }

            return valid;
        }
    }

    public enum Notes { fields, myticket, othertickets }
}
