using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destructable
{
   [SerializeField] float inSeconds;
   public override void Die()
    {
        base.Die();
        print("we died");
        GameManager.Instance.Respwaner.Despwan(gameObject, inSeconds);
    }

    void OnEnable()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {
        
        base.TakeDamage(amount);
        print("Remaining: " + HitPointsRemaining);
    }
}
