using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    WeaponReloader weaponLoader;
    Text text;
    int ammountLeft = 20;
    int ammo;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        weaponLoader = GameObject.Find("PlayerHandle").GetComponentInChildren<WeaponReloader>();
    }


    // Update is called once per frame
    void Update()
    {
        //ammountLeft = weaponLoader.shotFiredInClip; 
        AmmoLeft(20);
        text.text = ammo.ToString();
    }

    void AmmoLeft(int ammo)
    {
        ammo -= weaponLoader.shotFiredInClip;
        this.ammo = ammo;
    }
}
