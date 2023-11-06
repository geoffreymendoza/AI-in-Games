using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CubeChan {
public class EntityManager : MonoBehaviour {
    public static EntityManager Instance { private set; get; }
    
    [Header("References")]
    [SerializeField] private Material teamRedMat;
    [SerializeField] private Material teamGreenMat;
    
    [Header("Entity")]
    [SerializeField] private LayerMask agentMask;
    [SerializeField] private float detectRadius;
    [SerializeField] private Vector3 redTeamLoc;
    [SerializeField] private Vector3 greenTeamLoc;

    private List<CubeChanController> _entities = new List<CubeChanController>();
    public CubeChanController[ ] GetEntities() => _entities.ToArray();

    private void Awake() {
        Instance = this;
        _entities = new List<CubeChanController>();
    }

    private void OnDestroy() {
        Instance = null;
    }

    public void Register(CubeChanController controller) {
        if (_entities.Contains(controller)) return;
        _entities.Add(controller);
        SetEntities();
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

    public Vector3 GetRandomEnemyPosition(Team currentTeam) {
        Vector3 resultPos = Vector3.zero;
        foreach (CubeChanController cube in _entities.Where(cube => cube.isActiveAndEnabled && cube.IsAlive && cube.Team != currentTeam)) {
            resultPos = cube.transform.position;
            break;
        }
        return resultPos;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(redTeamLoc, detectRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(greenTeamLoc, detectRadius);
    }
}
}