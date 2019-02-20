using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Shooter gun;
    [SerializeField] 
    void Update()
    {
        if (GameManager.Instance.InputController.fire)
        {
            gun.Fire();
        }
    }
}
