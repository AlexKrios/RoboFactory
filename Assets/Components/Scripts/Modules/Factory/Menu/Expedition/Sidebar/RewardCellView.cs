using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Item.Models.Recipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Scripts.Modules.Factory.Menu.Expedition.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Reward Cell View")]
    public class RewardCellView : MonoBehaviour
    {
        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        
        #endregion

        public async void SetData(PartObject part)
        {
            icon.gameObject.SetActive(true);
            count.gameObject.SetActive(true);
            
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(part.data.IconRef);
            count.text = part.count.ToString();
        }

        public void Reset()
        {
            icon.gameObject.SetActive(false);
            count.gameObject.SetActive(false);
        }
    }
}
