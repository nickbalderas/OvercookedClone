using System;
using UnityEngine;

public class TrashCan : Countertop
{
    private AudioSource _gameAudio;
    public AudioClip trashAudioClip;

    private void Start()
    {
        _gameAudio = GameObject.Find("Game Manager").GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E) || !IsPlayerNear || !IsPlayerFacing) return;
        PlaceItem();
    }

    protected override void PlaceItem()
    {
        var heldItem = HeldItem.GetItem();
        if (!heldItem) return;
        _gameAudio.PlayOneShot(trashAudioClip);
        if (heldItem && !HeldItem.HeldPlate())Destroy(heldItem.gameObject);
        else heldItem.GetComponent<Plate>().ingredients.Clear();
    }
}