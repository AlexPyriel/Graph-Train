using System.Collections.Generic;
using Graph.Enums;
using Graph.Models;
using Graph.Strategies.Base;

namespace Graph.Strategies
{
    public class BreadthFirstSearchStrategy : FindPathStrategy
    {
        public override List<NodeModel> FindPath(NodeModel startNode, ENodeType targetType, IReadOnlyDictionary<NodeModel, List<EdgeModel>> edgeMap)
        {
            Queue<NodeModel> queue = new();
            Dictionary<NodeModel, NodeModel> previous = new();
            HashSet<NodeModel> visited = new();

            queue.Enqueue(startNode);
            visited.Add(startNode);

            while (queue.Count > 0)
            {
                NodeModel current = queue.Dequeue();

                if (current.Type == targetType)
                {
                    return ReconstructPath(current, previous);
                }

                foreach (EdgeModel edge in edgeMap[current])
                {
                    NodeModel neighbor = edge.GetNeighbor(current);

                    if (visited.Add(neighbor))
                    {
                        previous[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return null;
        }
    }
}