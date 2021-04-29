using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpot : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 0.2f)] float flickTime;
    [SerializeField] [Range(0.0f, 0.09f)] float addSize;

    float timer = 0f;
    bool bigger = true;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (flickTime > 0 && timer > flickTime) {
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
    }
}
