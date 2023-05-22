using System.Collections;
using System.Collections.Generic;
using Components.Scripts.Modules.General.Expedition;
using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Location;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Unit;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Expedition.Result
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Result Popup View")]
    public class ResultPopupView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly LocationManager _locationManager;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        [Inject] private readonly ExpeditionManager _expeditionManager;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
        
        [Space]
        [SerializeField] private RectTransform rewardsParent;
        [SerializeField] private List<ResultRewardCellView> rewards;
        
        [Space]
        [SerializeField] private RectTransform unitsParent;
        [SerializeField] private List<ResultUnitCellView> units;
        
        [Space] 
        [SerializeField] private Button acceptButton;

        #endregion

        #region Variables

        private CanvasGroup _canvasGroup;
        
        private ExpeditionObject _data;
        
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            _canvasGroup = GetComponent<CanvasGroup>();
            
            acceptButton.OnClickAsObservable().Subscribe(_ => AcceptButtonClick()).AddTo(_disposable);

            _uiController.AddUi(this);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion

        public void SetData(ExpeditionObject data)
        {
            _data = data;
            
            title.text = _localisationController.GetLanguageValue("expedition_complete");
            StartCoroutine(CreateRewardsCell());
            StartCoroutine(CreateUnitsCell());
        }

        private IEnumerator CreateRewardsCell()
        {
            if (rewards.Count != 0)
                RemoveResultCells();
            
            var locationData = _locationManager.GetLocation(_data.Key);
            foreach (var rewardData in locationData.Reward)
            {
                yield return new WaitForSeconds(.2f);
                
                var rewardCell = _expeditionMenuFactory.CreateResultRewardCell(rewardsParent);
                rewardCell.SetData(rewardData);
            }

            yield return null;
        }
        
        private void RemoveResultCells()
        {
            rewards.ForEach(x => Destroy(x.gameObject));
            rewards.Clear();
        }
        
        private IEnumerator CreateUnitsCell()
        {
            if (units.Count != 0)
                RemoveUnitCells();
            
            foreach (var unitData in _data.Units)
            {
                yield return new WaitForSeconds(.2f);

                var unit = _unitsManager.GetUnit(unitData);
                var unitCell = _expeditionMenuFactory.CreateResultUnitCell(unitsParent);
                unitCell.SetData(unit);
            }

            yield return null;
        }
        
        private void RemoveUnitCells()
        {
            units.ForEach(x => Destroy(x.gameObject));
            units.Clear();
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