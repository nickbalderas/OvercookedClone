using UnityEngine;

public class OrderFulfillment : Countertop
{
    private OrderQueue _orderQueue;

    protected override void Awake()
    {
        base.Awake();
        _orderQueue = GameObject.Find("OrderQueue").GetComponent<OrderQueue>();
    }

    protected override void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E) || !IsPlayerNear || !IsPlayerFacing || !HeldItem.HeldPlate()) return;
        PlaceItem();
    }

    protected override void PlaceItem()
    {
        base.PlaceItem();
        Plate plate = CountertopItem.GetComponent<Plate>();

        foreach (var order in _orderQueue.orders)
        {
            // Do the ingredients for this order match the ingredients on the plate?
            // If so, remove the order. Else do nothing.
            
            //TODO: The logic works. HOWEVER... This seems a bit forced for what should simply be a built-in equality check.

            var isMatch = true;
            foreach (var orderIngredient in order.ingredients)
            {
                isMatch = plate.ingredients.Exists(plateIngredient =>
                    plateIngredient.ingredient.Equals(orderIngredient.ingredient) &&
                    plateIngredient.isCut.Equals(orderIngredient.isCut));
            }

            if (isMatch)
            {
                _orderQueue.RemoveOrderFromQueue(order);
                break;
            }
        }

        Destroy(CountertopItem.gameObject);
    }
}