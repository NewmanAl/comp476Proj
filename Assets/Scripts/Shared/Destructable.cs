using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    [SerializeField] float hitPoints; //health

    [SerializeField]
    public event System.Action OnDeath;
    [SerializeField]
    public event System.Action OnDamageReceived;

    public float damageTaken;

    // Start is called before the first frame update
    public float HitPointsRemaining
    {
        get
        {
            return hitPoints - damageTaken;
        }
    }

    public bool IsAlive
    {
        get
        {
            return HitPointsRemaining > 0;
        }
    }

    public virtual void Die()
    {
        if (!IsAlive)
            return;
        if (OnDeath != null)
            OnDeath();
    }

    public virtual void TakeDamage(float amount)
    {
        damageTaken += amount;
        if (OnDamageReceived != null)
            OnDamageReceived();
        if (HitPointsRemaining <= 0)
        {
            Die();
        }

    }

    public void Reset()
    {
        damageTaken = 0;
    }

}
