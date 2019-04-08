using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthbar;
    Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("PlayerHandle").GetComponentInChildren<Health>();
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = (1 - (playerHealth.damageTaken / playerHealth.hitPoints)); ;
    }
}
