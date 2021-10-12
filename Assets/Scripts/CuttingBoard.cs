using Interfaces;
using UnityEngine;

public class CuttingBoard: Countertop
{
    private bool CanCutIngredient { get; set; }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.C) && IsPlayerNear && IsPlayerFacing)
        {
            CutIngredient();
        }
    }

    protected override void PlaceItem(GameObject item)
    {
        if (!canSetItem) return;

        ICarriable carriable = item.GetComponent<ICarriable>();
        if (carriable == null) return;
        
        Ingredient ingredient = item.GetComponent<Ingredient>();
        CanCutIngredient = ingredient is {isCut: false};
        
        canSetItem = false;
        canGetItem = true;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item.GetComponent<Rigidbody>().useGravity = true;
        placedItem = Instantiate(item, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
    }

    private void CutIngredient()
    {
        if (!CanCutIngredient) return;
        placedItem.GetComponent<Ingredient>().isCut = true;
    }

    protected override void CleanCountertop()
    {
        base.CleanCountertop();
        CanCutIngredient = false;
    }
}