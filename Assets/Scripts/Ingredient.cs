using Interfaces;
using UnityEngine;

public class Ingredient : MonoBehaviour, ICarriable
{
    public bool isCut;
    public bool canPickup { get; set; }
    public bool IsPlayerFacing { get; set; }
    public GameObject prefab;

    private void Start()
    {
        prefab.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    public void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }

    public GameObject PickUp()
    {
        return gameObject;
    }

    public void Drop()
    {
    }
}