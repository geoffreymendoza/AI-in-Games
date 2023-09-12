using UnityEngine;

public class MdMovementController : MonoBehaviour {
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rayDist = 100f;
    [SerializeField] private MdReticle reticle;
    
    private Camera _mainCam;
    private MdAgent _agent;
    
    // Start is called before the first frame update
    void Start() {
        _mainCam = Camera.main;
        _agent = GetComponent<MdAgent>();
        if(_agent == null)
            Debug.LogError($"{_agent} is missing. Pls assign");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDist, groundMask)) {
                reticle.transform.position = hitInfo.point + new Vector3(0, 0.1f, 0);
                reticle.Animate();
                _agent.NavAgent.SetDestination(hitInfo.point);
            }
        }
    }
}