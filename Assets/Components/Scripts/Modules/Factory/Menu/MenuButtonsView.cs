﻿using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.Factory.Menu.Conversion;
using Components.Scripts.Modules.Factory.Menu.Expedition;
using Components.Scripts.Modules.Factory.Menu.Order;
using Components.Scripts.Modules.Factory.Menu.Production;
using Components.Scripts.Modules.Factory.Menu.Storage;
using Components.Scripts.Modules.Factory.Menu.Units;
using Components.Scripts.Modules.General.Audio;
using Components.Scripts.Modules.General.Expedition;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Ui;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu
{
    [AddComponentMenu("Scripts/Factory/Menu/Menu Buttons View", 100)]
    public class MenuButtonsView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AudioManager _audioController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductionManager _productionManager;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;
        [Inject] private readonly StorageMenuFactory _storageMenuFactory;
        [Inject] private readonly ConversionMenuFactory _conversionMenuFactory;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;
        [Inject] private readonly ExpeditionManager expeditionManager;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        [Inject] private readonly OrderMenuFactory _orderMenuFactory;

        #endregion

        #region Variables

        private Dictionary<UiType, Button> _menuButtonsDictionary;
        private CompositeDisposable _disposable;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            _menuButtonsDictionary = new Dictionary<UiType, Button>
            {
                { UiType.Production, _productionMenuFactory.CreateButton(transform) },
                { UiType.Storage, _storageMenuFactory.CreateButton(transform) },
                { UiType.Conversion, _conversionMenuFactory.CreateButton(transform) },
                { UiType.Units, _unitsMenuFactory.CreateButton(transform) },
                { UiType.Expedition, _expeditionMenuFactory.CreateButton(transform) },
                { UiType.Order, _orderMenuFactory.CreateButton(transform) }
            };

            _menuButtonsDictionary[UiType.Production].OnClickAsObservable().Subscribe(_ => ProductionClick()).AddTo(_disposable);
            _menuButtonsDictionary[UiType.Storage].OnClickAsObservable().Subscribe(_ => StorageClick()).AddTo(_disposable);
            _menuButtonsDictionary[UiType.Conversion].OnClickAsObservable().Subscribe(_ => ConversionClick()).AddTo(_disposable);
            _menuButtonsDictionary[UiType.Units].OnClickAsObservable().Subscribe(_ => UnitsClick()).AddTo(_disposable);
            _menuButtonsDictionary[UiType.Expedition].OnClickAsObservable().Subscribe(_ => ExpeditionClick()).AddTo(_disposable);
            _menuButtonsDictionary[UiType.Order].OnClickAsObservable().Subscribe(_ => OrderClick()).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion

        public void SetMenuButtonsActive(bool value, List<UiType> types = null)
        {
            if (types != null)
                types.ForEach(x => _menuButtonsDictionary[x].gameObject.SetActive(true));
            else
                _menuButtonsDictionary.Values.ToList().ForEach(x => x.gameObject.SetActive(value));
        }
        
        private void ProductionClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            if (_productionManager.IsHaveFreeCell())
            {
                _productionMenuFactory.CreateMenu();
            }
            else
            {
                var parent = _uiController.GetCanvas(CanvasType.Ui);
                _productionMenuFactory.CreateUpgradeQueuePopup(parent.transform);
            }
        }
        
        private void StorageClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _storageMenuFactory.CreateMenu();
        }
        
        private void ConversionClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _conversionMenuFactory.CreateMenu();
        }
        
        private void UnitsClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _unitsMenuFactory.CreateMenu();
        }
        
        private void ExpeditionClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            if (expeditionManager.IsHaveFreeCell())
            {
                _expeditionMenuFactory.CreateMenu();
            }
            else
            {
                var parent = _uiController.GetCanvas(CanvasType.Ui);
                _expeditionMenuFactory.CreateUpgradeQueuePopup(parent.transform);
            }
        }
        
        private void OrderClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _orderMenuFactory.CreateMenu();
        }
    }
}