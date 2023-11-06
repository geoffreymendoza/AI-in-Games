using UnityEngine;

namespace CubeChan.Cube_chan {
[System.Serializable]
public class CubePickupState : BaseState<CubeChanController> {
    private bool _isPickingUp = false;
    
    public override void EnterState(CubeChanController ctx) {
        _isPickingUp = false;
        StateName = "Pickup";
    }

    public override void UpdateState(CubeChanController ctx) {
        if(!_isPickingUp)
            ctx.Navigate(ctx.BallPosition);
        if (!_isPickingUp && Vector3.Distance(ctx.transform.position, ctx.BallPosition) <= 1) {
            _isPickingUp = true;
            ctx.ReadyToThrow();
        }
    }
}
}