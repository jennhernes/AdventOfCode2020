using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }

        public static void Part1()
        {
            string filename = "../../../../input.txt";
            List<List<char>> map = new List<List<char>>();

            StreamReader sr = new StreamReader(filename);

            string line;
            int mapWidth = 0;
            int mapHeight = 0;

            while ((line = sr.ReadLine()) != null)
            {
                mapWidth = line.Length;
                map.Add(line.ToList());
                mapHeight++;
            }

            int copies = (mapHeight * 3) / mapWidth + 1;
            List<List<char>> wideMap;
            wideMap = DuplicateMap(map, copies);

            int x = 0;
            int y = mapHeight;
            int numTrees = 0;

            for (int i = 1; i < mapHeight; i++)
            {
                x += 3;
                if (wideMap[i][x] == '#')
                    numTrees++;
            }

            Console.WriteLine(numTrees);
        }

        public static void Part2()
        {
            string filename = "../../../../input.txt";
            List<List<char>> map = new List<List<char>>();

            StreamReader sr = new StreamReader(filename);

            string line;
            int mapWidth = 0;
            int mapHeight = 0;

            while ((line = sr.ReadLine()) != null)
            {
                mapWidth = line.Length;
                map.Add(line.ToList());
                mapHeight++;
            }

            int copies = (mapHeight * 7) / mapWidth + 1;
            List<List<char>> wideMap;
            wideMap = DuplicateMap(map, copies);

            int x = 0;
            int y = mapHeight;
            long product = 1;
            int numTrees = 0;

            for (int i = 1; i < mapHeight; i++)
            {
                x += 1;
                if (wideMap[i][x] == '#')
                    numTrees++;
            }

            product *= numTrees;
            numTrees = 0;
            x = 0;

            for (int i = 1; i < mapHeight; i++)
            {
                x += 3;
                if (wideMap[i][x] == '#')
                    numTrees++;
            }

            product *= numTrees;
            numTrees = 0;
            x = 0;

            for (int i = 1; i < mapHeight; i++)
            {
                x += 5;
                if (wideMap[i][x] == '#')
                    numTrees++;
            }

            product *= numTrees;
            numTrees = 0;
            x = 0;

            for (int i = 1; i < mapHeight; i++)
            {
                x += 7;
                if (wideMap[i][x] == '#')
                    numTrees++;
            }

            product *= numTrees;
            numTrees = 0;
            x = 0;

            for (int i = 1; i < mapHeight; i++)
            {
                x += 1;
                i++;
                if (wideMap[i][x] == '#')
                    numTrees++;
            }

            product *= numTrees;
            Console.WriteLine(product);
        }

        public static List<List<char>> DuplicateMap(List<List<char>> map, int copies)
        {
            int mapHeight = map.Count;
            List<List<char>> result = new List<List<char>>();

            foreach (List<char> l in map)
            {
                result.Add(new List<char>());
                for (int i = 0; i < copies; i++)
                {
                    foreach (char c in l)
                    {
                        result[result.Count - 1].Add(c);
                    }
                }
            }

            return result;
        }
    }
}
