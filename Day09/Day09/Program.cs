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
            Part1();
            // Part2();
        }

        static void Part1()
        {
            string filename = "../../../../test.txt";
            StreamReader sr = new StreamReader(filename);
            List<A> prevNum = new List<A>();
            int len = 5;

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
                    if (newNum > prevNum[^1].num)
                        break;

                    int low = prevNum.Count / 2;
                    int high = low + 1;
                    while (low >= 0 && high < prevNum.Count)
                    {
                        int sum = prevNum[low].num + prevNum[high].num;
                        if (sum == newNum)
                        {
                            valid = true;
                            break;
                        }
                        else if (sum > newNum)
                        {
                            low--;
                        }
                        else
                        {
                            high++;
                        }
                    }
                    if (valid)
                    {
                        int lastLine = prevNum.Min(x => x.line);
                        prevNum.RemoveAll(x => x.line == lastLine);
                        prevNum.Add(new A(newNum, numInstr));
                        prevNum.Sort()
                    }
                }
                else
                {
                    prevNum.Add(new A(newNum, numInstr));
                    prevNum = (List<A>)prevNum.OrderBy(x => x.num);
                }
                numInstr++;
            }
            Console.WriteLine(newNum);
        }
    }

    class A
    {
        public int num;
        public int line;

        public A(int num, int line)
        {
            this.num = num;
            this.line = line;
        }
    }
}
