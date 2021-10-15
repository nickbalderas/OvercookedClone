using System.Collections;
using UnityEngine;

public class OrderFulfillment : Countertop
{
    private OrderQueue _orderQueue;
    private ScoreController _scoreController;
    private PlateSpawner _plateSpawner;
    private WaitForSeconds _plateRespawnDelay;

    protected override void Awake()
    {
        base.Awake();
        _orderQueue = GameObject.Find("OrderQueue").GetComponent<OrderQueue>();
        _scoreController = GameObject.Find("Game Manager").GetComponent<ScoreController>();
        _plateSpawner = GameObject.Find("Plate Spawner").GetComponent<PlateSpawner>();
        _plateRespawnDelay = new WaitForSeconds(5.0f);
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
            foreach (var orderIngredient in order.recipe.ingredients)
            {
                isMatch = plate.ingredients.Exists(plateIngredient =>
                    plateIngredient.ingredient.Equals(orderIngredient.ingredient) &&
                    plateIngredient.isCut.Equals(orderIngredient.isCut));
            }

            if (isMatch)
            {
                _scoreController.UpdateScore(order.config.scoreOnComplete);
                _orderQueue.RemoveOrderFromQueue(order);
                break;
            }
        }

        Destroy(CountertopItem.gameObject);
        StartCoroutine(nameof(SpawnPlate));
    }

    private IEnumerator SpawnPlate()
    {
        yield return _plateRespawnDelay;
        _plateSpawner.SpawnPlate();
    }
}