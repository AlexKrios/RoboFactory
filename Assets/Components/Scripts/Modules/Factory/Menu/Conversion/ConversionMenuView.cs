using RoboFactory.General.Item.Raw;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Conversion Menu View")]
    public class ConversionMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly LocalizationService localizationController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private StarButtonView _star;
        
        [Space]
        [SerializeField] private TabsSectionView _tabs;
        [SerializeField] private PartsSectionView _parts;
        [SerializeField] private ConvertButtonView _convert;
        
        public ConvertButtonView Convert => _convert;

        #endregion

        #region Variables

        public RawObject ActiveRaw { get; set; }
        public int ActiveStar { get; set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _star.OnClickEvent += OnStarClick;
            _tabs.OnClickEvent += OnTabClick;
            _convert.OnClickEvent += OnConvertClick;
        }

        private void Start()
        {
            SetTitle();
            _tabs.SetData();
            _parts.SetData();
            _convert.SetState();
        }

        #endregion

        #region Click Handlers

        private void OnTabClick()
        {
            SetTitle();
            _parts.SetData();
            _convert.SetState();
        }

        private void OnStarClick()
        {
            _parts.SetData();
            _convert.SetState();
        }

        private void OnConvertClick()
        {
            _parts.SetData();
            _parts.StartConvertAnimation();
        }

        #endregion
        
        private void SetTitle()
        {
            _title.text = localizationController.GetLanguageValue(LocalizationKeys.ConversionMenuTitleKey);
        }
    }
}
