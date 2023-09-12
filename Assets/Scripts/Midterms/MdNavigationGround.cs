using UnityEngine;

public class MdNavigationGround : MonoBehaviour {
    [SerializeField] private float navRadius = 5f;
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, navRadius);
    }

    public Vector3 GetRandomPoint() {
        Vector2 point = Random.insideUnitCircle * 5;
        Vector3 result = this.transform.position;
        result.x += point.x;
        result.z += point.y;
        return result;
    }
}