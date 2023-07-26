using RoboFactory.General.Item.Raw;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Parts Section View")]
    public class PartsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly RawService _rawService;

        #endregion

        #region Components
        
        [SerializeField] private PartCellView _part1;
        [SerializeField] private PartCellView _part2;
        [SerializeField] private PartResultCellView _result;

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
            var recipe = _rawService.GetRaw(_menu.ActiveRaw.Key).Recipe;
            _part1.SetPartData(recipe.Parts[0]);
            _part2.SetPartData(recipe.Parts[1]);
            _result.SetResultData(_menu.ActiveRaw.IconRef, _menu.ActiveStar);
        }
        
        public void StartConvertAnimation()
        {
            _result.StartConvertAnimation();
        }
    }
}
