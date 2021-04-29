using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject attackEffect;
    [SerializeField] Transform attackEffectOrigin;

    static string targetTag = "Light Target";

    List<GameObject> targetsInRange;
    GameObject attackedTarget;

    void Awake() {
        targetsInRange = new List<GameObject>();
    }

    public void AttackFirstTarget() {
        if (targetsInRange.Count == 0) {
            return;
        }
        attackedTarget = targetsInRange[0];

        attackEffect.transform.position = attackEffectOrigin.position;
        attackEffect.SetActive(true);
    }

    void Update() {
        if (attackedTarget != null && attackEffect != null) {
            if (attackEffect.transform.position != attackedTarget.transform.position) {
                attackEffect.transform.position = Vector3.MoveTowards(
                    attackEffect.transform.position,
                    attackedTarget.transform.position,
                    10 * Time.deltaTime
                );
            } else {
                var targetInfo = attackedTarget.GetComponent<TargetObject>();
                targetInfo.Attack();
                attackedTarget = null;
                attackEffect.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) && !targetsInRange.Contains(other.gameObject))
        {
            targetsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) && targetsInRange.Contains(other.gameObject))
        {
            targetsInRange.Remove(other.gameObject);
        }
    }

}
