using System.Collections.Generic;
using Modules.General.Asset;
using Modules.General.Localisation;
using Modules.General.Location;
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
        [Inject] private readonly ExpeditionMenuManager _expeditionMenuManager;

        #endregion

        #region Components
        
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image icon;
        [SerializeField] private List<RewardCellView> rewards;

        #endregion
        
        #region Variables

        private LocationScriptable ActiveLocationData => _expeditionMenuManager.Locations.ActiveLocation.Data;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetData();
        }

        #endregion

        public async void SetData()
        {
            title.text = _localisationController.GetLanguageValue(ActiveLocationData.Key);
            icon.sprite = await AssetsController.LoadAsset<Sprite>(ActiveLocationData.IconRef);

            rewards.ForEach(x => x.Reset());
            for (var i = 0; i < ActiveLocationData.Reward.Count; i++)
            {
                rewards[i].SetData(ActiveLocationData.Reward[i]);
            }
        }
    }
}
