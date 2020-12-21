using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
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
            List<Dictionary<int, List<List<char>>>> cubes = new List<Dictionary<int, List<List<char>>>>();
            cubes.Add(new Dictionary<int, List<List<char>>>());
            cubes[0].Add(0, new List<List<char>>());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("......##...#.#......".ToList());
            cubes[0][0].Add("......#..##..#......".ToList());
            cubes[0][0].Add("........#.####......".ToList());
            cubes[0][0].Add(".......#..#.........".ToList());
            cubes[0][0].Add("......########......".ToList());
            cubes[0][0].Add("......######.#......".ToList());
            cubes[0][0].Add(".......####..#......".ToList());
            cubes[0][0].Add(".......###.#........".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());
            cubes[0][0].Add("....................".ToList());

            for (int cycle = 0; cycle < 6; cycle++)
            {
                cubes.Add(new Dictionary<int, List<List<char>>>());
                int z = cycle + 1;
                for (int k = -z; k < z + 1; k++)
                {
                    for (int i = 0; i < cubes[0].Count; i++)
                    {
                        for (int j = 0; j < cubes[0][0].Count; j++)
                        {

                        }
                    }
                }
            }
        }
    }
}
