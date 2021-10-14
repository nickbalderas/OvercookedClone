using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

public class OrderQueue : MonoBehaviour
{
    public List<Recipe> orders = new List<Recipe>();

    //TODO: Remove these context menus after testing
    [ContextMenu("Read Orders")]
    public void ReadOrders()
    {
        foreach (var recipe in orders)
        {
            Debug.Log(recipe.name);
        }
    }

    [ContextMenu("Add Order To Queue")]
    public void AddOrder()
    {
        GenerateRandomOrder("EasyRecipes.json");
    }

    [ContextMenu("Remove Order From Queue")]
    public void RemoveOrder()
    {
        RemoveOrderFromQueue(orders[Random.Range(0, orders.Count)]);
    }


    public void AddOrderToQueue(Recipe recipe)
    {
        orders.Add(recipe);
    }

    
    public bool RemoveOrderFromQueue(Recipe recipe)
    {
        return orders.Remove(recipe);
    }

    
    public void GenerateRandomOrder(string difficulty)
    {
        string path = Application.dataPath + "/Data/" + difficulty;
        string contents = File.ReadAllText(path);
        List<Recipe> existingRecipes = JsonHelper.FromJson<Recipe>(contents).ToList();
        var randomRecipe = existingRecipes[Random.Range(0, existingRecipes.Count)];
        AddOrderToQueue(randomRecipe);
    }
}