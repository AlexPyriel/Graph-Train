using System.Collections.Generic;
using Graph.Models;
using MVP.Interfaces;

namespace Graph.Interfaces
{
    public interface IGraphModel : IModel
    {
        public IReadOnlyDictionary<NodeModel, List<EdgeModel>> EdgeMap { get; }

        public void Initialize(IReadOnlyList<EdgeModel> edges);
        public NodeModel GetRandomNode();
    }
}