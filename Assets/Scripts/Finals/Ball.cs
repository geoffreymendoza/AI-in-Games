using UnityEngine;

namespace CubeChan {
public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool canTouchAgent = false;

    public void ResetBall() {
        canTouchAgent = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
    }
    
    public void Throw(Vector3 direction, float force) {
        canTouchAgent = true;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        GameManager.Instance.ChangeState(GameState.Throwing);
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.CompareTag("Object")) {
            // canTouchAgent = false;
            ResetBall();
        }
        if (!canTouchAgent) return;
        if (other.gameObject.CompareTag("Agent")) {
            var handler = other.gameObject.GetComponent<IEntityHandler>();
            handler.ApplyHit();
        }
    }
}
}