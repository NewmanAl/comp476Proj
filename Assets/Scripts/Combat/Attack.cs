using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Health target;
    public float damage;
    public float cooldownTime;

    private float nextAttackTime;

    public void NormalAttack() {
        SpecialAttack(damage);
    }

    public void SpecialAttack(float specialDamage) {
        if (target != null)
        {
            if (Time.time > nextAttackTime)
            {
                print("attack");
                nextAttackTime = Time.time + cooldownTime;
                target.TakeDamage(specialDamage); 
            }
        }
    }

    public void SetTarget(GameObject _target) {
        target = _target.GetComponent<Health>();
    }
}
