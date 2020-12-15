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
            var mX = new bool[36];
            long mOr = 0;

            while ((line = sr.ReadLine()) != null)
            {
                var tokens = line.Split();
                if (tokens[0] == "mask")
                {
                    mX = new bool[36];
                    mOr = 0;
                    for (int i = 0; i < 36; i++)
                    {
                        if (tokens[2][i] == '1')
                        {
                            mOr += (long)Math.Pow(2, 35 - i);
                        }
                        else if (tokens[2][i] == 'X')
                        {
                            mX[i] = true;
                        }
                    }
                }
                else
                {
                    var addr = long.Parse(tokens[0].Replace("mem[", "").Replace("]", "").Trim());
                    var value = long.Parse(tokens[2]);
                    addr = addr | mOr;
                    var addresses = ApplyFloatingMask(0, mX, addr);
                    foreach (long m in addresses)
                    {
                        if (!registers.ContainsKey(m))
                            registers.Add(m, value);
                        else
                            registers[m] = value;
                    }
                }
            }

            long sum = 0;
            foreach (KeyValuePair<long, long> kvp in registers)
            {
                sum += kvp.Value;
            }

            Console.WriteLine(sum);
        }

        static List<long> ApplyFloatingMask(long index, bool[] mask, long baseAddr)
        {
            var result = new List<long>();

            if (index == mask.Length - 1)
            {
                if (mask[index])
                {
                    result.Add(1);
                    result.Add(0);
                }
                else
                {
                    result.Add(baseAddr & 1);
                }
            }
            else
            {
                var temp = ApplyFloatingMask(index + 1, mask, baseAddr);

                if (mask[index])
                {
                    for (int i = 0; i < temp.Count; i++)
                    {
                        result.Add(temp[i]);
                        result.Add(temp[i] + (long)Math.Pow(2, 35 - index));
                    }
                }
                else
                {
                    foreach (long l in temp)
                        result.Add((baseAddr & ((long)Math.Pow(2, 35 - index))) + l);
                }
            }
            return result;
        }
    }
}
