using UnityEngine;

namespace Gizmo.Patrol
{
    public class PatrolPath : MonoBehaviour
    {
        //Radius of the gizmo point
        private const float waypointGizmoRadius = 0.3f;

        //this method increases the waypoint for the path and draws new lines from the previus waypoint to a new point
        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        //this one calculates if there is a waypoint and then adds 1 more waypoint if one is added from the previous one.
        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        //gets the transform position of the waypoint
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
