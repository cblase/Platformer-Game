using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public Sprite InteractedSprite;
    SpriteRenderer sr;
    UIManager uIManager;
    private bool interacted = false;
    public bool IsTreasure = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        uIManager = FindObjectOfType<UIManager>();
    }

    //give player a key on hit
    public void Hit(GameObject player)
    {
        if (!interacted)
        {
            if (IsTreasure)
            {
                CollectTreasure();
                return;
            }
            sr.sprite = InteractedSprite;
            interacted = true;
            uIManager.CollectItem(player, "Key");
            player.GetComponent<PlayerInventory>().CollectKey(1);
        }
    }

    //used for final chest to complete the game
    public void CollectTreasure()
    {
        sr.sprite = InteractedSprite;
        interacted = true;
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        gm.GameOver();
    }
}
