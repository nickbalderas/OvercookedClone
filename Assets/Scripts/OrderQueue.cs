using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

public class OrderQueue : MonoBehaviour
{
    private ScoreController _scoreController;
    private List<Order> _possibleOrders = new List<Order>();
    public List<Order> orders = new List<Order>();

    private void Awake()
    {
        _scoreController = GameObject.Find("Game Manager").GetComponent<ScoreController>();
        InitializePossibleOrders("EasyOrders.json");
    }

    //TODO: Remove these context menus after testing
    [ContextMenu("Read Orders")]
    public void ReadOrders()
    {
        foreach (var order in orders)
        {
            Debug.Log(order.recipe.name);
        }
    }

    [ContextMenu("Add Order To Queue")]
    public void AddOrder()
    {
        GenerateRandomOrder();
    }

    [ContextMenu("Remove Order From Queue")]
    public void RemoveOrder()
    {
        RemoveOrderFromQueue(orders[Random.Range(0, orders.Count)]);
    }


    public void AddOrderToQueue(Order order)
    {
        orders.Add(order);
    }

    
    public bool RemoveOrderFromQueue(Order order)
    {
        return orders.Remove(order);
    }

    private void InitializePossibleOrders(string difficulty)
    {
        string path = Application.dataPath + "/Data/" + difficulty;
        string contents = File.ReadAllText(path);
        _possibleOrders = JsonHelper.FromJson<Order>(contents).ToList();
    }

    private void HandleOrderExpiration(Order order)
    {
        _scoreController.UpdateScore(order.config.penaltyOnExpiration);
        RemoveOrderFromQueue(order);
    }

    private void GenerateRandomOrder()
    {
        var randomOrder = _possibleOrders[Random.Range(0, _possibleOrders.Count)];
        var order = new Order(randomOrder);
        order.HandleExpiration = () => HandleOrderExpiration(order);
        AddOrderToQueue(order);
    }
}