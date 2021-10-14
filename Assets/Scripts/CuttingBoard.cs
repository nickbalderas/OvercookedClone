using UnityEngine;

public class CuttingBoard: Countertop
{
    private bool CanCutIngredient { get; set; }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.C) && IsPlayerNear && IsPlayerFacing) CutIngredient();
    }

    protected override void PlaceItem()
    {
        if (CountertopItem) return;

        var item = HeldItem.GetItem();

        if (!item) return;

        Ingredient ingredient = item.GetComponent<Ingredient>();
        CanCutIngredient = ingredient is {isCut: false};

        item.GetComponent<Rigidbody>().isKinematic = false;
        var position = CountertopTransform.position;
        CountertopItem = Instantiate(item, new Vector3(position.x, 2, position.z), CountertopTransform.rotation);
        Destroy(item.gameObject);
    }

    private void CutIngredient()
    {
        if (!CanCutIngredient) return;
        
        CountertopItem.GetComponent<Ingredient>().isCut = true;
    }

    protected override void CleanCountertop()
    {
        base.CleanCountertop();
        CanCutIngredient = false;
    }
}