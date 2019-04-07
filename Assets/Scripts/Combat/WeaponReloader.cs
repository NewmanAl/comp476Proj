﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;//弹夹大小
    Animator animator;
    public int ammo;
   
    public int shotFiredInClip;
    bool isReloading;

   

    public int RoundRemainingInClip
    {
        get
        {
            return clipSize - shotFiredInClip;
        }


    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void Reload()
    {
        if (isReloading)
            return;
        isReloading = true;
        GameManager.Instance.Timer.Add(ExecuteReload, reloadTime);
    }

    private void ExecuteReload()
    {
        isReloading = false;
        GetComponentInParent<Player>().inputEnabled = true;
        ammo -= shotFiredInClip;
        shotFiredInClip = 0;

        if (ammo < 0)
        {
            ammo = 0;
            shotFiredInClip += -ammo;
        }

    }

    public void TakeFromClip(int amount)
    {
        shotFiredInClip += amount;
    }

}
