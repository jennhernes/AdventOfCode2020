using System;
using System.Collections.Generic;
using System.IO;

namespace Day05
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

            string line;
            long highestID = 1;
            while ((line = sr.ReadLine()) != null)
            {
                int row = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (line[i] == 'B')
                    {
                        row += (int)MathF.Pow(2, 6 - i);
                    }
                }
                int col = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (line[7 + i] == 'R')
                    {
                        col += (int)MathF.Pow(2, 2 - i);
                    }
                }
                highestID = (long)MathF.Max(highestID, (long)row * 8 + (long)col);
            }
            Console.WriteLine(highestID);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);
            List<Seat> plane = new List<Seat>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                int row = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (line[i] == 'B')
                    {
                        row += (int)MathF.Pow(2, 6 - i);
                    }
                }
                int col = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (line[7 + i] == 'R')
                    {
                        col += (int)MathF.Pow(2, 2 - i);
                    }
                }
                plane.Add(new Seat(row, col));
            }
            plane.Sort();
            for (int i = 1; i < 127; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int prevRow = plane.FindIndex(x => (x.row + 1 == i && x.col == j));
                    int nextRow = plane.FindIndex(x => (x.row - 1 == i && x.col == j));

                    if (prevRow != -1 && nextRow != -1)
                    {
                        if (!plane.Exists(x => (x.row == i && x.col == j)))
                            Console.WriteLine(i * 8 + j);
                    }
                }
            }
        }
    }

    public class Seat : IComparable
    {
        public int row;
        public int col;

        public Seat(int r, int c)
        {
            row = r;
            col = c;
        }

        public int CompareTo(Object obj)
        {
            Seat s = (Seat)obj;

            if (this.row > s.row)
                return -1;
            else if (this.row < s.row)
                return 1;
            else if (this.col < s.col)
                return -1;
            else if (this.col > s.col)
                return 1;
            else
                return 0;
        }
    }
}
