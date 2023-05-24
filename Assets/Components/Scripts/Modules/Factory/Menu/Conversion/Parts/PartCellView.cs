using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Part Cell View")]
    public class PartCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ManagersResolver managersResolver;

        #endregion

        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        [SerializeField] private TMP_Text level;

        #endregion

        public async void SetPartData(PartObject part)
        {
            var data = part.data;
            var store = managersResolver.GetManagerByType(data.ItemType);
            var itemCount = store.GetItem(data.Key).Count;
            var sprite = await AssetsManager.LoadAsset<Sprite>(data.IconRef);

            icon.sprite = sprite;
            count.text = $"{itemCount}/{part.count}";
            level.text = part.star.ToString();
        }
    }
}
