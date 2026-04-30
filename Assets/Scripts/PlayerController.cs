using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float airSpeed = 3f;
    public float jumpImpulse = 2f;
    public Collider2D doorTrigger;
    Vector2 moveInput;

    private bool _isMoving = false;
    private bool _isAttacking = false;
    private bool _isInteracting = false;
    private bool _isFacingRight = true;

    public float CurrentMoveSpeed 
    { 
        get 
        {
            if (CanMove)
            {
                if (!touchingDirections.IsGrounded) return airSpeed;
                else if (IsMoving) return walkSpeed;
                else return 0;
            }
            else
            {
                return 0;
            }
        } 
    }

    public bool CanMove 
    { 
        get
        {
            return animator.GetBool("CanMove");
        } 
    }

    public bool IsMoving { get
        {
            return _isMoving;
        } private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        } 
    }

    public bool IsAttacking
    {
        get
        {
            return _isAttacking;
        }
        private set
        {
            _isAttacking = value;
            animator.SetBool("IsAttack", value);
        }
    }

    public bool IsInteract
    {
        get
        {
            return _isInteracting;
        }
        private set
        {
            _isInteracting = value;
            animator.SetBool("IsInteract", value);
        }
    }

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value) 
            {
                //Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }   
            _isFacingRight = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return animator.GetBool("IsDead");
        }
    }


    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    PlayerInventory playerInventory;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    //updates player motion
    private void FixedUpdate()
    {
        if (touchingDirections.IsOnWall) return;
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }

    //handles player input for movement controls
    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

        if(!IsDead)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    //flips character based on movement direction
    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //Face the Right
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            //Face the Left
            IsFacingRight= false;
        }
    }

    //input for Opening doors
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("IsInteract");
        }
    }

    //input for opening chests
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("IsAttack");
        }
        
    }

    //input for jumping
    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO check if alive as well
        if(context.started && !IsDead && touchingDirections.IsGrounded && CanMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    //input for entering an opened door
    public void OnDoorEnter(InputAction.CallbackContext context)
    {
        
        if(context.started && !IsDead && touchingDirections.IsGrounded && CanMove)
        {
            doorTrigger.enabled = true;

        }
        else if(context.canceled)
        {
            doorTrigger.enabled = false;
        }
    }
}
