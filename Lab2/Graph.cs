using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    /// <summary>
    ///  A graph.
    /// </summary>
    public class Graph
    {
        /// <summary>
        ///  The maximum edge length in the graph.
        /// </summary>
        public const double MAX_EDGE_LENGTH = 1000.0;
        /// <summary>
        ///  The nodes of this graph.
        /// </summary>
        public List<Node> Nodes { get; private set; }
        /// <summary>
        ///  The edges of this graph.
        /// </summary>
        public List<Edge> Edges { get; private set; }
        /// <summary>
        ///  The length of all edges in this graph.
        /// </summary>
        public double[,]  AllEdgeLengths { get; private set; }

        /// <summary>
        ///  Creates a new graph.
        /// </summary>
        /// <param name="nodes">the number of nodes.</param>
        /// <param name="percentEdges">the percentage of edges for each node. Do not set this to above 80%.</param>
        /// <param name="doubleSidedEdges">should the edges be double-sided?</param>
        public Graph(int nodes, int percentEdges/*, bool doubleSidedEdges = false*/)
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
            for (int i = 0; i < nodes; i++) {
                Nodes.Add(new Node(i));
            }
            CreateEdges(percentEdges, true/*doubleSidedEdges*/);
        }

        /// <summary>
        ///  Print the edges of each node.
        /// </summary>
        public void Print()
        {
            Print(Edges);
        }

        /// <summary>
        ///  Print a list of edges for this graph.
        /// </summary>
        /// <param name="edges">the list of edges.</param>
        /// <exception cref="ArgumentException">something wasn't right.</exception>
        public void Print(List<Edge> edges)
        {
            if (Edges == null) {
                throw new ArgumentException("The list of edges is NULL.");
            }
            // Add detection of other problems.

            List<Edge> sortedEdges = new List<Edge>(edges);
            sortedEdges.Sort((a,b) => (a.Start <= b.Start && a.End <= b.End) ? -1 : 1);

            foreach (Node n in Nodes) {
                Console.Write($"Node {n.Id}: ");
                foreach (Edge e in sortedEdges) {
                    if (e.Start == n.Id) {
                        Console.Write($"({e.Start}--{e.Length}-->{e.End}), ");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        ///  Output a string showing the graph.
        /// </summary>
        /// <returns>the output string.</returns>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append($"Graph nodes: {Nodes.Count}\n");
            foreach (Node n in Nodes) {
                output.Append("  " + n.ToString());
            }
            return output.ToString();
        }

        private void CreateEdges(int percentEdges, bool doubleSidedEdges)
        {
            if (percentEdges < 0 || 80 <= percentEdges) {
                throw new ArgumentException("Bad percent of edges.");
            }
            AllEdgeLengths = new double[Nodes.Count, Nodes.Count];
            foreach (Node nodeA in Nodes) {
                foreach (Node nodeB in Nodes) {
                    AllEdgeLengths[nodeA.Id, nodeB.Id] = int.MaxValue;
                }
                AllEdgeLengths[nodeA.Id, nodeA.Id] = 0;
            }
            int edgesLeft = (int)(0.01 * percentEdges * Nodes.Count * Nodes.Count);
            double length = 0;
            int start = 0;
            int end = 0;

            while (edgesLeft > 0) {
                start = random.Next(0, Nodes.Count);
                end = random.Next(0, Nodes.Count);
                length = MAX_EDGE_LENGTH * random.NextDouble();
                length = (length == 0.0 ? 1.0 : length);

                // Using double sided edges.
                if (doubleSidedEdges) {
                    if (start != end && AllEdgeLengths[start, end] >= int.MaxValue && AllEdgeLengths[end, start] >= int.MaxValue) {
                        edgesLeft -= 2;
                        AllEdgeLengths[start, end] = length;
                        Edge e1 = new Edge(start, end, length);
                        Nodes[start].Edges.Add(e1);
                        Edges.Add(e1);
                        AllEdgeLengths[end, start] = length;
                        Edge e2 = new Edge(end, start, length);
                        Nodes[end].Edges.Add(e2);
                        Edges.Add(e2);
                    }
                } else {
                    if (start != end && AllEdgeLengths[start, end] >= int.MaxValue) {
                        edgesLeft--;
                        AllEdgeLengths[start, end] = length;
                        Edge e = new Edge(start, end, length);
                        Nodes[start].Edges.Add(e);
                        Edges.Add(e);
                    }
                }
            }
        }

        Random random = new Random();
    }
}
