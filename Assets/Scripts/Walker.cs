using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Walker : MonoBehaviour
{
    [SerializeField] UnityEvent onArriveDestination;
    [SerializeField] float speed;

    private Vector3 destinationPosition;
    private bool isWalking;
    private bool isPaused;
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking && !isPaused) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destinationPosition,
                speed * Time.deltaTime
            );

            if (transform.position.x == destinationPosition.x) {
                StopWalking();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPaused = false;
            UpdateAnimatorState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPaused = true;
            UpdateAnimatorState();
        }
    }

    private void UpdateAnimatorState() {
        animator.SetBool("isMoving", isWalking && !isPaused);
    }

    private void StopWalking() {
        isWalking = false;
        onArriveDestination.Invoke();
        UpdateAnimatorState();
    }

    public void WalkTo(Vector3 destination) {
        transform.rotation = Quaternion.Euler(0, destination.x >= transform.position.x ? 180 : 0, 0);
        destinationPosition = destination;
        isWalking = true;
        UpdateAnimatorState();
    }
}
