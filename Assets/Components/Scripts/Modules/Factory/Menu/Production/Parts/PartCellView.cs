using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Scripts/Factory/Menu/Production/Part Cell View")]
    public class PartCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly ManagersResolver managersResolver;

        #endregion

        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text star;
        [SerializeField] private TMP_Text count;

        #endregion
        
        #region Variables
        
        private PartObject _part;
        
        #endregion

        private void Awake()
        {
            icon.color = new Color(1, 1, 1, 0);
            SetPartText(string.Empty);
        }

        public void SetPartInfo(RecipeObject recipe)
        {
            var index = transform.GetSiblingIndex();
            if (index < recipe.Parts.Count)
            {
                _part = recipe.Parts[index];
                var data = _part.data;
                var store = managersResolver.GetManagerByType(data.ItemType);
                var itemCount = store.GetItem(data.Key).Count;
                
                gameObject.SetActive(true);
                SetPartIcon();
                SetPartText($"{itemCount}/{_part.count}");
                SetPartStar(_part.star);
            }
            else
                gameObject.SetActive(false);
        }

        private async void SetPartIcon()
        {
            icon.color = new Color(1, 1, 1, 0);
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(_part.data.IconRef);
            icon.DORestart();
            icon.DOFade(1f, 0.1f);
        }
        private void SetPartText(string textString)
        {
            count.text = textString;
        }
        private void SetPartStar(int level, bool isActive = true)
        {
            star.text = level.ToString();
            star.gameObject.SetActive(isActive);
        }
    }
}
