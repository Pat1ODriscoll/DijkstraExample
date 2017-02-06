using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    public class GraphNode<T>
    {
        public GraphNode(T node)
        {
            this.m_node = node;
        }

        public T m_node { get; set; }

        public Dictionary<GraphNode<T>, int> Edges
        {
            get
            {
                return EdgeList;
            }
        }

        public void isEdgeOf(GraphNode<T> v, int weight)
        {
            EdgeList.Add(v, weight);
        }

        Dictionary<GraphNode<T>, int> EdgeList = new Dictionary<GraphNode<T>, int>();
    }
}
