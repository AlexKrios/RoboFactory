using System.Collections.Generic;
using Modules.General.Asset;
using Modules.General.Localisation;
using Modules.General.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image icon;
        [SerializeField] private List<RewardCellView> rewards;

        #endregion
        
        #region Variables

        private ExpeditionMenuView _menu;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            SetData();
        }

        #endregion

        public async void SetData()
        {
            title.text = _localisationController.GetLanguageValue(_menu.ActiveLocation.Key);
            icon.sprite = await AssetsController.LoadAsset<Sprite>(_menu.ActiveLocation.IconRef);

            rewards.ForEach(x => x.Reset());
            for (var i = 0; i < _menu.ActiveLocation.Reward.Count; i++)
            {
                rewards[i].SetData(_menu.ActiveLocation.Reward[i]);
            }
        }
    }
}
