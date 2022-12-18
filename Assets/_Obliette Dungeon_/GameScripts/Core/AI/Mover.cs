using ActionHandler;
using Action;
using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        //[SerializeField] private Transform target;

        //max speed that the character has
        [SerializeField] private float maxSpeed = 6;
        //how far someone can move in the navmesh. this is a future feature if for an instance someone wants to use teleport system in VR but this is the max distance it can click to.
        [SerializeField] private float maxNavPathLength = 40f;

        //initiating variable
        private NavMeshAgent navMeshAgent;

        void Awake()
        {
            //getting the component to be able to use the methods inside it.
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        //starts the moving action and where to move
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        //this checks if it actually can move to that point or not, if it can't then it finds another path where it can move.
        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }


        //moves the character to where it is pointed with its speed calculated.
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }


        //a method that acncels an action.
        public void Cancel()
        {

            navMeshAgent.isStopped = true;
        }


        //this makes the velocity of the character to move along with the animation
        void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }


        //this calculates the path length to where it can move.
        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return 0;
        }
    }
}
