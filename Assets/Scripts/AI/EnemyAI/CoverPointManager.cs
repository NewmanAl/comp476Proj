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
        bool isObstacle = false;

        Debug.DrawRay(transform.position, distance, Color.red);

        if (Physics.Raycast(transform.position, distance, out RaycastHit hit))
        {
            isObstacle |= hit.collider.gameObject.tag == "Cover";
        }

        if (!isObstacle)
            return true;
        return false;
    }
}
