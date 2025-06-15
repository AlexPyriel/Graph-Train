using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Interfaces;

namespace Graph.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GraphModel : IGraphModel
    {
        private readonly Dictionary<NodeModel, List<EdgeModel>> _edgeMap = new();

        public IReadOnlyDictionary<NodeModel, List<EdgeModel>> EdgeMap => _edgeMap;

        public void Initialize(IReadOnlyList<EdgeModel> edges)
        {
            _edgeMap.Clear();

            foreach (EdgeModel edge in edges)
            {
                _edgeMap.TryAdd(edge.NodeA, new List<EdgeModel>());
                _edgeMap.TryAdd(edge.NodeB, new List<EdgeModel>());

                _edgeMap[edge.NodeA].Add(edge);
                _edgeMap[edge.NodeB].Add(edge);
            }

            if (!IsConnected())
            {
                throw new InvalidOperationException("Graph is not connected");
            }
        }

        public NodeModel GetRandomNode()
        {
            List<NodeModel> nodeList = _edgeMap.Keys.ToList();
            return nodeList[UnityEngine.Random.Range(0, nodeList.Count)];
        }

        private bool IsConnected()
        {
            if (_edgeMap.Count == 0)
            {
                return true;
            }

            HashSet<NodeModel> visited = new();
            DepthFirstSearch(_edgeMap.Keys.First(), visited);

            return visited.Count == _edgeMap.Count;
        }

        private void DepthFirstSearch(NodeModel node, HashSet<NodeModel> visited)
        {
            visited.Add(node);

            foreach (EdgeModel edge in _edgeMap[node])
            {
                NodeModel neighbor = edge.GetNeighbor(node);

                if (!visited.Contains(neighbor))
                {
                    DepthFirstSearch(neighbor, visited);
                }
            }
        }
    }
}
