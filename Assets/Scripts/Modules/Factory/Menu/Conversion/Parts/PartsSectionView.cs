using Modules.General.Item.Raw;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Conversion.Parts
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Parts Section View")]
    public class PartsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ConversionMenuManager _conversionMenuManager;
        [Inject] private readonly IRawController _rawController;

        #endregion

        #region Components
        
        [SerializeField] private PartCellView part1;
        [SerializeField] private PartCellView part2;
        [SerializeField] private PartResultCellView result;

        #endregion
        
        #region Unity Methods

        protected void Awake()
        {
            _conversionMenuManager.Parts = this;
        }

        #endregion

        public void SetData()
        {
            var raw = _conversionMenuManager.Tabs.ActiveTab.RawData;
            var star = _conversionMenuManager.Star.ActiveStar;
            
            var recipe = _rawController.GetRaw(raw.Key).Recipe;
            part1.SetPartData(recipe.Parts[0]);
            part2.SetPartData(recipe.Parts[1]);
            result.SetResultData(raw.IconRef, star);
        }
        
        public void StartConvertAnimation()
        {
            result.StartConvertAnimation();
        }
    }
}
