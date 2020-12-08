using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day07
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

            var graph = new Graph(); 

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(' ');
                string colour = "";
                int i = 0;
                for (i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i] == "bags")
                    {
                        i += 2;
                        break;
                    }

                    colour += tokens[i];
                }
                Vertex vertex = graph.CreateVertex(colour);

                if (tokens[i] == "no")
                    continue;
                else
                    i++;

                while (i < tokens.Length)
                {
                    colour = "";
                    for (; i < tokens.Length; i++)
                    {
                        if (tokens[i].Contains("bag"))
                        {
                            i += 2;
                            break;
                        }

                        colour += tokens[i];
                    }

                    Vertex v2 = graph.allVertices.FirstOrDefault(x => x.colour == colour);
                    if (v2 == null)
                    {
                        v2 = graph.CreateVertex(colour);
                    }

                    vertex.AddEdge(v2, 1);
                }
            }

            //foreach (Vertex v in graph.allVertices)
            //{
            //    Console.WriteLine(v.colour);
            //}

            // int?[,] adj = graph.CreateAdjMatrix();
            int valid = ValidColours(graph);
            Console.Write(valid);
        }

        public static int ValidColours(Graph g)
        {
            List<string> result = new List<string>();

            List<string> newColours = new List<string>();
            List<string> temp = new List<string>();
            newColours.Add("shinygold");

            while (newColours.Count > 0)
            {
                foreach (string c in newColours)
                {
                    Vertex v = g.allVertices.Find(x => x.colour == c);
                    foreach (Edge e in v.edges)
                    {
                        if (e.weight == -1 && !newColours.Contains(e.child.colour) && !result.Contains(e.child.colour))
                        {
                            temp.Add(e.child.colour);
                        }
                    }

                    result.Add(c);
                }
                foreach (string s in temp)
                    newColours.Add(s);
                temp.Clear();
                for (int i = 0; i < result.Count; i++)
                {
                    if (newColours.Contains(result[i]))
                        newColours.Remove(result[i]);
                }
            }

            return result.Distinct().Count();
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            var graph = new Graph();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(' ');
                string colour = "";
                int i = 0;
                for (i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i] == "bags")
                    {
                        i += 2;
                        break;
                    }

                    colour += tokens[i];
                }
                Vertex vertex = graph.CreateVertex(colour);

                if (tokens[i] == "no")
                    continue;
                else
                    i++;

                while (i < tokens.Length)
                {
                    colour = "";
                    int weight = Convert.ToInt32(tokens[i - 1]);
                    for (; i < tokens.Length; i++)
                    {
                        if (tokens[i].Contains("bag"))
                        {
                            i += 2;
                            break;
                        }

                        colour += tokens[i];
                    }

                    Vertex v2 = graph.allVertices.Find(x => x.colour == colour);
                    if (v2 == null)
                    {
                        v2 = graph.CreateVertex(colour);
                    }

                    vertex.AddEdge(v2, weight);
                }
            }

            Vertex shinygold = graph.allVertices.Find(x => x.colour == "shinygold");
            Console.WriteLine(CalculateNumBags(shinygold));
        }

        public static long CalculateNumBags(Vertex v)
        {
            long result = 0;
            List<Edge> edges = v.edges;
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].weight > 0)
                    result += edges[i].weight * CalculateNumBags(edges[i].child);
            }
            result += 1;

            return result;
        }
    }

    class Vertex
    {
        public string colour;
        public List<Edge> edges;

        public Vertex(string colour)
        {
            this.colour = colour;
            edges = new List<Edge>();
        }

        public Vertex AddEdge(Vertex v, int w)
        {
            edges.Add(new Edge(this, v, w));

            if (!v.edges.Exists(e => e.parent == v && e.child == this))
                v.AddEdge(this, -w);

            return this;
        }
    }

    class Edge
    {
        public Vertex parent;
        public Vertex child;
        public int weight;

        public Edge(Vertex parent, Vertex child, int weight)
        {
            this.parent = parent;
            this.child = child;
            this.weight = weight;
        }
    }

    class Graph
    {
        public Vertex root;
        public List<Vertex> allVertices = new List<Vertex>();

        public Vertex CreateVertex(string colour)
        {
            var v = allVertices.Find(x => x.colour == colour);
            if (v == null)
            {
                v = new Vertex(colour);
                allVertices.Add(v);
            }
            return v;
        }

        public int?[,] CreateAdjMatrix()
        {
            int?[,] adj = new int?[allVertices.Count, allVertices.Count];

            for (int i = 0; i < allVertices.Count; i++)
            {
                Vertex v1 = allVertices[i];

                for (int j = 0; j < allVertices.Count; j++)
                {
                    Vertex v2 = allVertices[j];

                    var edge = v1.edges.FirstOrDefault(e => e.child == v2);

                    if (edge != null)
                        adj[i, j] = edge.weight;
                }
            }

            return adj;
        }
    }
}
