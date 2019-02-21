using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : Shooter
{
    public override void Fire()
    {
        //excute the base code first
        base.Fire();

        if (fireEnable)
        {

        }
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
