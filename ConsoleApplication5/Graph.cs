using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Graph<T>
    {
        public Graph(int numVerts)
        {
            m_maxVerts = numVerts;
            m_adjMatrix = new int[m_maxVerts][];
            m_vertVisits = new int[m_maxVerts];

            for (int i = 0; i < m_maxVerts; i++)
            {
                m_adjMatrix[i] = new int[m_maxVerts];
                m_adjMatrix[i][i] = 0;
                m_vertVisits[i] = 0;
            }
        }
        
        public bool Push(T node)
        {
            if ((int)m_vertices.Count >= m_maxVerts)
                return false;

            m_vertices.Add(new GraphNode<T>(node));
            return true;
        }

        public List<GraphNode<T>> Node
        {
            get
            {
                return m_vertices;
            }
        }

        public void AttachEdge(int index1, int index2, int weight)
        {
            m_adjMatrix[index1][index2] = weight;
        }

        public void outputMatrix()
        {
            for (int i = 0; i < m_maxVerts; i++)
            {
                for (int j = 0; j < m_maxVerts; j++)
                {
                    if (j < m_maxVerts - 1)
                        Console.Write(" {0}", m_adjMatrix[i][j].ToString());
                    else
                        Console.WriteLine(" {0}", m_adjMatrix[i][j].ToString());
                }
            }
        }

        public void outputVertices()
        {
            for (int i = 0; i < m_maxVerts; i++)
                Console.WriteLine("{0}. {1}", i, m_vertices[i].m_node);
        }

        public void outputEdges()
        {
            for (int i = 0; i < m_maxVerts; i++)
            {
                for (int j = 0; j < m_maxVerts; j++)
                {
                    if (m_adjMatrix[i][j] != 0)
                    {
                        Console.WriteLine("{0}:{1}", m_vertices[i].m_node, m_vertices[j].m_node);
                    }
                }
            }
        }

        public int getNextUnvisitedNode(int index)
        {
            for (int i = 0; i < m_maxVerts; i++)
            {
                if (m_adjMatrix[index][i] > 0 && m_vertVisits[i] == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public int getNextUnvisitedNodeWeight(int index)
        {
            for (int i = 0; i < m_maxVerts; i++)
            {
                if (m_adjMatrix[index][i] > 0 && m_vertVisits[i] == 0)
                {
                    return m_adjMatrix[index][i];
                }
            }

            return -1;
        }

        public int getEdgeWeight(int index1, int index2)
        {
            return m_adjMatrix[index1][index2];
        }

        public string DirectRoute(GraphNode<T>[] route)
        {
            GraphNode<T> vert;
            GraphNode<T> nextVert;
            int weight = 0;
            int totalWeight = 0;
            for (int i = 0; i < route.Length - 1; i++)
            {
                vert = route[i];
                nextVert = route[i + 1];

                if (vert.Edges.ContainsKey(nextVert))
                {
                    weight = vert.Edges[nextVert];
                    totalWeight += weight;
                }
                else
                    return "NO SUCH ROUTE";
            }

            return totalWeight.ToString();
        }

        public string DepthFirstSearch(int startIndex, int endIndex)
        {
            m_vertVisits[startIndex] = 1;

            Console.WriteLine(m_vertices[startIndex].m_node);

            Stack<int> searchStack = new Stack<int>();
            int vert = 0;
            int weight = 0;
            int totalWeight = 0;

            searchStack.Push(startIndex);

            while (searchStack.Count != 0)
            {
                vert = getNextUnvisitedNode(searchStack.Peek());
                weight = getNextUnvisitedNodeWeight(searchStack.Peek());

                if (vert == -1)
                {
                    searchStack.Pop();
                }
                else
                {
                    m_vertVisits[vert] = 1;
                    totalWeight += weight;

                    Console.WriteLine(m_vertices[vert].m_node);

                    searchStack.Push(vert);
                }

                if (vert == endIndex)
                {
                    for (int i = 0; i < m_maxVerts; i++)
                    {
                        m_vertVisits[i] = 0;
                    }
                    return totalWeight.ToString();
                }
            }

            for (int i = 0; i < m_maxVerts; i++)
            {
                m_vertVisits[i] = 0;
            }
            return "NO SUCH ROUTE";
        }
        
        public int numOfRoutesUnderNStops(GraphNode<T> startIndex, GraphNode<T> endIndex, int stops)
        {
            List<Dictionary<GraphNode<T>, int>> edgeList = new List<Dictionary<GraphNode<T>, int>>();
            int numOfRoutes = 0;

            edgeList.Add(startIndex.Edges);

            for (int i = 0; i < stops; i++)
            {

                foreach (KeyValuePair<GraphNode<T>, int> edge in edgeList[i])
                {
                    edgeList.Add(edge.Key.Edges);
                }
            }

            foreach (Dictionary<GraphNode<T>, int> list in edgeList)
            {
                Console.WriteLine("List {0}", edgeList.IndexOf(list));
                foreach (KeyValuePair<GraphNode<T>, int> edge in list)
                {
                    Console.WriteLine(edge.Key.m_node);
                    if (edge.Key == endIndex)
                        numOfRoutes += 1;
                }
            }

            return numOfRoutes;
        }

        public int tripCount = 0;
        public void dfs(GraphNode<T> end, string path, int maxLength)
        {

            // this is for debug and trace
            // System.out.println(";; " + path);

            // if the path reach the maximum stops, then cancel search
            if (path.Length - 1 > maxLength) return;

            // check if we have reach the "end" node
            if (path.Length > 1 && path[path.Length - 1].ToString() == end.m_node.ToString())
            {
                tripCount++;
                Console.WriteLine(path + ", " + tripCount);
            }

            // caculate the lastest node index in map
            char lastChar = path[path.Length - 1];
            GraphNode<T> lastNodeIndex = Node[lastChar - 'A'];
            //Console.WriteLine(lastNodeIndex.m_node);

            // loop all nodes in map which connected to lastNode, and try it
            for (int i = 0; i < lastNodeIndex.Edges.Count; i++)
            {
                char newChar = lastNodeIndex.Edges.Keys.ToList()[i].m_node.ToString()[0];
                dfs(end, path + newChar, maxLength);
            }
        }
        
        // Shortest Route when from and to is the index
        public int shortestRoute(int startIndex, int endIndex)
        {
            List<int> tempStack = new List<int>();
            int totalChecked = 0;
            int nextVert = 0;
            int currentVert = startIndex;
            int totalWeight = 0;

            while (totalChecked < m_maxVerts - 1)
            {
                for (int i = 0; i < m_maxVerts; i++)
                {
                    if (m_adjMatrix[currentVert][i] != 0)
                    {
                        tempStack.Add(i);
                    }

                }

                nextVert = tempStack[0];

                for (int i = 0; i < tempStack.Count - 1; i++)
                {
                    if (m_adjMatrix[currentVert][tempStack[i + 1]] < m_adjMatrix[currentVert][nextVert] && m_adjMatrix[currentVert][tempStack[i + 1]] != 0)
                    {
                        nextVert = tempStack[i + 1];
                    }
                }

                totalWeight += m_adjMatrix[currentVert][nextVert];

                if (nextVert == endIndex)
                {
                    return totalWeight;
                }

                tempStack.Clear();

                currentVert = nextVert;
                totalChecked++;
            }

            return totalWeight;

        }

        public int cnt = 0;
        public void TripCountUnderDist30(String end, String path, int cost)
        {

            if (cost >= 30)
            {
                return;
            }

            if (cost > 0 && path.EndsWith(end))
            {
                cnt += 1;
                Console.WriteLine(path + ", " + cost + ", " + cnt);
            }

            char lastChar = path.ElementAt(path.Length - 1);
            int lastNodeIndex = lastChar - 'A';

            for (int i = 0; i < m_adjMatrix[lastNodeIndex].Length; i++)
            {
                char newChar = (char)(i + 'A');
                Console.WriteLine(i);
                int newCost = m_adjMatrix[lastNodeIndex][i];
                if (newCost > 0)
                {
                    TripCountUnderDist30(end, path + newChar, cost + newCost);
                }
            }
        }

        public List<GraphNode<T>> route = new List<GraphNode<T>>();

        private List<GraphNode<T>> m_vertices = new List<GraphNode<T>>();
        private int m_maxVerts;
        public int[][] m_adjMatrix;
        private int[] m_vertVisits;
    }
    
    public class Edge
    {
        public int from { get; private set; }
        public int to { get; private set; }
        public Double weight { get; private set; }

        public Edge(int from, int to, Double weight)
        {
            this.from = from;
            this.to = to;
            this.weight = weight;
        }
    }

    public class Node
    {
        //public Point p { get; private set; }
        public char ident { get; private set; }
        public int dist { get; set; }

        //Point p,
        public Node(char ident)
        {
            //this.p = p;
            this.ident = ident;
        }
    }


}
