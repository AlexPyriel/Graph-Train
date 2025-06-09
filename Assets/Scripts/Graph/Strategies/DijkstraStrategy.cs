using System.Collections.Generic;
using Graph.Enums;
using Graph.Models;
using Graph.Strategies.Base;
using Utils;

namespace Graph.Strategies
{
    public class DijkstraStrategy : FindPathStrategy
    {
        public override List<NodeModel> FindPath(NodeModel startNode, ENodeType targetType, IReadOnlyDictionary<NodeModel, List<EdgeModel>> edgeMap)
        {
            Dictionary<NodeModel, float> distances = new ();
            Dictionary<NodeModel, NodeModel> previous = new ();
            HashSet<NodeModel> visited = new ();
            PriorityQueue<NodeModel> queue = new ();

            foreach (NodeModel node in edgeMap.Keys)
                distances[node] = float.PositiveInfinity;

            distances[startNode] = 0;
            queue.Enqueue(startNode, 0);

            while (queue.Count > 0)
            {
                NodeModel current = queue.Dequeue();

                if (!visited.Add(current))
                {
                    continue;
                }

                if (current.Type == targetType)
                {
                    return ReconstructPath(current, previous);
                }

                foreach (EdgeModel edge in edgeMap[current])
                {
                    NodeModel neighbor = edge.GetNeighbor(current);

                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }

                    float alt = distances[current] + edge.Distance;
                    if (alt < distances[neighbor])
                    {
                        distances[neighbor] = alt;
                        previous[neighbor] = current;
                        queue.Enqueue(neighbor, alt);
                    }
                }
            }

            return null;
        }
    }
}