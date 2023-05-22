using Components.Scripts.Modules.General.Item.Products;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Unit;
using Components.Scripts.Modules.General.Unit.Type;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules
{
    public class BotTest : MonoBehaviour
    {
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly ProductsManager _productsManager;
        
        [SerializeField] private Renderer face;
        [SerializeField] private GameObject weaponParent;
        [SerializeField] private GameObject weaponObject;
        [SerializeField] private GameObject armorParent;
        [SerializeField] private GameObject armorObject;
        
        private void Awake()
        {
            var unit = _unitsManager.GetUnit("unit_trooper");
            var weapon = _productsManager.GetProduct(unit.Outfit[ProductGroup.Weapon]);
            var armor = _productsManager.GetProduct(unit.Outfit[ProductGroup.Armor]);
            
            if (weaponObject != null)
                Destroy(weaponObject);
            
            if (armorObject != null)
                Destroy(armorObject);
            
            weaponObject = Instantiate(weapon.Model, weaponParent.transform);
            armorObject = Instantiate(armor.Model, armorParent.transform);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                var material = _unitsManager.GetFace(FaceType.Smile);
                face.material = material;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                var material = _unitsManager.GetFace(FaceType.Sad);
                face.material = material;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                var material = _unitsManager.GetFace(FaceType.Poker);
                face.material = material;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                var material = _unitsManager.GetFace(FaceType.Angry);
                face.material = material;
            }
        }
    }
}