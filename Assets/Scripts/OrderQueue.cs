using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using UnityEngine;
using Utility;

public class OrderQueue
{
    public List<Recipe> orderQueue = new List<Recipe>();

    public void AddOrderToQueue(Recipe recipe)
    {
        orderQueue.Add(recipe);
    }

    public bool RemoveOrderFromQueue(Recipe recipe)
    {
        return orderQueue.Remove(recipe);
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