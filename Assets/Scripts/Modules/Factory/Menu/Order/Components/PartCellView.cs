using Modules.General.Asset;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Products;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Order.Components
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Scripts/Factory/Menu/Order/Part Cell View")]
    public class PartCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IProductsController _productsController;

        #endregion

        #region Components

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI count;
        [SerializeField] private Image starIcon;
        [SerializeField] private TextMeshProUGUI starLevel;

        #endregion

        public async void SetPartInfo(PartObject part)
        {
            var itemCount = _productsController.GetProduct(part.data.Key).Count;
            var sprite = await AssetsController.LoadAsset<Sprite>(part.data.IconRef);

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
