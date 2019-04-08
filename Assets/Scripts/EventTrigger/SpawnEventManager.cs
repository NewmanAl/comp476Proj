using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEventManager : MonoBehaviour
{
    public delegate void spawnEvent();
    public event spawnEvent theSpawnEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallEvent()
    {
        theSpawnEvent?.Invoke();
    }
}
