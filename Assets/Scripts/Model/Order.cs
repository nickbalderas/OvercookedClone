using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model
{
    [Serializable]
    public class Order
    {
        public Recipe recipe;
        public OrderConfig config;
        [FormerlySerializedAs("_timer")] public GameTimer timer;
        public Action HandleExpiration;

        public Order(Order order)
        {
            recipe = order.recipe;
            config = new OrderConfig(order.config);
            
            timer = new GameObject().AddComponent<GameTimer>();
            timer.InitialDuration = order.config.timeDuration;
            timer.HandleTimerExpiration = HandleExpiredOrder;
            timer.ResetTimer();
        }

        public void HandleExpiredOrder()
        {
            HandleExpiration();
        }
    }
}