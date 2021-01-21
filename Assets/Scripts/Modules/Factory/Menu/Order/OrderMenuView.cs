using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Factory.Menu.Order.Components;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Order;
using Modules.General.Save;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Order
{
    [AddComponentMenu("Scripts/Factory/Menu/Order/Order Menu View")]
    public class OrderMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IOrderController _orderController;
        [Inject] private readonly ISaveController _saveController;

        #endregion

        #region Components
        
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private List<ItemCellView> orders;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.OrderMenuTitleKey);

            SetData();
            StartCoroutine(StartRefreshTimer());
        }

        #endregion

        private IEnumerator StartRefreshTimer()
        {
            var waitTime = new WaitForSeconds(1);
            while (true)
            {
                var ticks = (DateUtil.EndOfTheDay(DateTime.Now) - DateTime.Now).Ticks;
                timer.text = new DateTime(ticks).ToString("HH:mm:ss");
                
                yield return waitTime;
                
                if (ticks <= 0)
                    SetData();
            }
            // ReSharper disable once IteratorNeverReturns
        }
        
        private void SetData()
        {
            if (_orderController.IsNeedRefreshOrders())
            {
                _orderController.RefreshOrders();
            
                foreach (var orderCell in orders)
                {
                    var order = _orderController.GetRandomOrderByGroup(orderCell.Group);
                    order.isActive = true;
                    orderCell.SetData(order);
                }
                
                _saveController.SaveOrders();
            }
            else
            {
                foreach (var orderCell in orders)
                {
                    var order = _orderController.GetActiveOrderByGroup(orderCell.Group);
                    orderCell.SetData(order);
                }
            }
        }
    }
}
