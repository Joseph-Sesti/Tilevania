using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider;

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        Debug.Log(LayerMask.GetMask("Ground"));
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        myCollider = GetComponent<Collider2D>();

        if (value.isPressed && myCollider.IsTouchingLayers(64))
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHassHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHassHorizontalSpeed);
        
    }

    void FlipSprite()
    {
        bool playerHassHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHassHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
