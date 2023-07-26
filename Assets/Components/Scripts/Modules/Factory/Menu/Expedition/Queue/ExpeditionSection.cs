using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RoboFactory.General.Expedition;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class ExpeditionSection : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly ExpeditionService expeditionService;

        [SerializeField] private ExpeditionCell _cellPrototype;

        private CanvasGroup _canvasGroup;
        private List<ExpeditionCell> _cells;

        private void Awake()
        {
            _cells = new List<ExpeditionCell>();

            expeditionService.OnExpeditionComplete += InitCells;
        }

        private void OnEnable()
        {
            InitCells();
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f);
        }
        
        private void OnDisable()
        {
            RemoveCells();
        }
        
        private void OnDestroy()
        {
            expeditionService.OnExpeditionComplete -= InitCells;
        }

        private void InitCells()
        {
            if (_cells.Count != 0)
                RemoveCells();

            var expeditions = expeditionService.GetAllExpeditions()
                .OrderBy(x => x.TimeEnd).ToList();

            for (var i = 0; i < expeditionService.CellCount; i++)
            {
                var cell = CreateCell();
                if (i < expeditions.Count)
                    cell.SetData(expeditions[i]);
            }
        }

        private ExpeditionCell CreateCell()
        {
            var cell = _container.InstantiatePrefabForComponent<ExpeditionCell>(_cellPrototype, transform);
            _cells.Add(cell);
            
            return cell;
        }

        private void RemoveCells()
        {
            _cells.ForEach(x => Destroy(x.gameObject));
            _cells.Clear();
        }

        public void IncreaseCellCount()
        {
            expeditionService.CellCount++;
        }
    }
}
