using System.Collections.Generic;
using System.Linq;
using RoboFactory.Factory.Menu.Conversion;
using RoboFactory.Factory.Menu.Expedition;
using RoboFactory.Factory.Menu.Order;
using RoboFactory.Factory.Menu.Production;
using RoboFactory.Factory.Menu.Storage;
using RoboFactory.General.Audio;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Ui;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu
{
    public class MenuButtonsView : MonoBehaviour
    {
        [Inject] private readonly AudioService _audioService;
        [Inject] private readonly ProductionService productionService;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;
        [Inject] private readonly StorageMenuFactory _storageMenuFactory;
        [Inject] private readonly ConversionMenuFactory _conversionMenuFactory;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;
        [Inject] private readonly ExpeditionService expeditionService;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        [Inject] private readonly OrderMenuFactory _orderMenuFactory;
        [Inject(Id = Constants.PopupsParentKey)] private readonly Transform _popupsParent;

        private Dictionary<UiType, Button> _menuButtonsDictionary;
        private readonly CompositeDisposable _disposable = new();
        
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

        public void SetMenuButtonsActive(bool value, List<UiType> types = null)
        {
            if (types != null)
                types.ForEach(x => _menuButtonsDictionary[x].gameObject.SetActive(true));
            else
                _menuButtonsDictionary.Values.ToList().ForEach(x => x.gameObject.SetActive(value));
        }
        
        private void ProductionClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            if (productionService.IsHaveFreeCell())
            {
                _productionMenuFactory.CreateMenu();
            }
            else
            {
                _productionMenuFactory.CreateUpgradeQueuePopup(_popupsParent);
            }
        }
        
        private void StorageClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            _storageMenuFactory.CreateMenu();
        }
        
        private void ConversionClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            _conversionMenuFactory.CreateMenu();
        }
        
        private void UnitsClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            _unitsMenuFactory.CreateMenu();
        }
        
        private void ExpeditionClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            if (expeditionService.IsHaveFreeCell())
            {
                _expeditionMenuFactory.CreateMenu();
            }
            else
            {
                _expeditionMenuFactory.CreateUpgradeQueuePopup(_popupsParent);
            }
        }
        
        private void OrderClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            _orderMenuFactory.CreateMenu();
        }
    }
}
