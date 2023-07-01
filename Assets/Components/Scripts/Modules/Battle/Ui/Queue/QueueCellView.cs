using RoboFactory.General.Asset;
using RoboFactory.General.Unit.Battle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    [AddComponentMenu("Scripts/Battle/Ui/Queue Cell View")]
    public class QueueCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AssetsManager _assetsManager;

        #endregion
        
        #region Components

        [SerializeField] private Image icon;

        [SerializeField] private Color allyColor;
        [SerializeField] private Color enemyColor;

        #endregion

        #region Variables

        public BattleUnitObject Data { get; private set; }
        
        private Image _background;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _background = GetComponent<Image>();
        }

        #endregion

        public async void SetCellData(BattleUnitObject unit)
        {
            Data = unit;
            
            var sprite = await _assetsManager.LoadAssetAsync<Sprite>(unit.Info.IconRef);
            
            SetCellBg(unit.Team);
            SetCellIcon(sprite);
        }

        private void SetCellBg(BattleUnitTeamType team)
        {
            if (team == BattleUnitTeamType.Ally)
                _background.color = allyColor;
            
            if (team == BattleUnitTeamType.Enemy)
                _background.color = enemyColor;
        }
        private void SetCellIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}