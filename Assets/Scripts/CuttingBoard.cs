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

    protected override void PlaceItem()
    {
        if (_Item) return;

        var item = GameObject.Find("HeldItem").GetComponentInChildren<Item>();
        
        ICarriable carriable = item.GetComponent<ICarriable>();
        if (carriable == null) return;
        
        Ingredient ingredient = item.GetComponent<Ingredient>();
        CanCutIngredient = ingredient is {isCut: false};

        item.GetComponent<Rigidbody>().isKinematic = false;
        _Item = Instantiate(item, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
        Destroy(item.gameObject);
    }

    private void CutIngredient()
    {
        if (!CanCutIngredient) return;
        _Item.GetComponent<Ingredient>().isCut = true;
    }

    protected override void CleanCountertop()
    {
        base.CleanCountertop();
        CanCutIngredient = false;
    }
}