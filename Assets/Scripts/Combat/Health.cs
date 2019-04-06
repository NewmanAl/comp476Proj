using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : Destructable
{
   [SerializeField] float inSeconds;
   public override void Die()
    {
        base.Die();
        print("we died");
        if(gameObject.name == "PlayerHandle")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("endScene");
        }
        //GameManager.Instance.Respwaner.Despwan(gameObject, inSeconds);
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
