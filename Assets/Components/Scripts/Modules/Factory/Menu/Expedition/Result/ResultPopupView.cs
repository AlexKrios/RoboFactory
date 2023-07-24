using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Localization;
using RoboFactory.General.Location;
using RoboFactory.General.Ui;
using RoboFactory.General.Unit;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ResultPopupView : MonoBehaviour
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly LocationManager _locationManager;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        [Inject] private readonly ExpeditionManager _expeditionManager;

        [SerializeField] private TMP_Text _title;
        
        [Space]
        [SerializeField] private RectTransform _rewardsParent;
        [SerializeField] private List<ResultRewardCellView> _rewards;
        
        [Space]
        [SerializeField] private RectTransform _unitsParent;
        [SerializeField] private List<ResultUnitCellView> _units;
        
        [Space] 
        [SerializeField] private Button _acceptButton;

        private CanvasGroup _canvasGroup;
        private readonly CompositeDisposable _disposable = new();
        
        private ExpeditionObject _data;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _acceptButton.OnClickAsObservable().Subscribe(_ => AcceptButtonClick()).AddTo(_disposable);

            _uiController.AddUi(this);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        public void SetData(ExpeditionObject data)
        {
            _data = data;
            
            _title.text = _localizationService.GetLanguageValue("expedition_complete");
            StartCoroutine(CreateRewardsCell());
            StartCoroutine(CreateUnitsCell());
        }

        private IEnumerator CreateRewardsCell()
        {
            if (_rewards.Count != 0)
                RemoveResultCells();
            
            var locationData = _locationManager.GetLocation(_data.Key);
            foreach (var rewardData in locationData.Reward)
            {
                yield return new WaitForSeconds(.2f);
                
                var rewardCell = _expeditionMenuFactory.CreateResultRewardCell(_rewardsParent);
                rewardCell.SetData(rewardData);
            }

            yield return null;
        }
        
        private void RemoveResultCells()
        {
            _rewards.ForEach(x => Destroy(x.gameObject));
            _rewards.Clear();
        }
        
        private IEnumerator CreateUnitsCell()
        {
            if (_units.Count != 0)
                RemoveUnitCells();
            
            foreach (var unitData in _data.Units)
            {
                yield return new WaitForSeconds(.2f);

                var unit = _unitsManager.GetUnit(unitData);
                var unitCell = _expeditionMenuFactory.CreateResultUnitCell(_unitsParent);
                unitCell.SetData(unit);
            }

            yield return null;
        }
        
        private void RemoveUnitCells()
        {
            _units.ForEach(x => Destroy(x.gameObject));
            _units.Clear();
        }

        private async void AcceptButtonClick()
        {
            var locationData = _locationManager.GetLocation(_data.Key);
            await _rawManager.AddItemsThenSend(locationData.Reward);
            await _expeditionManager.RemoveExpedition(_data.Id);

            _canvasGroup.DOFade(0, 0.25f).SetEase(Ease.OutCubic)
                .OnComplete(() => _uiController.RemoveUi(this, gameObject));
        }
    }
}