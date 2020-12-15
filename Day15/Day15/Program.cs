using System;
using System.IO;
using System.Collections.Generic;

namespace Day15
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
            List<int> nums = new List<int>() { 0, 3, 1, 6, 7, 5 };
            // List<int> nums = new List<int>() { 0, 3, 6 };
            int start = nums.Count;
            for (int i = start; i < 2020; i++)
            {
                var index = nums.FindLastIndex(nums.Count - 2, x => x == nums[nums.Count - 1]);
                if (index > -1)
                {
                    nums.Add(nums.Count - index - 1);
                }
                else
                {
                    nums.Add(0);
                }
            }

            Console.WriteLine(nums[2019]);
        }

        static void Part2()
        {
            var final = 30000000;
            var nums = new Dictionary<long, long>();
            nums.Add(0, 0);
            nums.Add(3, 1);
            nums.Add(1, 2);
            nums.Add(6, 3);
            nums.Add(7, 4);
            var nextNum = 5L;
            // var nums = new List<long>() { 0, 3, 6 };
            //nums.Add(0, 0);
            //nums.Add(3, 1);
            //var nextNum = 6L;
            int start = nums.Count;
            for (long i = start; i < final; i++)
            {
                if (nums.ContainsKey(nextNum))
                {
                    var temp = i - nums[nextNum];
                    nums[nextNum] = i;
                    nextNum = temp;
                }
                else
                {
                    nums.Add(nextNum, i);
                    nextNum = 0;
                }
            }

            foreach(KeyValuePair<long, long> kvp in nums)
            {
                if (kvp.Value == final - 1)
                    Console.WriteLine(kvp.Key);
            }
        }
    }
}
