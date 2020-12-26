using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day20
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

            // list is top, left, right, bottom borders of given tile orientations
            var tiles = new Dictionary<int, List<(string, int)>>();

            string line;
            long product = 1;
            int currentTile = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("Tile"))
                {
                    line = line.Replace("Tile ", "").Replace(":", "");
                    currentTile = int.Parse(line);
                    tiles.Add(currentTile, new List<(string, int)>());
                    line = sr.ReadLine();
                    tiles[currentTile].Add((line, -1));
                    tiles[currentTile].Add((line[0].ToString(), -1));
                    tiles[currentTile].Add((line[^1].ToString(), -1));
                }
                else if (line.Contains('.') || line.Contains('#'))
                {
                    if (sr.Peek() != '\n' && sr.Peek() != '\r')
                    {
                        tiles[currentTile][1] = (tiles[currentTile][1].Item1 + line[0], -1);
                        tiles[currentTile][2] = (tiles[currentTile][2].Item1 + line[^1], -1);
                    }
                    else
                    {
                        tiles[currentTile].Add((line, -1));
                        tiles[currentTile][1] = (tiles[currentTile][1].Item1 + line[0], -1);
                        tiles[currentTile][2] = (tiles[currentTile][2].Item1 + line[^1], -1);
                    }
                }
            }

            foreach (KeyValuePair<int, List<(string, int)>> i in tiles)
            {
                foreach (KeyValuePair<int, List<(string, int)>> j in tiles)
                {
                    if (i.Key != j.Key)
                    {
                        if (i.Value[0].Item1 == j.Value[0].Item1 ||
                            i.Value[0].Item1 == j.Value[1].Item1 ||
                            i.Value[0].Item1 == j.Value[2].Item1 ||
                            i.Value[0].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][0] = (tiles[index][0].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[0].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][0] = (tiles[index][0].Item1, -j.Key);
                        }
                        if (i.Value[1].Item1 == j.Value[0].Item1 ||
                            i.Value[1].Item1 == j.Value[1].Item1 ||
                            i.Value[1].Item1 == j.Value[2].Item1 ||
                            i.Value[1].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][1] = (tiles[index][1].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[1].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][1] = (tiles[index][1].Item1, -j.Key);
                        }
                        if (i.Value[2].Item1 == j.Value[0].Item1 ||
                            i.Value[2].Item1 == j.Value[1].Item1 ||
                            i.Value[2].Item1 == j.Value[2].Item1 ||
                            i.Value[2].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][2] = (tiles[index][2].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[2].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][2] = (tiles[index][2].Item1, -j.Key);
                        }
                        if (i.Value[3].Item1 == j.Value[0].Item1 ||
                            i.Value[3].Item1 == j.Value[1].Item1 ||
                            i.Value[3].Item1 == j.Value[2].Item1 ||
                            i.Value[3].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][3] = (tiles[index][3].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[3].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][3] = (tiles[index][3].Item1, -j.Key);
                        }
                    }
                }
            }

            foreach (KeyValuePair<int, List<(string, int)>> kvp in tiles)
            {
                int numMatched = 0;
                if (kvp.Value[0].Item2 != -1) numMatched++;
                if (kvp.Value[1].Item2 != -1) numMatched++;
                if (kvp.Value[2].Item2 != -1) numMatched++;
                if (kvp.Value[3].Item2 != -1) numMatched++;

                if (numMatched == 2)
                    product *= kvp.Key;
            }

            Console.WriteLine(product);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            const int imageDim = 12;
            StreamReader sr = new StreamReader(filename);

            // list is top, left, right, bottom borders of given tile orientations
            var tiles = new Dictionary<int, List<(string, int)>>();
            var imagePieces = new Dictionary<int, List<List<char>>>();

            string line;
            int currentTile = 0;
            int tileDim = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("Tile"))
                {
                    line = line.Replace("Tile ", "").Replace(":", "");
                    currentTile = int.Parse(line);
                    tiles.Add(currentTile, new List<(string, int)>());
                    line = sr.ReadLine();
                    tiles[currentTile].Add((line, -1));
                    tiles[currentTile].Add((line[0].ToString(), -1));
                    tiles[currentTile].Add((line[^1].ToString(), -1));
                    imagePieces.Add(currentTile, new List<List<char>>());
                    tileDim = 0;
                }
                else if (line.Contains('.') || line.Contains('#'))
                {
                    if (sr.Peek() != '\n' && sr.Peek() != '\r')
                    {
                        tiles[currentTile][1] = (tiles[currentTile][1].Item1 + line[0], -1);
                        tiles[currentTile][2] = (tiles[currentTile][2].Item1 + line[^1], -1);
                        imagePieces[currentTile].Add(new List<char>(line.Substring(1, line.Length - 2)));
                        tileDim++;
                    }
                    else
                    {
                        tiles[currentTile].Add((line, -1));
                        tiles[currentTile][1] = (tiles[currentTile][1].Item1 + line[0], -1);
                        tiles[currentTile][2] = (tiles[currentTile][2].Item1 + line[^1], -1);
                    }
                }
            }

            foreach (KeyValuePair<int, List<(string, int)>> i in tiles)
            {
                foreach (KeyValuePair<int, List<(string, int)>> j in tiles)
                {
                    if (i.Key != j.Key)
                    {
                        if (i.Value[0].Item1 == j.Value[0].Item1 ||
                            i.Value[0].Item1 == j.Value[1].Item1 ||
                            i.Value[0].Item1 == j.Value[2].Item1 ||
                            i.Value[0].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][0] = (tiles[index][0].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[0].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][0] = (tiles[index][0].Item1, -j.Key);
                        }
                        if (i.Value[1].Item1 == j.Value[0].Item1 ||
                            i.Value[1].Item1 == j.Value[1].Item1 ||
                            i.Value[1].Item1 == j.Value[2].Item1 ||
                            i.Value[1].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][1] = (tiles[index][1].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[1].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][1] = (tiles[index][1].Item1, -j.Key);
                        }
                        if (i.Value[2].Item1 == j.Value[0].Item1 ||
                            i.Value[2].Item1 == j.Value[1].Item1 ||
                            i.Value[2].Item1 == j.Value[2].Item1 ||
                            i.Value[2].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][2] = (tiles[index][2].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[2].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][2] = (tiles[index][2].Item1, -j.Key);
                        }
                        if (i.Value[3].Item1 == j.Value[0].Item1 ||
                            i.Value[3].Item1 == j.Value[1].Item1 ||
                            i.Value[3].Item1 == j.Value[2].Item1 ||
                            i.Value[3].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][3] = (tiles[index][3].Item1, j.Key);
                        }
                        else if (Reverse(i.Value[3].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][3] = (tiles[index][3].Item1, -j.Key);
                        }
                    }
                }
            }

            Dictionary<(int x, int y), (int tileID, int xflip, int yflip)> image = new Dictionary<(int, int), (int, int, int)>();

            foreach (KeyValuePair<int, List<(string, int)>> kvp in tiles)
            {
                if (kvp.Value[0].Item2 == -1 && kvp.Value[1].Item2 == -1)
                {
                    image.Add((0, 0), (kvp.Key, 1, 1));
                    break;
                }
                else if (kvp.Value[0].Item2 == -1 && kvp.Value[2].Item2 == -1)
                {
                    image.Add((0, 0), (kvp.Key, -1, 1));
                    break;
                }
                else if (kvp.Value[3].Item2 == -1 && kvp.Value[1].Item2 == -1)
                {
                    image.Add((0, 0), (kvp.Key, 1, -1));
                    break;
                }
                else if (kvp.Value[3].Item2 == -1 && kvp.Value[2].Item2 == -1)
                {
                    image.Add((0, 0), (kvp.Key, -1, -1));
                    break;
                }
            }

            int[,] xflip = new int[imageDim, imageDim];
            for (int i = 0; i < imageDim * imageDim; i++)
            {
                xflip[i / imageDim, i % imageDim] = image[(0, 0)].xflip;
            }
            int[,] yflip = new int[imageDim, imageDim];
            for (int i = 0; i < imageDim * imageDim; i++)
            {
                yflip[i / imageDim, i % imageDim] = image[(0, 0)].yflip;
            }
            int nextid = 0;
            for (int j = 0; j < imageDim; j++)
            {
                for (int i = 1; i < imageDim; i++)
                {
                    if (image[(i - 1, j)].xflip == 1)
                    {
                        if (yflip[i-1,j] == 1)
                            nextid = tiles[image[(i - 1, j)].tileID][1].Item2;
                        else
                            nextid = tiles[image[(i - 1, j)].tileID][3].Item2;
                        yflip[i, j] = yflip[i - 1, j] * Math.Sign(nextid);
                    }
                    else
                    {
                        if (yflip[i-1, j] == 1)
                            nextid = tiles[image[(i - 1, j)].tileID][0].Item2;
                        else
                            nextid = tiles[image[(i - 1, j)].tileID][2].Item2;
                        yflip[i, j] = yflip[i - 1, j] * Math.Sign(nextid);
                    }


                    image.Add((i, j), (Math.Abs(nextid), xflip[i, j], yflip[i, j]));
                    if (j < imageDim - 1)
                    {
                        if (image[(i, j)].yflip == 1)
                        {
                            if (image[(i, j)].xflip == 1)
                                xflip[i, j + 1] = xflip[i, j] * Math.Sign(tiles[image[(i, j)].tileID][2].Item2);
                            else
                                xflip[i, j + 1] = xflip[i, j] * Math.Sign(tiles[image[(i, j)].tileID][3].Item2);
                        }
                        else
                        {
                            if (image[(i, j)].xflip == 1)
                                xflip[i, j + 1] = xflip[i, j] * Math.Sign(tiles[image[(i, j)].tileID][0].Item2);
                            else
                                xflip[i, j + 1] = xflip[i, j] * Math.Sign(tiles[image[(i, j)].tileID][1].Item2);
                        }
                    }
                }

                if (j < imageDim - 1)
                {
                    if (image[(0, j)].yflip == 1)
                    {
                        nextid = tiles[image[(0, j)].tileID][3].Item2;
                    }
                    else
                    {
                        nextid = tiles[image[(0, j)].tileID][0].Item2;
                    }
                    image.Add((0, j + 1), (Math.Abs(nextid), xflip[0, j + 1], yflip[0, j + 1]));
                }
            }

            foreach (KeyValuePair<(int, int), (int, int, int)> kvp in image)
            {
                Console.WriteLine(kvp.Key.Item1 + ", " + kvp.Key.Item2 + ": " + kvp.Value.Item1 + " x: " + kvp.Value.Item2 + " y: " + kvp.Value.Item3);
            }

            var completeImage = new List<List<char>>();

            for (int j = 0; j < imageDim; j++)
            {
                for (int j2 = 0; j2 < tileDim; j2++)
                {
                    completeImage.Add(new List<char>());
                    for (int i = 0; i < imageDim; i++)
                    {
                        int id = image[(i, j)].tileID;
                        List<char> s = imagePieces[id][j2];
                        if (image[(i, j)].yflip == -1)
                        {
                            s = imagePieces[id][tileDim - j2 - 1];
                            if (image[(i, j)].xflip == -1)
                            {
                                s = new List<char>();
                                foreach (List<char> l in imagePieces[id])
                                {
                                    s.Add(l[tileDim - j2 - 1]);
                                }
                            }
                        }
                        if (image[(i, j)].xflip == -1)
                            s = Reverse(s).ToList();

                        foreach (char c in s)
                            completeImage[^1].Add(c);
                    }
                }
            }

            int numSeaMonsters = 0;
            int numHash = 0;
            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
                        numHash++;
                        if (i < completeImage[0].Count - 20 && j > 1 && j < completeImage.Count - 2 &&
                            completeImage[j + 1][i + 1] == '#' && completeImage[j + 1][i + 4] == '#' &&
                            completeImage[j][i + 5] == '#' && completeImage[j][i + 6] == '#' &&
                            completeImage[j + 1][i + 7] == '#' && completeImage[j + 1][i + 10] == '#' &&
                            completeImage[j][i + 11] == '#' && completeImage[j][i + 12] == '#' &&
                            completeImage[j + 1][i + 13] == '#' && completeImage[j + 1][i + 16] == '#' &&
                            completeImage[j][i + 17] == '#' && completeImage[j - 1][i + 18] == '#' &&
                            completeImage[j][i + 18] == '#' && completeImage[j][i + 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }

            if (numSeaMonsters == 0) // flip y
            {
                for (int j = 0; j < completeImage.Count; j++)
                {
                    for (int i = 0; i < completeImage[0].Count; i++)
                    {
                        if (i < completeImage[0].Count - 20 && j > 1 && j < completeImage.Count - 2 &&
                            completeImage[j - 1][i + 1] == '#' && completeImage[j - 1][i + 4] == '#' &&
                            completeImage[j][i + 5] == '#' && completeImage[j][i + 6] == '#' &&
                            completeImage[j - 1][i + 7] == '#' && completeImage[j - 1][i + 10] == '#' &&
                            completeImage[j][i + 11] == '#' && completeImage[j][i + 12] == '#' &&
                            completeImage[j - 1][i + 13] == '#' && completeImage[j - 1][i + 16] == '#' &&
                            completeImage[j][i + 17] == '#' && completeImage[j + 1][i + 18] == '#' &&
                            completeImage[j][i + 18] == '#' && completeImage[j][i + 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }

            if (numSeaMonsters == 0) // flip x
            {
                for (int j = 0; j < completeImage.Count; j++)
                {
                    for (int i = 0; i < completeImage[0].Count; i++)
                    {
                        if (i >= 19 && j > 1 && j < completeImage.Count - 2 &&
                            completeImage[j + 1][i - 1] == '#' && completeImage[j + 1][i - 4] == '#' &&
                            completeImage[j][i - 5] == '#' && completeImage[j][i - 6] == '#' &&
                            completeImage[j + 1][i - 7] == '#' && completeImage[j + 1][i - 10] == '#' &&
                            completeImage[j][i - 11] == '#' && completeImage[j][i - 12] == '#' &&
                            completeImage[j + 1][i - 13] == '#' && completeImage[j + 1][i - 16] == '#' &&
                            completeImage[j][i - 17] == '#' && completeImage[j - 1][i - 18] == '#' &&
                            completeImage[j][i - 18] == '#' && completeImage[j][i - 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }

            if (numSeaMonsters == 0)
            {
                for (int j = 0; j < completeImage.Count; j++)
                {
                    for (int i = 0; i < completeImage[0].Count; i++)
                    {
                        if (i >= 19 && j > 1 && j < completeImage.Count - 2 &&
                            completeImage[j - 1][i - 1] == '#' && completeImage[j - 1][i - 4] == '#' &&
                            completeImage[j][i - 5] == '#' && completeImage[j][i - 6] == '#' &&
                            completeImage[j - 1][i - 7] == '#' && completeImage[j - 1][i - 10] == '#' &&
                            completeImage[j][i - 11] == '#' && completeImage[j][i - 12] == '#' &&
                            completeImage[j - 1][i - 13] == '#' && completeImage[j - 1][i - 16] == '#' &&
                            completeImage[j][i - 17] == '#' && completeImage[j + 1][i - 18] == '#' &&
                            completeImage[j][i - 18] == '#' && completeImage[j][i - 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }

            if (numSeaMonsters == 0)
            {
                for (int j = 0; j < completeImage.Count; j++)
                {
                    for (int i = 0; i < completeImage[0].Count; i++)
                    {
                        if (completeImage[i][j] == '#')
                        {
                            if (i < completeImage[0].Count - 20 && j > 1 && j < completeImage.Count - 2 &&
                                completeImage[i + 1][j + 1] == '#' && completeImage[i + 4][j + 1] == '#' &&
                                completeImage[i + 5][j] == '#' && completeImage[i + 6][j] == '#' &&
                                completeImage[i + 7][j + 1] == '#' && completeImage[i + 10][j + 1] == '#' &&
                                completeImage[i + 11][j] == '#' && completeImage[i + 12][j] == '#' &&
                                completeImage[i + 13][j + 1] == '#' && completeImage[i + 16][j + 1] == '#' &&
                                completeImage[i + 17][j] == '#' && completeImage[i + 18][j - 1] == '#' &&
                                completeImage[i + 18][j] == '#' && completeImage[i + 19][j] == '#')
                            {
                                numSeaMonsters++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(numSeaMonsters);
            Console.WriteLine(numHash - (numSeaMonsters * 15));
        }

        public static string Reverse(List<char> s)
        {
            string result = "";
            foreach (char c in s)
            {
                result = c + result;
            }

            return result;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
