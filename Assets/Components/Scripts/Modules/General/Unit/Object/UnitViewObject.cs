using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Unit
{
    public class UnitViewObject : MonoBehaviour
    {
        [Inject] private readonly ProductsManager _productsManager;

        [SerializeField] private Animator bodyAnimator;
        [SerializeField] private List<ProductViewData> parts;

        public void SetData(UnitObject unit)
        {
            for (var i = 1; i <= Constants.ProductTypeCount; i++)
            {
                SetEquipment(_productsManager.GetProduct(unit.Outfit[(ProductGroup) i]));
            }
        }

        public void SetEquipment(ProductObject product)
        {
            var partObject = parts.First(x => x.group == product.ProductGroup);
            if (partObject.model != null)
                Destroy(partObject.model);
            
            partObject.model = Instantiate(product.Model, partObject.parent);
            parts.ForEach(x =>
            {
                if (x.animator != null)
                    x.animator.Play("Idle", -1, 0f);
            });
            bodyAnimator.Play("Idle", -1 , 0f);
        }
    }

    [Serializable]
    public class ProductViewData
    {
        public ProductGroup group;
        public Transform parent;
        public GameObject model;
        public Animator animator;
    }
}