using UnityEngine;

namespace CubeChan {
public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody rb;

    private bool canCollide = false;

    public void ResetBall() {
        canCollide = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
    }
    
    public void Throw(Vector3 direction, float force) {
        canCollide = true;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        if (!canCollide) return;
        if (other.gameObject.CompareTag("Agent")) {
            var handler = other.gameObject.GetComponent<IEntityHandler>();
            handler.ApplyHit();
        }
    }
}
}