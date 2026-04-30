using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, 0.25f);
    //}

    //Used for spike damage trigger and spinning chains
    public void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();

        if (damageable)
        {
            damageable.Hit(10);
        }
    }
}
