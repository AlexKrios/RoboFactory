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
        
        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly ManagersResolver managersResolver;

        #endregion

        #region Components
        
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _level;

        #endregion

        public async void SetPartData(PartObject part)
        {
            var data = part.data;
            var store = managersResolver.GetManagerByType(data.ItemType);
            var itemCount = store.GetItem(data.Key).Count;
            var sprite = await addressableService.LoadAssetAsync<Sprite>(data.IconRef);

            _icon.sprite = sprite;
            _count.text = $"{itemCount}/{part.count}";
            _level.text = part.star.ToString();
        }
    }
}
