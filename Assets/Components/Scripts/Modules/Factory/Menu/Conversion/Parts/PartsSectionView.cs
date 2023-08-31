using RoboFactory.General.Item.Raw;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    public class PartsSectionView : MonoBehaviour
    {
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly RawService _rawService;

        [SerializeField] private PartCellView _part1;
        [SerializeField] private PartCellView _part2;
        [SerializeField] private PartResultCellView _result;

        private ConversionMenuView _menu;
        
        protected void Awake()
        {
            _menu = _uiController.FindUi<ConversionMenuView>();
        }

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
