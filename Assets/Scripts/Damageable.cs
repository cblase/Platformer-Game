using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    private int _health = 10;
    private bool isInvicible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if(_health <= 0)
            {
                IsDead = true;
            }
        }
    }

    private bool _isDead = false;
    public bool IsDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            _isDead = value;
            animator.SetBool("IsDead", _isDead);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //used for hits less than full health (not currently used)
    public void Update()
    {
        if(isInvicible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvicible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    //transfers damage to the player
    public void Hit(int damage)
    {
        if (!IsDead && !isInvicible)
        {
            Health -= damage;
            isInvicible = true;
        }
    }
}
