using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Reward Cell View")]
    public class RewardCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AssetsManager _assetsManager;

        #endregion
        
        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        
        #endregion

        public async void SetData(PartObject part)
        {
            icon.gameObject.SetActive(true);
            count.gameObject.SetActive(true);
            
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(part.data.IconRef);
            count.text = part.count.ToString();
        }

        public void Reset()
        {
            icon.gameObject.SetActive(false);
            count.gameObject.SetActive(false);
        }
    }
}
