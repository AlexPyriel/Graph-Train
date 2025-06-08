using System.Collections.Generic;
using Game.Enums;
using Graph.Models;

namespace Graph.Interfaces
{
    public interface IFindPathStrategy
    {
        public List<NodeModel> FindPath(NodeModel startNode, ENodeType targetType, IReadOnlyDictionary<NodeModel, List<EdgeModel>> edgeMap);
    }
}