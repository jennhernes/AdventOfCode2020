using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day24
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

            var instructions = new List<string>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instructions.Add(line);
            }

            var flippedTiles = new Dictionary<int, List<int>>();

            foreach (string path in instructions)
            {
                int x = 0;
                int y = 0;
                for (int i = 0; i < path.Length; i++)
                {
                    string next = path[i].ToString();
                    if (next == "n" || next == "s")
                    {
                        next += path[i + 1];
                        i++;
                    }
                    if (next.Contains("e"))
                    {
                        if (next.Length == 1)
                            x += 2;
                        else
                            x++;
                    }
                    else if (next.Contains("w"))
                    {
                        if (next.Length == 1)
                            x -= 2;
                        else
                            x--;
                    }

                    if (next.Contains("n")) y++;
                    else if (next.Contains("s")) y--;
                }

                if (!flippedTiles.ContainsKey(x)) flippedTiles.Add(x, new List<int>());

                if (flippedTiles[x].Contains(y)) flippedTiles[x].Remove(y);
                else flippedTiles[x].Add(y);
            }

            int numBlack = 0;
            foreach (KeyValuePair<int, List<int>> kvp in flippedTiles)
            {
                foreach (int i in kvp.Value)
                {
                    numBlack++;
                }
            }

            Console.WriteLine(numBlack);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            var instructions = new List<string>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instructions.Add(line);
            }

            var flippedTiles = new List<Dictionary<int, List<int>>>();
            flippedTiles.Add(new Dictionary<int, List<int>>());

            foreach (string path in instructions)
            {
                int x = 0;
                int y = 0;
                for (int i = 0; i < path.Length; i++)
                {
                    string next = path[i].ToString();
                    if (next == "n" || next == "s")
                    {
                        next += path[i + 1];
                        i++;
                    }
                    if (next.Contains("e"))
                    {
                        if (next.Length == 1)
                            x += 2;
                        else
                            x++;
                    }
                    else if (next.Contains("w"))
                    {
                        if (next.Length == 1)
                            x -= 2;
                        else
                            x--;
                    }

                    if (next.Contains("n")) y++;
                    else if (next.Contains("s")) y--;
                }

                if (!flippedTiles[0].ContainsKey(x)) flippedTiles[0].Add(x, new List<int>());

                if (flippedTiles[0][x].Contains(y)) flippedTiles[0][x].Remove(y);
                else flippedTiles[0][x].Add(y);
            }

            int numBlack = 0;
            foreach (KeyValuePair<int, List<int>> kvp in flippedTiles[0])
            {
                foreach (int j in kvp.Value)
                {
                    numBlack++;
                }
            }

            Console.WriteLine("Day 0: " + numBlack);

            for (int i = 1; i < 101; i++)
            {
                flippedTiles.Add(new Dictionary<int, List<int>>());

                int numNeighbours = 0;
                var possibleNewTiles = new Dictionary<(int x, int y), int>();
                foreach (KeyValuePair<int, List<int>> kvp in flippedTiles[i-1])
                {
                    int x = kvp.Key;
                    foreach (int y in kvp.Value)
                    {
                        numNeighbours = 0;
                        if (flippedTiles[i-1].ContainsKey(x - 2) && 
                            flippedTiles[i-1][x-2].Contains(y)) // west
                            numNeighbours++;
                        if (flippedTiles[i - 1].ContainsKey(x + 2) && 
                            flippedTiles[i - 1][x + 2].Contains(y)) // east
                            numNeighbours++;
                        if (flippedTiles[i - 1].ContainsKey(x - 1) && 
                            flippedTiles[i-1][x - 1].Contains(y - 1)) // sw
                            numNeighbours++;
                        if (flippedTiles[i - 1].ContainsKey(x - 1) && 
                            flippedTiles[i - 1][x - 1].Contains(y + 1)) // nw
                            numNeighbours++;
                        if (flippedTiles[i - 1].ContainsKey(x + 1) && 
                            flippedTiles[i - 1][x + 1].Contains(y - 1)) // se
                            numNeighbours++;
                        if (flippedTiles[i - 1].ContainsKey(x + 1) && 
                            flippedTiles[i - 1][x + 1].Contains(y + 1)) // ne
                            numNeighbours++;

                        if (numNeighbours == 1 || numNeighbours == 2)
                        {
                            if (!flippedTiles[i].ContainsKey(x)) flippedTiles[i].Add(x, new List<int>());

                            flippedTiles[i][x].Add(y);
                        }

                        if (!possibleNewTiles.ContainsKey((x - 2, y))) 
                            possibleNewTiles.Add((x - 2, y), 1);
                        else possibleNewTiles[(x - 2, y)]++;
                        if (!possibleNewTiles.ContainsKey((x + 2, y))) 
                            possibleNewTiles.Add((x + 2, y), 1);
                        else possibleNewTiles[(x + 2, y)]++;
                        if (!possibleNewTiles.ContainsKey((x - 1, y - 1))) 
                            possibleNewTiles.Add((x - 1, y - 1), 1);
                        else possibleNewTiles[(x - 1, y - 1)]++;
                        if (!possibleNewTiles.ContainsKey((x - 1, y + 1))) 
                            possibleNewTiles.Add((x - 1, y + 1), 1);
                        else possibleNewTiles[(x - 1, y + 1)]++;
                        if (!possibleNewTiles.ContainsKey((x + 1, y - 1))) 
                            possibleNewTiles.Add((x + 1, y - 1), 1);
                        else possibleNewTiles[(x + 1, y - 1)]++;
                        if (!possibleNewTiles.ContainsKey((x + 1, y + 1)))
                            possibleNewTiles.Add((x + 1, y + 1), 1);
                        else possibleNewTiles[(x + 1, y + 1)]++;
                    }
                }

                foreach (KeyValuePair<(int x, int y), int> kvp in possibleNewTiles)
                {
                    if (kvp.Value == 2)
                    {
                        if (!flippedTiles[i].ContainsKey(kvp.Key.x)) 
                            flippedTiles[i].Add(kvp.Key.x, new List<int>());
                        if (!flippedTiles[i][kvp.Key.x].Contains(kvp.Key.y))
                            flippedTiles[i][kvp.Key.x].Add(kvp.Key.y);
                    }
                }

                numBlack = 0;
                foreach (KeyValuePair<int, List<int>> kvp in flippedTiles[^1])
                {
                    foreach (int j in kvp.Value)
                    {
                        numBlack++;
                    }
                }

                Console.WriteLine("Day " + i + ": " + numBlack);
            }
        }
    }
}
