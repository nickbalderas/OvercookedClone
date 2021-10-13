using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using UnityEngine;
using Utility;

public static class OrderController
{
    public static Recipe GenerateRandomRecipe()
    {
        string path = Application.dataPath + "/Data/EasyRecipes.json";
        string contents = File.ReadAllText(path);
        List<Recipe> existingRecipes = JsonHelper.FromJson<Recipe>(contents).ToList();
        return existingRecipes[Random.Range(0, existingRecipes.Count)];
    }
}