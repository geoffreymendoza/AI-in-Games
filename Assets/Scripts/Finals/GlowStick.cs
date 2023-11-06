using DG.Tweening;
using UnityEngine;

namespace CubeChan {
public class GlowStick : MonoBehaviour {
    [SerializeField] private Vector2 durationRange = new Vector2(0.3f, 0.6f);
    private float duration;
    
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    
    [SerializeField] private Transform[] glowsticks;

    private void Init() {
        float rand = Random.value;
        foreach (var gs in glowsticks) {
            var mr = gs.GetComponentInChildren<MeshRenderer>();
            if (rand > 0.5f) { // red
                mr.material = red;
            } else { //green
                mr.material = green;
            }
        }
    }
    
    private void Start() {
        Init();
        duration = Random.Range(durationRange.x, durationRange.y);
        MoveLeft();
    }

    private void MoveLeft() {
        foreach (var stick in glowsticks) {
            stick.DORotate(Vector3.left * 30f, duration).SetEase(Ease.InOutCubic);
        }
        Invoke(nameof(MoveRight), duration);
    }

    private void MoveRight() {
        foreach (var stick in glowsticks) {
            stick.DORotate(Vector3.right * 30f, duration).SetEase(Ease.InOutCubic);
        }
        Invoke(nameof(MoveLeft), duration);
    }
}
}