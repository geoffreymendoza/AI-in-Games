using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CubeChan {
public class CubeChanController : MonoBehaviour, IEntityHandler {
    [Header("References")]
    [SerializeField] private SkinnedMeshRenderer hairInnerRenderer;
    [SerializeField] private SkinnedMeshRenderer shirtRenderer;
    [SerializeField] private Transform palmTr;
    [SerializeField] private AnimationEventHelper throwAnimationHelper;
    [SerializeField] private Vector2 throwForceMinMax = new Vector2(20f, 100f);
    [SerializeField] private GameObject ballClone;

    [Header("Team")]
    [SerializeField] private Team team;

    private Collider _col;
    private NavMeshAgent _agent;
    private Animator _anim;
    private Ball _ball;
    
    private void Awake() {
        AssignComponents();
        EntityManager.Register(this);
    }

    private void AssignComponents() {
        _col = GetComponentInChildren<Collider>();
        _agent = GetComponentInChildren<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        throwAnimationHelper.Init(ThrowBall);
    }

    public void ThrowAnimation(Ball ball) {
        ballClone.SetActive(true);
        _ball = ball;
        _ball.ResetBall();
        _ball.transform.SetParent(palmTr, true);
        _ball.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _ball.transform.gameObject.SetActive(false);
        ChangeAnim(Data.THROW_ANIMATION);
    }

    private void ThrowBall() {
        ballClone.SetActive(false);
        _ball.transform.gameObject.SetActive(true);
        _ball.transform.SetParent(null);
        float throwForce = Random.Range(throwForceMinMax.x, throwForceMinMax.y);
        _ball.Throw(this.transform.forward, throwForce);
    }
    
    private void ChangeAnim(int state) {
        _anim.CrossFade(state,0,0);
    }

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
}
}