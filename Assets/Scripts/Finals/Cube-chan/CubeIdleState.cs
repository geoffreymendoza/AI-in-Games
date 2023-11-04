using UnityEngine;

namespace CubeChan.Cube_chan {
[System.Serializable]
public class CubeIdleState : BaseState<CubeChanController> {
    [SerializeField] private float resetIdleDuration = 2f;
    private float idleDuration = 2f;
    private CubeChanController _controller;
    
    public override void EnterState(CubeChanController ctx) {
        _controller = ctx;
        StateName = "Idle";
        idleDuration = resetIdleDuration;
        // ctx.ChangeAnim(Data.IDLE_ANIMATION);
    }

    public override void UpdateState(CubeChanController ctx) {
        ctx.DetectBall();
        if (idleDuration <= 0) return;
        idleDuration -= Time.deltaTime;
        if (idleDuration > 0) return;
        ctx.ChangeState(ctx.PatrolState);
    }
}
}