using System;
using System.IO;
using System.Collections.Generic;

namespace Day12
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
                    facing = (Facing)(((int)facing - change + 4) % 4);
                    
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

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            Waypoint wp = new Waypoint(Facing.East, 10, Facing.North, 1);
            string line;
            Facing facing = Facing.East;
            int hor = 0;
            int ver = 0;

            while ((line = sr.ReadLine()) != null)
            {
                char action = line[0];
                int sign = 1;
                if (action == 'F')
                {
                    if (wp.facingHor == Facing.East)
                        hor += int.Parse(line.Substring(1)) * wp.distHor;
                    else if (wp.facingHor == Facing.West)
                        hor -= int.Parse(line.Substring(1)) * wp.distHor;

                    if (wp.facingVer == Facing.North)
                        ver += int.Parse(line.Substring(1)) * wp.distVer;
                    else if (wp.facingVer == Facing.South)
                        ver -= int.Parse(line.Substring(1)) * wp.distVer;
                }
                else if (action == 'N')
                {
                    if (wp.facingVer == Facing.South)
                        sign = -1;
                    wp.distVer += int.Parse(line.Substring(1)) * sign;
                }
                else if (action == 'S')
                {
                    if (wp.facingVer == Facing.North)
                        sign = -1;
                    wp.distVer += int.Parse(line.Substring(1)) * sign;
                }
                else if (action == 'E')
                {
                    if (wp.facingHor == Facing.West)
                        sign = -1;
                    wp.distHor += int.Parse(line.Substring(1)) * sign;
                }
                else if (action == 'W')
                {
                    if (wp.facingHor == Facing.East)
                        sign = -1;
                    wp.distHor += int.Parse(line.Substring(1)) * sign;
                }
                else if (action == 'L')
                {
                    wp.Rotate(int.Parse(line.Substring(1)) * -1);

                }
                else if (action == 'R')
                {
                    wp.Rotate(int.Parse(line.Substring(1)) * 1);
                }
            }

            Console.WriteLine(hor);
            Console.WriteLine(ver);
            Console.WriteLine(Math.Abs(hor) + Math.Abs(ver));
        }
    }

    public enum Facing { North, East, South, West }

    class Waypoint
    {
        public Facing facingHor;
        public int distHor;
        public Facing facingVer;
        public int distVer;

        public Waypoint(Facing facingHor, int distHor, Facing facingVer, int distVer)
        {
            this.facingHor = facingHor;
            this.distHor = distHor;
            this.facingVer = facingVer;
            this.distVer = distVer;
        }

        public void Rotate(int degree)
        {
            int turns = (degree / 90 + 4) % 4;
            for (int i = 0; i < turns; i++)
            {
                if ((facingHor == Facing.East && facingVer == Facing.North) || 
                    (facingHor == Facing.West && facingVer == Facing.South))
                    this.facingVer = (Facing)((int)(this.facingVer + 2) % 4);
                else 
                    this.facingHor = (Facing)((int)(this.facingHor + 2) % 4);
                
                int temp = this.distHor;
                this.distHor = this.distVer;
                this.distVer = temp;
            }

        }
    }
}
