using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Sprite InteractedSprite;
    public GameObject PartnerDoor;
    public bool KeyRequired;
    SpriteRenderer sr;
    UIManager uIManager;
    public bool Interacted = false;
    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        uIManager = FindObjectOfType<UIManager>();
    }

    //used to determine if door needs to be unlocked
    //handles UI message accordingly
    public void Interact(GameObject player)
    {
        if (!Interacted)
        {
            if (KeyRequired)
            {
                if (player.GetComponent<PlayerInventory>().UseKey())
                {
                    uIManager.UseItem(player, "Key");
                }
                else
                {
                    uIManager.NeedItem(gameObject, "Key");
                    return;
                }
            }
            sr.sprite = InteractedSprite;
            Interacted = true;
        }
    }
}
