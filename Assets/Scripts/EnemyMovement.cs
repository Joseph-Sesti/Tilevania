using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D enemyBody;
    Animator myAnimator;
    BoxCollider2D wallDetection;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        Debug.Log(transform.rotation.x);
        //Debug.Log(transform.rotation.y);
        //Debug.Log(transform.rotation.z);
    }


    void Update()
    {
        enemyBody.velocity = new Vector2 (moveSpeed, 0f);
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(enemyBody.velocity.x)), 1f);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }
}
