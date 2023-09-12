using UnityEngine;

public class MdReticle : MonoBehaviour {
    private Animator _anim;
    private readonly int Pop = Animator.StringToHash("Reticle");

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void Animate() {
        _anim.CrossFade(Pop, 0 ,0);
    }
}