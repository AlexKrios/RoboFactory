using System.Collections.Generic;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Localisation;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Storage.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly StorageMenuManager _storageMenuManager;

        #endregion

        #region Components

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image icon;

        [Space]
        [SerializeField] private List<SpecCellView> specs;

        #endregion

        #region Variables

        private ProductObject ActiveItem => _storageMenuManager.Items.ActiveCell.ItemData;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _storageMenuManager.Sidebar = this;
            
            SetData();
        }

        #endregion

        public async void SetData()
        {
            gameObject.SetActive(!_storageMenuManager.IsItemEmpty);
            if (_storageMenuManager.IsItemEmpty)
                return;

            title.text = _localisationController.GetLanguageValue(ActiveItem.Key);
            icon.sprite = await AssetsController.LoadAsset<Sprite>(ActiveItem.IconRef);

            SetSpecsData();
        }

        private void SetSpecsData()
        {
            foreach (var specData in ActiveItem.Recipe.Specs)
            {
                var spec = specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}