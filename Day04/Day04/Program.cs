using System;
using System.Collections.Generic;
using System.IO;

namespace Day04
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

            List<string> keys = new List<string>();
            keys.Add("byr");
            keys.Add("iyr");
            keys.Add("eyr");
            keys.Add("hgt");
            keys.Add("hcl");
            keys.Add("ecl");
            keys.Add("pid");
            keys.Add("cid");

            string line;
            int numValid = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line != "")
                {
                    bool[] fields = new bool[keys.Count];

                label1:
                    string[] tokens = line.Split();

                    foreach (string s in tokens)
                    {
                        string[] kvp = s.Split(':');
                        int index = keys.IndexOf(kvp[0]);
                        fields[index] = true;
                    }
                    if (sr.Peek() != '\n')
                    {
                        if ((line = sr.ReadLine()) != null)
                            goto label1;
                    }

                    int sum = 0;
                    for (int i = 0; i < fields.Length - 1; i++)
                    {
                        if (fields[i])
                            sum++;
                    }
                    if (sum == 7)
                        numValid++;
                }
            }

            Console.WriteLine(numValid);
        }

        static void Part2()
        {

            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            List<string> keys = new List<string>();
            keys.Add("byr");
            keys.Add("iyr");
            keys.Add("eyr");
            keys.Add("hgt");
            keys.Add("hcl");
            keys.Add("ecl");
            keys.Add("pid");
            keys.Add("cid");

            string line;
            int numValid = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line != "")
                {
                    bool[] fields = new bool[keys.Count];

                label:
                    string[] tokens = line.Split();

                    foreach (string s in tokens)
                    {
                        string[] kvp = s.Split(':');
                        int index = keys.IndexOf(kvp[0]);
                        int year;
                        string hex;
                        switch (index)
                        {
                            case 0:
                                if (kvp[1].Length == 4)
                                {
                                    year = Convert.ToInt32(kvp[1]);
                                    if (year >= 1920 && year <= 2002)
                                        fields[index] = true;
                                }
                                break;
                            case 1:
                                if (kvp[1].Length == 4)
                                {
                                    year = Convert.ToInt32(kvp[1]);
                                    if (year >= 2010 && year <= 2020)
                                        fields[index] = true;
                                }
                                break;
                            case 2:
                                if (kvp[1].Length == 4)
                                {
                                    year = Convert.ToInt32(kvp[1]);
                                    if (year >= 2020 && year <= 2030)
                                        fields[index] = true;
                                }
                                break;
                            case 3:
                                try
                                {
                                    int height = Convert.ToInt32(kvp[1].Substring(0, kvp[1].Length - 2));
                                    string dim = kvp[1].Substring(kvp[1].Length - 2);
                                    if ((dim == "cm" && height >= 150 && height <= 193) ||
                                        (dim == "in" && height >= 59 && height <= 76))
                                    {
                                        fields[index] = true;
                                    }
                                }
                                catch (Exception e)
                                { }
                                break;
                            case 4:
                                if (kvp[1][0] == '#')
                                {
                                    if (kvp[1].Length == 7)
                                    {
                                        hex = kvp[1].Substring(1, kvp[1].Length - 1);
                                        fields[index] = true;
                                        foreach (char c in hex)
                                        {
                                            if (!(c >= '0' && c <= '9') && !(c >= 'a' && c <= 'f'))
                                            {
                                                fields[index] = false;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 5:
                                string val = kvp[1];
                                if (val == "amb" || val == "blu" || val == "brn" || val == "gry" ||
                                    val == "grn" || val == "hzl" || val == "oth")
                                {
                                    fields[index] = true;
                                }
                                break;
                            case 6:
                                if (kvp[1].Length == 9)
                                {
                                    fields[index] = true;

                                    foreach (char c in kvp[1])
                                    {
                                        if (c < '0' || c > '9')
                                        {
                                            fields[index] = false;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (sr.Peek() != '\n')
                    {
                        if ((line = sr.ReadLine()) != null)
                            goto label;
                    }

                    int sum = 0;
                    for (int i = 0; i < fields.Length - 1; i++)
                    {
                        if (fields[i])
                            sum++;
                    }
                    if (sum == 7)
                        numValid++;
                }
            }

            Console.WriteLine(numValid);
        }
    }
}
