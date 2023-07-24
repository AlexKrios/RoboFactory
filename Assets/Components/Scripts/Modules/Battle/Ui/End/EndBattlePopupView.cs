using System.Collections;
using RoboFactory.General.Expedition;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    public class EndBattlePopupView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly EndBattleFactory _endBattleFactory;
        [Inject] private readonly ExpeditionManager expeditionManager;
        [Inject] private readonly BattleController _battleController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text _title;
        
        [Space]
        [SerializeField] private RectTransform _itemsParent;
        
        [Space] 
        [SerializeField] private Button _acceptButton;

        #endregion

        #region Variables

        private CompositeDisposable _disposable;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            _acceptButton.OnClickAsObservable().Subscribe(_ => AcceptButtonClick()).AddTo(_disposable);

            SetTitle(_battleController.BattleResult);
            StartCoroutine(CreateItemsCell());
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion

        private IEnumerator CreateItemsCell()
        {
            foreach (var rewardData in expeditionManager.CurrentBattleLocation.Reward)
            {
                yield return new WaitForSeconds(.2f);
                
                var item = _endBattleFactory.CreateItemCell(_itemsParent);
                item.SetItemData(rewardData);
            }

            yield return null;
        }

        private void AcceptButtonClick()
        {
            _battleController.CollectItems(expeditionManager.CurrentBattleLocation.Reward);
            SceneManager.LoadScene("Factory");
        }

        private void SetTitle(BattleResult result)
        {
            _title.text = result == BattleResult.Win ? "Win" : "Lose";
        }
    }
}