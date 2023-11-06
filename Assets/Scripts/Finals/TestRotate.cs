using System;
using System.Collections;
using UnityEngine;

namespace CubeChan {
public class TestRotate : MonoBehaviour {
    private void Start() {
        StartCoroutine(Rotate( 90f,1f));
    }

    IEnumerator Rotate(float targetAngle, float duration, Action onDone = null) {
        float startRotation = transform.eulerAngles.y;
        // float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration) {
            t += Time.deltaTime;

            float yRotation = Mathf.Lerp(startRotation, targetAngle, t / duration) % 360.0f;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

            yield return null;
        }
        onDone?.Invoke();
    }
}
}