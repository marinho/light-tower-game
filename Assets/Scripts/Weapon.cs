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
    GameObject attackEffectClone;

    void Awake() {
        targetsInRange = new List<GameObject>();
    }

    public void AttackFirstTarget() {
        if (targetsInRange.Count == 0) {
            return;
        }
        attackedTarget = targetsInRange[0];

        if (attackedTarget.GetComponent<TargetObject>().CanBeAttacked()) {
            attackEffectClone = Instantiate(attackEffect, attackEffectOrigin.position, Quaternion.identity);
            attackEffectClone.SetActive(true);
        }
    }

    void Update() {
        if (attackedTarget != null && attackEffect != null) {
            if (attackEffectClone.transform.position != attackedTarget.transform.position) {
                attackEffectClone.transform.position = Vector3.MoveTowards(
                    attackEffectClone.transform.position,
                    attackedTarget.transform.position,
                    10 * Time.deltaTime
                );
            } else {
                var targetInfo = attackedTarget.GetComponent<TargetObject>();
                targetInfo.Attack();
                attackedTarget = null;
                attackEffectClone.SetActive(false);
                Destroy(attackEffectClone);
                attackEffectClone = null;
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
