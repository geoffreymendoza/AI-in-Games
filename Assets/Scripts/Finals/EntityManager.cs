using System.Collections.Generic;
using UnityEngine;

namespace CubeChan {
public class EntityManager : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Material teamRedMat;
    [SerializeField] private Material teamGreenMat;
    
    [Header("Entity")]
    [SerializeField] private LayerMask agentMask;
    [SerializeField] private float detectRadius;
    [SerializeField] private Vector3 redTeamLoc;
    [SerializeField] private Vector3 greenTeamLoc;

    private static List<CubeChanController> _entities = new List<CubeChanController>();

    private void Start() {
        SetEntities();
    }

    public static void Register(CubeChanController controller) {
        if(_entities == null)
            _entities = new List<CubeChanController>();
        if (_entities.Contains(controller)) return;
        _entities.Add(controller);
    }
    
    private void SetEntities() {
        CheckColliders(Team.Red, redTeamLoc, teamRedMat);
        CheckColliders(Team.Green, greenTeamLoc, teamGreenMat);
    }

    private void CheckColliders(Team team,Vector3 pos, Material newMaterial) {
        Collider[ ] cols = Physics.OverlapSphere(pos, detectRadius, agentMask);
        if (cols.Length > 0) {
            foreach (var col in cols) {
                var handler = col.GetComponent<IEntityHandler>();
                if (handler != null) {
                    handler.HairInnerRenderer.material = newMaterial;
                    handler.ShirtRenderer.material = newMaterial;
                    handler.SetTeam(team);
                }
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(redTeamLoc, detectRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(greenTeamLoc, detectRadius);
    }
}
}