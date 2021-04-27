using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PlayerState
//{
//    idle,
//    running
//}

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform feetPosition;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField]  float jumpForce;
    [SerializeField] float jumpTime;

    //private PlayerState currentState = PlayerState.idle;
    private float moveInput;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);

        if (isGrounded && Input.GetButtonDown("Jump")) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetButton("Jump")) {
            if (jumpTimeCounter > 0) {
                rigidBody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetBool("isFalling", rigidBody.velocity.y < 0);
        animator.SetBool("isJumping", rigidBody.velocity.y > 0);
        animator.SetBool("isMoving", rigidBody.velocity.x != 0);

        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        if (moveInput != 0)
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        int direction = moveInput < 0 ? 180 : 0;
        transform.rotation = Quaternion.Euler(0, direction, 0);

        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
    }

}
