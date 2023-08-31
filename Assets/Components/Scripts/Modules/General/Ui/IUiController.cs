using UnityEngine;

namespace RoboFactory.General.Ui
{
    public interface IUiController
    {
        void AddUi<T>(T element) where T : class;
        T FindUi<T>();
        void RemoveUi<T>(T element, GameObject gameObject, float timeout = 0f);
    }
}