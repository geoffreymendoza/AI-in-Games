using UnityEngine;

public class MdSpawner : MonoBehaviour {
    [SerializeField] private MdAgent neutralAgentPr;
    [SerializeField] private Transform navMeshParentTr;
    
    private void Start() {
        var clone = Instantiate(neutralAgentPr, this.transform.position, Quaternion.identity);
        clone.Init(navMeshParentTr);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 1f);
    }
}