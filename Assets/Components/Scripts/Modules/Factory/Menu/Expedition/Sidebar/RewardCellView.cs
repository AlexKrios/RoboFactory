using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class RewardCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        public async void SetData(PartObject part)
        {
            _icon.gameObject.SetActive(true);
            _count.gameObject.SetActive(true);
            
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(part.Data.IconRef);
            _count.text = part.Count.ToString();
        }

        public void Reset()
        {
            _icon.gameObject.SetActive(false);
            _count.gameObject.SetActive(false);
        }
    }
}
