using System.Collections.Generic;
using RoboFactory.General.Asset;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class SidebarView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _timer;
        [SerializeField] private List<RewardCellView> _rewards;
        
        private ExpeditionMenuView _menu;

        public void Initialize()
        {
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            _title.text = _localizationService.GetLanguageValue(_menu.ActiveLocation.Key);
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(_menu.ActiveLocation.IconRef);
            
            _timer.text = TimeUtil.DateCraftTimer(_menu.ActiveLocation.Time);

            _rewards.ForEach(x => x.Reset());
            for (var i = 0; i < _menu.ActiveLocation.Reward.Count; i++)
            {
                _rewards[i].SetData(_menu.ActiveLocation.Reward[i]);
            }
        }
    }
}
