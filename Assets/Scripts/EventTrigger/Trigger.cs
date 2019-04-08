using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private SpawnEventManager eventScript;
    // Start is called before the first frame update
    void Start()
    {
        eventScript = GameObject.Find("SpawnEventManager").GetComponent<SpawnEventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            eventScript.CallEvent();
            Destroy(gameObject);
        }
    }
}
