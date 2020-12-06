using System;
using System.IO;

namespace Day06
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
            string filename = "../../../../test.txt";
            StreamReader sr = new StreamReader(filename);

            string line;
            bool[] answers = new bool[26];
            int planeSum = 0;

            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    int groupSum = 0;
                    for (int i = 0; i < 26; i++)
                    {
                        if (answers[i])
                            groupSum++;
                    }
                    planeSum += groupSum;

                    answers = new bool[26];
                }
                foreach (char c in line)
                {
                    answers[c - 'a'] = true;
                }
            }
            Console.WriteLine(planeSum);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;
            bool[] answers = new bool[26];
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i] = true;
            }
            int planeSum = 0;

            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    int groupSum = 0;
                    for (int i = 0; i < 26; i++)
                    {
                        if (answers[i])
                            groupSum++;
                    }
                    planeSum += groupSum;

                    answers = new bool[26];
                    for (int i = 0; i < answers.Length; i++)
                    {
                        answers[i] = true;
                    }
                }
                else
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (!line.Contains((char)('a' + i)))
                        {
                            answers[i] = false;
                        }
                    }
                }
            }
            Console.WriteLine(planeSum);
        }
    }
}
