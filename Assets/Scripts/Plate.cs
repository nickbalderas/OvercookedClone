using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

public class Plate : MonoBehaviour, ICarriable
{
    public bool IsPlayerFacing { get; set; }
    public bool canPickup { get; set; }
    private List<Ingredient> _ingredientsOnPlate = new List<Ingredient>();
    
    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }
    
    public void AddIngredient(Ingredient ingredient)
    {
        _ingredientsOnPlate.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        _ingredientsOnPlate.Remove(ingredient);
    }

    public bool DoesContainIngredients(List<Ingredient> ingredients)
    {
        return _ingredientsOnPlate.SequenceEqual(ingredients);
    }

    public void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
    public GameObject PickUp()
    {
        return null;
    }

    public void Drop()
    {
    }
}