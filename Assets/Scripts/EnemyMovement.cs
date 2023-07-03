using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    }
}
