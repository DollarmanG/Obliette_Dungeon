using ActionHandler;
using Gizmo.Patrol;
using Movement;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolling : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float suspicionTime = 3f;
    [SerializeField] private PatrolPath patrolPath;
    [SerializeField] private float waypointTolerance = 1f;
    [SerializeField] private float waypointDwellTime = 3f;
    [SerializeField] private AudioSource _chaseplayerAudio;

    [Range(0, 1)][SerializeField] private float patrolSpeedFraction = 0.2f;

    private GameObject player;
    private Mover mover;

    LazyValue<Vector3> guardPosition;
    private float timeSinceLastSawPlayer = Mathf.Infinity;
    private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    private int currentWaypointIndex = 0;

    private GameObject target;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] private Vector3 offsetRay;

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        mover = GetComponent<Mover>();
        player = GameObject.FindWithTag("Player");

        guardPosition = new LazyValue<Vector3>(GetGuardPosition);
    }

    private Vector3 GetGuardPosition()
    {
        return transform.position;
    }

    void Start()
    {
        guardPosition.ForceInit();
    }

    void Update()
    {

        if (IsAggrevated() || RaycastStraightLine())
        {
            ChaseTarget();
        }
        else if (timeSinceLastSawPlayer < suspicionTime)
        {
            SuspicionBehaviour();
        }
        else
        {
            PatrolBehaviour();
        }

        UpdateTimers();
    }


    private void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    bool RaycastStraightLine()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + offsetRay, transform.TransformDirection(Vector3.forward), out hitInfo, rayLength))
        {
            if (IsInLayerMask(hitInfo.collider.gameObject, collisionMask))
            {
                ICanFollow obj = hitInfo.collider.GetComponent<ICanFollow>();
                if (obj != null)
                {
                    
                    target = obj.FollowTarget();
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }


    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition.value;
        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }

            nextPosition = GetCurrentWaypoint();
        }

        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void SuspicionBehaviour()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private bool IsAggrevated()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }

    private void ChaseTarget()
    {
        UpdateAnimator();
        _chaseplayerAudio.PlayOneShot(_chaseplayerAudio.clip);
        
        if (target != null)
        {
            navMeshAgent.speed = 3f;
            navMeshAgent.SetDestination(target.transform.position);
        }
        
    }

    void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + offsetRay, transform.TransformDirection(Vector3.forward) * rayLength);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
