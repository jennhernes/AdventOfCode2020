using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
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
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add(".......#.......".ToList());
            //cubes[0][0].Add("........#......".ToList());
            //cubes[0][0].Add("......###......".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());
            //cubes[0][0].Add("...............".ToList());

            int numCycles = 6;
            for (int cycle = 0; cycle < numCycles; cycle++)
            {
                Console.WriteLine("Cycle " + cycle);
                cubes.Add(new Dictionary<int, List<List<char>>>());
                int numSlices = cycle + 1;
                for (int k = -numSlices; k < numSlices + 1; k++)
                {
                    Console.WriteLine("z = " + k);
                    cubes[^1].Add(k, new List<List<char>>());
                    for (int j = 0; j < cubes[0][0].Count; j++)
                    {
                        cubes[^1][k].Add(new List<char>());
                        for (int i = 0; i < cubes[0][0][0].Count; i++)
                        {
                            int activeNeighbours = 0;
                            for (int dx = -1; dx < 2; dx++)
                            {
                                for (int dy = -1; dy < 2; dy++)
                                {
                                    for (int dz = -1; dz < 2; dz++)
                                    {
                                        if (dx != 0 || dy != 0 || dz != 0)
                                        {
                                            int x = i + dx;
                                            int y = j + dy;
                                            int z = k + dz;
                                            if (x >= 0 && y >= 0 && z > -numSlices && z < numSlices &&
                                                y < cubes[0][0].Count && x < cubes[0][0][0].Count)
                                            {
                                                if (cubes[^2][z][y][x] == '#')
                                                    activeNeighbours++;
                                            }
                                        }
                                    }
                                }
                            }
                            if (((Math.Abs(k) == numSlices || cubes[^2][k][j][i] == '.') &&
                                activeNeighbours == 3) ||
                                (Math.Abs(k) != numSlices && cubes[^2][k][j][i] == '#' &&
                                (activeNeighbours == 2) || activeNeighbours == 3))
                            {
                                cubes[^1][k][j].Add('#');
                            }
                            else
                            {
                                cubes[^1][k][j].Add('.');
                            }
                            Console.Write(cubes[^1][k][j][i]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                }
            }

            int totActive = 0;
            foreach (KeyValuePair<int, List<List<char>>> kvp in cubes[^1])
            {
                foreach (List<char> l in kvp.Value)
                {
                    foreach (char c in l)
                    {
                        if (c == '#')
                            totActive++;
                    }
                }
            }

            Console.WriteLine(totActive);
        }

        static void Part2()
        {
            List<Dictionary<int, Dictionary<int, List<List<char>>>>> cubes = new List<Dictionary<int, Dictionary<int, List<List<char>>>>>();
            cubes.Add(new Dictionary<int, Dictionary<int, List<List<char>>>>());
            cubes[0].Add(0, new Dictionary<int, List<List<char>>>());
            cubes[0][0].Add(0, new List<List<char>>());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("......##...#.#......".ToList());
            cubes[0][0][0].Add("......#..##..#......".ToList());
            cubes[0][0][0].Add("........#.####......".ToList());
            cubes[0][0][0].Add(".......#..#.........".ToList());
            cubes[0][0][0].Add("......########......".ToList());
            cubes[0][0][0].Add("......######.#......".ToList());
            cubes[0][0][0].Add(".......####..#......".ToList());
            cubes[0][0][0].Add(".......###.#........".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            cubes[0][0][0].Add("....................".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add(".......#.......".ToList());
            //cubes[0][0][0].Add("........#......".ToList());
            //cubes[0][0][0].Add("......###......".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());
            //cubes[0][0][0].Add("...............".ToList());

            int numCycles = 6;
            for (int cycle = 0; cycle < numCycles; cycle++)
            {
                Console.WriteLine("Cycle " + cycle);
                int numSlices = cycle + 1;
                cubes.Add(new Dictionary<int, Dictionary<int, List<List<char>>>>());
                for (int l = -numSlices; l < numSlices + 1; l++)
                {
                    cubes[^1].Add(l, new Dictionary<int, List<List<char>>>());
                    for (int k = -numSlices; k < numSlices + 1; k++)
                    {
                        Console.WriteLine("z = " + k + ", w = " + l);
                        cubes[^1][l].Add(k, new List<List<char>>());
                        for (int j = 0; j < cubes[0][0][0].Count; j++)
                        {
                            cubes[^1][l][k].Add(new List<char>());
                            for (int i = 0; i < cubes[0][0][0][0].Count; i++)
                            {
                                int activeNeighbours = 0;
                                for (int dx = -1; dx < 2; dx++)
                                {
                                    for (int dy = -1; dy < 2; dy++)
                                    {
                                        for (int dz = -1; dz < 2; dz++)
                                        {
                                            for (int dw = -1; dw < 2; dw++)
                                            {
                                                if (dx != 0 || dy != 0 || dz != 0 || dw != 0)
                                                {
                                                    int x = i + dx;
                                                    int y = j + dy;
                                                    int z = k + dz;
                                                    int w = l + dw;
                                                    if (x >= 0 && y >= 0 && z > -numSlices &&
                                                        w > -numSlices && w < numSlices && z < numSlices &&
                                                        y < cubes[0][0][0].Count && x < cubes[0][0][0][0].Count)
                                                    {
                                                        if (cubes[^2][w][z][y][x] == '#')
                                                            activeNeighbours++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if ((((Math.Abs(k) == numSlices || Math.Abs(l) == numSlices) ||
                                    cubes[^2][l][k][j][i] == '.') && activeNeighbours == 3) ||
                                    ((Math.Abs(k) != numSlices && Math.Abs(l) != numSlices) &&
                                    cubes[^2][l][k][j][i] == '#' &&
                                    (activeNeighbours == 2) || activeNeighbours == 3))
                                {
                                    cubes[^1][l][k][j].Add('#');
                                }
                                else
                                {
                                    cubes[^1][l][k][j].Add('.');
                                }
                                Console.Write(cubes[^1][l][k][j][i]);
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }

            int totActive = 0;
            foreach (KeyValuePair<int, Dictionary<int, List<List<char>>>> kvp in cubes[^1])
            {
                foreach (KeyValuePair<int, List<List<char>>> kvp2 in kvp.Value)
                {
                    foreach (List<char> l in kvp2.Value)
                    {
                        foreach (char c in l)
                        {
                            if (c == '#')
                                totActive++;
                        }
                    }
                }
            }

            Console.WriteLine(totActive);
        }
    }
}
