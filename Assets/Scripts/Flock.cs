using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager manager;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(manager.minSpeed, manager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        FlockRules();
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void FlockRules()
    {
        GameObject[] crowsHolder;
        crowsHolder = manager.crows;

        Vector3 avgCenter = Vector3.zero;
        Vector3 avoidance = Vector3.zero;
        float avgSpeed = 0.01f;
        float nDist;
        int groupSize = 0;

        foreach (GameObject go in crowsHolder)
        {
            if (go != this.gameObject)
            {
                nDist = Vector3.Distance(go.transform.position, transform.position);
                if (nDist <= manager.neighborDist)
                {
                    avgCenter += go.transform.position;
                    groupSize++;

                    if (nDist < 1.0f)
                    {
                        avoidance += (transform.position - go.transform.position);
                    }

                    Flock flock = go.GetComponent<Flock>();
                    avgSpeed += flock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            avgCenter /= groupSize;
            speed = avgSpeed / groupSize;

            Vector3 direction = (avgCenter + avoidance) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotSpeed * Time.deltaTime);
            }
        }
    }
}
