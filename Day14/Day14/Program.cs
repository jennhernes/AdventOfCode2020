using System;
using System.IO;
using System.Collections.Generic;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Part1();
            Part2();
        }

        static void Part1()
        {
            var filename = "../../../../input.txt";
            var sr = new StreamReader(filename);
            var registers = new Dictionary<long, long>();

            string line;
            long mAnd = long.MaxValue;
            long mOr = 0;

            while ((line = sr.ReadLine()) != null)
            {
                var tokens = line.Split();
                if (tokens[0] == "mask")
                {
                    mAnd = (long)Math.Pow(2, 36) - 1;
                    mOr = 0;
                    for (int i = 0; i < 36; i++)
                    {
                        if (tokens[2][i] == '1')
                        {
                            mOr += (long)Math.Pow(2, 35 - i);
                        }
                        else if (tokens[2][i] == '0')
                        {
                            mAnd -= (long)Math.Pow(2, 35 - i);
                        }
                    }
                }
                else
                {
                    var addr = long.Parse(tokens[0].Replace("mem[", "").Replace("]", "").Trim());
                    var value = long.Parse(tokens[2]);
                    value = value & mAnd;
                    value = value | mOr;
                    if (!registers.ContainsKey(addr))
                        registers.Add(addr, value);
                    else
                        registers[addr] = value;
                }
            }

            long sum = 0;
            foreach (KeyValuePair<long, long> kvp in registers)
            {
                sum += kvp.Value;
            }

            Console.WriteLine(sum);
        }

        static void Part2()
        {
            var filename = "../../../../input.txt";
            var sr = new StreamReader(filename);
            var registers = new Dictionary<long, long>();

            string line;
            long mAnd = long.MaxValue;
            long mOr = 0;

            while ((line = sr.ReadLine()) != null)
            {
                var tokens = line.Split();
                if (tokens[0] == "mask")
                {
                    mAnd = (long)Math.Pow(2, 36) - 1;
                    mOr = 0;
                    for (int i = 0; i < 36; i++)
                    {
                        if (tokens[2][i] == '1')
                        {
                            mOr += (long)Math.Pow(2, 35 - i);
                        }
                        else if (tokens[2][i] == '0')
                        {
                            mAnd -= (long)Math.Pow(2, 35 - i);
                        }
                    }
                }
                else
                {
                    var addr = long.Parse(tokens[0].Replace("mem[", "").Replace("]", "").Trim());
                    var value = long.Parse(tokens[2]);
                    value = value & mAnd;
                    value = value | mOr;
                    if (!registers.ContainsKey(addr))
                        registers.Add(addr, value);
                    else
                        registers[addr] = value;
                }
            }

            long sum = 0;
            foreach (KeyValuePair<long, long> kvp in registers)
            {
                sum += kvp.Value;
            }

            Console.WriteLine(sum);
        }
    }
    }
}
