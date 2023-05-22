using System;
using System.Collections.Generic;
using Components.Scripts.Modules.Factory.Menu.Production.Queue.Cell.State;
using Components.Scripts.Modules.General.Item.Production.Object;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Queue.Cell
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Queue/Production Cell")]
    public class ProductionCell : MonoBehaviour, IPointerClickHandler
    {
        #region Zenject

        [Inject] private readonly ProductionCellEmpty.Factory _emptyFactory;
        [Inject] private readonly ProductionCellBusy.Factory _busyFactory;
        [Inject] private readonly ProductionCellFinish.Factory _finishFactory;

        #endregion
        
        #region Components

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text timer;
        [SerializeField] private RectTransform rectTransform;
        
        public RectTransform RectTransform => rectTransform;

        #endregion
        
        #region Variables

        private Dictionary<Type, IProductionCellState> _stateMap;
        private IProductionCellState _currentState;

        public int Id { get; set; }
        public ProductionObject Data { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            InitStates();
            SetStateByDefault();
        }

        #endregion

        private void InitStates()
        {
            _stateMap = new Dictionary<Type, IProductionCellState>
            {
                [typeof(ProductionCellEmpty)] = _emptyFactory.Create(this),
                [typeof(ProductionCellBusy)] = _busyFactory.Create(this),
                [typeof(ProductionCellFinish)] = _finishFactory.Create(this)
            };
        }

        private IProductionCellState GetState<T>() where T : IProductionCellState
        {
            var type = typeof(T);
            return _stateMap[type];
        }

        private void SetState(IProductionCellState newState)
        {
            _currentState?.Exit();

            _currentState = newState;
            _currentState.Enter();
        }

        private void SetStateByDefault()
        {
            SetStateEmpty();
        }

        public void SetStateEmpty()
        {
            var state = GetState<ProductionCellEmpty>();
            SetState(state);
        }

        public void SetStateBusy()
        {
            var state = GetState<ProductionCellBusy>();
            SetState(state);
        }

        public void SetStateFinish()
        {
            var state = GetState<ProductionCellFinish>();
            SetState(state);
        }

        public void SetData(ProductionObject data)
        {
            Data = data;
            SetStateBusy();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _currentState.Click();
        }

        public void ResetCell()
        {
            icon.color = new Color(1, 1, 1, 0);
            icon.sprite = null;

            timer.text = null;
        }

        public void SetCellIcon(Sprite itemIcon)
        {
            icon.color = new Color(1, 1, 1, 1);
            icon.sprite = itemIcon;
        }

        public void SetCellTimer(string itemTimer)
        {
            timer.text = itemTimer;
        }
    }
}
