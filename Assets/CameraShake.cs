using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 originalPosition;

    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.1f;

    private void Start()
    {
        cameraTransform = transform;
    }

    public void ShakeCamera()
    {
        originalPosition = cameraTransform.localPosition;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }
}
