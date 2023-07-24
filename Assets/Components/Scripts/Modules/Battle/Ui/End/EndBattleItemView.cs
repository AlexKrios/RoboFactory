using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    [AddComponentMenu("Scripts/Battle/Ui/End Battle Item View")]
    public class EndBattleItemView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AddressableService addressableService;

        #endregion
        
        #region Components

        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemCount;
        [SerializeField] private TMP_Text _itemLevel;

        #endregion
        
        public async void SetItemData(PartObject reward)
        {
            _itemIcon.sprite = await addressableService.LoadAssetAsync<Sprite>(reward.Data.IconRef);
            _itemCount.text = reward.Count.ToString();
            _itemLevel.text = reward.Star.ToString();
        }
    }
}