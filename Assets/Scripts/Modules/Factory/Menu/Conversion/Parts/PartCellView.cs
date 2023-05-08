using Modules.General.Asset;
using Modules.General.Item;
using Modules.General.Item.Models.Recipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Conversion.Parts
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Part Cell View")]
    public class PartCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ControllersResolver _controllersResolver;

        #endregion

        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        [SerializeField] private TMP_Text level;

        #endregion

        public async void SetPartData(PartObject part)
        {
            var data = part.data;
            var store = _controllersResolver.GetStoreByType(data.ItemType);
            var itemCount = store.GetItem(data.Key).Count;
            var sprite = await AssetsController.LoadAsset<Sprite>(data.IconRef);

            icon.sprite = sprite;
            count.text = $"{itemCount}/{part.count}";
            level.text = part.star.ToString();
        }
    }
}
