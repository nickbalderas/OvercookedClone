using System;

[Serializable]
public class Ingredient : Item
{
    public string Name { get; set; }
    public bool isCut;
}