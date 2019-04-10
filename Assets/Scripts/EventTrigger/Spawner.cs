using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    [SerializeField]int numberOfZombies = 5;
    private int spawnRadius = 10;
    private Vector3 spawnPosition;

    private SpawnEventManager eventScript;

    private void OnEnable()
    {
        Initializing();
        eventScript.theSpawnEvent += Spawn;
    }

    private void OnDisable()
    {
        eventScript.theSpawnEvent -= Spawn;
    }

    private void Spawn()
    {
        Debug.Log("It happens");
        for(int i = 0; i < numberOfZombies; i++)
        {
            spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Vector3 position = new Vector3(spawnPosition.x, 1, spawnPosition.z);
            Instantiate(zombiePrefab, position, Quaternion.identity);
        }
    }

    private void Initializing()
    {
        eventScript = GameObject.Find("SpawnEventManager").GetComponent<SpawnEventManager>();
    }
}
