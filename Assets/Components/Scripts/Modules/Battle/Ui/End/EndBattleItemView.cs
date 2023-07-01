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

        [Inject] private readonly AssetsManager _assetsManager;

        #endregion
        
        #region Components

        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemCount;
        [SerializeField] private TMP_Text itemLevel;

        #endregion
        
        public async void SetItemData(PartObject reward)
        {
            itemIcon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(reward.data.IconRef);
            itemCount.text = reward.count.ToString();
            itemLevel.text = reward.star.ToString();
        }
    }
}