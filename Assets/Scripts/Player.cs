using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PlayerState
//{
//    idle,
//    running
//}

public class Player : Singleton<Player>
{
    [SerializeField] float speed;
    [SerializeField] Transform feetPosition;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField]  float jumpForce;
    [SerializeField] float jumpTime;
    [SerializeField] Transform respawnLocation;
    [SerializeField] WeaponHandler weaponHandler;

    //private PlayerState currentState = PlayerState.idle;
    private float moveInput;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;
    bool movementIsEnabled = true;
    private Collider2D currentGround;
    bool towerEntranceInRange = false;

    // Prevent non-singleton constructor use.
    protected Player() { }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // FIXME: it's not working
        transform.position = Vector3.MoveTowards(transform.position, respawnLocation.position, 0);
    }

    void Update() {
        weaponHandler.gameObject.SetActive(weaponHandler.currentWeapon != null);

        if (!movementIsEnabled) {
            return;
        }

        var newGround = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);
        isGrounded = newGround;

        if (isGrounded) {
            if (currentGround && newGround != currentGround && currentGround.CompareTag("CanGoDown")) {
                currentGround.enabled = true;
            }

            currentGround = newGround;

            if (currentGround.CompareTag("CanGoDown") && Input.GetAxisRaw("Vertical") < 0) {
                currentGround.enabled = false;
            } else if (towerEntranceInRange && Input.GetAxisRaw("Vertical") > 0) {
                NPCHandler.Instance.MovePlayerToTowerTop();
            }
        }

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

        var currentWeapon = weaponHandler.GetCurrentWeapon();
        if (currentWeapon != null) {
            if (Input.GetButtonDown("Attack")) {
                currentWeapon.GetComponent<Animator>().SetBool("isActive", true);
                currentWeapon.GetComponent<Weapon>().AttackFirstTarget();
            }
            else if (Input.GetButtonUp("Attack")) {
                currentWeapon.GetComponent<Animator>().SetBool("isActive", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (!movementIsEnabled) {
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetBool("isFalling", rigidBody.velocity.y < -0.1f);
        animator.SetBool("isJumping", rigidBody.velocity.y > 0.1f);
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

    public void EnablePlayerMovements() {
        movementIsEnabled = true;
    }

    public void DisablePlayerMovements() {
        movementIsEnabled = false;
        animator.SetBool("isFalling", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isMoving", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tower Entrance"))
        {
            towerEntranceInRange = NPCHandler.Instance.towerEntranceIsReady;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tower Entrance"))
        {
            towerEntranceInRange = false;
        }
    }

}
