using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LessonOfflinkCurve : MonoBehaviour {
    private NavMeshAgent agent;
    public OfflinkMoveType moveType;
    public AnimationCurve animCurve;
    public float height = 2.0f;
    public float duration = 0.5f;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    IEnumerator Start() {
        agent.autoTraverseOffMeshLink = false;
        while (true) {
            if(agent.isOnOffMeshLink) {
                switch (moveType) {
                    case OfflinkMoveType.Invalid:
                        break;
                    case OfflinkMoveType.Normal:
                        yield return StartCoroutine(NormalSpeed(agent));
                        break;
                    case OfflinkMoveType.Parabola:
                        yield return StartCoroutine(Parabola(agent, height, duration));
                        break;
                    case OfflinkMoveType.Curve:
                        yield return StartCoroutine(Curve(agent, duration));
                        break;
                    case OfflinkMoveType.Climb:
                        yield return StartCoroutine(Climb(agent));
                        break;
                }
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    // Derives the speed
    private IEnumerator NormalSpeed(NavMeshAgent agent) {
        OffMeshLinkData linkData = agent.currentOffMeshLinkData;
        Vector3 endPos = linkData.endPos + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos) {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Climb(NavMeshAgent agent) { 
        OffMeshLinkData data = agent.currentOffMeshLinkData; ;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        Vector3 targetDir = endPos - agent.transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.z);
        while (agent.transform.position != endPos) {
            Vector3 targetPos = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            Quaternion targetRot = Quaternion.Euler(0, angle, 0);
            agent.transform.SetPositionAndRotation(targetPos, targetRot);
            yield return null;
        }
    }

    // fixed height and
    private IEnumerator Parabola(NavMeshAgent agent, float height, float duration) {
        OffMeshLinkData linkData = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = linkData.endPos + Vector3.up * agent.baseOffset;
        float normalizeTime = 0.0f;
        while(normalizeTime <= 1) {
            float yOffset = height * 4.0f * (normalizeTime - normalizeTime * normalizeTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, yOffset) + yOffset * Vector3.up;
            normalizeTime += Time.deltaTime / duration;
            yield return null;
        }
    }

    // depends on curve animation
    private IEnumerator Curve(NavMeshAgent agent, float duration) {
        OffMeshLinkData linkData = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = linkData.endPos + Vector3.up * agent.baseOffset;
        float normalizeTime = 0.0f;
        while(normalizeTime <= 1) {
            float yOffset = animCurve.Evaluate(normalizeTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, yOffset) + yOffset * Vector3.up;
            normalizeTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}


public enum OfflinkMoveType {
    Invalid = -1,
    Normal,
    Teleport,
    Parabola,
    Curve,
    Climb,
}