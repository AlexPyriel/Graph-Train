using Game.Interfaces;
using MVP.Presenters;

namespace Game.Presenters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GamePresenter : Presenter<IGameView, IGameModel>
    {
        public GamePresenter(IGameView view, IGameModel model) : base(view, model)
        {
            
        }
    }
}