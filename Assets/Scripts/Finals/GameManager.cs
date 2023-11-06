using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeChan {
public class GameManager : MonoBehaviour {
    public static event Action OnPlayState;
    public static GameManager Instance { private set; get; }
    
    [Header("References")]
    [SerializeField] private Ball ballPr;
    [SerializeField] private Team currentTeamPlay;
    [SerializeField] private EntityManager entityMgr;
    [SerializeField] private float patrolRange;
    [SerializeField] private Vector3 redTeamSpot;
    [SerializeField] private Vector3 greenTeamSpot;
    [SerializeField] private Transform lineTr;
    
    private Ball _ball;
    public Vector3 CenterLine => lineTr.position;
    public GameState GameState { private set; get; }
    
    public Vector3 GetPatrolSpot(Team team) {
        Vector3 position = Vector3.zero;
        switch (team) {
            case Team.Red:
                position = redTeamSpot + Random.insideUnitSphere * patrolRange;
                position.y = 0;
                break;
            case Team.Green:
                position = greenTeamSpot + Random.insideUnitSphere * patrolRange;
                position.y = 0;
                break;
        }
        return position;
    }
    
    private void Awake() {
        Instance = this;
    }

    private void OnDestroy() {
        Instance = null;
    }

    private void Start() {
        Invoke(nameof(InitTossCoin), 1f);
    }

    private void InitTossCoin() {
        float result = Random.value;
        currentTeamPlay = result > 0.5f ? Team.Green : Team.Red;
        _ball = Instantiate(ballPr);
        switch (currentTeamPlay) {
            case Team.Red:
                _ball.transform.position = new Vector3(0, 0.5f, 35f);
                break;
            case Team.Green:
                _ball.transform.position = new Vector3(0, 0.5f, 20f);
                break;
        }
        GameState = GameState.Throwing;
        AssignNewThrower();
    }

    public void InvokePlay(Team team) {
        OnPlayState?.Invoke();
        // currentTeamPlay = team;
        switch (team) {
            case Team.Red:
                currentTeamPlay = Team.Green;
                break;
            case Team.Green:
                currentTeamPlay = Team.Red;
                break;

        }
        GameState = GameState.Dodging;
        Invoke(nameof( AssignNewThrower ), 2f);
    }

    private void AssignNewThrower() {
        _ball.ResetBall();
        var entities = entityMgr.GetEntities();
        foreach (var cube in entities) {
            // Debug.Log($"checking {cube.Team}");
            if(!cube.IsAlive || cube.Team != currentTeamPlay)
                continue;
            Debug.Log($"found {cube.name}");
            cube.PickupBall(_ball);
            break;
        }
    }

    public void ChangeState(GameState state) {
        GameState = state;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(redTeamSpot, patrolRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(greenTeamSpot, patrolRange);
    }
}
}