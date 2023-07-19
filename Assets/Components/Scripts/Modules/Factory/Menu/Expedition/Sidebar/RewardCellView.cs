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

        [Inject] private readonly AddressableService addressableService;

        #endregion
        
        #region Components
        
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        
        #endregion

        public async void SetData(PartObject part)
        {
            _icon.gameObject.SetActive(true);
            _count.gameObject.SetActive(true);
            
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(part.data.IconRef);
            _count.text = part.count.ToString();
        }

        public void Reset()
        {
            _icon.gameObject.SetActive(false);
            _count.gameObject.SetActive(false);
        }
    }
}
