using System;
using System.Collections.Generic;
using System.Linq;
using Recipe;
using UnityEngine;

namespace UI
{
    public class OrderManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject orderTemplate;
        
        private readonly List<OrderTemplate> _orders = new ();
        private OrderManager _orderManager;
        private struct OrderTemplate
        {
            public RecipeSO Order;
            public GameObject OrderUI;
        }

        private void Start()
        {
            _orderManager = OrderManager.Instance;
            _orderManager.OrderReceived += OnOrderReceived;
            _orderManager.OrderCompleted += OnOrderCompleted;
        }

        private void OnOrderCompleted(RecipeSO recipe)
        {
            var order = _orders.FirstOrDefault(order => order.Order == recipe);
            Destroy(order.OrderUI);
            _orders.Remove(order);
        }

        private void OnOrderReceived(RecipeSO order)
        {
            var newOrderTemplate = Instantiate(orderTemplate, container);
            var newOrder = new OrderTemplate
            {
                Order = order,
                OrderUI = newOrderTemplate
            };

            _orders.Add(newOrder);
            newOrderTemplate.SetActive(true);
            newOrderTemplate.GetComponent<OrderTemplateUI>().SetOrderVisuals(order);
        }
    }
}