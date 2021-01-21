using Modules.General.Asset;
using Modules.General.Unit.Battle.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Battle.Ui.Units
{
    [AddComponentMenu("Scripts/Battle/Ui/Unit Cell View")]
    public class UnitCellView : MonoBehaviour
    {
        #region Components

        [SerializeField] private Image unitIcon;
        [SerializeField] private TextMeshProUGUI unitHealthText;
        [SerializeField] private Image unitHealth;

        #endregion

        #region Variables

        private BattleUnitObject _data;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            unitIcon.gameObject.SetActive(false);
            unitHealthText.gameObject.SetActive(false);
            unitHealth.gameObject.SetActive(false);
        }

        #endregion

        public async void SetCellData(BattleUnitObject unit)
        {
            _data = unit;
            
            var sprite = await AssetsController.LoadAsset<Sprite>(_data.Info.IconRef);

            unitIcon.gameObject.SetActive(true);
            unitHealthText.gameObject.SetActive(true);
            unitHealth.gameObject.SetActive(true);
            
            SetIcon(sprite);
            SetHealthText();
            SetHealthFill();

            _data.OnSetHealth += SetHealthText;
            _data.OnSetHealth += SetHealthFill;
        }

        private void SetIcon(Sprite sprite)
        {
            unitIcon.sprite = sprite;
        }

        private void SetHealthText()
        {
            unitHealthText.text = $"{_data.CurrentHealth}/{_data.Info.Health}";
        }
        
        private void SetHealthFill()
        {
            var difference = _data.Info.Health - _data.CurrentHealth;
            unitHealth.fillAmount = (float)(100 / _data.Info.Health * difference) / 100;
        }
    }
}