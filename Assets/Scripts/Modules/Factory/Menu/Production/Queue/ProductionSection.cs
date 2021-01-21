using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Modules.Factory.Menu.Production.Queue.Cell;
using Modules.General.Item.Production;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Queue
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Production Section")]
    public class ProductionSection : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly IProductionController _productionController;

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

            _productionController.OnProductionComplete += InitCells;
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
            _productionController.OnProductionComplete -= InitCells;
        }

        #endregion

        private void InitCells()
        {
            if (_cells.Count != 0)
                RemoveCells();
            
            var production = _productionController.GetAllProduction()
                .OrderBy(x => x.TimeEnd).ToList();

            for (var i = 0; i < _productionController.CellCount; i++)
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

        public void IncreaseCellCount()
        {
            _productionController.CellCount++;
        }
    }
}
