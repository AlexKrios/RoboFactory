using DG.Tweening;
using Modules.Factory.Menu.Production.Queue;
using Modules.Factory.Menu.Settings;
using Modules.General.Audio;
using Modules.General.Audio.Models;
using Modules.General.Level;
using Modules.General.Money;
using Modules.General.Ui.Admin;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Modules.Factory.Menu
{
    [AddComponentMenu("Scripts/Factory/Factory Ui")]
    public class FactoryUi : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly ILevelController _levelController;
        [Inject] private readonly SettingsMenuFactory _settingsMenuFactory;

        #endregion

        #region Components

        [SerializeField] private DebugMenuView debug;
        
        [Header("Money")]
        [SerializeField] private TextMeshProUGUI moneyCount;
        
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI levelCount;
        [SerializeField] private Slider levelSlider;
        
        [Header("Settings")]
        [SerializeField] private Button settingsButton;
        
        [Header("Production")]
        [SerializeField] private ProductionSection production;
        
        public ProductionSection Production => production;

        #endregion

        #region Variables

        private CompositeDisposable _disposable;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            _moneyController.OnMoneySet += SetMoneyData;
            _levelController.OnExperienceSet += SetExperienceData;
            _levelController.OnLevelSet += SetLevelData;
            
            settingsButton.OnClickAsObservable().Subscribe(_ => SettingsClick()).AddTo(_disposable);

            debug.gameObject.SetActive(true);
            
            levelSlider.value = 0;
        }
        
        private void Start()
        {
            SetMoneyData();
            SetExperienceData();
            SetLevelData();
        }

        private void OnDestroy()
        {
            _moneyController.OnMoneySet -= SetMoneyData;
            _levelController.OnExperienceSet -= SetExperienceData;
            _levelController.OnLevelSet -= SetLevelData;

            _disposable.Dispose();
        }

        #endregion

        private void SetMoneyData()
        {
            moneyCount.text = StringUtil.PriceFullFormat(_moneyController.Money);
        }
        
        private void SetExperienceData()
        {
            var currentExperience = _levelController.Experience - _levelController.GetPreviousLevelCap();
            levelSlider.maxValue = _levelController.GetCurrentLevelCap() - _levelController.GetPreviousLevelCap();
            if (currentExperience < levelSlider.value)
            {
                var sequence = DOTween.Sequence();
                sequence
                    .Append(levelSlider.DOValue(1, 0.1f).SetEase(Ease.OutCubic))
                    .AppendInterval(0.1f)
                    .Append(levelSlider.DOValue(0, 0))
                    .Append(levelSlider.DOValue(currentExperience, 0.5f).SetEase(Ease.OutCubic));
            }
            else
            {
                levelSlider.DOValue(currentExperience, 1f).SetEase(Ease.OutCubic);
            }
        }

        private void SetLevelData()
        {
            levelCount.text = _levelController.Level.ToString();
        }
        
        private void SettingsClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _settingsMenuFactory.CreateMenu();
        }
    }
}