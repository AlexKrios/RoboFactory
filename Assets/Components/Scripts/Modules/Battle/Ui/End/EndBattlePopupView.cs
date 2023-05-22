using System.Collections;
using Components.Scripts.Modules.General.Expedition;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Battle.Ui.End
{
    [AddComponentMenu("Scripts/Battle/Ui/End Battle Popup View")]
    public class EndBattlePopupView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly EndBattleFactory _endBattleFactory;
        [Inject] private readonly ExpeditionManager expeditionManager;
        [Inject] private readonly BattleController _battleController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text title;
        
        [Space]
        [SerializeField] private RectTransform itemsParent;
        
        [Space] 
        [SerializeField] private Button acceptButton;

        #endregion

        #region Variables

        private CompositeDisposable _disposable;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            acceptButton.OnClickAsObservable().Subscribe(_ => AcceptButtonClick()).AddTo(_disposable);

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
                
                var item = _endBattleFactory.CreateItemCell(itemsParent);
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
            title.text = result == BattleResult.Win ? "Win" : "Lose";
        }
    }
}