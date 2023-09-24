using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 2f;
    public Vector3 offSet;

    private Transform cameraTransform;
    private Vector3 originalPosition;

    private Vector3 desiredPosition; // Store desired position including follow and shake
    private Vector3 smoothDampedVelocity; // Velocity for smooth camera follow

    private void Start()
    {
        cameraTransform = transform;
    }

    void FixedUpdate()
    {
        // Calculate desired position based on the target's position and offset
        desiredPosition = target.position + offSet;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref smoothDampedVelocity, 1f / smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        originalPosition = cameraTransform.localPosition;
        StartCoroutine(Shake(duration, magnitude));
    }

   private IEnumerator Shake(float duration, float magnitude)
{
    float elapsed = 0.0f;
    Vector3 originalLocalPosition = cameraTransform.localPosition;

    while (elapsed < duration)
    {
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;

        // Apply shake offsets to the original camera local position
        Vector3 shakeOffset = new Vector3(x, y, 0f);
        cameraTransform.localPosition = originalLocalPosition + shakeOffset;

        elapsed += Time.deltaTime;

        yield return null;
    }

    cameraTransform.localPosition = originalLocalPosition;
}
}
