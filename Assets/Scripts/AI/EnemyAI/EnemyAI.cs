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
    Animator animator;
    private float rotationSpeed = 1f;
    private float visDistance = 10f;
    private float visAngle = 80f;
    private int currentWaypoint;
    public CoverPointManager cover;
    private bool isDecomposing = false;
    private Material[] zombieMaterials;
    private Attack zombieAttack;
    private bool isAttacking = false;

    Health enemieHealth;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        waypoints = GameObject.FindGameObjectsWithTag("Cover");
        enemieHealth = GetComponent<Health>();
        InvokeRepeating("RecoverHealth", 5, 5f);
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        zombieMaterials = new Material[renderers.Length];
        for(int i = 0; i < renderers.Length; i++)
        {
            zombieMaterials[i] = renderers[i].material;
        }
        zombieAttack = GetComponent<Attack>();
        zombieAttack.SetTarget(GameObject.Find("PlayerHandle"));
    }

    // Update is called once per frame
    void Update()
    {
        if(isDecomposing)
        {
            foreach(Material m in zombieMaterials)
            {
                Color c = m.color;
                if(c.a <= 0)
                {
                    Destroy(gameObject);
                    break;
                }
                c.a -= 0.01f;
                m.color = c;
            }
        }
    }

    [Task]
    void Destination(float x, float z)
    {
        Vector3 destination = new Vector3(x, 0, z);
        agent.SetDestination(destination);
        animator.SetTrigger("Walk");
        animator.SetFloat("Speed", agent.speed);
        Task.current.Succeed();
    }

    [Task]
    void RandomDestination()
    {
        Vector3 destination = new Vector3(Random.Range(0.0f, 80f), 0, Random.Range(-30f, 40f));
        agent.speed = 1.5f;
        agent.SetDestination(destination);
        animator.SetTrigger("Walk");
        animator.SetFloat("Speed", agent.speed);
        Task.current.Succeed();
    }

    [Task]
    void GoToDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            animator.SetTrigger("Idle");
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
        animator.SetFloat("Speed", 2.5f);
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
            isCover |= hit.collider.gameObject.tag == "Obstacle";
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
            if (enemieHealth.HitPointsRemaining > 4f)
            {
                target = player.transform.position;
                updateAgentTarget(target);
            }
        }
    }

    [Task]
    void SetTarget()
    {
        updateAgentTarget(target);
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
        if (enemieHealth.HitPointsRemaining < 20 && enemieHealth.HitPointsRemaining > 0)
            enemieHealth.damageTaken--;
    }

    [Task]
    bool IsInDanger(float minDist)
    {
        Vector3 distance = player.transform.position - transform.position;
        return (distance.magnitude < minDist);
    }

    [Task]
    bool AttackPlayer()
    {
        agent.SetDestination(transform.position);
        animator.SetTrigger("Attack");
        zombieAttack.NormalAttack();
        isAttacking = IsInDanger(1);
        return isAttacking;
    }

    [Task]
    void Flee()
    {
        Vector3 awayFromPlayer = transform.position - player.transform.position;
        Vector3 destination = transform.position + awayFromPlayer / 5;
        updateAgentTarget(destination, 2.0f);
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
            if (Vector3.Distance(transform.position, waypoints[i].transform.position) < 10f && !cover.SeePlayer())
            {
                coverPoint = waypoints[i];
                Task.current.Succeed();
            }
        }
    }

    [Task]
    void AIDead()
    {
        animator.SetTrigger("Die");
        agent.enabled = false;
        GetComponent<PandaBehaviour>().enabled = false;
        //begin decomposing in 10 seconds
        GameManager.Instance.Timer.Add(removeCorpse, 10f);
            
    }

    private void removeCorpse()
    {
        isDecomposing = true;
        foreach (Material m in zombieMaterials)
        {
            https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html
            //set material to transparent render mode
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.DisableKeyword("_ALPHABLEND_ON");
            m.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
        }
    }

    private void updateAgentTarget(Vector3 target)
    {
        updateAgentTarget(target, agent.speed);
    }
    private void updateAgentTarget(Vector3 target, float speed)
    {
        if (!isAttacking)
        {
            agent.SetDestination(target);
            agent.speed = speed;
            animator.SetTrigger("Walk");
            animator.SetFloat("Speed", speed);
        }
    }
}
