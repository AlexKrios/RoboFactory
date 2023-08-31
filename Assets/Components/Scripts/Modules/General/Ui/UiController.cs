using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Ui
{
    [UsedImplicitly]
    public class UiController : IUiController
    {
        [Inject] private readonly DiContainer _container;
        [Inject(Id = Constants.HudParentKey)] private readonly Transform _hudParent;

        private readonly List<GameObject> _allScreens = new();

        private void SetHudActive(bool value)
        {
            switch (value)
            {
                case false when _hudParent.gameObject.activeSelf:
                    _hudParent.gameObject.SetActive(false);
                    break;
                case true when _allScreens.Count == 0:
                    _hudParent.gameObject.SetActive(true);
                    break;
            }
        }
        
        public void AddUi<T>(T element) where T : class
        {
            _allScreens.Add(element as GameObject);
            SetHudActive(false);
            
            if (_container.TryResolve<T>() != null)
                _container.Unbind<T>();
            
            _container.BindInstance(element);
        }
        
        public T FindUi<T>()
        {
            return _container.Resolve<T>();
        }

        public void RemoveUi<T>(T element, GameObject gameObject, float timeout = 0f)
        {
            _allScreens.Remove(element as GameObject);
            SetHudActive(true);
            
            _container.Unbind<T>();
            Object.Destroy(gameObject, timeout);
        }
    }
}
