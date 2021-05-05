using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingBird : MonoBehaviour
{
    [SerializeField] List<Transform> landingPlaces;
    [SerializeField] float speed;
    [SerializeField] float timeFlying;
    [SerializeField] float timeWaiting;
    [SerializeField] float timeBeforeApproaching;
    [SerializeField] Transform positionAfterCaught;
    [SerializeField] UnityEvent onCaught;

    bool isFlying = false;
    float timeCounter = 0;
    int nextLandingPlaceIndex = 0;
    bool playerInRange = false;
    float approachTimeCounter = 0;
    bool wasCaught;
    bool canFly = false;
    bool isApproaching;
    Animator animator;
    Vector3 nextPosition;

    void Awake() {
        animator = GetComponent<Animator>();
        if (landingPlaces.Count > 0) {
            transform.position = landingPlaces[0].position;
        }
    }

    void Update() {
        if (wasCaught) {
            return;
        }
        approachTimeCounter += Time.deltaTime;
        if (playerInRange) {
            approachTimeCounter = 0;
        } else if (approachTimeCounter >= timeBeforeApproaching) {
            isApproaching = true;
            FlyToPosition(positionAfterCaught.position);
        }
    }

    void FixedUpdate() {
        UpdateTimers();
        UpdateMovement();
    }

    void UpdateTimers() {
        if (!canFly) {
            return;
        }

        timeCounter += Time.deltaTime;
        if (isFlying) {
            if (timeCounter >= timeFlying || transform.position == nextPosition) {
                timeCounter = 0;
                isFlying = false;
                if (isApproaching) {
                    SetBirdAsCaught();
                }
            }
        } else if (!wasCaught && timeCounter >= timeWaiting) {
            timeCounter = 0;
            ChangeNextLandingPlace();
        }
    }

    void ChangeNextLandingPlace() {
        nextLandingPlaceIndex = (nextLandingPlaceIndex >= landingPlaces.Count - 1) ? 0 : nextLandingPlaceIndex + 1;
        FlyToPosition(landingPlaces[nextLandingPlaceIndex].position);
    }

    void FlyToPosition(Vector3 position) {
        isFlying = true;
        nextPosition = position;
    }

    void UpdateMovement() {
        if (!isFlying) {
            animator.SetBool("isFlying", false);
            animator.SetBool("isLanding", false);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            nextPosition,
            speed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(0, nextPosition.x > transform.position.x ? 180 : 0, 0);

        UpdateAnimatorParams();
    }

    void UpdateAnimatorParams() {
        animator.SetBool("isFlying", nextPosition.y > transform.position.y);
        animator.SetBool("isLanding", !animator.GetBool("isFlying"));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            approachTimeCounter = 0;
            if (isApproaching) {
                isApproaching = false;
                ChangeNextLandingPlace();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void SetBirdAsCaught() {
        isApproaching = false;
        wasCaught = true;
        canFly = false;
        onCaught.Invoke();
    }

    public void SetBirdCanFly() {
        canFly = !wasCaught && true;
    }

    public void UnsetBirdCanFly() {
        canFly = false;
    }

}
