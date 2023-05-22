using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Scripts.Modules.General.Ui.Common.Menu
{
    [RequireComponent(typeof(Slider))]
    public abstract class VolumeSlider : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private TMP_Text volumeText;

        #endregion

        #region Components

        private const int Step = 10;
        
        protected Slider Slider;

        #endregion

        protected virtual void Awake()
        {
            Slider = GetComponent<Slider>();

            Slider.wholeNumbers = true;
            Slider.minValue = 0f;
            Slider.maxValue = 100f / Step;

            Slider.onValueChanged.AddListener(ChangeValue);
        }

        protected virtual void ChangeValue(float value)
        {
            var volumeValue = value * Step;
            volumeText.text = volumeValue.ToString(CultureInfo.CurrentCulture);
        }
    }
}