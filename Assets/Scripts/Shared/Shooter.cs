using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] Bullet bullet;
    [SerializeField] Transform hand;
    public Transform firePot;

    private WeaponReloader reloader;


    float nextFire; //开火间隔
    public bool fireEnable;

    void Awake()
    {
        firePot = transform.Find("firePot");


        reloader = GetComponent<WeaponReloader>();

        transform.SetParent(hand);

    }

    public void Reload()
    {
        if (reloader == null)
            return;

        reloader.Reload();
    }

    public virtual void Fire()
    {
        print(reloader.RoundRemainingInClip);
        fireEnable = false;
        if (Time.time < nextFire)
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundRemainingInClip == 0)
                return;

            reloader.TakeFromClip(1); //fire one bullet from clip
        }

        nextFire = Time.time + fireRate;
        //Vector3 dir =;
        //    
   //     Instantiate(bullet, firePot.position, firePot.rotation);
        print("fire");

        fireEnable = true;


    }
}
