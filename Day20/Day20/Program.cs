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

            // int is tileID, list is top, left, right, bottom borders of given tile orientations with matching
            // tile's ID
            var tiles = new Dictionary<int, List<(string border, int matchingID)>>();
            // int is tileID, list is the tiles with the border removed
            var imagePieces = new Dictionary<int, List<List<char>>>();

            string line;
            int currentTile = 0;
            int tileDim = 0;
            // read input and separate borders (into tiles) and actual image pieces (into imagePieces)
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
                        tiles[currentTile][1] = (tiles[currentTile][1].border + line[0], -1);
                        tiles[currentTile][2] = (tiles[currentTile][2].border + line[^1], -1);
                        imagePieces[currentTile].Add(new List<char>(line[1..^1]));
                        tileDim++;
                    }
                    else
                    {
                        tiles[currentTile].Add((line, -1));
                        tiles[currentTile][1] = (tiles[currentTile][1].border + line[0], -1);
                        tiles[currentTile][2] = (tiles[currentTile][2].border + line[^1], -1);
                    }
                }
            }

            // determine the matchingID for each list for each entry in tiles
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
                            tiles[index][0] = (tiles[index][0].border, j.Key);
                        }
                        else if (Reverse(i.Value[0].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[0].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][0] = (tiles[index][0].border, -j.Key);
                        }
                        if (i.Value[1].Item1 == j.Value[0].Item1 ||
                            i.Value[1].Item1 == j.Value[1].Item1 ||
                            i.Value[1].Item1 == j.Value[2].Item1 ||
                            i.Value[1].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][1] = (tiles[index][1].border, j.Key);
                        }
                        else if (Reverse(i.Value[1].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[1].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][1] = (tiles[index][1].border, -j.Key);
                        }
                        if (i.Value[2].Item1 == j.Value[0].Item1 ||
                            i.Value[2].Item1 == j.Value[1].Item1 ||
                            i.Value[2].Item1 == j.Value[2].Item1 ||
                            i.Value[2].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][2] = (tiles[index][2].border, j.Key);
                        }
                        else if (Reverse(i.Value[2].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[2].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][2] = (tiles[index][2].border, -j.Key);
                        }
                        if (i.Value[3].Item1 == j.Value[0].Item1 ||
                            i.Value[3].Item1 == j.Value[1].Item1 ||
                            i.Value[3].Item1 == j.Value[2].Item1 ||
                            i.Value[3].Item1 == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][3] = (tiles[index][3].border, j.Key);
                        }
                        else if (Reverse(i.Value[3].Item1) == j.Value[0].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[1].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[2].Item1 ||
                            Reverse(i.Value[3].Item1) == j.Value[3].Item1)
                        {
                            int index = i.Key;
                            tiles[index][3] = (tiles[index][3].border, -j.Key);
                        }
                    }
                }
            }

            Dictionary<(int x, int y), int> image = new Dictionary<(int, int), int>();

            // determine the top left corner of the image
            // Use the first tile that is a corner and rotate the tile to get the orientation right
            foreach (KeyValuePair<int, List<(string, int)>> kvp in tiles)
            {
                if (kvp.Value[0].Item2 == -1 && kvp.Value[1].Item2 == -1) // no need to rotate
                {
                    image.Add((0, 0), kvp.Key);
                    break;
                }
                else if (kvp.Value[0].Item2 == -1 && kvp.Value[2].Item2 == -1) // rot 90
                {
                    tiles[kvp.Key] = Rotate(kvp.Value, 90);
                    for (int i = 0; i < 4; i++)
                    {
                        int id = Math.Abs(kvp.Value[2].Item2);
                        if (Math.Abs(id) != 1 && Math.Abs(tiles[id][i].matchingID) == kvp.Key)
                        {
                            tiles[id][i] = (tiles[id][i].border, -tiles[id][i].matchingID);
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        int id = Math.Abs(kvp.Value[1].Item2);
                        if (Math.Abs(id) != 1 && Math.Abs(tiles[id][i].matchingID) == kvp.Key)
                        {
                            tiles[id][i] = (tiles[id][i].border, -tiles[id][i].matchingID);
                        }
                    }
                    imagePieces[kvp.Key] = Rotate(imagePieces[kvp.Key], 90);
                    image.Add((0, 0), kvp.Key);
                    break;
                }
                else if (kvp.Value[3].Item2 == -1 && kvp.Value[1].Item2 == -1) // rot -90
                {
                    tiles[kvp.Key] = Rotate(tiles[kvp.Key], 270);
                    for (int i = 0; i < 4; i++)
                    {
                        int id = Math.Abs(kvp.Value[3].Item2);
                        if (Math.Abs(id) != 1 && Math.Abs(tiles[id][i].matchingID) == kvp.Key)
                        {
                            tiles[id][i] = (tiles[id][i].border, -tiles[id][i].matchingID);
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        int id = Math.Abs(kvp.Value[0].Item2);
                        if (Math.Abs(id) != 1 && Math.Abs(tiles[id][i].matchingID) == kvp.Key)
                        {
                            tiles[id][i] = (tiles[id][i].border, -tiles[id][i].matchingID);
                        }
                    }
                    imagePieces[kvp.Key] = Rotate(imagePieces[kvp.Key], 270);
                    image.Add((0, 0), kvp.Key);
                    break;
                }
                else if (kvp.Value[3].Item2 == -1 && kvp.Value[2].Item2 == -1) // rot 180
                {
                    tiles[kvp.Key] = Rotate(tiles[kvp.Key], 180);
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            int id = Math.Abs(kvp.Value[j].Item2);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][i].matchingID) == kvp.Key)
                            {
                                tiles[id][i] = (tiles[id][i].border, -tiles[id][i].matchingID);
                            }
                        }
                    }
                    imagePieces[kvp.Key] = Rotate(imagePieces[kvp.Key], 180);
                    image.Add((0, 0), kvp.Key);
                    break;
                }
            }

            Console.WriteLine("Image Order");
            Console.Write(image[(0, 0)] + " ");

            int prevId = -1;
            int nextId = -1;

            // rotate and flip all the tiles, and store the order in image
            for (int j = 0; j < imageDim; j++)
            {
                for (int i = 1; i < imageDim; i++)
                {
                    prevId = image[(i - 1, j)];
                    nextId = tiles[prevId][2].matchingID;
                    if (nextId < 0)
                    {
                        nextId = Math.Abs(nextId);
                        tiles[nextId] = Flip(tiles[nextId], "y");
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][1].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][2].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        imagePieces[nextId] = Flip(imagePieces[nextId], "y");
                    }
                    image.Add((i, j), nextId);
                    Console.Write(image[(i, j)] + " ");

                    if (tiles[nextId][0].matchingID == -prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 90);
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][2].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][1].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 90);
                    }
                    else if (tiles[nextId][0].matchingID == prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 90);
                        tiles[nextId] = Flip(tiles[nextId], "y");
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 90);
                        imagePieces[nextId] = Flip(imagePieces[nextId], "y");
                    }
                    else if (tiles[nextId][2].matchingID == -prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 180);
                        for (int t = 0; t < 4; t++)
                        {
                            for (int s = 0; s < 4; s++)
                            {
                                int id = Math.Abs(tiles[nextId][t].matchingID);
                                if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                                {
                                    tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                                }
                            }
                        }
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 180);
                    }
                    else if (tiles[nextId][2].matchingID == prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 180);
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][0].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][3].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        tiles[nextId] = Flip(tiles[nextId], "y");
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 180);
                        imagePieces[nextId] = Flip(imagePieces[nextId], "y");
                    }
                    else if (tiles[nextId][3].matchingID == prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 270);
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][0].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][3].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 270);
                    }
                    else if (tiles[nextId][3].matchingID == -prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 270);
                        for (int t = 0; t < 4; t++)
                        {
                            for (int s = 0; s < 4; s++)
                            {
                                int id = Math.Abs(tiles[nextId][t].matchingID);
                                if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                                {
                                    tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                                }
                            }
                        }
                        tiles[nextId] = Flip(tiles[nextId], "y");
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 270);
                        imagePieces[nextId] = Flip(imagePieces[nextId], "y");
                    }
                }
                Console.WriteLine();

                if (j < imageDim - 1)
                {
                    prevId = image[(0, j)];
                    nextId = tiles[prevId][3].matchingID;
                    if (nextId < 0)
                    {
                        nextId = Math.Abs(nextId);
                        tiles[nextId] = Flip(tiles[nextId], "x");
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][0].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][3].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        imagePieces[nextId] = Flip(imagePieces[nextId], "x");
                    }
                    image.Add((0, j + 1), nextId);
                    Console.Write(image[(0, j + 1)] + " ");

                    if (tiles[nextId][2].matchingID == prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 90);
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][1].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][2].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 90);
                    }
                    else if (tiles[nextId][2].matchingID == -prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 90);
                        for (int t = 0; t < 4; t++)
                        {
                            for (int s = 0; s < 4; s++)
                            {
                                int id = Math.Abs(tiles[nextId][t].matchingID);
                                if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                                {
                                    tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                                }
                            }
                        }
                        tiles[nextId] = Flip(tiles[nextId], "x");
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 90);
                        imagePieces[nextId] = Flip(imagePieces[nextId], "x");
                    }
                    else if (tiles[nextId][3].matchingID == -prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 180);
                        for (int t = 0; t < 4; t++)
                        {
                            for (int s = 0; s < 4; s++)
                            {
                                int id = Math.Abs(tiles[nextId][t].matchingID);
                                if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                                {
                                    tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                                }
                            }
                        }
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 180);
                    }
                    else if (tiles[nextId][3].matchingID == prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 180);
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][1].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][2].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        tiles[nextId] = Flip(tiles[nextId], "x");
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 180);
                        imagePieces[nextId] = Flip(imagePieces[nextId], "x");
                    }
                    else if (tiles[nextId][1].matchingID == -prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 270);
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][0].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        for (int s = 0; s < 4; s++)
                        {
                            int id = Math.Abs(tiles[nextId][3].matchingID);
                            if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                            {
                                tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                            }
                        }
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 270);
                    }
                    else if (tiles[nextId][1].matchingID == prevId)
                    {
                        tiles[nextId] = Rotate(tiles[nextId], 270);
                        for (int t = 0; t < 4; t++)
                        {
                            for (int s = 0; s < 4; s++)
                            {
                                int id = Math.Abs(tiles[nextId][t].matchingID);
                                if (Math.Abs(id) != 1 && Math.Abs(tiles[id][s].matchingID) == nextId)
                                {
                                    tiles[id][s] = (tiles[id][s].border, -tiles[id][s].matchingID);
                                }
                            }
                        }
                        tiles[nextId] = Flip(tiles[nextId], "x");
                        imagePieces[nextId] = Rotate(imagePieces[nextId], 270);
                        imagePieces[nextId] = Flip(imagePieces[nextId], "x");
                    }
                }
            }

            // compile the tiles into one large 2d list
            var completeImage = new List<List<char>>();

            for (int j = 0; j < imageDim; j++)
            {
                for (int j2 = 0; j2 < tileDim; j2++)
                {
                    completeImage.Add(new List<char>());
                    for (int i = 0; i < imageDim; i++)
                    {
                        int id = image[(i, j)];
                        List<char> s = imagePieces[id][j2];
                        foreach (char c in s)
                            completeImage[^1].Add(c);
                    }
                }
            }

            int numSeaMonsters = 0;
            int maxNumSM = 0;
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // flip x

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = completeImage[0].Count; i >= 0; i--)
                {
                    if (completeImage[i][j] == '#')
                    {
                        if (i >= 20 && j > 1 && j < completeImage.Count - 2 &&
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // flip x, y

            for (int j = completeImage.Count; j >= 0; j--)
            {
                for (int i = completeImage[0].Count - 1; i >= 0; i--)
                {
                    if (completeImage[i][j] == '#')
                    {
                        if (i >= 20 && j > 1 && j < completeImage.Count - 2 &&
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // flip y
            completeImage = Flip(completeImage, "x");

            for (int j = completeImage.Count - 1; j >= 0; j--)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
                        if (i < completeImage[0].Count - 20 && j > 1 && j < completeImage.Count - 2 &&
                            completeImage[j - 1][i + 1] == '#' && completeImage[j - 1][i + 4] == '#' &&
                            completeImage[j][i + 5] == '#' && completeImage[j][i + 6] == '#' &&
                            completeImage[j - 1][i + 7] == '#' && completeImage[j + 1][i + 10] == '#' &&
                            completeImage[j][i + 11] == '#' && completeImage[j][i + 12] == '#' &&
                            completeImage[j - 1][i + 13] == '#' && completeImage[j - 1][i + 16] == '#' &&
                            completeImage[j][i + 17] == '#' && completeImage[j - 1][i + 18] == '#' &&
                            completeImage[j][i + 18] == '#' && completeImage[j][i + 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 90
            completeImage = Flip(completeImage, "y");
            completeImage = Rotate(completeImage, 90);

            for (int j = completeImage[0].Count - 1; j >= 0; j++)
            {
                for (int i = 0; i < completeImage.Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
                        if (j < completeImage[0].Count - 20 && i > 1 && i < completeImage.Count - 2 &&
                            completeImage[i + 1][j - 1] == '#' && completeImage[i + 1][j - 4] == '#' &&
                            completeImage[i][j - 5] == '#' && completeImage[i][j - 6] == '#' &&
                            completeImage[i + 1][j - 7] == '#' && completeImage[i + 1][j - 10] == '#' &&
                            completeImage[i][j - 11] == '#' && completeImage[i][j - 12] == '#' &&
                            completeImage[i + 1][j - 13] == '#' && completeImage[i + 1][j - 16] == '#' &&
                            completeImage[i][j - 17] == '#' && completeImage[i - 1][j - 18] == '#' &&
                            completeImage[i][j - 18] == '#' && completeImage[i][j - 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 90, flip x
            completeImage = Flip(completeImage, "x");

            for (int j = completeImage[0].Count - 1; j >= 0; j++)
            {
                for (int i = completeImage.Count - 1; i >= 0 ; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
                        if (j < completeImage[0].Count - 20 && i > 1 && i < completeImage.Count - 2 &&
                            completeImage[i - 1][j - 1] == '#' && completeImage[i - 1][j - 4] == '#' &&
                            completeImage[i][j - 5] == '#' && completeImage[i][j - 6] == '#' &&
                            completeImage[i - 1][j - 7] == '#' && completeImage[i - 1][j - 10] == '#' &&
                            completeImage[i][j - 11] == '#' && completeImage[i][j - 12] == '#' &&
                            completeImage[i - 1][j - 13] == '#' && completeImage[i - 1][j - 16] == '#' &&
                            completeImage[i][j - 17] == '#' && completeImage[i + 1][j - 18] == '#' &&
                            completeImage[i][j - 18] == '#' && completeImage[i][j - 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 90, flip x, y
            completeImage = Flip(completeImage, "y");

            for (int j = 0; j < completeImage[0].Count; j++)
            {
                for (int i = completeImage.Count - 1; i >= 0; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
                        if (j < completeImage[0].Count - 20 && i > 1 && i < completeImage.Count - 2 &&
                            completeImage[i - 1][j + 1] == '#' && completeImage[i - 1][j + 4] == '#' &&
                            completeImage[i][j + 5] == '#' && completeImage[i][j + 6] == '#' &&
                            completeImage[i - 1][j + 7] == '#' && completeImage[i - 1][j + 10] == '#' &&
                            completeImage[i][j + 11] == '#' && completeImage[i][j + 12] == '#' &&
                            completeImage[i - 1][j + 13] == '#' && completeImage[i - 1][j + 16] == '#' &&
                            completeImage[i][j + 17] == '#' && completeImage[i + 1][j + 18] == '#' &&
                            completeImage[i][j + 18] == '#' && completeImage[i][j + 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 90, flip y
            completeImage = Flip(completeImage, "x");
            //stopped here
            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
                        if (j < completeImage[0].Count - 20 && i > 1 && i < completeImage.Count - 2 &&
                            completeImage[i - 1][j - 1] == '#' && completeImage[i - 1][j - 4] == '#' &&
                            completeImage[i][j - 5] == '#' && completeImage[i][j - 6] == '#' &&
                            completeImage[i - 1][j - 7] == '#' && completeImage[i - 1][j - 10] == '#' &&
                            completeImage[i][j - 11] == '#' && completeImage[i][j - 12] == '#' &&
                            completeImage[i - 1][j - 13] == '#' && completeImage[i - 1][j - 16] == '#' &&
                            completeImage[i][j - 17] == '#' && completeImage[i + 1][j - 18] == '#' &&
                            completeImage[i][j - 18] == '#' && completeImage[i][j - 19] == '#')
                        {
                            numSeaMonsters++;
                        }
                    }
                }
            }
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 180
            completeImage = Flip(completeImage, "y");
            completeImage = Rotate(completeImage, 90);

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 180, flip x
            completeImage = Rotate(completeImage, 90);
            completeImage = Flip(completeImage, "x");

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 180, flip x, y
            completeImage = Flip(completeImage, "y");

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 180, flip y
            completeImage = Flip(completeImage, "x");

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 270
            completeImage = Flip(completeImage, "y");
            completeImage = Rotate(completeImage, 90);

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // roate 270, flip x
            completeImage = Rotate(completeImage, 90);
            completeImage = Flip(completeImage, "x");

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 270, flip x,y
            completeImage = Flip(completeImage, "y");

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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
            maxNumSM = Math.Max(maxNumSM, numSeaMonsters);

            numSeaMonsters = 0; // rotate 270, flip y
            completeImage = Flip(completeImage, "x");

            for (int j = 0; j < completeImage.Count; j++)
            {
                for (int i = 0; i < completeImage[0].Count; i++)
                {
                    if (completeImage[i][j] == '#')
                    {
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

            Console.WriteLine(maxNumSM);
            Console.WriteLine(numHash);
            Console.WriteLine(numHash - (maxNumSM * 15));
        }

        public static List<(string, int)> Flip(List<(string, int)> borders, string dir)
        {
            var result = new List<(string, int)>();

            if (dir == "x")
            {
                result.Add((Reverse(borders[0].Item1), -borders[0].Item2));
                result.Add((borders[2].Item1, borders[2].Item2));
                result.Add((borders[1].Item1, borders[1].Item2));
                result.Add((Reverse(borders[3].Item1), -borders[3].Item2));
            }
            else if (dir == "y")
            {
                result.Add((borders[3].Item1, borders[3].Item2));
                result.Add((Reverse(borders[1].Item1), -borders[1].Item2));
                result.Add((Reverse(borders[2].Item1), -borders[2].Item2));
                result.Add((borders[0].Item1, borders[0].Item2));
            }

            return result;
        }

        public static List<List<char>> Flip(List<List<char>> map, string dir)
        {
            var result = new List<List<char>>();

            if (dir == "x")
            {
                foreach (List<char> lc in map)
                {
                    result.Add(Reverse(lc));
                }
            }
            else if (dir == "y")
            {
                for (int i = map.Count - 1; i >= 0; i--)
                {
                    result.Add(map[i]);
                }
            }

            return result;
        }

        public static List<(string, int)> Rotate(List<(string, int)> borders, int angle)
        {
            (string, int) temp;

            if (angle == 90)
            {
                temp = (borders[2].Item1, borders[2].Item2);
                borders[2] = (Reverse(borders[3].Item1), -borders[3].Item2);
                borders[3] = (borders[1].Item1, borders[1].Item2);
                borders[1] = (Reverse(borders[0].Item1), -borders[0].Item2);
                borders[0] = (temp.Item1, temp.Item2);
            }
            else if (angle == 180)
            {
                temp = (borders[3].Item1, borders[3].Item2);
                borders[3] = (Reverse(borders[0].Item1), -borders[0].Item2);
                borders[0] = (Reverse(temp.Item1), -temp.Item2);
                temp = (borders[2].Item1, borders[2].Item2);
                borders[2] = (Reverse(borders[1].Item1), -borders[1].Item2);
                borders[1] = (Reverse(temp.Item1), -temp.Item2);
            }
            else if (angle == 270)
            {
                temp = (borders[3].Item1, borders[3].Item2);
                borders[3] = (Reverse(borders[2].Item1), -borders[2].Item2);
                borders[2] = (borders[0].Item1, borders[0].Item2);
                borders[0] = (Reverse(borders[1].Item1), -borders[1].Item2);
                borders[1] = (temp.Item1, temp.Item2);
            }

            return borders;
        }

        public static List<List<char>> Rotate(List<List<char>> map, int angle)
        {
            var result = new List<List<char>>();
            for (int i = 0; i < map[0].Count; i++)
            {
                result.Add(new List<char>());
            }


            if (angle == 90)
            {
                for (int i = 0; i < map[0].Count; i++)
                {
                    for (int j = 0; j < map.Count; j++)
                    {
                        result[i].Add(map[j][i]);
                    }
                }
            }
            else if (angle == 180)
            {
                for (int i = map.Count - 1; i >= 0; i--)
                {
                    for (int j = map[0].Count - 1; j >= 0; j--)
                    {
                        result[i].Add(map[i][j]);
                    }
                }
            }
            else if (angle == 270)
            {
                for (int i = 0; i < map[0].Count; i++)
                {
                    for (int j = map.Count - 1; j >= 0; j--)
                    {
                        result[i].Add(map[j][i]);
                    }
                }
            }

            return result;
        }

        public static List<char> Reverse(List<char> s)
        {
            var result = new List<char>();
            foreach (char c in s)
            {
                result.Add(c);
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
