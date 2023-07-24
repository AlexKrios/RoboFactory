using UnityEngine;

namespace RoboFactory.Battle.Units
{
    [AddComponentMenu("Scripts/Battle/Unit Marks", 1)]
    public class UnitMarks : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _turnMark;
        [SerializeField] private SpriteRenderer _allyMark;
        [SerializeField] private SpriteRenderer _enemyMark;

        public void ActivateTurnMark(bool value)
        {
            _turnMark.gameObject.SetActive(value);
        }
        
        public void ActivateAllyMark(bool value)
        {
            _allyMark.gameObject.SetActive(value);
        }
        
        public void ActivateEnemyMark(bool value)
        {
            _enemyMark.gameObject.SetActive(value);
        }
    }
}