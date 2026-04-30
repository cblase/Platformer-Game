using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
   public ContactFilter2D contactFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    CapsuleCollider2D capsuleCollider;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private bool _isGrounded = true;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    [SerializeField]
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set 
        {
            _isGrounded = value;
            animator.SetBool("IsGrounded", _isGrounded);
        }
    }

    private bool _isOnWall = true;

    [SerializeField]
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
        }
    }

    private bool _isOnCeiling = true;

    [SerializeField]
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
        }
    }

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    //checks for nearby surfaces to see if player is up against the floor, walls or ceiling
    private void FixedUpdate()
    {
        IsGrounded = capsuleCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = capsuleCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capsuleCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
