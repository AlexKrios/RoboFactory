using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using RoboFactory.General.Item.Products;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Order
{
    [RequireComponent(typeof(Image))]
    public class PartCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly ProductsManager _productsManager;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        
        [Space]
        [SerializeField] private Image _starIcon;
        [SerializeField] private TMP_Text _starLevel;

        public async void SetPartInfo(PartObject part)
        {
            var itemCount = _productsManager.GetProduct(part.Data.Key).Count;
            var sprite = await _addressableService.LoadAssetAsync<Sprite>(part.Data.IconRef);

            SetPartIcon(sprite);
            SetPartText($"{itemCount}/{part.Count}");
            SetPartStar(part.Star);
        }

        private void SetPartIcon(Sprite partIcon, float alpha = 1f)
        {
            _icon.sprite = partIcon;
            _icon.color = new Color(1, 1, 1, alpha);
        }

        private void SetPartText(string text)
        {
            _count.text = text;
        }
        
        private void SetPartStar(int star, bool isActive = true)
        {
            _starIcon.gameObject.SetActive(isActive);
            _starLevel.text = star.ToString();
        }
    }
}
