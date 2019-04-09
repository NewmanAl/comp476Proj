using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : Shooter
{
    private AudioSource[] audios;

    private void Awake()
    {
        base.Awake();
        audios = GetComponents<AudioSource>();
    }

    public override bool Fire()
    {

        bool didFire = base.Fire();
        if (didFire)
        {
            audios[0].Play();
            audios[1].Play();
        }
        return didFire;

        //return base.Fire();
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
