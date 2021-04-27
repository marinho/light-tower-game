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

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // animator.SetFloat("moveX", 0);
        // animator.SetFloat("moveY", -1);
    }

    void Update() {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);

        if (isGrounded && Input.GetButtonDown("Jump")) {
            isJumping = true;
            animator.SetBool("jumping", true);
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping) {
            if (jumpTimeCounter > 0) {
                rigidBody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
                animator.SetBool("jumping", false);
            }
        }

        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
            animator.SetBool("jumping", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // change = Vector3.zero;
        moveInput = Input.GetAxisRaw("Horizontal");
        // change.y = Input.GetAxisRaw("Vertical");

        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        if (moveInput != 0)
        {
            MoveCharacter();
            animator.SetFloat("moveX", moveInput);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        int direction = moveInput < 0 ? 180 : 0;
        transform.rotation = Quaternion.Euler(0, direction, 0);

        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);

        // change.Normalize();

        // int direction = change.x < 0 ? 180 : 0;
        // transform.rotation = Quaternion.Euler(0, direction, 0);

        // myRigidbody.MovePosition(
        //     transform.position + change * speed * Time.deltaTime
        // );
    }
}
