using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Dijkstra
{
    public class SearchNode<T> : IComparable
    {
        #region Fields

        GraphNode<T> graphNode;
        double distance;
        SearchNode<T> previous;

        #endregion

        #region Constructors
        
        public SearchNode(GraphNode<T> graphNode)
        {
            this.graphNode = graphNode;
            distance = double.MaxValue;
            previous = null;
        }
        
        #endregion

        #region Properties
        
        public GraphNode<T> GraphNode
        {
            get { return graphNode; }
        }

        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public SearchNode<T> Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares this instance to the provided object and
        /// returns their relative order
        /// </summary>
        /// <returns>relative order</returns>
        /// <param name="obj">object to compare to</param>
        public int CompareTo(object obj)
        {
            // instance is always greater than null
            if (obj == null)
            {
                return 1;
            }

            // check for correct object type
            SearchNode<T> otherSearchNode = obj as SearchNode<T>;
            if (otherSearchNode != null)
            {
                if (distance < otherSearchNode.Distance)
                {
                    return -1;
                }
                else if (distance == otherSearchNode.Distance)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                throw new ArgumentException("Object is not a SearchNode");
            }
        }

        #endregion

    }
}
