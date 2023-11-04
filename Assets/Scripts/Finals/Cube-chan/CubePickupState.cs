using UnityEngine;

namespace CubeChan.Cube_chan {
public class CubePickupState : BaseState<CubeChanController> {
    
    public override void EnterState(CubeChanController ctx) {
        StateName = "Pickup";
        ctx.Navigate(ctx.BallPosition);
    }

    public override void UpdateState(CubeChanController ctx) {
        if (Vector3.Distance(ctx.transform.position, ctx.BallPosition) <= 1) {
            // ctx.ChangeAnim(Data.PICKUP_ANIMATION);
            ctx.ReadyToThrow();
        }
    }
}
}