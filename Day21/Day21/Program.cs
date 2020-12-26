using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day21
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
            var allergens = new Dictionary<string, List<string>>();
            var ingredients = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace(",", "").Replace(")", "");
                var tokens = line.Split().ToList();
                int index = tokens.FindIndex(x => x == "(contains");
                if (index == -1) index = tokens.Count;
                if (line.Contains("contains"))
                {
                    for (int i = index + 1; i < tokens.Count; i++)
                    {
                        var allergen = tokens[i];
                        if (!allergens.ContainsKey(allergen))
                        {
                            allergens.Add(allergen, new List<string>());
                            for (int j = 0; j < index; j++)
                            {
                                allergens[allergen].Add(tokens[j]);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < allergens[allergen].Count; j++)
                            {

                                if (!tokens.Contains(allergens[allergen][j]))
                                {
                                    allergens[allergen].RemoveAt(j);
                                    j--;
                                }
                            }
                        }
                    }
                }
                for (int j = 0; j < index; j++)
                {
                    ingredients.Add(tokens[j]);
                }
            }

            foreach (KeyValuePair<string, List<string>> kvp in allergens)
            {
                foreach (string s in kvp.Value)
                {
                    ingredients.RemoveAll(x => x == s);
                }
            }

            Console.WriteLine(ingredients.Count);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;
            var temp = new List<string>();
            var allergens = new Dictionary<string, List<string>>();

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace(",", "").Replace(")", "");
                var tokens = line.Split().ToList();
                int index = tokens.FindIndex(x => x == "(contains");
                if (index == -1) index = tokens.Count;
                if (line.Contains("contains"))
                {
                    for (int i = index + 1; i < tokens.Count; i++)
                    {
                        var allergen = tokens[i];
                        if (!allergens.ContainsKey(allergen))
                        {
                            temp.Add(allergen);
                            allergens.Add(allergen, new List<string>());
                            for (int j = 0; j < index; j++)
                            {
                                allergens[allergen].Add(tokens[j]);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < allergens[allergen].Count; j++)
                            {

                                if (!tokens.Contains(allergens[allergen][j]))
                                {
                                    allergens[allergen].RemoveAt(j);
                                    j--;
                                }
                            }
                        }
                    }
                }
            }

            temp.Sort();

            while (allergens.Count(x => x.Value.Count > 1) > 1)
            {
                foreach (string s in temp)
                {
                    if (allergens[s].Count == 1)
                    {
                        foreach (string s2 in temp)
                        {
                            if (s != s2)
                                allergens[s2].Remove(allergens[s][0]);
                        }
                    }
                }
            }

            foreach (string s in temp)
            {
                Console.Write(allergens[s][0] + ",");
            }    

            Console.WriteLine();
        }
    }
}
