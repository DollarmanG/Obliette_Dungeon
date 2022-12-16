using ActionHandler;
using Gizmo.Patrol;
using Movement;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolling : MonoBehaviour
{
    //the float references are tweakable in inspector to choose your speed on each function
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float suspicionTime = 3f;
    //is a reference to the patrolpath script where it draws out the various waypoints on the path.
    [SerializeField] private PatrolPath patrolPath;
    [SerializeField] private float waypointTolerance = 1f;
    [SerializeField] private float waypointDwellTime = 3f;

    // Yell command audio source
    [SerializeField]
    private AudioSource yellCommand;

    [SerializeField]
    private AudioSource whistle;

    // Counter to prevent multiple yells
    private int dialogueCounter;

    //this decides the speed of the character
    [Range(0, 1)][SerializeField] private float patrolSpeedFraction = 0.2f;

    private GameObject player;
    private Mover mover;

    //these variables are a reference to positions in each action.
    LazyValue<Vector3> guardPosition;
    private float timeSinceLastSawPlayer = Mathf.Infinity;
    private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    private int currentWaypointIndex = 0;

    private GameObject target;
    //this just gives the designer to choose the length of the ray and what layermask to collide with.
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask collisionMask;

    //the placement of the ray from where it should shoot out from
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
        //this checks the guard position and makes sure there is no race conditions.
        guardPosition.ForceInit();

        dialogueCounter = 0;
    }

    void Update()
    {
        //checks if the character is aggrevated by its foe or if the raycastlinear hits the foe.
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

    //casts the ray in a direction and follows the characters direction view.
    bool RaycastStraightLine()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + offsetRay, transform.TransformDirection(Vector3.forward), out hitInfo, rayLength))
        {
            //checks what object this collides with.
            if (IsInLayerMask(hitInfo.collider.gameObject, collisionMask))
            {
                ICanFollow obj = hitInfo.collider.GetComponent<ICanFollow>();
                if (obj != null)
                {
                    //if it hits someone then it follows that target
                    target = obj.FollowTarget();
                    if (dialogueCounter == 0)
                    {
                        yellCommand.PlayOneShot(yellCommand.clip);
                        dialogueCounter++;
                    }
                    
                    return true;
                }
            }

            dialogueCounter = 0;
        }

        return false;
    }

    //checks between bit values if the flag is matching the other flag as in if the masks compare to eachother.
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }


    
    private void PatrolBehaviour()
    {
        //sets the guards position to the next position
        Vector3 nextPosition = guardPosition.value;
        //if there is a waypoint to go to then it should do what is inside the statement
        if (patrolPath != null)
        {

            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            //if there isn't a next waypoint then it should go back to its start
            nextPosition = GetCurrentWaypoint();
        }
        //if the guard ran away from the waypoint before it finished dwelltime, then it goes back there.
        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }
    }

    //what it does at the waypoint, calculates the distance to the next waypoint.
    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    //checks the next waypoint so it makes a cycle of it.
    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    //gets the current waypoint that the character is currently in, so if it moves to another one, that becomes the new current waypoint.
    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    //cancels an action and makes a suspicious behavior
    private void SuspicionBehaviour()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    //if aggrevated then it starts chasing the target depending on its distance.
    private bool IsAggrevated()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }

    //calculates the speed and checks the animator to change the speed when chasing.
    private void ChaseTarget()
    {
        UpdateAnimator();
        
        if (target != null)
        {
            navMeshAgent.speed = 3f;
            navMeshAgent.SetDestination(target.transform.position);
        }
        
    }

    //updates the velocity and movement speed of the animation
    void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    //just draws the gizmo in the scene field of the ray.
    void OnDrawGizmosSelected()
    {
        //ray straight gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + offsetRay, transform.TransformDirection(Vector3.forward) * rayLength);

        //ray sphere gizmo
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
