using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day08
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

            List<string> instr = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instr.Add(line);
            }

            List<int> prev = new List<int>();
            int acc = 0;
            int i = 0;
            while (!prev.Contains(i) && i < instr.Count)
            {
                prev.Add(i);
                string[] tokens = instr[i].Split();
                switch (tokens[0])
                {
                    case "nop":
                        i++;
                        break;
                    case "acc":
                        acc += Convert.ToInt32(tokens[1]);
                        i++;
                        break;
                    case "jmp":
                        i += Convert.ToInt32(tokens[1]);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine(acc);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            List<string> instr = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instr.Add(line);
            }

            List<int> prev = new List<int>();
            int acc = 0;
            int i = 0;
            int indexChanged = 0;
            string temp = "";
            while (i < instr.Count)
            {
                prev.Clear();
                acc = 0;
                i = 0;
                int prevIndex = indexChanged;
                indexChanged = instr.FindIndex(prevIndex + 1, x => (x.Split()[0] == "nop" || x.Split()[0] == "jmp"));
                if (instr[indexChanged].Substring(0, 3) == "nop")
                {
                    temp = "jmp ";
                    temp += instr[indexChanged].Split()[1];
                    instr[indexChanged] = temp;
                }
                else
                {
                    temp = "nop ";
                    temp += instr[indexChanged].Split()[1];
                    instr[indexChanged] = temp;
                }
                while (!prev.Contains(i) && i < instr.Count)
                {
                    prev.Add(i);
                    string[] tokens = instr[i].Split();
                    switch (tokens[0])
                    {
                        case "nop":
                            i++;
                            break;
                        case "acc":
                            acc += Convert.ToInt32(tokens[1]);
                            i++;
                            break;
                        case "jmp":
                            i += Convert.ToInt32(tokens[1]);
                            break;
                        default:
                            break;
                    }
                }
                temp = instr[indexChanged].Split()[1];
                if (instr[indexChanged][0] == 'n')
                    instr[indexChanged] = "jmp " + temp;
                else
                    instr[indexChanged] = "nop " + temp;

            }
            Console.WriteLine(acc);
        }
    }
}
