using System;
using System.IO;
using System.Collections.Generic;

namespace Day10
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
            var filename = "../../../../input.txt";
            var sr = new StreamReader(filename);
            var adapters = new List<int>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                adapters.Add(int.Parse(line));
            }

            adapters.Sort();

            var diff1 = 0;
            var diff3 = 1;

            for (int i = 0; i < adapters.Count; i++)
            {
                int prev;
                if (i == 0)
                {
                    prev = 0;
                }
                else
                {
                    prev = adapters[i - 1];
                }

                if (adapters[i] - prev == 1) diff1++;
                else if (adapters[i] - prev == 3) diff3++;
            }

            Console.WriteLine(diff1 * diff3);
        }

        static void Part2()
        {
            var filename = "../../../../input.txt";
            var sr = new StreamReader(filename);
            var adapters = new List<int>();

            adapters.Add(0);

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                adapters.Add(int.Parse(line));
            }

            adapters.Sort();
            adapters.Add(adapters[adapters.Count - 1] + 3);

            Console.WriteLine(FindPaths(adapters.Count, adapters));
            // Console.WriteLine(numArr);
        }

        static long FindPaths(int n, List<int> adapters)
        {
            //int result = 0;
            //if (n < 0) return 0;
            //if (n == 0) return 1;
            //if (adapters[n] - adapters[n - 1] <= 3) result += Recursion(n - 1, adapters);
            //if (n > 1 && adapters[n] - adapters[n - 2] <= 3) result += Recursion(n - 2, adapters);
            //if (n > 2 && adapters[n] - adapters[n - 3] <= 3) result += Recursion(n - 3, adapters);
            //return result;
            var memo = new long[n];
            memo[0] = 1;
            for (int i = 1; i < n; i++)
            {
                if (adapters[i] - adapters[i - 1] <= 3)
                    memo[i] += memo[i - 1];

                if (i > 1 && adapters[i] - adapters[i - 2] <= 3)
                    memo[i] += memo[i - 2];

                if (i > 2 && adapters[i] - adapters[i - 3] <= 3)
                    memo[i] += memo[i - 3];
            }

            return memo[n - 1];
        }
    }
}
