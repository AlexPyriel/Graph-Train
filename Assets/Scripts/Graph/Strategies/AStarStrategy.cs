using System.Collections.Generic;
using System.Linq;
using Game.Enums;
using Graph.Models;
using Graph.Strategies.Base;
using UnityEngine;
using Utils;

namespace Graph.Strategies
{
    public class AStarStrategy : FindPathStrategy
    {
        private Vector3 _targetPosition;
        public override List<NodeModel> FindPath(NodeModel startNode, ENodeType targetType, IReadOnlyDictionary<NodeModel, List<EdgeModel>> edgeMap)
        {
            NodeModel targetNode = edgeMap.Keys.FirstOrDefault(nodeModel => nodeModel.type == targetType);

            if (!targetNode)
            {
                return null;
            }
            _targetPosition = targetNode.transform.position;

            Dictionary<NodeModel, float> gScore = new();
            Dictionary<NodeModel, float> fScore = new();
            Dictionary<NodeModel, NodeModel> cameFrom = new();
            PriorityQueue<NodeModel> openSet = new();
            HashSet<NodeModel> visited = new();

            foreach (NodeModel node in edgeMap.Keys)
            {
                gScore[node] = float.PositiveInfinity;
                fScore[node] = float.PositiveInfinity;
            }

            gScore[startNode] = 0;
            fScore[startNode] = Heuristic(startNode);
            openSet.Enqueue(startNode, fScore[startNode]);

            while (openSet.Count > 0)
            {
                NodeModel current = openSet.Dequeue();
                if (!visited.Add(current))
                {
                    continue;
                }

                if (current.type == targetType)
                {
                    return ReconstructPath(current, cameFrom);
                }

                foreach (EdgeModel edge in edgeMap[current])
                {
                    NodeModel neighbor = edge.GetNeighbor(current);
                    float tentativeGScore = gScore[current] + edge.Distance;

                    if (tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor);
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }

            return null;
        }
        
        private float Heuristic(NodeModel node)
        {
            return Vector3.Distance(node.transform.position, _targetPosition);
        }
    }
}