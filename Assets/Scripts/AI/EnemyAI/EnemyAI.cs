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
    float rotationSpeed = 1f;
    float accuracy = 2f;

    float visDistance = 80f;
    float attackrange = 50f;

    int currentWaypoint = 0;
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
    public void RandomDestinationToWander()
    {
        Vector3 destination = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        agent.SetDestination(destination);
        Task.current.Succeed();
    }

    [Task]
    public void WanderingDestination()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
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
    public void LookAndCheck()
    {
        Vector3 direction = target - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        if (Vector3.Angle(transform.forward, direction) < 5.0f)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void LookAtTarget()
    {
        Vector3 direction = target - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        Task.current.Succeed();
    }

    [Task]
    public void TargetWaypoint()
    {
        target = waypoints[currentWaypoint].transform.position;
        Task.current.Succeed();
    }

    [Task]
    public void TargetPlayer()
    {
        target = player.transform.position;
        Task.current.Succeed();
    }

    [Task]
    public void TargetCoverPoint()
    {
        target = coverPoint.transform.position;
        Task.current.Succeed();
    }

    [Task]
    bool SeePlayer()
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

        if (distance.magnitude < visDistance && !isWall)
            return true;
        else
            return false;
    }

    [Task]
    public void MoveToTarget()
    {
        if (target == player.transform.position)
            speed = ZombieAttackSpeed;
        else
            speed = ZombieWanderSpeed;

        transform.Translate(0, 0, speed * Time.deltaTime);
        Task.current.Succeed();
    }

    [Task]
    public void SetTarget()
    {
        agent.SetDestination(target);
        Task.current.Succeed();
    }

    [Task]
    public bool IsFiring()
    {
        if (Input.GetButtonDown("Fire1"))
            return true;
        return false;
    }

    [Task]
    public void FindTheClosesetCoverPoint()
    {
        if (waypoints.Length == 0)
            Task.current.Fail();
        foreach(var coverPoint in waypoints)
        {
            cover = coverPoint.GetComponent<CoverPointManager>();
            if(Vector3.Distance(transform.position, coverPoint.transform.position) < 30f && !cover.SeePlayer())
            {
                this.coverPoint = coverPoint;
                Task.current.Succeed();
            }
        }
    }
}
