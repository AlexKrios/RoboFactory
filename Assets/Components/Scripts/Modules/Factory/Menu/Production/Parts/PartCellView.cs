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
    public class PartCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly ManagersResolver _managersResolver;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _star;
        [SerializeField] private TMP_Text _count;
        
        private PartObject _part;

        private void Awake()
        {
            _icon.color = new Color(1, 1, 1, 0);
            SetPartText(string.Empty);
        }

        public void SetPartInfo(RecipeObject recipe)
        {
            var index = transform.GetSiblingIndex();
            if (index < recipe.Parts.Count)
            {
                _part = recipe.Parts[index];
                var data = _part.Data;
                var store = _managersResolver.GetManagerByType(data.ItemType);
                var itemCount = store.GetItem(data.Key).Count;
                
                gameObject.SetActive(true);
                SetPartIcon();
                SetPartText($"{itemCount}/{_part.Count}");
                SetPartStar(_part.Star);
            }
            else
                gameObject.SetActive(false);
        }

        private async void SetPartIcon()
        {
            _icon.color = new Color(1, 1, 1, 0);
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(_part.Data.IconRef);
            _icon.DORestart();
            _icon.DOFade(1f, 0.1f);
        }
        private void SetPartText(string textString)
        {
            _count.text = textString;
        }
        private void SetPartStar(int level, bool isActive = true)
        {
            _star.text = level.ToString();
            _star.gameObject.SetActive(isActive);
        }
    }
}
