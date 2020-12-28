using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
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
            // var cups = new List<int>() { 2, 7, 8, 0, 1, 4, 3, 5, 6 };
            var cups = new List<int>() { 4, 1, 2, 6, 5, 3, 7, 0, 8 };
            var numCups = cups.Count;
            int currentCup = cups[0];
            int currentCupIndex = 0;
            int destinationCup = -1;
            int destinationCupIndex = -1;
            for (int i = 0; i < 100; i++)
            {
                Console.Write("Cups: ");
                foreach (int cup in cups)
                {
                    Console.Write(cup + " ");
                }
                Console.WriteLine();

                Console.WriteLine("Current cup: " + cups[currentCupIndex]);
                int cup1 = cups[(currentCupIndex + 1) % numCups];
                int cup2 = cups[(currentCupIndex + 2) % numCups];
                int cup3 = cups[(currentCupIndex + 3) % numCups];
                cups.Remove(cup1);
                cups.Remove(cup2);
                cups.Remove(cup3);

                Console.WriteLine("Pick up: " + cup1 + " " + cup2 + " " + cup3);
                destinationCup = currentCup;
                do
                {
                    destinationCup--;
                    destinationCup = (destinationCup + numCups) % numCups;
                } while (destinationCup == cup1 || destinationCup == cup2 || destinationCup == cup3);
                destinationCupIndex = cups.IndexOf(destinationCup);

                Console.WriteLine("Destination Cup: " + cups[destinationCupIndex]);

                int insertIndex = (destinationCupIndex + 1) % cups.Count;

                cups.Insert(insertIndex, cup3);
                cups.Insert(insertIndex, cup2);
                cups.Insert(insertIndex, cup1);

                currentCupIndex = (cups.IndexOf(currentCup) + 1) % numCups;
                currentCup = cups[currentCupIndex];
            }
            Console.Write("Final Cups: ");
            foreach (int cup in cups)
            {
                Console.Write(cup + " ");
            }
            Console.WriteLine();
        }

        static void Part2()
        {
            var numCups = 1000000;
            //var nextCup = new int[1000001]; //389125467
            //nextCup[0] = -1;
            //nextCup[1] = 2;
            //nextCup[2] = 5;
            //nextCup[3] = 8;
            //nextCup[4] = 6;
            //nextCup[5] = 4;
            //nextCup[6] = 7;
            //nextCup[7] = 10;
            //nextCup[8] = 9;
            //nextCup[9] = 1;
            //int currentCup = 3;
            var nextCup = new int[1000001]; // new int[10]; // 523764819
            nextCup[0] = -1;
            nextCup[1] = 9;
            nextCup[2] = 3;
            nextCup[3] = 7;
            nextCup[4] = 8;
            nextCup[5] = 2;
            nextCup[6] = 4;
            nextCup[7] = 6;
            nextCup[8] = 1;
            nextCup[9] = 10;
            int currentCup = 5;

            for (int i = 10; i < 1000000; i++)
            {
                nextCup[i] = i + 1;
            }
            nextCup[1000000] = currentCup;

            int destinationCup = -1;
            int printCup;
            for (long i = 0; i < 10000000; i++)
            {
                //Console.Write("Cups: ");
                //printCup = currentCup;
                //for (int j = 1; j < nextCup.Length; j++)
                //{
                //    Console.Write(printCup + " ");
                //    printCup = nextCup[printCup];
                //}
                //Console.WriteLine();

                //Console.WriteLine("Current cup: " + currentCup);
                int cup1 = nextCup[currentCup];
                int cup2 = nextCup[cup1];
                int cup3 = nextCup[cup2];
                nextCup[currentCup] = nextCup[cup3];
                nextCup[cup1] = 0;
                nextCup[cup2] = 0;
                nextCup[cup3] = 0;

                //Console.WriteLine("Pick up: " + cup1 + " " + cup2 + " " + cup3);
                destinationCup = currentCup;
                do
                {
                    destinationCup--;
                    destinationCup = (destinationCup + numCups) % numCups;
                    if (destinationCup == 0) destinationCup = numCups;
                } while (destinationCup == cup1 || destinationCup == cup2 || destinationCup == cup3);
                
                //Console.WriteLine("Destination Cup: " + destinationCup);

                nextCup[cup3] = nextCup[destinationCup];
                nextCup[cup2] = cup3;
                nextCup[cup1] = cup2;
                nextCup[destinationCup] = cup1;

                currentCup = nextCup[currentCup];
            }

            //Console.Write("Final Cups: ");
            //printCup = currentCup;
            //for (int j = 1; j < nextCup.Length; j++)
            //{
            //    Console.Write(printCup + " ");
            //    printCup = nextCup[printCup];
            //}
            //Console.WriteLine();

            int answer1 = nextCup[1];
            int answer2 = nextCup[answer1];
            Console.WriteLine(answer1 + " " + answer2);
        }
    }
}
