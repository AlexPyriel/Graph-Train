using MVP.Interfaces;
using UnityEngine;

namespace MVP.Views
{
    public abstract class View : MonoBehaviour, IView
    {
        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);
    }
}