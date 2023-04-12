using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Modules.General.Item.Raw;
using Modules.General.Localisation;
using Modules.General.Location;
using Modules.General.Location.Model;
using Modules.General.Save;
using Modules.General.Ui;
using Modules.General.Unit;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Result
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Result Popup View")]
    public class ResultPopupView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly IRawController _rawController;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        [Inject] private readonly IExpeditionController _expeditionController;

        #endregion

        #region Components
        
        [SerializeField] private TextMeshProUGUI title;
        
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
            
            var locationData = _expeditionController.GetLocation(_data.Key);
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

                var unit = _unitsController.GetUnit(unitData);
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

        private void AcceptButtonClick()
        {
            var locationData = _expeditionController.GetLocation(_data.Key);
            locationData.Reward.ForEach(x => _rawController.AddRaw(x));

            _expeditionController.RemoveExpedition(_data.Id);
            
            _saveController.SaveExpeditions();
            _saveController.SaveStores();

            _canvasGroup.DOFade(0, 0.25f).SetEase(Ease.OutCubic)
                .OnComplete(() => _uiController.RemoveUi(this, gameObject));
        }
    }
}