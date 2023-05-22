using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.Factory.Menu.Production.Queue.Cell;
using Components.Scripts.Modules.General.Item.Production;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Queue
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Production Section")]
    public class ProductionSection : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly ProductionManager _productionManager;

        #endregion

        #region Components
        
        [SerializeField] private ProductionCell cellPrototype;

        #endregion

        #region Variables

        private List<ProductionCell> _cells;
        
        private CanvasGroup _canvasGroup;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _cells = new List<ProductionCell>();

            _productionManager.OnProductionComplete += InitCells;
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

        private void OnApplicationPause(bool isPause)
        {
            if (!isPause)
                InitCells();
        }

        private void OnDisable()
        {
            RemoveCells();
        }

        private void OnDestroy()
        {
            _productionManager.OnProductionComplete -= InitCells;
        }

        #endregion

        private void InitCells()
        {
            if (_cells.Count != 0)
                RemoveCells();
            
            var production = _productionManager.GetAllProduction()
                .OrderBy(x => x.TimeEnd).ToList();

            for (var i = 0; i < _productionManager.CellCount; i++)
            {
                var cell = CreateCell();
                if (i < production.Count)
                    cell.SetData(production[i]);
            }
        }

        private ProductionCell CreateCell()
        {
            var cell = _container.InstantiatePrefabForComponent<ProductionCell>(cellPrototype, transform);
            _cells.Add(cell);
            
            return cell;
        }

        private void RemoveCells()
        {
            _cells.ForEach(x => Destroy(x.gameObject));
            _cells.Clear();
        }
    }
}
