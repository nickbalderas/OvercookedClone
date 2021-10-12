using Interfaces;
using UnityEngine;

public class CuttingBoard: Countertop
{
    private bool CanCutIngredient { get; set; }

    private new void PlaceItem(GameObject item)
    {
        if (!canSetItem) return;

        ICarriable carriable = item.GetComponent<ICarriable>();
        if (carriable == null) return;
        
        Ingredient ingredient = item.GetComponent<Ingredient>();
        CanCutIngredient = ingredient is {IsCut: false};
        
        canSetItem = false;
        canGetItem = true;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item.GetComponent<Rigidbody>().useGravity = true;
        placedItem = Instantiate(item, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
    }

    private void CutIngredient()
    {
        if (!CanCutIngredient) return;
        placedItem.GetComponent<Ingredient>().IsCut = true;
    }

    private new void CleanCountertop()
    {
        if (placedItem) Destroy(placedItem);
        canSetItem = true;
        canGetItem = false;
        CanCutIngredient = false;
    }
}