using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ConsoleApplication5
{
    [TestClass]
    public class UnitTest1
    {
        
        Graph<char> TESTGraph = new Graph<char>(5);

        [TestMethod]
        public void BuildTESTGraphTestOneRoute()
        {

            //Act
            //add vertices to the graph
            TESTGraph.Push('A');
            TESTGraph.Push('B');

            TESTGraph.AttachEdge(0, 1, 5);

            TESTGraph.Node[0].isEdgeOf(TESTGraph.Node[1], 5);

            //ACT
            TESTGraph.route.Add(TESTGraph.Node[0]);
            TESTGraph.route.Add(TESTGraph.Node[1]);


            //Assert
            Assert.AreEqual("5", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));

            // CLEAR
            TESTGraph.route.Clear();

        }

        [TestMethod]
        public void BuildTESTGraphTestMANYRoutes()
        {
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

            //ACT
            TESTGraph.route.Add(TESTGraph.Node[0]);
            TESTGraph.route.Add(TESTGraph.Node[4]);
            TESTGraph.route.Add(TESTGraph.Node[1]);
            TESTGraph.route.Add(TESTGraph.Node[2]);
            TESTGraph.route.Add(TESTGraph.Node[3]);

            //ASSERT
            Assert.AreEqual("22",TESTGraph.DirectRoute(TESTGraph.route.ToArray()));

            //CLEAR
            TESTGraph.route.Clear();

        }

        [TestMethod]
        public void BuildTESTGraphShortestDistancesFromNodeA()
        {
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

            //ACT
            Dijkstra dijk = new Dijkstra(TESTGraph.m_adjMatrix, 0);
            double[] dist = dijk.dist;

            //ASSERT
            Assert.AreEqual(0, dist[0]);
            Assert.AreEqual(5, dist[1]);
            Assert.AreEqual(9, dist[2]);
            Assert.AreEqual(5, dist[3]);
            Assert.AreEqual(7, dist[4]);
        }

        [TestMethod]
        public void BuildTESTGraphNumOfRoutesUnderNStops()
        {
            //ARRANGE
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




            int numOfStops = 3;
            char from = 'A';
            GraphNode<char> start = TESTGraph.Node.FirstOrDefault(x => x.m_node == from);

            char to = 'C';
            GraphNode<char> end = TESTGraph.Node.FirstOrDefault(x => x.m_node == to);
            
            //ACT
            int routes = TESTGraph.numOfRoutesUnderNStops(start, end, numOfStops);
            
            //ASSERT
            Assert.AreEqual(2, routes);

        }
        
        [TestMethod]
        public void BuildTESTGraphAttachingEdges()
        {

            //ARRANGE
            TESTGraph.Push('A');
            TESTGraph.Push('B');

            TESTGraph.AttachEdge(0, 1, 5);

            TESTGraph.Node[0].isEdgeOf(TESTGraph.Node[1], 5);


            //ARRANGE          
            int weight = TESTGraph.getEdgeWeight(0, 1);

            var test = TESTGraph.Node;


            //ASSERT
            Assert.AreEqual(5, weight);
            
        }

        [TestMethod]
        public void BuildTESTGraphCountingNodes()
        {

            //ARRANGE
            TESTGraph.Push('A');
            TESTGraph.Push('B');

            TESTGraph.AttachEdge(0, 1, 5);

            TESTGraph.Node[0].isEdgeOf(TESTGraph.Node[1], 5);


            //ARRANGE          
            int cnt = TESTGraph.Node.Count;


            //ASSERT
            Assert.AreEqual(2, cnt);

        }

        [TestMethod]
        public void BuildTESTGraphANSWERToQuestion1()
        {
            //ARRANGE
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

            //ACT
            TESTGraph.route.Add(TESTGraph.Node[0]);
            TESTGraph.route.Add(TESTGraph.Node[1]);
            TESTGraph.route.Add(TESTGraph.Node[2]);
  
            //ASSERT
            Assert.AreEqual("9", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));

            
            //CLEAR
            TESTGraph.route.Clear();

        }

        [TestMethod]
        public void BuildTESTGraphANSWERToQuestion2()
        {
            //ARRANGE
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

          //ACT
            TESTGraph.route.Add(TESTGraph.Node[0]);
            TESTGraph.route.Add(TESTGraph.Node[3]);

            //ASSERT
            Assert.AreEqual("5", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
        }

        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion3()
        {
            //ARRANGE
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

            //ACT
            TESTGraph.route.Add(TESTGraph.Node[0]);
            TESTGraph.route.Add(TESTGraph.Node[3]);
            TESTGraph.route.Add(TESTGraph.Node[2]);


            //ASSERT
            Assert.AreEqual("13", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
        }

        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion4()
        {
            //ARRANGE
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

            //ACT
            TESTGraph.route.Add(TESTGraph.Node[0]);
            TESTGraph.route.Add(TESTGraph.Node[4]);
            TESTGraph.route.Add(TESTGraph.Node[1]);
            TESTGraph.route.Add(TESTGraph.Node[2]);
            TESTGraph.route.Add(TESTGraph.Node[3]);
            
            //ASSERT
            Assert.AreEqual("22", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));
        }


        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion5()
        {
            //ARRANGE
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

            //ACT
            TESTGraph.route.Add(TESTGraph.Node[0]);
            TESTGraph.route.Add(TESTGraph.Node[4]);
            TESTGraph.route.Add(TESTGraph.Node[3]);


            //ASSERT
            Assert.AreEqual("NO SUCH ROUTE", TESTGraph.DirectRoute(TESTGraph.route.ToArray()));

        }

        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion6()
        {
            //ARRANGE
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

            //ACT
            int numOfStops = 3;

            char from = 'C';

            GraphNode<char> start = TESTGraph.Node.FirstOrDefault(x => x.m_node == from);
          
            char to = 'C';

            GraphNode<char> end = TESTGraph.Node.FirstOrDefault(x => x.m_node == to);

            //ASSERT
            Assert.AreEqual(2, TESTGraph.numOfRoutesUnderNStops(start, end , numOfStops));
        }

        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion7()
        {

            //ARRANGE
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

            //ACT
            int numOfStops = 4;

            char from = 'A';

            GraphNode<char> start = TESTGraph.Node.FirstOrDefault(x => x.m_node == from);

            char to = 'C';

            GraphNode<char> end = TESTGraph.Node.FirstOrDefault(x => x.m_node == to);

            //ASSERT
            Assert.AreEqual(3, TESTGraph.numOfRoutesUnderNStops(start, end, numOfStops));
        }

        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion8()
        {
            //ARRANGE
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

            //ACT

            char from = 'A';
            int start = TESTGraph.Node.FindIndex(x => x.m_node == from);
            
            char to = 'C';
            int end = TESTGraph.Node.FindIndex(x => x.m_node == to);

            Dijkstra dijk = new Dijkstra(TESTGraph.m_adjMatrix, start);
            double[] dist = dijk.dist;

            //ASSERT
            Assert.AreEqual(9, dist[end]);
        }

        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion9()
        {

            //ARRANGE
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

            //ACT

            char from = 'B';
            int start = TESTGraph.Node.FindIndex(x => x.m_node == from);

            char to = 'B';
            int end = TESTGraph.Node.FindIndex(x => x.m_node == to);

            //ASSERT
            Assert.AreEqual(9, TESTGraph.shortestRoute(start, end));
        }

        [TestMethod]
        public void BuildTESTGraphANSWERSToQuestion10()
        {
            //ARRANGE
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

            //ACT
            string from = "C";
           
            string to = "C";

            TESTGraph.TripCountUnderDist30(from, to, 0);

            //ASSERT
            Assert.AreEqual(7, TESTGraph.cnt);

            

        }



        }
}
