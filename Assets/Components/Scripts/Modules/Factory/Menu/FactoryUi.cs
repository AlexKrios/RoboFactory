using DG.Tweening;
using RoboFactory.Factory.Cameras;
using RoboFactory.Factory.Menu.Production;
using RoboFactory.Factory.Menu.Settings;
using RoboFactory.General.Audio;
using RoboFactory.General.Level;
using RoboFactory.General.Money;
using RoboFactory.General.Ui.Admin;
using RoboFactory.Utils;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu
{
    [AddComponentMenu("Scripts/Factory/Factory Ui")]
    public class FactoryUi : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AudioManager _audioController;
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly SettingsMenuFactory _settingsMenuFactory;
        [Inject] private readonly FactoryCameraController _factoryCameraController;

        #endregion

        #region Components
        
        [SerializeField] private DebugMenuView debug;
        
        [Header("Money")]
        [SerializeField] private TMP_Text moneyCount;
        
        [Header("Level")]
        [SerializeField] private TMP_Text levelCount;
        [SerializeField] private Slider levelSlider;
        
        [Header("Avatar")]
        [SerializeField] private Button avatarButton;
        
        [Header("Settings")]
        [SerializeField] private Button settingsButton;

        [Header("Elevator")]
        [SerializeField] private Button floorUpButton;
        [SerializeField] private Button floorDownButton;
        
        [Header("Production")]
        [SerializeField] private ProductionSection production;
        
        [Header("Menu Buttons")]
        [SerializeField] private MenuButtonsView menuButtons;

        public ProductionSection Production => production;
        public MenuButtonsView MenuButtons => menuButtons;

        #endregion

        #region Variables

        private CompositeDisposable _disposable;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            _moneyManager.OnMoneySet += SetMoneyData;
            _levelManager.OnExperienceSet += SetExperienceData;
            _levelManager.OnLevelSet += SetLevelData;
            
            settingsButton.OnClickAsObservable().Subscribe(_ => OnSettingsClick()).AddTo(_disposable);
            floorUpButton.OnClickAsObservable().Subscribe(_ => OnFloorUpClick()).AddTo(_disposable);
            floorDownButton.OnClickAsObservable().Subscribe(_ => OnFloorDownClick()).AddTo(_disposable);
            avatarButton.OnClickAsObservable().Subscribe(_ => OnAvatarClick()).AddTo(_disposable);

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
            _moneyManager.OnMoneySet -= SetMoneyData;
            _levelManager.OnExperienceSet -= SetExperienceData;
            _levelManager.OnLevelSet -= SetLevelData;

            _disposable.Dispose();
        }

        #endregion

        private void SetMoneyData()
        {
            moneyCount.text = StringUtil.PriceFullFormat(_moneyManager.Money);
        }
        
        private void SetExperienceData()
        {
            var currentExperience = _levelManager.Experience - _levelManager.GetPreviousLevelCap();
            levelSlider.maxValue = _levelManager.GetCurrentLevelCap() - _levelManager.GetPreviousLevelCap();
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
            levelCount.text = _levelManager.Level.ToString();
        }
        
        private void OnAvatarClick()
        {
            SceneManager.LoadScene("UnitTest");
        }
        
        private void OnSettingsClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _settingsMenuFactory.CreateMenu();
        }

        private void OnFloorUpClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _factoryCameraController.Move(1);
        }

        private void OnFloorDownClick()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _factoryCameraController.Move(-1);
        }
    }
}