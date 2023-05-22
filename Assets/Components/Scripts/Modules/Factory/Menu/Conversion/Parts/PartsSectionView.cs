using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Ui;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Conversion.Parts
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Parts Section View")]
    public class PartsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly RawManager _rawManager;

        #endregion

        #region Components
        
        [SerializeField] private PartCellView part1;
        [SerializeField] private PartCellView part2;
        [SerializeField] private PartResultCellView result;

        #endregion

        #region Variables

        private ConversionMenuView _menu;

        #endregion
        
        #region Unity Methods

        protected void Awake()
        {
            _menu = _uiController.FindUi<ConversionMenuView>();
        }

        #endregion

        public void SetData()
        {
            var recipe = _rawManager.GetRaw(_menu.ActiveRaw.Key).Recipe;
            part1.SetPartData(recipe.Parts[0]);
            part2.SetPartData(recipe.Parts[1]);
            result.SetResultData(_menu.ActiveRaw.IconRef, _menu.ActiveStar);
        }
        
        public void StartConvertAnimation()
        {
            result.StartConvertAnimation();
        }
    }
}
