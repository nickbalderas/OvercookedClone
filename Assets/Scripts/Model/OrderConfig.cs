using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class OrderConfig
    {
        public float timeDuration;
        public int scoreOnComplete;
        public int penaltyOnExpiration;
        public string imageName;
        public Sprite image;

        public OrderConfig(OrderConfig orderConfig)
        {
            timeDuration = orderConfig.timeDuration;
            scoreOnComplete = orderConfig.scoreOnComplete;
            penaltyOnExpiration = orderConfig.penaltyOnExpiration;
            image = Resources.Load<Sprite>(orderConfig.imageName);
        }
    }
}