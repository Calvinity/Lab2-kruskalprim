using System;
using System.Collections.Generic;
using System.Diagnostics;
using Utilities;

namespace Lab2
{
    internal class Program
    {
        // Useful constant.
        private const int NO_OF_DIFFERENT_GRAPHS_PER_NO_NODES_AND_EDGES = 5;

        // Group made. This is one way you could create a list of algorithms and data-structures to test.
        private static KeyValuePair<string, Func<Graph, List<Edge>>>[] ALGORITHMS = {
        };

        // Group made.
        static void Main()
        {
            Graph g = new Graph(16, 20);
            Console.WriteLine("The graph:");
            g.Print();
            List<Edge> result = MSTKruskal(g);
            Console.WriteLine();
            Console.WriteLine("MSTKruskal:");
            foreach (Edge edge in result)
            {
                Console.WriteLine(edge);
            }
        }

        // Group made.
        static void SeveralGraphsTwoAlgorithms(int[] nodesArray, int[] percentEdgesArray,
                                               StreamWriter output, bool debug = false)
        {
        }
        // Group made. Or this way.
        static void SeveralGraphsSeveralAlgorithms(int[] nodesArray, int[] percentEdgesArray,
                                                   StreamWriter output, bool debug = false)
        {
        }

        // Group made.
        static void OneGraphTwoAlgorithms(int nodes, int percentEdges,
                                          StreamWriter output,
                                          bool debug = false)
        {
        }
        // Group made. Or this way.
        static void OneGraphSeveralAlgorithms(int nodes, int percentEdges,
                                              StreamWriter output,
                                              bool debug = false)
        {
        }

        /// <summary>
        ///  Computes a MST between the nodes using Prim's algorithm with the priority queue from System.Collections.Generic .
        /// </summary>
        /// <param name="graph">the graph.</param>
        /// <returns>A MST.</returns>
        public static List<Edge> MSTPrimPQFSCG(Graph graph)
        {
            return MSTPrim(graph, new PriorityQueueFromSystemCollectionsGeneric<int, double>());
        }

        private static List<Edge> MSTPrim2Primier(Graph graph, IPriorityQueue<int, double> pq)
        {
            int[] previous = new int[graph.Nodes.Count];
            previous[0] = -1;
            int[] minLength = new int[graph.Nodes.Count];
            Array.Fill(minLength, int.MaxValue);
            minLength[0] = 0;
            bool[] isInMST = new bool[graph.Nodes.Count];
            Array.Fill(isInMST, false);
            List<Edge> result = [];
            List<Edge> knownEdges = [];
            knownEdges.Add();
            int current = 0;
            isInMST[0] = true;
            for (int i = 0; i < graph.Nodes.Count - 1; i++)
            {
                if (isInMST[knownEdges[current].End] == false)
                {
                    previous[i] = graph.Nodes[knownEdges[current].End].Id;
                    isInMST[knownEdges[current].End] = true;
                    foreach (Edge edge in graph.Nodes[knownEdges[current].End].Edges)
                    {
                        knownEdges.Add(edge);
                        pq.Enqueue(knownEdges.Count, edge.Length);

                    }
                    current = pq.Dequeue();
                    if (isInMST[knownEdges[current].End] == false)
                    {
                        result.Add(knownEdges[current]);
                    }
                    else
                    {

                    }
                }
            }




            return null;
        }

        //static int GetMinLengthIndex(int[] lengths, bool[] isInMST, int nodes)
        //{
        //    int minValue = int.MaxValue;
        //    int minIndex = 0;
        //    for (int i =0; i < )
        }
        private static List<Edge> MSTPrim(Graph graph, IPriorityQueue<int, double> pq)
        {
            int[] previous = new int[graph.Nodes.Count];
            previous[0] = -1;
            int[] minLength = new int[graph.Nodes.Count];
            Array.Fill(minLength, int.MaxValue);
            minLength[0] = 0;
            bool[] isInMST = new bool[graph.Nodes.Count];
            Array.Fill(isInMST, false);
            int current = 0;
            List<Edge> result = [];
            for (int i = 0; i < graph.Nodes.Count - 1; i++)
            {
                if (isInMST[current] == false)
                {

                    int nodeFrom = 0;
                    previous[i] = graph.Nodes[current].Id;
                    isInMST[current] = true;
                    foreach (Edge edge in graph.Nodes[current].Edges)
                    {
                        pq.Enqueue(edge.End, edge.Length);
                    }

                    nodeFrom = current;
                    current = pq.Dequeue();
                    int[,] edgeCase = [nodeFrom, current]

                }
                else if (pq.IsEmpty == true)
                {
                    Console.WriteLine("Queue is empty, all nodes could not be connected");
                    break;
                }
                else
                {
                    current = pq.Dequeue();
                    i--;
                }
            }

            for (int i = 1; i < graph.Nodes.Count; i++)
            {
                result.Add(graph.Nodes.Edges)
            }
            //for (int i = 0; i < graph.Nodes.Count -1; i++)
            //{
            //    int minimumLengthIndex = pq
            //}
            return null;
        }

        /// <summary>
        ///  Computes a MST between the nodes using Kruskal's algorithm.
        /// </summary>
        /// <param name="graph">the graph.</param>
        /// <returns>A MST.</returns>
        public static List<Edge> MSTKruskal(Graph graph)
        {
            List<Edge> edges = graph.Edges;
            edges.Sort((a, b) => a.Length.CompareTo(b.Length));
            Queue<Edge> queue = new(edges);
            Subset[] subsets = new Subset[graph.Nodes.Count];
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                subsets[i] = new() { Parent = graph.Nodes[i].Id };
            }
            List<Edge> result = [];
            while (result.Count < graph.Nodes.Count - 1)
            {
                if (queue.Count <= 0)
                {
                    Console.WriteLine("Kön är tom, komplett lista kunde inte skapas");
                    break;
                }

                Edge edge = queue.Dequeue();
                int from = GetRoot(subsets, edge.Start);
                int to = GetRoot(subsets, edge.End);
                if (from == to) { continue; }
                result.Add(edge);
                Union(subsets, from, to);
            }
            return result;
        }

        /// <summary>
        ///  Get the root of this node id.
        /// </summary>
        /// <param name="subsets"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static int GetRoot(Subset[] subsets, int id)
        {
            int i = id;
            subsets[i].Parent = subsets[i].Parent != id
                ? GetRoot(subsets, subsets[i].Parent) : subsets[i].Parent;
            return subsets[i].Parent;
        }

        /// <summary>
        ///  Union two subsets.
        /// </summary>
        /// <param name="subsets"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private static void Union(Subset[] subsets, int a, int b)
        {
            subsets[b].Parent =
                subsets[a].Rank >= subsets[b].Rank
                    ? a : subsets[b].Parent;
            subsets[a].Parent =
                subsets[a].Rank < subsets[b].Rank
                    ? b : subsets[a].Parent;
            if (subsets[a].Rank == subsets[b].Rank)
            {
                subsets[a].Rank++;
            }
        }

        /// <summary>
        ///  Compare the minimum spanning tree between the two algorithms.
        /// </summary>
        /// <param name="a">the MST from algorithm a.</param>
        /// <param name="b">the MST from algorithm b.</param>
        /// <exception cref="ArgumentException">indicates that the distances are not ok.</exception>
        private static void Verify(List<Edge> a, List<Edge> b)
        {
            const double MAX_OK = 0.01;

            double lengthA = 0;
            foreach (Edge e in a)
            {
                lengthA += e.Length;
            }
            double lengthB = 0;
            foreach (Edge e in b)
            {
                lengthB += e.Length;
            }
            if (lengthA - lengthB > MAX_OK || lengthB - lengthA > MAX_OK)
            {
                string str = $"the MST's do not have the same length: {lengthA} != {lengthB}.";
                throw new ArgumentException(str);
            }
        }

        /// <summary>
        ///  Compare the minimum spanning tree between a set of algorithms and data structures.
        /// </summary>
        /// <param name="algorithms">the MST from the algorithms.</param>
        /// <exception cref="ArgumentException">indicates that the distances are not ok.</exception>
        private static void Verify(List<List<Edge>> algorithms)
        {
            const double MAX_OK = 0.01;

            foreach (List<Edge> a in algorithms)
            {
                foreach (List<Edge> b in algorithms)
                {
                    if (a.Equals(b))
                    {
                        continue;
                    }
                    double lengthA = 0;
                    foreach (Edge e in a)
                    {
                        lengthA += e.Length;
                    }
                    double lengthB = 0;
                    foreach (Edge e in b)
                    {
                        lengthB += e.Length;
                    }
                    if (lengthA - lengthB > MAX_OK || lengthB - lengthA > MAX_OK)
                    {
                        string str = $"the MST's of algorithm {a} and {b} do not have the same length: {lengthA} != {lengthB}.";
                        throw new ArgumentException(str);
                    }
                }
            }
        }
    }
}
