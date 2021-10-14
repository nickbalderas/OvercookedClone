using System;
using Interfaces;

[Serializable]
public class Ingredient : Item
{
    public string ingredient;
    public bool isCut;

    public Model.Ingredient ConvertToModel()
    {
        return new Model.Ingredient(ingredient, isCut);
    }

    // public bool CanPickup { get; set; } = true;

    // protected override void Update()
    // {
    //     if (!CanPickup) return;
    //     base.Update();
    // }
    //
    // public override void PickUp()
    // {
    //     if (!CanPickup) return;
    //     base.PickUp();
    // }
    //
    // public override void Highlight(bool indicator)
    // {
    //     if (!CanPickup) return;
    //     base.Highlight(indicator);
    // }
}