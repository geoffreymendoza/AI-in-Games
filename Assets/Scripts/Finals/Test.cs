using UnityEngine;

namespace CubeChan {
public class Test : MonoBehaviour {
    [SerializeField] private CubeChanController cubeChanTest;
    [SerializeField] private Ball ball;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            cubeChanTest.ReadyToThrow();
        }
    }
}
}