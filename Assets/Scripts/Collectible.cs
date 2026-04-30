using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    //play sound and add a coin to player's inventory
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory inventory = collision.GetComponent<PlayerInventory>();

        if (inventory)
        {
            if (pickupSource)
            {
                AudioSource.PlayClipAtPoint(pickupSource.clip, pickupSource.transform.position, pickupSource.volume);
            }
            inventory.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
