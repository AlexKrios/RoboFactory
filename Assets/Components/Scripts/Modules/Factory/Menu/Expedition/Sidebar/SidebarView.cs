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
        
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text timer;
        [SerializeField] private List<RewardCellView> rewards;

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
            title.text = _localisationController.GetLanguageValue(_menu.ActiveLocation.Key);
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(_menu.ActiveLocation.IconRef);
            
            timer.text = TimeUtil.DateCraftTimer(_menu.ActiveLocation.Time);

            rewards.ForEach(x => x.Reset());
            for (var i = 0; i < _menu.ActiveLocation.Reward.Count; i++)
            {
                rewards[i].SetData(_menu.ActiveLocation.Reward[i]);
            }
        }
    }
}
