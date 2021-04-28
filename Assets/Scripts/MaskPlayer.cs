using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPlayer : MonoBehaviour
{
    [SerializeField] [Range(0.05f, 0.2f)] float flickTime;
    [SerializeField] [Range(0.02f, 0.09f)] float addSize;
    [SerializeField] Transform target;

    float timer = 0f;
    bool bigger = true;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > flickTime) {
            if (bigger)  {
                transform.localScale = new Vector3(
                    transform.localScale.x + addSize,
                    transform.localScale.y + addSize,
                    transform.localScale.z
                );
            } else {
                transform.localScale = new Vector3(
                    transform.localScale.x - addSize,
                    transform.localScale.y - addSize,
                    transform.localScale.z
                );
            }

            timer = 0f;
            bigger = !bigger;
        }

        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 20 * Time.deltaTime);
        }
    }
}
