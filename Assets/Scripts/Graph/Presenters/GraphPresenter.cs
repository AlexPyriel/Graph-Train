using Graph.Interfaces;
using MVP.Presenters;
using Zenject;

namespace Graph.Presenters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GraphPresenter : Presenter<IGraphView, IGraphModel>, IInitializable
    {
        public GraphPresenter(IGraphView view, IGraphModel model) : base(view, model) { }

        public void Initialize()
        {
            Model.Initialize(View.Edges);
        }
    }
}