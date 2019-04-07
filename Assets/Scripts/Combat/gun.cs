using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : Shooter
{
    public override bool Fire()
    {
        return base.Fire();
    }

    public void Update()
    {
        if (GameManager.Instance.InputController.Reload)
        {
            GetComponentInParent<Player>().inputEnabled = false; //lock movement when reloading

            Reload();
        }
    }


}
