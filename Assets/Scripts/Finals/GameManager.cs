using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeChan {
public class GameManager : MonoBehaviour {
    public static event Action OnPlayState;
    public static GameManager Instance { private set; get; }
    
    [SerializeField] private Ball ballPr;
    [SerializeField] private Team currentTeamPlay;
    [SerializeField] private EntityManager entityMgr;
    [SerializeField] private float patrolRange;
    [SerializeField] private Vector3 redTeamSpot;
    [SerializeField] private Vector3 greenTeamSpot;

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
        InitTossCoin();
    }

    private void InitTossCoin() {
        float result = Random.value;
        currentTeamPlay = result > 0.5f ? Team.Green : Team.Red; // Team green
        var ball = Instantiate(ballPr);
        switch (currentTeamPlay) {
            case Team.Red:
                ball.transform.position = new Vector3(0, 0.5f, 35f);
                break;
            case Team.Green:
                ball.transform.position = new Vector3(0, 0.5f, 20f);
                break;
        }
    }

    public void InvokePlay() {
        OnPlayState?.Invoke();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(redTeamSpot, patrolRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(greenTeamSpot, patrolRange);
    }
}
}