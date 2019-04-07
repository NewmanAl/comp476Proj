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
        fireEnable = true;

    }

    public void Reload()
    {
        if (reloader == null)
            return;

        reloader.Reload();
    }

    public virtual bool Fire()
    {
        fireEnable = false;
        if (Time.time < nextFire)
            return false;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return false;
            if (reloader.RoundRemainingInClip == 0)
                return false;

            reloader.TakeFromClip(1); //fire one bullet from clip
        }

        Debug.Log("firing");
        nextFire = Time.time + fireRate;
        //Vector3 dir =;
        //    
   //     Instantiate(bullet, firePot.position, firePot.rotation);

        fireEnable = true;
        return true;


    }
}
