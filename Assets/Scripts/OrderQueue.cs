using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.Networking;
using Utility;
using Random = UnityEngine.Random;

public class OrderQueue : MonoBehaviour
{
    private ScoreController _scoreController;
    private List<Order> _possibleOrders = new List<Order>();
    private Transform _transform;
    
    public List<Order> orders = new List<Order>();
    public OrderUI orderUIPrefab;

    private void Awake()
    {
        _transform = transform;
        _scoreController = GameObject.Find("Game Manager").GetComponent<ScoreController>();
        InitializePossibleOrders("EasyOrders.json");
    }

    private void Start()
    {
        GenerateRandomOrder();
    }

    public void AddOrderToQueue(Order order)
    {
        orders.Add(order);
        PositionOrdersInQueue();
    }

    
    public bool RemoveOrderFromQueue(Order order)
    {
        order.DestroyOrder();
        orders.Remove(order);
        PositionOrdersInQueue();
        GenerateRandomOrder();
        return true;
    }

    private void InitializePossibleOrders(string difficulty)
    {
        string path = Application.dataPath + "/StreamingAssets/" + difficulty;
        
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine("WebGLJsonRead", path);
        }
        else
        {
            string contents = File.ReadAllText(path);
            _possibleOrders = JsonHelper.FromJson<Order>(contents).ToList();
        }
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
        order.display = Instantiate(orderUIPrefab, transform);
        order.display.order = order;
        AddOrderToQueue(order);
    }

    private void PositionOrdersInQueue()
    {
        var setOrderUIWidth = 400;
        var orderPositionXY = new Vector3(-766, 0);

        foreach (var order in orders)
        {
            order.display.transform.localPosition = orderPositionXY;
            orderPositionXY.x = orderPositionXY.x + setOrderUIWidth + 5;
        }

    }
    
    IEnumerator WebGLJsonRead(string path)
    {
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        _possibleOrders = JsonHelper.FromJson<Order>(www.downloadHandler.text).ToList();
    }
}