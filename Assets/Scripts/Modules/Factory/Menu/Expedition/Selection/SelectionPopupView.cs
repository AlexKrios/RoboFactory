using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Audio;
using Modules.General.Audio.Models;
using Modules.General.Item.Products.Models.Object.Spec;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Localisation;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Selection
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Selection Popup View")]
    public class SelectionPopupView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly ExpeditionMenuManager _expeditionMenuManager;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;

        #endregion

        #region Components

        [SerializeField] private UiType type;

        [Space]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Button close;
        
        [Space]
        [SerializeField] private Transform unitParent;
        [SerializeField] private List<SelectionCellView> units;
        
        [Space]
        [UsedImplicitly, SerializeField] private List<SpecCellView> specs;
        [UsedImplicitly, SerializeField] private SelectButtonView select;

        #endregion
        
        #region Varaibles

        private SelectionCellView _activeUnit;
        public SelectionCellView ActiveUnit
        {
            get => _activeUnit;
            private set
            {
                if (_activeUnit != null)
                    _activeUnit.SetInactive();

                _activeUnit = value;
                _activeUnit.SetActive();
            }
        }

        #endregion

        #region Unity Methods
        
        private void Awake()
        {
            _expeditionMenuManager.Selection = this;
            
            _uiController.AddUi(type, gameObject);
            close.onClick.AddListener(Close);

            CreateUnits();
            SetTitleData();
            SetSpecsData();
        }

        #endregion

        private void SetTitleData()
        {
            title.text = _localisationController.GetLanguageValue(ActiveUnit.Data.Key);
        }

        public void Close()
        {
            _audioController.PlayAudio(AudioClipType.CloseClick);
            _uiController.RemoveUi(type);
        }

        private void CreateUnits()
        {
            if (units.Count != 0)
                RemoveUnits();
            
            var unitAttackType = _expeditionMenuManager.Units.ActiveUnit.AttackTypes;
            var unitsObject = _unitsController.GetUnits()
                    .Where(x => unitAttackType.Contains(x.AttackType))
                    .Where(x => _expeditionMenuManager.Units.GetUnitsWithData()
                        .All(y => x != y.Data))
                    .OrderBy(x => x.UnitType)
                    .ToList();

            foreach (var unitObject in unitsObject)
            {
                var unit = _expeditionMenuFactory.CreateSelectionUnit(unitParent);
                unit.OnEquipmentClick += OnEquipmentClick;
                unit.SetData(unitObject);
                units.Add(unit);
            }

            ActiveUnit = units.First();
        }

        private void RemoveUnits()
        {
            units.ForEach(x => Destroy(x.gameObject));
            units.Clear();
        }

        private void OnEquipmentClick(SelectionCellView cell)
        {
            if (ActiveUnit == cell)
                return;
            
            ActiveUnit = cell;
            
            SetTitleData();
            SetSpecsData();
        }
        
        private void SetSpecsData()
        {
            var specsData = _unitsController.GetUnit(ActiveUnit.Data.Key).Specs;
            for (var i = 0; i < specs.Count; i++)
            {
                var newSpec = new SpecObject((SpecType) i, specsData[(SpecType) i]);
                specs[i].SetData(newSpec);
            }
        }
    }
}