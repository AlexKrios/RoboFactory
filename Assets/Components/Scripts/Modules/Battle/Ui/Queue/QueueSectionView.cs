using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Battle.Ui.Queue
{
    [AddComponentMenu("Scripts/Battle/Ui/Queue Section View")]
    public class QueueSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly BattleController _battleController;
        [Inject] private readonly BattleUiFactory _battleUiFactory;

        #endregion

        #region Variables

        private RectTransform _rectTransform;
        
        private List<QueueCellView> _cells;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _battleController.Queue = this;
            
            _cells = new List<QueueCellView>();
        }

        #endregion

        public void CreateUnitsQueue()
        {
            var queue = _battleController.AllUnits;
            foreach (var unit in queue)
            {
                var cell = _battleUiFactory.CreateQueueCell(transform);
                cell.SetCellData(unit.Data);
                _cells.Add(cell);
            }

            CalculateSectionSize();
        }
        
        public void SortUnitsQueue()
        {
            var queue = _battleController.AllUnits
                .OrderByDescending(x => !x.Data.IsEnded)
                .ToList();
            for (var i = 0; i < queue.Count; i++)
            {
                if (!_cells[i].Data.IsAlive)
                {
                    _cells[i].gameObject.SetActive(false);
                    continue;
                }
                
                var cell = _cells.First(x => x.Data == queue[i].Data);
                cell.transform.SetSiblingIndex(i);
            }

            CalculateSectionSize();
        }

        private void CalculateSectionSize()
        {
            var count = _battleController.AllUnits.Count(x => x.Data.IsAlive);
            var width = 100 * count + 10 * (count + 1);
            _rectTransform.sizeDelta = new Vector2(width, 120);
        }
    }
}