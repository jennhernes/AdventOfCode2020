using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day09
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
            List<A> prevNum = new List<A>();
            int len = 25;

            int newNum = 0;
            bool valid;
            int numInstr = 1;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                newNum = Convert.ToInt32(line);
                if (numInstr > len)
                {
                    valid = false;
                    if (newNum > prevNum[^1].num * 2)
                        break;

                    int low = 0;
                    int high = prevNum.Count - 1;
                    while (low < high)
                    {
                        int sum = prevNum[low].num + prevNum[high].num;
                        if (sum == newNum)
                        {
                            valid = true;
                            break;
                        }
                        else if (sum > newNum)
                        {
                            high--;
                        }
                        else
                        {
                            low++;
                        }
                    }
                    if (valid)
                    {
                        int lastLine = prevNum.Min(x => x.line);
                        prevNum.RemoveAll(x => x.line == lastLine);
                        prevNum.Add(new A(newNum, numInstr));
                        prevNum.Sort();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    prevNum.Add(new A(newNum, numInstr));
                    prevNum.Sort();
                }
                numInstr++;
            }
            Console.WriteLine(newNum);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);
            List<long> allInstr = new List<long>();
            long problem = 88311122;

            long answer = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                allInstr.Add(Convert.ToInt64(line));
                for (int i = 0; i < allInstr.Count; i++)
                {
                    long contSum = 0;
                    long min = long.MaxValue;
                    long max = long.MinValue;
                    for (int j = i; j < allInstr.Count; j++)
                    {
                        contSum += allInstr[j];
                        min = Math.Min(min, allInstr[j]);
                        max = Math.Max(max, allInstr[j]);
                        if (contSum == problem)
                        {
                            answer = min + max;
                            goto end;
                        }
                        if (contSum > problem)
                            break;
                    }
                }
            }
        end:
            Console.WriteLine(answer);
        }
    }

    class A : IComparable
    {
        public int num;
        public int line;

        public A(int num, int line)
        {
            this.num = num;
            this.line = line;
        }

        public int CompareTo(object obj)
        {
            A a = obj as A;
            return this.num.CompareTo(a.num);
        }
    }
}
