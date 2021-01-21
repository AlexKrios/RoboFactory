using System.Collections;
using Modules.General.Location;
using Modules.General.Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Modules.Battle.Ui.End
{
    [AddComponentMenu("Scripts/Battle/Ui/End Battle Popup View")]
    public class EndBattlePopupView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly EndBattleFactory _endBattleFactory;
        [Inject] private readonly IExpeditionController _expeditionController;
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly BattleController _battleController;

        #endregion

        #region Components

        [SerializeField] private TextMeshProUGUI title;
        
        [Space]
        [SerializeField] private RectTransform itemsParent;
        
        [Space] 
        [SerializeField] private Button acceptButton;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            acceptButton.onClick.AddListener(AcceptButtonClick);

            SetTitle(_battleController.BattleResult);
            StartCoroutine(CreateItemsCell());
        }

        private void OnDestroy()
        {
            acceptButton.onClick.RemoveListener(AcceptButtonClick);
        }

        #endregion

        private IEnumerator CreateItemsCell()
        {
            foreach (var rewardData in _expeditionController.CurrentBattleLocation.Reward)
            {
                yield return new WaitForSeconds(.2f);
                
                var item = _endBattleFactory.CreateItemCell(itemsParent);
                item.SetItemData(rewardData);
            }

            yield return null;
        }

        private void AcceptButtonClick()
        {
            _battleController.CollectItems(_expeditionController.CurrentBattleLocation.Reward);
            _saveController.SaveStores();
            SceneManager.LoadScene("Factory");
        }

        private void SetTitle(BattleResult result)
        {
            title.text = result == BattleResult.Win ? "Win" : "Lose";
        }
    }
}