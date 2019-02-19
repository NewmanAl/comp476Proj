using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject crow;
    public int numCrows = 130;
    public GameObject[] crows;
    public Vector3 flyRange = new Vector3(70, 90, 30);
    public Vector3 goalPosition;

    public float minSpeed = 10f;
    public float maxSpeed = 15f;

    float flockManagerMinRange = -20f;
    float flockManagerMaxRange = 20f;

    [Range(1.0f, 30.0f)]
    public float neighborDist;

    [Range(0.0f, 1.0f)]
    public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCrows();
    }

    // Update is called once per frame
    void Update()
    {
       FlockManagerMovement();
       GoalPosition();
    }

    private void SpawnCrows()
    {
        crows = new GameObject[numCrows];
        for (int i = 0; i < numCrows; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-flyRange.x, flyRange.x), Random.Range(-flyRange.y, flyRange.y), Random.Range(-flyRange.z, flyRange.z));

            crows[i] = (GameObject)Instantiate(crow, position, Quaternion.identity);
            crows[i].GetComponent<Flock>().manager = this;
        }
    }

    private void GoalPosition()
    {
        if (Random.Range(0, 100) < 10)
        {
            goalPosition = transform.position + new Vector3(Random.Range(-flyRange.x, flyRange.x), Random.Range(-flyRange.y, flyRange.y), Random.Range(-flyRange.z, flyRange.z));
        }
    }

    private void FlockManagerMovement()
    {
        if (transform.position.x <= 90 && transform.position.x >= -90)
        {
            if (Random.Range(0, 100) < 0.01)
            {
                transform.position += new Vector3(Random.Range(flockManagerMinRange, flockManagerMaxRange), 0, 0);
            }
        }

        else
        {
            transform.position = new Vector3(0, 70, 0);
        }
    }
}
