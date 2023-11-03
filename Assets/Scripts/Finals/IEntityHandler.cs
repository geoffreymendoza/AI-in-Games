using UnityEngine;

namespace CubeChan {
public interface IEntityHandler {
    public int ID { get; }
    public Team Team { get; }
    public int Hp { get; }
    public SkinnedMeshRenderer HairInnerRenderer { get; }
    public SkinnedMeshRenderer ShirtRenderer { get; }
    public void ApplyHit();
    public void SetTeam(Team team);
}
}