using UnityEngine;
using UnityEngine.AI;
using Panda;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]Transform player;
    [SerializeField]GameObject coverPoint;
    [SerializeField]GameObject[] waypoints;
    [SerializeField]Vector3 target;
    NavMeshAgent agent;
    private float rotationSpeed = 1f;
    private float visDistance = 10f;
    private float visAngle = 80f;
    private int currentWaypoint;
    public CoverPointManager cover;

    Health enemieHealth;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        enemieHealth = GetComponent<Health>();
        InvokeRepeating("RecoverHealth", 5, 2f);
    }

    // Update is called once per frame
    void Update()
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
        Vector3 destination = new Vector3(Random.Range(0.0f, 80f), 0, Random.Range(-30f, 40f));
        agent.speed = 1.5f;
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
        agent.speed = 2.5f;
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SoundTrigger")
        {
            if (enemieHealth.HitPointsRemaining > 10f)
            {
                target = player.transform.position;
                SetTarget();
            }
        }
    }

    [Task]
    void SetTarget()
    {
        agent.SetDestination(target);
        Task.current.Succeed();
    }

    [Task]
    bool IsNotHealthy(float h)
    {
        return enemieHealth.HitPointsRemaining < h;
    }

    [Task]
    void RecoverHealth()
    {
        if (enemieHealth.HitPointsRemaining < 20)
            enemieHealth.damageTaken--;
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
        Vector3 destination = transform.position + awayFromPlayer / 5;
        agent.SetDestination(destination);
        agent.speed = 2.0f;
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
            if (Vector3.Distance(transform.position, waypoints[i].transform.position) < 100f && !cover.SeePlayer())
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
