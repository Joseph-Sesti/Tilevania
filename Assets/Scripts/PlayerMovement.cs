using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D playerBody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    BoxCollider2D playerFeet;

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    float startingGravity;
    bool isAlive = true;
    

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        startingGravity = playerBody.gravityScale;
        //Debug.Log(myCollider.enabled);
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        playerFeet = GetComponent<BoxCollider2D>();

        if (value.isPressed && playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(playerBody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        myCollider = GetComponent<CapsuleCollider2D>();

        if(!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerBody.gravityScale = startingGravity;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2 (playerBody.velocity.x, moveInput.y * climbSpeed);
        playerBody.velocity = climbVelocity;
        playerBody.gravityScale = 0f;
        
        bool playerHasVerticalSpeed = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Die()
    {
        playerFeet = GetComponent<BoxCollider2D>();
        if (playerBody.IsTouchingLayers(LayerMask.GetMask("Enemies", "Spikes")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Death");
            playerBody.velocity += new Vector2(0f, 30f);
            playerBody.gravityScale = 0f;
            myCollider.enabled = false;
            playerFeet.enabled = false;
        }
    }
}
