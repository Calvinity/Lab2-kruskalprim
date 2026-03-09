using System;
using System.Collections;
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
            Graph g = new Graph(5, 30);
            Console.WriteLine("The graph:");
            g.Print();
            List<Edge> result = MSTKruskal(g);
            Console.WriteLine();
            Console.WriteLine("MSTKruskal:");
            foreach (Edge edge in result)
            {
                Console.WriteLine(edge);
            }
            List<Edge> result2 = MSTPrimPQFSCG2ElectricPrimalo(g);
            Console.WriteLine();
            Console.WriteLine("MSTPrim2:");
            foreach (Edge edge in result2)
            {
                Console.WriteLine(edge);
            }
            List<Edge> result3 = MSTPrimPQHeap2ElectricPrimalo(g);
            Console.WriteLine();
            Console.WriteLine("MSTPrim2:");
            foreach (Edge edge in result3)
            {
                Console.WriteLine(edge);
            }
            //Verify(result, result2);
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
        //public static List<Edge> MSTPrimPQFSCG(Graph graph)
        //{
        //    return MSTPrim(graph, new PriorityQueueFromSystemCollectionsGeneric<int, double>());
        //}
        public static List<Edge> MSTPrimPQFSCG2ElectricPrimalo(Graph graph)
        {
            return MSTPrim2ElectricPrimalo(graph, new PriorityQueueFromSystemCollectionsGeneric<int, double>());
        }

        public static List<Edge> MSTPrimPQHeap2ElectricPrimalo(Graph graph)
        {
            return MSTPrim2ElectricPrimalo(graph, new PriorityQueueHeap<int, double>());
        }
        private static List<Edge> MSTPrim2ElectricPrimalo(Graph graph, IPriorityQueue<int, double> pq)
        {
            bool[] isInMST = new bool[graph.Nodes.Count];
            Array.Fill(isInMST, false);
            List<Edge> result = [];
            List<Edge> knownEdges = [];
            int current = 0;
            while (result.Count < graph.Nodes.Count -1)
            {
                if (knownEdges.Count == 0)
                {
                    isInMST[current] = true;
                    foreach (Edge edge in graph.Nodes[current].Edges)
                    {
                        knownEdges.Add(edge);
                        pq.Enqueue(knownEdges.Count - 1, edge.Length);

                    }
                    if (pq.IsEmpty)
                    {
                        Console.WriteLine("Prim: Kön är tom, komplett lista kunde inte skapas");
                        break;
                    }
                    current = pq.Dequeue();
                    result.Add(knownEdges[current]);
                    isInMST[knownEdges[current].End] = true;
                    foreach (Edge edge in graph.Nodes[knownEdges[current].End].Edges)
                    {
                        knownEdges.Add(edge);
                        pq.Enqueue(knownEdges.Count - 1, edge.Length);

                    }
                    if (pq.IsEmpty)
                    {
                        Console.WriteLine("Prim: Kön är tom, komplett lista kunde inte skapas");
                        break;
                    }
                    current = pq.Dequeue();
                }
                else if (isInMST[knownEdges[current].End] == false)
                {
                    result.Add(knownEdges[current]);
                    isInMST[knownEdges[current].End] = true;
                    foreach (Edge edge in graph.Nodes[knownEdges[current].End].Edges)
                    {
                        knownEdges.Add(edge);
                        pq.Enqueue(knownEdges.Count - 1, edge.Length);

                    }
                    {
                        if (pq.IsEmpty)
                        {
                            Console.WriteLine("Prim: Kön är tom, komplett lista kunde inte skapas");
                            break;
                        }
                        current = pq.Dequeue();
                    }
                }
                else
                {
                    if (pq.IsEmpty)
                    {
                        Console.WriteLine("Prim: Kön är tom, komplett lista kunde inte skapas");
                        break;
                    }
                    current = pq.Dequeue();
                }
            }
            return result;
        }
        private static List<Edge> MSTPrim(Graph graph, IPriorityQueue<int, double> pq)
        {
            int[] nodeFrom = new int[graph.Nodes.Count];
            nodeFrom[0] = -1;
            int[] nodeTo = new int[graph.Nodes.Count];
            double[] minLength = new double[graph.Nodes.Count];
            Array.Fill(minLength, int.MaxValue);
            bool[] isInMST = new bool[graph.Nodes.Count];
            Array.Fill(isInMST, false);
            List<Edge> result = [];
            int current = 0;
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                Console.WriteLine("Node not in MST " + i);
                isInMST[current] = true;
                nodeFrom[i] = current;
                foreach (Edge edge in graph.Nodes[current].Edges)
                {
                    if (isInMST[edge.End] == false && edge.Length < minLength[edge.End])
                    {

                        minLength[edge.End] = edge.Length;
                        pq.Enqueue(edge.End, edge.Length);
                    }
                }
                if (pq.IsEmpty)
                {
                    Console.WriteLine("Prim: Queue is empty");
                    break;
                }
                current = pq.Dequeue();
                while (isInMST[current] == true && pq.IsEmpty == false)
                {
                    current = pq.Dequeue();
                }
                nodeTo[i] = current;

            }
            for (int i = 0; i < nodeTo.Length - 1; i++)
            {
                foreach (Edge e in graph.Nodes[nodeTo[i]].Edges)
                {
                    if (e.Length == minLength[e.Start])
                    {
                        result.Add(e);
                    }
                }
            }
            Console.WriteLine();
            return result;
        }
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
            else
            {
                Console.WriteLine("Wow, grymt, de matchar!");
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
