using System.Collections;
using UnityEngine;

public class BoomerangScriptWeapon : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    private Vector2 initialDirection;
    private float maxDistance;
    Vector2 target;
    private Transform playerTransform;
    private Vector2 initialPosition;
    private float boomerangSpeed;
    private bool isReturning = false;
    Rigidbody2D rb;
    public float rotationSpeed = 20.0f; // Adjust this value in the Inspector
    private void Start()
{
    FindObjectOfType<AudioManager>().Play("Boomerang Spinning");
    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    initialPosition = (Vector2)transform.position;

    maxDistance = weaponData.range * 2; // Double the range for complete round-trip
    
}
    public void MoveBoomerang(Vector2 direction)
{
    target = direction;
    boomerangSpeed = weaponData.speed;
    transform.right = direction;
    rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    
    rb.velocity = direction.normalized * boomerangSpeed; // Set the velocity
}

    private void Update()
    {
        // Apply rotation on the z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
       if (!isReturning)
    {
        float distanceTraveled = Vector2.Distance(initialPosition, (Vector2)transform.position);

        if (distanceTraveled >= weaponData.range)
        {
            isReturning = true;
        }
        else
        {
            float remainingDistance = weaponData.range - distanceTraveled;
            float slowdownFactor = Mathf.Clamp01(remainingDistance / weaponData.range);

            // Gradually reduce the boomerang's speed using lerping
            boomerangSpeed = Mathf.Lerp(boomerangSpeed, 1, slowdownFactor * 0.01f); // Adjust the factor here
            rb.velocity = target.normalized * boomerangSpeed;
        }
    }

        if (isReturning)
        {
            float distanceTraveled = Vector2.Distance(initialPosition, (Vector2)transform.position);
            // Calculate the direction to move the boomerang towards
             float remainingDistance = weaponData.range + distanceTraveled;
            Vector2 returnDirection = (initialPosition - (Vector2)transform.position).normalized;
            float speedupFactor = Mathf.Clamp01(remainingDistance / weaponData.range);
            boomerangSpeed = Mathf.Lerp(boomerangSpeed, weaponData.speed, speedupFactor * 0.01f); // Adjust the factor here
            // Move the boomerang in the return direction
          rb.velocity = returnDirection.normalized * boomerangSpeed;

            // Check if the boomerang has returned beyond the maxDistance
            
            if (Vector2.Distance((Vector2)transform.position, initialPosition) < 0.5f)
{
    // Destroy the boomerang object
    Destroy(gameObject);
}
        }
    }
}
