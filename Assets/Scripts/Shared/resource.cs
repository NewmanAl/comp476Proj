﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource : MonoBehaviour
{

    public GameObject player;
  


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //resource1 = true;
            player.GetComponent<Player>().resource1 = true;
            Items_UI.instance.UIAcquireResource(0);
            //print(resource1);
            Destroy(this.gameObject);
        }
    }


}
