using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMovement : MonoBehaviour {
    //[SerializeField] private float moveSpeed = 5f;

    public Transform waypointA, waypointB;
    public Transform targetWaypoint;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        if (targetWaypoint == null) {
            Debug.LogError("No Target Waypoint");
        } else {
            SetDestination(waypointA);
        }
    }

    // Update is called once per frame
    private void Update() {
        Patrol();
    }

    private void SetDestination(Transform destination) {
        if (agent != null) {
            targetWaypoint = destination;
            agent.SetDestination(destination.position);
        }
    }

    private void Patrol() {
        // SINGLE WAYPOINT
        //if(targetWaypoint.hasChanged)
        //{
        //    agent.SetDestination(targetWaypoint.position);
        //    targetWaypoint.hasChanged = false;
        //}

        // MULTIPLE WAYPOINT
        if (targetWaypoint != waypointA && targetWaypoint != waypointB) {
            return;
        }

        if(agent.remainingDistance < 0.1f) { 
        //if (Vector3.Distance(transform.position, targetWaypoint.position) <= 1f) {
            // switches to other waypoint
            if (targetWaypoint == waypointA) {
                SetDestination(waypointB);
            }
            else if (targetWaypoint == waypointB) {
                SetDestination(waypointA);
            }
        }
    }
}