using MVP.Interfaces;

namespace MVP.Presenters
{
    public abstract class Presenter<TView, TModel>
        where TView : IView
        where TModel : IModel
    {
        protected TView View { get; }
        protected TModel Model { get; }
        
        protected Presenter(TView view, TModel model)
        {
            View = view;
            Model = model;
        }
    }
}