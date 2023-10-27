using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour {
    private NavMeshAgent _agent;
    [SerializeField] Transform waypoint;
    [SerializeField] private float distThreshold = 1f;
    [SerializeField] Transform[] wayPoints;
    private int curIdx = 0;
    private bool _isMoving = false;
    private Vector3 destination;

    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        
        
    }

    // Start is called before the first frame update
    void Start() {
        curIdx = 0;
    }

    // Update is called once per frame
    void Update() {
        // Optimization strat
        //if (waypoint.hasChanged) {
        //    _agent.SetDestination(waypoint.position);
        //    waypoint.hasChanged = false;
        //}

        if(!_isMoving) {
            destination = wayPoints[curIdx].position;
            AssignDestination(destination);
        }

        if(_isMoving && Vector3.Distance(_agent.transform.position, destination) <= distThreshold) {
            curIdx++;
            if(curIdx > wayPoints.Length - 1) {
                curIdx = 0;
            }
            _isMoving = false;
        }
    }

    private void AssignDestination(Vector3 position) {
        _agent.SetDestination(position);
        _isMoving = true;
    }
}