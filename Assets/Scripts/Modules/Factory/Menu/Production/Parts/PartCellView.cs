﻿using Modules.General.Asset;
using Modules.General.Item;
using Modules.General.Item.Models.Recipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Production.Parts
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Scripts/Factory/Menu/Production/Part Cell View")]
    public class PartCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ControllersResolver _controllersResolver;

        #endregion

        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI star;
        [SerializeField] private TextMeshProUGUI count;

        #endregion
        
        #region Variables
        
        private PartObject _part;
        
        #endregion

        public async void SetPartInfo(RecipeObject recipe)
        {
            var index = transform.GetSiblingIndex();
            if (index < recipe.Parts.Count)
            {
                _part = recipe.Parts[index];
                var data = _part.data;
                var store = _controllersResolver.GetStoreByType(data.ItemType);
                var itemCount = store.GetItem(data.Key).Count;
                var sprite = await AssetsController.LoadAsset<Sprite>(data.IconRef);

                gameObject.SetActive(true);
                SetPartIcon(sprite);
                SetPartText($"{itemCount}/{_part.count}");
                SetPartStar(_part.star);
            }
            else
                ResetPartInfo();
        }

        private void ResetPartInfo()
        {
            gameObject.SetActive(false);
            SetPartIcon(null, 0f);
            SetPartText(null);
            SetPartStar(0, false);
        }

        private void SetPartIcon(Sprite partIcon, float alpha = 1f)
        {
            icon.sprite = partIcon;
            icon.color = new Color(1, 1, 1, alpha);
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