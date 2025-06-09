using System.Collections.Generic;
using Graph.Enums;
using Graph.Models;
using Graph.Strategies.Base;

namespace Graph.Strategies
{
    public class DepthFirstSearchStrategy : FindPathStrategy
    {
        public override List<NodeModel> FindPath(NodeModel startNode, ENodeType targetType, IReadOnlyDictionary<NodeModel, List<EdgeModel>> edgeMap)
        {
            Stack<NodeModel> stack = new();
            Dictionary<NodeModel, NodeModel> previous = new();
            HashSet<NodeModel> visited = new();

            stack.Push(startNode);

            while (stack.Count > 0)
            {
                NodeModel current = stack.Pop();
 
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

                    if (!visited.Contains(neighbor))
                    {
                        previous[neighbor] = current;
                        stack.Push(neighbor);
                    }
                }
            }

            return null;
        }
    }
}