using UnityEngine;

namespace CubeChan.Cube_chan {
[System.Serializable]
public class CubePatrolState : BaseState<CubeChanController> {
    private Vector3 _destination;
    
    public override void EnterState(CubeChanController ctx) {
        StateName = "Patrol";
        _destination = GameManager.Instance.GetPatrolSpot(ctx.Team);
        ctx.ChangeAnim(Data.NAVIGATION);
        ctx.Navigate(_destination);
    }

    public override void UpdateState(CubeChanController ctx) {
        if (Vector3.Distance(ctx.CurrentPosition, _destination) <= 1f) {
            ctx.ChangeState(ctx.IdleState);
        }
    }
}
}