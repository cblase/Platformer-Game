using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterDoor : MonoBehaviour
{
    Collider2D doorCollider;

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    //transports player from this door to it's partner door elsewhere
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Interactable door = collision.GetComponent<Interactable>();

        if (door && door.Interacted)
        {
            doorCollider.enabled = false;
            transform.parent.transform.position = door.PartnerDoor.transform.position;
            return;
        }
    }
}
