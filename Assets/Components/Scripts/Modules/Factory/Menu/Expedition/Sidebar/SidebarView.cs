using System.Collections.Generic;
using RoboFactory.General.Asset;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _timer;
        [SerializeField] private List<RewardCellView> _rewards;

        #endregion
        
        #region Variables

        private ExpeditionMenuView _menu;

        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            _title.text = localizationController.GetLanguageValue(_menu.ActiveLocation.Key);
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(_menu.ActiveLocation.IconRef);
            
            _timer.text = TimeUtil.DateCraftTimer(_menu.ActiveLocation.Time);

            _rewards.ForEach(x => x.Reset());
            for (var i = 0; i < _menu.ActiveLocation.Reward.Count; i++)
            {
                _rewards[i].SetData(_menu.ActiveLocation.Reward[i]);
            }
        }
    }
}
