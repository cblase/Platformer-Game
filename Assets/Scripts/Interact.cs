using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interact : MonoBehaviour
{
    UIManager uIManager;
    PlayerInventory inventory;
    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
        inventory = GetComponentInParent<PlayerInventory>();
    }

    //used for opening doors
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if(interactable && !interactable.Interacted)
        {           
            interactable.Interact(transform.parent.gameObject);
    
        }
    }
}
