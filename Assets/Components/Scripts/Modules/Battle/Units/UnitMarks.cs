using UnityEngine;

namespace Components.Scripts.Modules.Battle.Units
{
    [AddComponentMenu("Scripts/Battle/Unit Marks", 1)]
    public class UnitMarks : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer turnMark;
        [SerializeField] private SpriteRenderer allyMark;
        [SerializeField] private SpriteRenderer enemyMark;

        public void ActivateTurnMark(bool value)
        {
            turnMark.gameObject.SetActive(value);
        }
        
        public void ActivateAllyMark(bool value)
        {
            allyMark.gameObject.SetActive(value);
        }
        
        public void ActivateEnemyMark(bool value)
        {
            enemyMark.gameObject.SetActive(value);
        }
    }
}