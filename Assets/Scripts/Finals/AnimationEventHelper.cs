using System;
using UnityEngine;

namespace CubeChan {
public class AnimationEventHelper : MonoBehaviour {
    private Action _onDone;
    
    public void Init(Action onDone) {
        _onDone = onDone;
    }
    
    public void InvokeAnimation() {
        _onDone?.Invoke();
    }
}
}