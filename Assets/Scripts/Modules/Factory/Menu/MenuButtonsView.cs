using System.Collections.Generic;
using System.Linq;
using Modules.Factory.Menu.Conversion;
using Modules.Factory.Menu.Expedition;
using Modules.Factory.Menu.Order;
using Modules.Factory.Menu.Production;
using Modules.Factory.Menu.Storage;
using Modules.Factory.Menu.Units;
using Modules.General.Audio;
using Modules.General.Audio.Models;
using Modules.General.Item.Production;
using Modules.General.Location;
using Modules.General.Ui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu
{
    [AddComponentMenu("Scripts/Factory/Menu/Menu Buttons View", 100)]
    public class MenuButtonsView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductionController _productionController;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;
        [Inject] private readonly StorageMenuFactory _storageMenuFactory;
        [Inject] private readonly ConversionMenuFactory _conversionMenuFactory;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;
        [Inject] private readonly IExpeditionController _expeditionController;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        [Inject] private readonly OrderMenuFactory _orderMenuFactory;

        #endregion

        #region Variables

        private Dictionary<UiType, Button> _menuButtonsDictionary;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _menuButtonsDictionary = new Dictionary<UiType, Button>
            {
                { UiType.Production, _productionMenuFactory.CreateButton(transform) },
                { UiType.Storage, _storageMenuFactory.CreateButton(transform) },
                { UiType.Conversion, _conversionMenuFactory.CreateButton(transform) },
                { UiType.Units, _unitsMenuFactory.CreateButton(transform) },
                { UiType.Expedition, _expeditionMenuFactory.CreateButton(transform) },
                { UiType.Order, _orderMenuFactory.CreateButton(transform) }
            };

            _menuButtonsDictionary[UiType.Production].onClick.AddListener(ProductionClick);
            _menuButtonsDictionary[UiType.Storage].onClick.AddListener(StorageClick);
            _menuButtonsDictionary[UiType.Conversion].onClick.AddListener(ConversionClick);
            _menuButtonsDictionary[UiType.Units].onClick.AddListener(UnitsClick);
            _menuButtonsDictionary[UiType.Expedition].onClick.AddListener(ExpeditionClick);
            _menuButtonsDictionary[UiType.Order].onClick.AddListener(OrderClick);
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
            if (_productionController.IsHaveFreeCell())
            {
                _productionMenuFactory.CreateMenu();
            }
            else
            {
                var parent = _uiController.FindCanvas(CanvasType.Menu);
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
            if (_expeditionController.IsHaveFreeCell())
            {
                _expeditionMenuFactory.CreateMenu();
            }
            else
            {
                var parent = _uiController.FindCanvas(CanvasType.Menu);
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
