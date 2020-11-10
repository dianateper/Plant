using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Model;

namespace Server.Dijkstra
{
    /// <summary>
    /// Builds the graph
    /// </summary>
    class GraphBuilder
    {
        static Graph<Position> graph;
        LinkedList<Position> path;
        LinkedListNode<Position> currentTarget;

        /*
        void Awake()
        {

            Position start = GameObject.FindGameObjectWithTag("Start").GetComponent<Position>();
            Position end = GameObject.FindGameObjectWithTag("End").GetComponent<Position>();
            Position[] positions = GameObject.FindGameObjectsWithTag("Waypoint");

            // add nodes (all waypoints, including start and end) to graph
            graph = new Graph<Position>();
            graph.AddNode(start);
            graph.AddNode(end);
            foreach (Position position in positions)
            {
                graph.AddNode(position.GetComponent<Position>());
            }

            // add edges to graph
            foreach (GraphNode<Position> firstNode in graph.Nodes)
            {
                foreach (GraphNode<Position> secondNode in graph.Nodes)
                {
                    // no self edges
                    if (firstNode != secondNode)
                    {
                        Vector2 positionDelta = firstNode.Value.Position - secondNode.Value.Position;
                        if (Mathf.Abs(positionDelta.x) < 3.5f && Mathf.Abs(positionDelta.y) < 3f)
                        {
                            firstNode.AddNeighbor(secondNode, positionDelta.magnitude);
                        }
                    }
                }
            }
        }
        */

        public static Graph<Position> Graph
        {
            get { return graph; }
        }

        public LinkedList<Position> Search(Position start, Position end, Graph<Position> graph)
        {
            SortedLinkedList<SearchNode<Position>> searchList = new SortedLinkedList<SearchNode<Position>>();
            Dictionary<GraphNode<Position>, SearchNode<Position>> mapping =
                new Dictionary<GraphNode<Position>, SearchNode<Position>>();

            // save references to start and end graph nodes
            GraphNode<Position> startNode = graph.Find(start);
            GraphNode<Position> endNode = graph.Find(end);

            // add search nodes for all graph nodes to list
            foreach (GraphNode<Position> node in graph.Nodes)
            {
                SearchNode<Position> searchNode = new SearchNode<Position>(node);
                if (node == startNode)
                {
                    searchNode.Distance = 0;
                }
                searchList.Add(searchNode);
                mapping.Add(node, searchNode);
            }

            string debug = ConvertSearchListToString(searchList);

            // search until find end node or list is empty
            while (searchList.Count > 0)
            {
                // front of search list has smallest distance
                SearchNode<Position> currentSearchNode = searchList.First.Value;
                searchList.RemoveFirst();
                GraphNode<Position> currentGraphNode = currentSearchNode.GraphNode;
                mapping.Remove(currentGraphNode);

                // check for found end node
                if (currentGraphNode == endNode)
                {
                    return BuildPath(currentSearchNode);
                }

                // loop through the current graph node's neighbors
                foreach (GraphNode<Position> neighbor in currentGraphNode.Neighbors)
                {
                    // only process neighbors still in the search list
                    if (mapping.ContainsKey(neighbor))
                    {
                        // check for new shortest distance on path from start to neighbor
                        double currentDistance = currentSearchNode.Distance +
                                                currentGraphNode.GetEdgeWeight(neighbor);
                        SearchNode<Position> neighborSearchNode = mapping[neighbor];
                        if (currentDistance < neighborSearchNode.Distance)
                        {
                            // found a shorter path to the neighbor
                            neighborSearchNode.Distance = currentDistance;
                            neighborSearchNode.Previous = currentSearchNode;
                            searchList.Reposition(neighborSearchNode);

                            debug = ConvertSearchListToString(searchList);
                        }

                    }
                }
            }

            // didn't find a path from start to end nodes
            return null;
        }

        LinkedList<Position> BuildPath(SearchNode<Position> endNode)
        {
            LinkedList<Position> path = new LinkedList<Position>();
            path.AddFirst(endNode.GraphNode.Value);
            SearchNode<Position> previous = endNode.Previous;
            while (previous != null)
            {
                path.AddFirst(previous.GraphNode.Value);
                previous = previous.Previous;
            }
            return path;
        }

        string ConvertSearchListToString(SortedLinkedList<SearchNode<Position>> searchList)
        {
            StringBuilder listString = new StringBuilder();
            LinkedListNode<SearchNode<Position>> currentNode = searchList.First;
            while (currentNode != null)
            {
                listString.Append("[");
                listString.Append(currentNode.Value.GraphNode.Value.PositionId + " ");
                listString.Append(currentNode.Value.Distance + "] ");
                currentNode = currentNode.Next;
            }
            return listString.ToString();
        }

    }
}
