﻿using Components.Scripts.Modules.Factory.Menu.Conversion.Components;
using Components.Scripts.Modules.Factory.Menu.Conversion.Parts;
using Components.Scripts.Modules.Factory.Menu.Conversion.Tabs;
using Components.Scripts.Modules.General.Item.Raw.Object;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Conversion
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Conversion Menu View")]
    public class ConversionMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
        [SerializeField] private StarButtonView star;

        [Space]
        [SerializeField] private TabsSectionView tabs;
        [SerializeField] private PartsSectionView parts;
        [SerializeField] private ConvertButtonView convert;
        
        public ConvertButtonView Convert => convert;

        #endregion

        #region Variables

        public RawObject ActiveRaw { get; set; }
        public int ActiveStar { get; set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            star.OnClickEvent += OnStarClick;
            tabs.OnClickEvent += OnTabClick;
            convert.OnClickEvent += OnConvertClick;
        }

        private void Start()
        {
            SetTitle();
            tabs.SetData();
            parts.SetData();
            convert.SetState();
        }

        #endregion

        #region Click Handlers

        private void OnTabClick()
        {
            SetTitle();
            parts.SetData();
            convert.SetState();
        }

        private void OnStarClick()
        {
            parts.SetData();
            convert.SetState();
        }

        private void OnConvertClick()
        {
            parts.SetData();
            parts.StartConvertAnimation();
        }

        #endregion
        
        private void SetTitle()
        {
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.ConversionMenuTitleKey);
        }
    }
}