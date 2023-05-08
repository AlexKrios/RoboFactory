using Modules.General.Asset;
using Modules.General.Item.Models.Recipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Battle.Ui.End
{
    [AddComponentMenu("Scripts/Battle/Ui/End Battle Item View")]
    public class EndBattleItemView : MonoBehaviour
    {
        #region Components

        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemCount;
        [SerializeField] private TMP_Text itemLevel;

        #endregion
        
        public async void SetItemData(PartObject reward)
        {
            itemIcon.sprite = await AssetsController.LoadAsset<Sprite>(reward.data.IconRef);
            itemCount.text = reward.count.ToString();
            itemLevel.text = reward.star.ToString();
        }
    }
}