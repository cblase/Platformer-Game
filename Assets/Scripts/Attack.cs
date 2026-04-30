using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    //used to interact with objects like chests
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Breakable breakable = collision.GetComponent<Breakable>();

        if(breakable != null)
        {
            breakable.Hit(transform.parent.gameObject);
        }
    }
}
