using Modules.General.Item.Products;
using Modules.General.Unit;
using Modules.General.Unit.Type;
using UnityEngine;
using Zenject;

namespace Modules
{
    public class BotTest : MonoBehaviour
    {
        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly IProductsController _productsController;
        
        [SerializeField] private Renderer face;
        [SerializeField] private GameObject weaponParent;
        [SerializeField] private GameObject weaponObject;
        [SerializeField] private GameObject armorParent;
        [SerializeField] private GameObject armorObject;
        
        private void Awake()
        {
            var unit = _unitsController.GetUnit("unit_trooper");
            var weapon = _productsController.GetProduct(unit.Outfit[0]);
            var armor = _productsController.GetProduct(unit.Outfit[1]);
            
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
                var material = _unitsController.GetFace(FaceType.Smile);
                face.material = material;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                var material = _unitsController.GetFace(FaceType.Sad);
                face.material = material;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                var material = _unitsController.GetFace(FaceType.Poker);
                face.material = material;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                var material = _unitsController.GetFace(FaceType.Angry);
                face.material = material;
            }
        }
    }
}