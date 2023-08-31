using RoboFactory.General.Asset;
using RoboFactory.General.Unit.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    public class UnitCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AddressableService _addressableService;

        #endregion
        
        #region Components

        [SerializeField] private Image _unitIcon;
        [SerializeField] private TMP_Text _unitHealthText;
        [SerializeField] private Image _unitHealth;

        #endregion

        #region Variables

        private BattleUnitObject _data;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _unitIcon.gameObject.SetActive(false);
            _unitHealthText.gameObject.SetActive(false);
            _unitHealth.gameObject.SetActive(false);
        }

        #endregion

        public async void SetCellData(BattleUnitObject unit)
        {
            _data = unit;
            
            var sprite = await _addressableService.LoadAssetAsync<Sprite>(_data.Info.IconRef);

            _unitIcon.gameObject.SetActive(true);
            _unitHealthText.gameObject.SetActive(true);
            _unitHealth.gameObject.SetActive(true);
            
            SetIcon(sprite);
            SetHealthText();
            SetHealthFill();

            _data.OnSetHealth += SetHealthText;
            _data.OnSetHealth += SetHealthFill;
        }

        private void SetIcon(Sprite sprite)
        {
            _unitIcon.sprite = sprite;
        }

        private void SetHealthText()
        {
            _unitHealthText.text = $"{_data.CurrentHealth}/{_data.Info.Health}";
        }
        
        private void SetHealthFill()
        {
            var difference = _data.Info.Health - _data.CurrentHealth;
            _unitHealth.fillAmount = (float)(100 / _data.Info.Health * difference) / 100;
        }
    }
}