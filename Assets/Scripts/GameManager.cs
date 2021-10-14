using UnityEngine;

public class  GameManager : MonoBehaviour
{
    private OrderQueue _orderQueue;
    private void Awake()
    {
        _orderQueue = new OrderQueue();
    }

    private void Start()
    {
        // TODO: Remove hard-coded string for easy mode. Enum?
        // _orderQueue.GenerateRandomOrder("EasyRecipes.json");
    }
    
    [ContextMenu("Add Order To Queue")]
    void AddOrder()
    {
        _orderQueue.GenerateRandomOrder("EasyRecipes.json");
    }

    [ContextMenu("Remove Order From Queue")]
    void RemoveOrder()
    {
        _orderQueue.RemoveOrderFromQueue(_orderQueue.orderQueue[Random.Range(0, _orderQueue.orderQueue.Count)]);
    }

    [ContextMenu("Read Orders")]
    void ReadOrders()
    {
        foreach (var order in _orderQueue.orderQueue)
        {
            Debug.Log(order.name);
        }
    }
}