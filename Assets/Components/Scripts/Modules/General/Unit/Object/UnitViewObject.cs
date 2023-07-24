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

        [SerializeField] private Animator _bodyAnimator;
        [SerializeField] private List<ProductViewData> _parts;

        public void SetData(UnitObject unit)
        {
            for (var i = 1; i <= Constants.ProductTypeCount; i++)
            {
                SetEquipment(_productsManager.GetProduct(unit.Outfit[(ProductGroup) i]));
            }
        }

        public void SetEquipment(ProductObject product)
        {
            var partObject = _parts.First(x => x._group == product.ProductGroup);
            if (partObject._model != null)
                Destroy(partObject._model);
            
            partObject._model = Instantiate(product.Model, partObject._parent);
            _parts.ForEach(x =>
            {
                if (x._animator != null)
                    x._animator.Play("Idle", -1, 0f);
            });
            _bodyAnimator.Play("Idle", -1 , 0f);
        }
    }

    [Serializable]
    public class ProductViewData
    {
        public ProductGroup _group;
        public Transform _parent;
        public GameObject _model;
        public Animator _animator;
    }
}