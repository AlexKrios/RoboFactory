using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    public class EndBattleItemView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AddressableService _addressableService;

        #endregion
        
        #region Components

        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemCount;
        [SerializeField] private TMP_Text _itemLevel;

        #endregion
        
        public async void SetItemData(PartObject reward)
        {
            _itemIcon.sprite = await _addressableService.LoadAssetAsync<Sprite>(reward.Data.IconRef);
            _itemCount.text = reward.Count.ToString();
            _itemLevel.text = reward.Star.ToString();
        }
    }
}