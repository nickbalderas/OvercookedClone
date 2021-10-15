using System;

namespace Model
{
    [Serializable]
    public class OrderConfig
    {
        public float timeDuration;
        public int scoreOnComplete;
        public int penaltyOnExpiration;

        public OrderConfig(OrderConfig orderConfig)
        {
            timeDuration = orderConfig.timeDuration;
            scoreOnComplete = orderConfig.scoreOnComplete;
            penaltyOnExpiration = orderConfig.penaltyOnExpiration;
        }
    }
}