using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPointManager : MonoBehaviour
{
    [SerializeField]Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SeePlayer()
    {
        Vector3 distance = player.transform.position - transform.position;
        RaycastHit hit;
        bool isWall = false;

        Debug.DrawRay(transform.position, distance, Color.red);

        if (Physics.Raycast(transform.position, distance, out hit))
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                isWall = true;
            }
        }

        if (!isWall)
            return true;
        else
            return false;
    }
}
