using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MdAgent : MonoBehaviour {
    public NavMeshAgent NavAgent { private set; get; }
    [SerializeField] private AgentType type;
    [SerializeField] private Team team;
    [SerializeField] private float distThresh = 1f;
    
    private List<MdNavigationGround> _navGndList = new List<MdNavigationGround>();
    private Vector3 _currPatrolPoint;
    private bool _isMoving = false;

    [Header("Detect")]
    [SerializeField] private float touchRadius = 2f;
    [SerializeField] private LayerMask agentMask;
    private bool isFollowPlayer = false;
    private Transform playerTr;
    private float _curDetectTime = 0;
    [SerializeField] private float timeBtwnDetect = 0.1f;
    
    [Header("Reference")]
    [SerializeField] private Material playerMat;
    [SerializeField] private Material enemyMat;
    [SerializeField] private Material neutralMat;
    private MeshRenderer _meshRend;

    private void Awake() {
        NavAgent = GetComponent<NavMeshAgent>();
        _meshRend = GetComponent<MeshRenderer>();
    }

    public void Init(Transform navParentTransform) {
        switch (type) {
            case AgentType.Player:
                _curDetectTime = timeBtwnDetect;
                break;
            case AgentType.EnemyAI:
            case AgentType.PatrolAI:
                foreach (MdNavigationGround gnd in navParentTransform.GetComponentsInChildren<MdNavigationGround>())
                    _navGndList.Add(gnd);
                AssignPatrolPoint();
                break;
        }
    }

    private void Update() {
        Navigate();
        Detect();
    }

    private void Detect() {
        if (type != AgentType.Player) return;
        _curDetectTime -= Time.deltaTime;
        if (_curDetectTime > 0) return;
        _curDetectTime = timeBtwnDetect;
        
        var cols = Physics.OverlapSphere(this.transform.position, touchRadius, agentMask);
        if (cols.Length <= 0) return;
        foreach (var col in cols) {
            col.GetComponent<MdAgent>().FollowPlayer(this.transform);
        }
    }

    private void FollowPlayer(Transform tr) {
        _meshRend.material = playerMat;
        NavAgent.speed = 1.75f;
        NavAgent.stoppingDistance = 3f;
        playerTr = tr;
        NavAgent.SetDestination(playerTr.position);
        isFollowPlayer = true;
    }

    private void Navigate() {
        if (type == AgentType.Player) return;
        if (!_isMoving) return;
        if (isFollowPlayer) {
            NavAgent.SetDestination(playerTr.position);
        } else {
            if (Vector3.Distance(_currPatrolPoint, this.transform.position) <= distThresh) {
                _isMoving = false;
                AssignPatrolPoint();
            } 
        }
    }

    private void AssignPatrolPoint() {
        var ground = GetRandomGround();
        var point = ground.GetRandomPoint();
        if(point == _currPatrolPoint)
            AssignPatrolPoint();
        _currPatrolPoint = point;
        NavAgent.SetDestination(_currPatrolPoint);
        _isMoving = true;
    }

    private MdNavigationGround GetRandomGround() {
        var idx = UnityEngine.Random.Range(0, _navGndList.Count);
        return _navGndList[idx];
    }

    private void OnDrawGizmos() {
        if (type != AgentType.Player) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position,touchRadius);
    }
}

public enum Team {
    Neutral,
    Blue,
    Red
}

public enum AgentType {
    Unassigned,
    Player,
    EnemyAI,
    PatrolAI,
}