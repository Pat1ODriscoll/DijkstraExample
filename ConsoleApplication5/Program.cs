using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    
        public class Program
        {
            public static int tripCount = 0;
            public static List<Node> Vertices = new List<Node>();
            public static List<Edge> Edges = new List<Edge>();

            /* Counter for giving nodes a unique ID */
            //private static int numV = 0;
   

            static void Main(string[] args)
            {

            Graph<char> TESTGraph = new Graph<char>(5);

            try
                {

          
      
                //add vertices to the graph
                TESTGraph.Push('A');
                TESTGraph.Push('B');
                TESTGraph.Push('C');
                TESTGraph.Push('D');
                TESTGraph.Push('E');

                TESTGraph.AttachEdge(0, 1, 5);
                TESTGraph.AttachEdge(1, 2, 4);
                TESTGraph.AttachEdge(2, 3, 8);
                TESTGraph.AttachEdge(3, 2, 8);
                TESTGraph.AttachEdge(3, 4, 6);
                TESTGraph.AttachEdge(0, 3, 5);
                TESTGraph.AttachEdge(2, 4, 2);
                TESTGraph.AttachEdge(4, 1, 3);
                TESTGraph.AttachEdge(0, 4, 7);

                TESTGraph.Node[0].isEdgeOf(TESTGraph.Node[1], 5);
                TESTGraph.Node[1].isEdgeOf(TESTGraph.Node[2], 4);
                TESTGraph.Node[2].isEdgeOf(TESTGraph.Node[3], 8);
                TESTGraph.Node[3].isEdgeOf(TESTGraph.Node[2], 8);
                TESTGraph.Node[3].isEdgeOf(TESTGraph.Node[4], 6);
                TESTGraph.Node[0].isEdgeOf(TESTGraph.Node[3], 5);
                TESTGraph.Node[2].isEdgeOf(TESTGraph.Node[4], 2);
                TESTGraph.Node[4].isEdgeOf(TESTGraph.Node[1], 3);
                TESTGraph.Node[0].isEdgeOf(TESTGraph.Node[4], 7);


            }
            catch (Exception)
            {

                throw;
            }


            string input = "";



                while (input != "-1")
                {




                    Console.WriteLine();
                    Console.WriteLine("Please enter numbers 1 to 4 for relevant functions and anything else to exit: ");
                    Console.WriteLine();
                    Console.WriteLine("1 - findTheDistance for all distances stated in the problem..." );
                    Console.WriteLine();
                    Console.WriteLine("2 - findTheNumOfTrips between any two nodes under a number of stops entered...");
                    Console.WriteLine();
                    Console.WriteLine("3 - findTheShortestDistance between any two nodes...");
                    Console.WriteLine();
                    Console.WriteLine("4 - findTheNumOfTripsWithConstraintOfDistance between two nodes...");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":

                            findTheDistance(TESTGraph);

                            break;

                        case "2":

                            findTheNumOfTrips(TESTGraph);

                            break;

                        case "3":

                            findTheShortestDistance(TESTGraph);

                            break;

                        case "4":

                            findTheNumOfTripsWithConstraintOfDistance( TESTGraph);

                            break;

                        default:

                            Console.WriteLine("You entered an incorrect ");
                            Environment.Exit(0);
                            break;
                    }
                }


                Console.ReadLine();

            }

            public static int numOfRoutesUnderNStops(GraphNode<int> startIndex, GraphNode<int> endIndex, int stops)
            {
                List<Dictionary<GraphNode<int>, int>> edgeList = new List<Dictionary<GraphNode<int>, int>>();
                int numOfRoutes = 0;

                edgeList.Add(startIndex.Edges);

                for (int i = 0; i < stops; i++)
                {

                    foreach (KeyValuePair<GraphNode<int>, int> edge in edgeList[i])
                    {
                        edgeList.Add(edge.Key.Edges);
                    }
                }

                foreach (Dictionary<GraphNode<int>, int> list in edgeList)
                {
                    Console.WriteLine("List {0}", edgeList.IndexOf(list));
                    foreach (KeyValuePair<GraphNode<int>, int> edge in list)
                    {
                        Console.WriteLine(edge.Key.m_node);
                        if (edge.Key == endIndex)
                            numOfRoutes += 1;
                    }
                }

                return numOfRoutes;
            }

            private static void findTheNumOfTripsWithConstraintOfDistance(Graph<char> TESTGraph)
            {
                Console.WriteLine("Please enter the Node to go from");
                string from = Console.ReadLine();
                Console.WriteLine("Please enter the Node to go to");
                string to = Console.ReadLine();

                
                TESTGraph.TripCountUnderDist30(from, to, 0);
            
            }

            private static void findTheShortestDistance(Graph<char> TESTGraph)
            {
                Console.WriteLine("Please enter the Node to go from");
                char from = Convert.ToChar(Console.ReadLine());
                int start = TESTGraph.Node.FindIndex(x => x.m_node == from);
                Console.WriteLine("Please enter the Node to go to");
                char to = Convert.ToChar(Console.ReadLine());
                int end = TESTGraph.Node.FindIndex(x => x.m_node == to);

                if (start == end)
                {
                    Console.WriteLine("The shortest route is:" + TESTGraph.shortestRoute(start, end));
                }
                else
                {

                    // start at node A
                    try
                    {

                        // Second argument is a starting node!!
                        Dijkstra dijk = new Dijkstra(TESTGraph.m_adjMatrix, start);
                        double[] dist = dijk.dist;
                        int[] path = dijk.path;

                        Console.WriteLine("The shortest route is:" + dist[end]);
                    }
                    catch (ArgumentException err)
                    {
                        Console.WriteLine(err.Message);
                    }
                }

            }

            private static void findTheNumOfTrips(Graph<char> TESTGraph)
            {
                //Implement the numOfRoutesUnderNStops
                //create Graph object to hold the directed graph;
               

                Console.WriteLine("Please enter the maximum number of stops");
                int numOfStops = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter the Node to go from");
                char from  = Convert.ToChar(Console.ReadLine());
                GraphNode<char> start = TESTGraph.Node.FirstOrDefault(x => x.m_node == from);
                Console.WriteLine("Please enter the Node to go to");
                char to = Convert.ToChar(Console.ReadLine());
                GraphNode<char> end = TESTGraph.Node.FirstOrDefault(x => x.m_node == to);

                Console.WriteLine("The number of trips is:" +  TESTGraph.numOfRoutesUnderNStops(start, end, numOfStops));

            }

            private static void findTheDistance(Graph<char> TESTGraph)
            {
                Console.WriteLine("This calculates the distances between nodes stated in the problem:");
                Console.WriteLine("This calculates the distances between A-B-C:");
                TESTGraph.route.Add(TESTGraph.Node[0]);
                TESTGraph.route.Add(TESTGraph.Node[1]);
                TESTGraph.route.Add(TESTGraph.Node[2]);
                Console.WriteLine("Output #1: {0}", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
                TESTGraph.route.Clear();

                Console.WriteLine("This calculates the distances between A-D:");
                TESTGraph.route.Add(TESTGraph.Node[0]);
                TESTGraph.route.Add(TESTGraph.Node[3]);
                Console.WriteLine("Output #2: {0}", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
                TESTGraph.route.Clear();

                Console.WriteLine("This calculates the distances between A-D-C:");
                TESTGraph.route.Add(TESTGraph.Node[0]);
                TESTGraph.route.Add(TESTGraph.Node[3]);
                TESTGraph.route.Add(TESTGraph.Node[2]);
                Console.WriteLine("Output #3: {0}", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
                TESTGraph.route.Clear();

                Console.WriteLine("This calculates the distances between A-E-B-C-D:");
                TESTGraph.route.Add(TESTGraph.Node[0]);
                TESTGraph.route.Add(TESTGraph.Node[4]);
                TESTGraph.route.Add(TESTGraph.Node[1]);
                TESTGraph.route.Add(TESTGraph.Node[2]);
                TESTGraph.route.Add(TESTGraph.Node[3]);
                Console.WriteLine("Output #4: {0}", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
                TESTGraph.route.Clear();

                Console.WriteLine("This calculates the distances between A-E-D:");
                TESTGraph.route.Add(TESTGraph.Node[0]);
                TESTGraph.route.Add(TESTGraph.Node[4]);
                TESTGraph.route.Add(TESTGraph.Node[3]);
                Console.WriteLine("Output #5: {0}", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
                TESTGraph.route.Clear();

            }


        }

 }


