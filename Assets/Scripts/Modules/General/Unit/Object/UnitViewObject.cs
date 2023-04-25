using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Item.Products.Models.Types;
using UnityEngine;
using Zenject;

namespace Modules.General.Unit.Object
{
    public class UnitViewObject : MonoBehaviour
    {
        [Inject] private readonly IProductsController _productsController;
        
        public List<KeyValuePair<ProductGroup, Transform>> parents;
        public List<KeyValuePair<ProductGroup, GameObject>> objects;
        public List<Animator> animators;

        private UnitObject _data;

        public void SetData(UnitObject unit)
        {
            _data = unit;

            for (var i = 0; i < Constants.ProductTypeCount; i++)
            {
                SetEquipment(_productsController.GetProduct(unit.Outfit[i]));
            }
        }

        public void SetEquipment(ProductObject product)
        {
            var equipmentObject = objects.First(x => x.Key == product.ProductGroup);
            var equipmentParent = parents.First(x => x.Key == product.ProductGroup).Value;
            if (equipmentObject.Value != null)
                Destroy(equipmentObject.Value);
            equipmentObject.Value = Instantiate(product.Model, equipmentParent);
            
            var animator = equipmentObject.Value.GetComponentInChildren<Animator>();
            if (animator != null)
                animators[(int) product.ProductGroup] = animator;

            animators.ForEach(x => x.Play("Idle", -1 , 0f));
        }
    }
}