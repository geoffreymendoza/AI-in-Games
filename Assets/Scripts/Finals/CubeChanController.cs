using System.Collections;
using CubeChan.Cube_chan;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CubeChan {
public class CubeChanController : MonoBehaviour, IEntityHandler {
    [Header("References")]
    [SerializeField] private SkinnedMeshRenderer hairInnerRenderer;
    [SerializeField] private SkinnedMeshRenderer shirtRenderer;
    [SerializeField] private Transform palmTr;
    [SerializeField] private AnimationEventHelper throwAnimationHelper;
    
    [Header("Ball")]
    [SerializeField] private Vector2 throwForceMinMax = new Vector2(20f, 100f);
    [SerializeField] private GameObject ballClone;
    [SerializeField] private LayerMask ballMask;
    [SerializeField] private float detectBallRange;
    public Vector3 BallPosition => _ball.transform.position;

    [Header("Team")]
    [SerializeField] private Team team;

    [Header("States")]
    public CubeIdleState IdleState = new CubeIdleState();
    public CubePatrolState PatrolState = new CubePatrolState();
    public CubePickupState PickupState = new CubePickupState();
    private BaseState<CubeChanController> _currentState;

    private Collider _col;
    private NavMeshAgent _agent;
    private Animator _anim;
    private Ball _ball;

    public Vector3 CurrentPosition => this.transform.position;
    
    private void Awake() {
        AssignComponents();
        EntityManager.Register(this);
    }

    private void Start() {
        _currentState = IdleState;
        _currentState.EnterState(this);
    }

    private void OnEnable() {
        GameManager.OnPlayState += OnPlayState;
    }

    private void OnDisable() {
        GameManager.OnPlayState -= OnPlayState;
    }
    
    private void OnPlayState() {
        // either change to patrol or idle state
        float chance = Random.value;
    }

    private void Update() {
        _currentState.UpdateState(this);
        _anim.SetFloat(Data.NAVIGATION_ANIMATION, _agent.velocity.magnitude);
        Debug.Log(_currentState.StateName);
    }

    private void AssignComponents() {
        _col = GetComponentInChildren<Collider>();
        _agent = GetComponentInChildren<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        throwAnimationHelper.Init(ThrowBall);
    }

    public void ReadyToThrow() {
        GameManager.Instance.InvokePlay();
        ballClone.SetActive(true);
        _ball.ResetBall();
        _ball.transform.SetParent(palmTr, true);
        _ball.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _ball.transform.gameObject.SetActive(false);
        ChangeAnim(Data.PICKUP_ANIMATION);
    }

    private void ThrowBall() {
        ballClone.SetActive(false);
        _ball.transform.gameObject.SetActive(true);
        _ball.transform.SetParent(null);
        float throwForce = Random.Range(throwForceMinMax.x, throwForceMinMax.y);
        _ball.Throw(this.transform.forward, throwForce);
        _ball = null;
        ChangeState(PatrolState);
    }

    #region Outside Calls

    public void DetectBall() {
        var cols = Physics.OverlapSphere(this.transform.position, detectBallRange, ballMask);
        if (cols.Length > 0) {
            _ball = cols[0].GetComponent<Ball>();
            ChangeState(PickupState);
        }
    }

    public void ChangeAnim(int state) {
        _anim.CrossFade(state,0,0);
    }

    public void Navigate(Vector3 position) {
        _agent.SetDestination(position);
    }

    public void ChangeState(BaseState<CubeChanController> state) {
        _currentState = state;
        _currentState.EnterState(this);
    }

    #endregion

    #region Entity Handler

    public int ID => this.gameObject.GetInstanceID();
    public Team Team => team;
    public int Hp { get; private set; } = 1;
    public SkinnedMeshRenderer HairInnerRenderer => hairInnerRenderer;
    public SkinnedMeshRenderer ShirtRenderer => shirtRenderer;

    public void ApplyHit() {
        if (Hp <= 0) return;
        Hp -= 1;
        if (Hp <= 0) {
            // Cannot play anymore
            ChangeAnim(Data.DEATH_ANIMATION);
            _agent.enabled = false;
            _col.enabled = false;
        }
    }

    // private IEnumerator Disappear_Co {
    //     
    // }

    public void SetTeam(Team team) {
        this.team = team;
        switch (this.team) {
            case Team.Red:
                this.transform.Rotate(Vector3.up * 180f, Space.World);
                break;
            case Team.Green:
                break;
        }
    }

    #endregion

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, detectBallRange);
    }
}
}