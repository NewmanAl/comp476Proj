﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource2 : MonoBehaviour
{
    public GameObject player;



    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //resource1 = true;
            player.GetComponent<Player>().resource2 = true;
            QuestText.instance.incrementResource();
            Items_UI.instance.UIAcquireResource(1);
            //print(resource1);
            Destroy(this.gameObject);
        }
    }
}
