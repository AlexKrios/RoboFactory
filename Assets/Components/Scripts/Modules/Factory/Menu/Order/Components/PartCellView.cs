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
    [AddComponentMenu("Scripts/Factory/Menu/Order/Part Cell View")]
    public class PartCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly ProductsManager _productsManager;

        #endregion

        #region Components

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        [SerializeField] private Image starIcon;
        [SerializeField] private TMP_Text starLevel;

        #endregion

        public async void SetPartInfo(PartObject part)
        {
            var itemCount = _productsManager.GetProduct(part.data.Key).Count;
            var sprite = await _assetsManager.LoadAssetAsync<Sprite>(part.data.IconRef);

            SetPartIcon(sprite);
            SetPartText($"{itemCount}/{part.count}");
            SetPartStar(part.star);
        }

        private void SetPartIcon(Sprite partIcon, float alpha = 1f)
        {
            icon.sprite = partIcon;
            icon.color = new Color(1, 1, 1, alpha);
        }

        private void SetPartText(string text)
        {
            count.text = text;
        }
        
        private void SetPartStar(int star, bool isActive = true)
        {
            starIcon.gameObject.SetActive(isActive);
            starLevel.text = star.ToString();
        }
    }
}
