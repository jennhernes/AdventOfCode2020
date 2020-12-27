using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day22
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

            var deck1 = new List<int>();
            var deck2 = new List<int>();
            string line;
            int currentPlayer = 1;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("Player"))
                {
                    currentPlayer = int.Parse(line[^2].ToString());
                }
                else if (currentPlayer == 1 && line != "")
                {
                    deck1.Add(int.Parse(line));
                }
                else if (currentPlayer == 2 && line != "")
                {
                    deck2.Add(int.Parse(line));
                }
            }

            while (deck1.Count > 0 && deck2.Count > 0)
            {
                if (deck1[0] > deck2[0])
                {
                    deck1.Add(deck1[0]);
                    deck1.Add(deck2[0]);
                }
                else
                {
                    deck2.Add(deck2[0]);
                    deck2.Add(deck1[0]);
                }
                deck1.RemoveAt(0);
                deck2.RemoveAt(0);
            }

            long score = 0;
            if (deck1.Count > 0)
            {
                for (int i = 0; i < deck1.Count; i++)
                {
                    score += deck1[i] * (deck1.Count - i);
                }
            }
            else if (deck2.Count > 0)
            {
                for (int i = 0; i < deck2.Count; i++)
                {
                    score += deck2[i] * (deck2.Count - i);
                }
            }

            Console.WriteLine(score);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            var deck1 = new List<int>();
            var deck2 = new List<int>();
            string line;
            int currentPlayer = 1;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("Player"))
                {
                    currentPlayer = int.Parse(line[^2].ToString());
                }
                else if (currentPlayer == 1 && line != "")
                {
                    deck1.Add(int.Parse(line));
                }
                else if (currentPlayer == 2 && line != "")
                {
                    deck2.Add(int.Parse(line));
                }
            }

            int winner = PlayGame(deck1, deck2, new List<string>());

            long score = 0;
            if (winner == 1)
            {
                for (int i = 0; i < deck1.Count; i++)
                {
                    score += deck1[i] * (deck1.Count - i);
                }
            }
            else if (winner == 2)
            {
                for (int i = 0; i < deck2.Count; i++)
                {
                    score += deck2[i] * (deck2.Count - i);
                }
            }

            Console.WriteLine(score);
        }

        static int PlayGame(List<int> deck1, List<int> deck2, List<string> prevRounds)
        {
            int winner = 1;
            bool dupRound = false;
            if (deck1.Count == 0)
            {
                // Console.WriteLine("WINNER IS PLAYER 2");
                return 2;
            }
            else if (deck2.Count == 0)
            {
                // Console.WriteLine("WINNER IS PLAYER 1");
                return 1;
            }
            else
            {
                while (deck1.Count > 0 && deck2.Count > 0)
                {
                    winner = 1;
                    if (prevRounds.Count > 0)
                    {
                        string current = ConvertToString(deck1, deck2);

                        if (prevRounds.Contains(current))
                        {
                            dupRound = true;
                            // Console.WriteLine("DUPLICATE");
                            break;
                        }
                    }

                    prevRounds.Add(ConvertToString(deck1, deck2));

                    if (deck1[0] <= deck1.Count - 1 && deck2[0] <= deck2.Count - 1)
                    {
                        var tempDeck1 = new List<int>();
                        for (int i = 1; i < deck1[0] + 1; i++)
                        {
                            tempDeck1.Add(deck1[i]);
                        }
                        var tempDeck2 = new List<int>();
                        for (int i = 1; i < deck2[0] + 1; i++)
                        {
                            tempDeck2.Add(deck2[i]);
                        }
                        // Console.WriteLine("SUBGAME");
                        winner = PlayGame(tempDeck1, tempDeck2, new List<string>());
                    }
                    else if (deck1[0] > deck2[0])
                        winner = 1;
                    else if (deck1[0] < deck2[0])
                        winner = 2;
                    if (winner == 1)
                    {
                        deck1.Add(deck1[0]);
                        deck1.Add(deck2[0]);
                    }
                    else if (winner == 2)
                    {
                        deck2.Add(deck2[0]);
                        deck2.Add(deck1[0]);
                    }
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);

                    //Console.Write("Player 1's deck: ");
                    //foreach (int card in deck1)
                    //{
                    //    Console.Write(card + ", ");
                    //}
                    //Console.WriteLine();

                    //Console.Write("Player 2's deck: ");
                    //foreach (int card in deck2)
                    //{
                    //    Console.Write(card + ", ");
                    //}
                    //Console.WriteLine();
                }
                return winner;
            }
        }

        static string ConvertToString(List<int> l1, List<int> l2)
        {
            string result = "";
            foreach (int i in l1)
            {
                result += i.ToString();
            }
            result += " ";
            foreach (int i in l2)
            {
                result += i.ToString();
            }
            return result;
        }
    }
}
