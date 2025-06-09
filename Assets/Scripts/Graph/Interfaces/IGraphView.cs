using System.Collections.Generic;
using Graph.Models;
using MVP.Interfaces;

namespace Graph.Interfaces
{
    public interface IGraphView : IView
    {
        public IReadOnlyList<EdgeModel> Edges { get; }
    }
}