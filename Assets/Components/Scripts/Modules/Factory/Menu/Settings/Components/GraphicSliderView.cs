using RoboFactory.General.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Graphic Slider View")]
    public class GraphicSliderView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly SettingsManager _settingsController;

        #endregion
        
        #region Components

        [SerializeField] private TMP_Text qualityText;

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
            _settingsController.SetGraphics(graphics);
        }
    }
}
