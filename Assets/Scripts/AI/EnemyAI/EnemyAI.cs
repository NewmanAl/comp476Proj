using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    GameObject coverPoint;
    public GameObject[] waypoints;
    public Vector3 target;
    NavMeshAgent agent;
    float ZombieWanderSpeed = 1f;
    float ZombieAttackSpeed = 3f;
    float speed;
    float health = 20f;
    float rotationSpeed = 1f;
    float accuracy = 2f;
    float visDistance = 15f;
    float visAngle = 80f;
    float attackrange = 50f;
    int currentWaypoint;
    public CoverPointManager cover;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    [Task]
    void Destination(float x, float z)
    {
        Vector3 destination = new Vector3(x, 0, z);
        agent.SetDestination(destination);
        Task.current.Succeed();
    }

    [Task]
    void RandomDestination()
    {
        Vector3 destination = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50f, 50));
        agent.SetDestination(destination);
        Task.current.Succeed();
    }

    [Task]
    void GoToDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    bool CheckAround(float angle)
    {
        Vector3 lookDirection = transform.position + Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
        target = lookDirection;
        return true;
    }

    [Task]
    void LookAndCheck()
    {
        Vector3 direction = target - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        if (Vector3.Angle(transform.forward, direction) < 5.0f)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void LookAtTarget()
    {
        Vector3 direction = target - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        Task.current.Succeed();
    }

    [Task]
    void TargetWaypoint()
    {
        target = waypoints[currentWaypoint].transform.position;
        Task.current.Succeed();
    }

    [Task]
    void TargetPlayer()
    {
        target = player.transform.position;
        Task.current.Succeed();
    }

    [Task]
    void TargetCoverPoint()
    {
        target = coverPoint.transform.position;
        Task.current.Succeed();
    }

    [Task]
    bool SeePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float visibleAngle = Vector3.Angle(direction, transform.forward);
        bool isCover = false;

        Debug.DrawRay(transform.position, direction, Color.red);

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            isCover |= hit.collider.gameObject.tag == "Cover";
        }

        if (direction.magnitude < visDistance && visibleAngle < visAngle && !isCover)
            return true;
        return false;
    }

    [Task]
    void SetTarget()
    {
        agent.SetDestination(target);
        Task.current.Succeed();
    }

    [Task]
    bool IsFiring()
    {
        if (Input.GetButtonDown("Fire1"))
            return true;
        return false;
    }

    [Task]
    void DecreaseHealth()
    {
        health--;
        Task.current.Succeed();
    }

    [Task]
    bool IsNotHealthy(float h)
    {
        return health < h;
    }

    void RecoverHealth()
    {
        if (health < 20)
            health++;
    }

    [Task]
    bool IsInDanger(float minDist)
    {
        Vector3 distance = player.transform.position - transform.position;
        return (distance.magnitude < minDist);
    }

    [Task]
    void Flee()
    {
        Vector3 awayFromPlayer = transform.position - player.transform.position;
        Vector3 destination = transform.position + awayFromPlayer * 2;
        agent.SetDestination(destination);
        Task.current.Succeed();
    }

    [Task]
    void FindTheClosesetCoverPoint()
    {
        if (waypoints.Length == 0)
            Task.current.Fail();
        for (int i = 0; i < waypoints.Length; i++)
        {
            cover = waypoints[i].GetComponent<CoverPointManager>();
            if (Vector3.Distance(transform.position, waypoints[i].transform.position) < 30f && !cover.SeePlayer())
            {
                coverPoint = waypoints[i];
                Task.current.Succeed();
            }
        }
    }

    [Task]
    void AIDead()
    {
        Destroy(gameObject);
    }
}
