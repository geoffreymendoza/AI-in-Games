using UnityEngine;

namespace CubeChan.Cube_chan {
[System.Serializable]
public abstract class BaseState<T> where T : MonoBehaviour {
    public string StateName;
    public abstract void EnterState(T ctx);
    public abstract void UpdateState(T ctx);
}
}