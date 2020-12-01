using System;
using System.Collections.Generic;
using System.IO;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        static void Part1()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);
            List<int> input = new List<int>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                input.Add(Convert.ToInt32(line));
            }

            input.Sort();

            for (int i = 0; i < input.Count - 1; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    int sum = input[i] + input[j];
                    if (sum == 2020)
                    {
                        Console.WriteLine(input[i] * input[j]);
                    }
                    else if (sum > 2020)
                    {
                        break;
                    }
                }
            }
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);
            List<int> input = new List<int>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                input.Add(Convert.ToInt32(line));
            }

            input.Sort();

            for (int i = 0; i < input.Count - 2; i++)
            {
                for (int j = i + 1; j < input.Count - 1; j++)
                {
                    int sum = input[i] + input[j];
                    if (sum > 2020)
                    {
                        break;
                    }
                    for (int k = 0; k < input.Count; k++)
                    {
                        sum = input[i] + input[j] + input[k];
                        if (sum == 2020)
                        {
                            Console.WriteLine(input[i] * input[j] * input[k]);
                        }
                        else if (sum > 2020)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
