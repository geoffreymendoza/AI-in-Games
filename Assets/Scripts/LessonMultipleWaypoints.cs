using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LessonMultipleWaypoints : MonoBehaviour {
    private NavMeshAgent agent;
    public Transform targetWaypoint;
    public List<Transform> waypoints = new List<Transform>();
    public int waypointNumber;
    public bool isMoving;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();     
        
        foreach (Transform t in targetWaypoint.GetComponentInChildren<Transform>()) { 
            waypoints.Add(t);
        }
    
    }

    // Update is called once per frame
    void Update() {
        // returns true or false, if AI has next destination
        if (!agent.pathPending) {
            // returns the distance of agent if it is the same of stopping distance
            if (agent.remainingDistance <= agent.stoppingDistance) {
                //if agent has no path or agent is standing still
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    MoveToRandomWaypoint();
                }
            }
        }
    }

    private void MoveToRandomWaypoint() {
        if(waypoints.Count == 0) {
            Debug.LogWarning("No waypoints");
            return;
        }
        isMoving = true;
        int newWayPointIndex = GetRandomWayPointIndex();
        if (newWayPointIndex != waypointNumber) {
            // we make this equal to random waypoint
            waypointNumber = newWayPointIndex;
            //setting the agent to new destination
            agent.SetDestination(waypoints[waypointNumber].position);
        } else
            MoveToRandomWaypoint();

    }

    private int GetRandomWayPointIndex() => UnityEngine.Random.Range(0, waypoints.Count);
}
