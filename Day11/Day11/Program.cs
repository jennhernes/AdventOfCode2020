using System;
using System.IO;
using System.Collections.Generic;

namespace Day11
{
    public enum Space { Floor = '.', Empty = 'L', Occupied = '#' }
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
            var boat = new List<List<List<char>>>();

            string line;
            boat.Add(new List<List<char>>());
            while ((line = sr.ReadLine()) != null)
            {
                boat[0].Add(new List<char>());
                boat[0][^1].AddRange(line);
            }

            var numChanged = 1;
            int iter = 1;

            while (numChanged > 0)
            {
                numChanged = 0;
                boat.Add(new List<List<char>>());
                for (int i = 0; i < boat[^2].Count; i++)
                {
                    int width = boat[^2][i].Count;
                    boat[^1].Add(new List<char>());
                    for (int j = 0; j < width; j++)
                    {
                        int numTaken = 0;
                        boat[^1][i].Add(boat[^2][i][j]);
                        if (boat[^1][i][j] != (char)Space.Floor)
                        {
                            for (int x = -1; x < 2; x++)
                            {
                                for (int y = -1; y < 2; y++)
                                {
                                    if (!(x == 0 && y == 0))
                                    {
                                        if (i + x >= 0 && i + x < boat[^2].Count && 
                                            j + y >= 0 && j + y < width)
                                        {
                                            if (boat[^2][i + x][j + y] == (char)Space.Occupied)
                                                numTaken++;
                                        }
                                    }
                                }
                            }
                            if (boat[^2][i][j] == (char)Space.Empty && numTaken == 0)
                            {
                                boat[^1][i][j] = (char)Space.Occupied;
                                numChanged++;
                            }
                            else if (boat[^2][i][j] == (char)Space.Occupied && numTaken >= 4)
                            {
                                boat[^1][i][j] = (char)Space.Empty;
                                numChanged++;
                            }
                        }
                    }
                }
            }

            int numOcc = 0;
            for (int i = 0; i < boat[^1].Count; i++)
            {
                int width = boat[^1][i].Count;
                for (int j = 0; j < width; j++)
                {
                    if (boat[^1][i][j] == (char)Space.Occupied)
                        numOcc++;
                }
            }

            Console.WriteLine(boat.Count);
            Console.WriteLine(numOcc);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);
            var boat = new List<List<List<char>>>();

            string line;
            boat.Add(new List<List<char>>());
            while ((line = sr.ReadLine()) != null)
            {
                boat[0].Add(new List<char>());
                boat[0][^1].AddRange(line);
            }

            var numChanged = 1;
            int iter = 1;

            while (numChanged > 0)
            {
                numChanged = 0;
                boat.Add(new List<List<char>>());
                for (int i = 0; i < boat[^2].Count; i++)
                {
                    int width = boat[^2][i].Count;
                    boat[^1].Add(new List<char>());
                    for (int j = 0; j < width; j++)
                    {
                        int numTaken = 0;
                        boat[^1][i].Add(boat[^2][i][j]);
                        if (boat[^1][i][j] != (char)Space.Floor)
                        {
                            for (int x = -1; x < 2; x++)
                            {
                                for (int y = -1; y < 2; y++)
                                {
                                    if (!(x == 0 && y == 0))
                                    {
                                        int round = 1;
                                        while (i + (x * round) >= 0 && i + (x * round) < boat[^2].Count &&
                                                j + (y * round) >= 0 && j + (y * round) < width)
                                        {
                                            char space = boat[^2][i + (x * round)][j + (y * round)];
                                            if (space == (char)Space.Floor)
                                            {
                                                round++;
                                            }
                                            else
                                            {
                                                if (space == (char)Space.Occupied)
                                                    numTaken++;

                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (boat[^2][i][j] == (char)Space.Empty && numTaken == 0)
                            {
                                boat[^1][i][j] = (char)Space.Occupied;
                                numChanged++;
                            }
                            else if (boat[^2][i][j] == (char)Space.Occupied && numTaken >= 5)
                            {
                                boat[^1][i][j] = (char)Space.Empty;
                                numChanged++;
                            }
                        }
                    }
                }
            }

            int numOcc = 0;
            for (int i = 0; i < boat[^1].Count; i++)
            {
                int width = boat[^1][i].Count;
                for (int j = 0; j < width; j++)
                {
                    if (boat[^1][i][j] == (char)Space.Occupied)
                        numOcc++;
                }
            }

            Console.WriteLine(boat.Count);
            Console.WriteLine(numOcc);
        }
    }
}
