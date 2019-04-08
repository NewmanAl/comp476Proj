using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resrouce3 : MonoBehaviour
{
    public GameObject player;



    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //resource1 = true;
            player.GetComponent<Player>().resource3 = true;
            //print(resource1);
            Destroy(this.gameObject);
        }
    }
}
