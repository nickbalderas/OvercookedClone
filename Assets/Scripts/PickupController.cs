using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupController: MonoBehaviour
{
    public Item item;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, itemContainer, cam;

    public float pickupRange;
    public float dropDownwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        if (!equipped)
        {
            item.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            item.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position = transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull) Pickup();
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void Pickup()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(itemContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.zero;
        

        rb.isKinematic = true;
        coll.isTrigger = true;

        item.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;
        
        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        
        rb.AddForce(cam.forward * dropDownwardForce, ForceMode.Impulse);
        rb.AddForce(cam.forward * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        item.enabled = false;
    }
}