using UnityEngine;

namespace CubeChan.Cube_chan {
[System.Serializable]
public class CubeIdleState : BaseState<CubeChanController> {
    [SerializeField] private float resetIdleDuration = 2f;
    [SerializeField] private float idleDuration = 2f;
    
    public override void EnterState(CubeChanController ctx) {
        StateName = "Idle";
        idleDuration = resetIdleDuration;
    }

    public override void UpdateState(CubeChanController ctx) {
        if (idleDuration <= 0) return;
        idleDuration -= Time.deltaTime;
        if (idleDuration > 0) return;
        ctx.ChangeState(ctx.PatrolState);
    }
}
}