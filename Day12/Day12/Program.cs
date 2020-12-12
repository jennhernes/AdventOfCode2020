using System;
using System.IO;
using System.Collections.Generic;

namespace Day12
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
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;
            Facing facing = Facing.East;
            int hor = 0;
            int ver = 0;

            while ((line = sr.ReadLine()) != null)
            {
                char action = line[0];
                if (action == 'F')
                {
                    if (facing == Facing.East)
                        hor += int.Parse(line.Substring(1));
                    else if (facing == Facing.West)
                        hor -= int.Parse(line.Substring(1));
                    else if (facing == Facing.North)
                        ver += int.Parse(line.Substring(1));
                    else if (facing == Facing.South)
                        ver -= int.Parse(line.Substring(1));
                }
                else if (action == 'N')
                    ver += int.Parse(line.Substring(1));
                else if (action == 'S')
                    ver -= int.Parse(line.Substring(1));
                else if (action == 'E')
                    hor += int.Parse(line.Substring(1));
                else if (action == 'W')
                    hor -= int.Parse(line.Substring(1));
                else if (action == 'L')
                {
                    int change = int.Parse(line.Substring(1)) / 90;
                    facing = (Facing)(((int)facing - change) % 4);
                }
                else if (action == 'R')
                {
                    int change = int.Parse(line.Substring(1)) / 90;
                    facing = (Facing)(((int)facing + change) % 4);
                }
            }

            Console.WriteLine(hor);
            Console.WriteLine(ver);
            Console.WriteLine(Math.Abs(hor) + Math.Abs(ver));
        }
    }

    public enum Facing { North, East, South, West }
}
