using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager manager;
    float speed;

    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(manager.minSpeed, manager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = new Bounds(manager.transform.position, manager.flyRange * 5);

        if (!bounds.Contains(transform.position))
        {
            turning = true;
        }

        else
            turning = false;

        if (turning)
        {
            Vector3 direction = manager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotSpeed * Time.deltaTime);
        }

        else
        {
            if (Random.Range(0, 100) < 10)
            {
                speed = Random.Range(manager.minSpeed, manager.maxSpeed);
            }

            if (Random.Range(0, 100) < 20)
            {
                FlockBehaviour();
            }
        }


        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void FlockBehaviour()
    {
        GameObject[] crows;
        crows = manager.crows;

        Vector3 avgCenter = Vector3.zero;
        Vector3 avoidance = Vector3.zero;
        float avgSpeed = 0.01f;
        float neibourDistance;
        int groupSize = 0;

        foreach (GameObject corw in crows)
        {
            if (corw != this.gameObject)
            {
                neibourDistance = Vector3.Distance(corw.transform.position, transform.position);
                if (neibourDistance <= manager.neighborDist)
                {
                    avgCenter += corw.transform.position;
                    groupSize++;

                    if (neibourDistance < 3.0f)
                    {
                        avoidance += (transform.position - corw.transform.position);
                    }

                    Flock flock = corw.GetComponent<Flock>();
                    avgSpeed += flock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            avgCenter = avgCenter / groupSize + (manager.goalPosition - transform.position);
            speed = avgSpeed / groupSize;

            Vector3 direction = (avgCenter + avoidance) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotSpeed * Time.deltaTime);
            }
        }
    }
}
