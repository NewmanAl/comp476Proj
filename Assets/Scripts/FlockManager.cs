using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject crow;
    public int numCrows = 20;
    public GameObject[] crows;
    public Vector3 flyRange = new Vector3(10, 20, 10);

    [Header("Crow Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighborDist;
    [Range(0.0f, 5.0f)]
    public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        crows = new GameObject[numCrows];
        for (int i = 0; i < numCrows; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-flyRange.x, flyRange.x),
                                                                Random.Range(-flyRange.y, flyRange.y),
                                                                Random.Range(-flyRange.z, flyRange.z));
            crows[i] = (GameObject)Instantiate(crow, position, Quaternion.identity);
            crows[i].GetComponent<Flock>().manager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
