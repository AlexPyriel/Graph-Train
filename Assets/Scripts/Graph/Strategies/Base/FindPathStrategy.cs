using System.Collections.Generic;
using Game.Enums;
using Graph.Interfaces;
using Graph.Models;

namespace Graph.Strategies.Base
{
    public abstract class FindPathStrategy : IFindPathStrategy
    {
        public abstract List<NodeModel> FindPath(NodeModel startNode, ENodeType targetType,
            IReadOnlyDictionary<NodeModel, List<EdgeModel>> edgeMap);
        
        protected List<NodeModel> ReconstructPath(NodeModel target, Dictionary<NodeModel, NodeModel> previous)
        {
            List<NodeModel> path = new ();

            while (target != null)
            {
                path.Add(target);
                previous.TryGetValue(target, out target);
            }

            path.Reverse();
            return path;
        }
    }
}