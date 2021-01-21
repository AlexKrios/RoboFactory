using Modules.General.Save;
using Modules.General.Settings;
using Modules.General.Settings.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Settings.Components
{
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Graphic Slider View")]
    public class GraphicSliderView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ISettingsController _settingsController;
        [Inject] private readonly ISaveController _saveController;

        #endregion
        
        #region Components

        [SerializeField] private TextMeshProUGUI qualityText;

        #endregion

        #region Variables

        private Slider _slider;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            
            _slider.onValueChanged.AddListener(ChangeValue);
        }
        
        private void Start()
        {
            var graphics = _settingsController.Graphics;
            _slider.value = (float) graphics;
            qualityText.text = graphics.ToString();
        }
        
        #endregion

        private void ChangeValue(float value)
        {
            var graphics = (GraphicsType) value;
            qualityText.text = graphics.ToString();
            _settingsController.Graphics = graphics;
            QualitySettings.SetQualityLevel((int) graphics);
            
            _saveController.SaveSettings();
        }
    }
}
