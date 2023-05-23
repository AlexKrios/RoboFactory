﻿using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Item.Raw.Convert;
using Components.Scripts.Modules.General.Ui;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Conversion.Parts
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Part Result Cell View")]
    public class PartResultCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ConvertRawController _convertRawController;

        #endregion

        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text level;
        [SerializeField] private Image progress;

        #endregion

        #region Variables

        private ConversionMenuView _menu;

        #endregion
        
        #region Unity Methods

        protected void Awake()
        {
            _menu = _uiController.FindUi<ConversionMenuView>();
        }

        #endregion

        public async void SetResultData(AssetReference iconRef, int star)
        {
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(iconRef);
            level.text = star.ToString();
        }
        
        public void StartConvertAnimation()
        {
            _menu.Convert.SetInteractable(false);
            progress.DOFillAmount(1, 1).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _menu.Convert.SetState();
                progress.fillAmount = 0;

                _convertRawController.AddRaw();
            });
        }
    }
}