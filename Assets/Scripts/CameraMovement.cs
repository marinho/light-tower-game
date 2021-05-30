using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : Singleton<CameraMovement>
{

    [SerializeField] Transform target;
    [SerializeField] float smoothing = .1f;
    [SerializeField] Vector2 maxPosition;
    [SerializeField] Vector2 minPosition;
    [SerializeField] UnityEvent onArriveDestination;
    [SerializeField] float speed;

    Vector3 destinationPosition;
    bool isMoving;
    bool isFollowingTarget = true;

    // Prevent non-singleton constructor use.
    protected CameraMovement() { }

    // [HideInInspector]

    void LateUpdate()
    {
        if (isFollowingTarget && transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(
                target.position.x,
                target.position.y,
                transform.position.z
            );
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                smoothing
            );
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destinationPosition,
                speed * Time.deltaTime
            );

            if (transform.position.x == destinationPosition.x && transform.position.y == destinationPosition.y) {
                StopMoving();
            }
        }
    }

    private void StopMoving() {
        isMoving = false;
        onArriveDestination.Invoke();
    }

    public void EnableFollowingTarget() {
        isFollowingTarget = true;
    }

    public void DisableFollowingTarget() {
        isFollowingTarget = false;
    }
    
    public void MoveTo(Vector3 destination) {
        DisableFollowingTarget();

        destinationPosition = new Vector3(
            destination.x,
            destination.y,
            transform.position.z
        );
        isMoving = true;
    }
}
