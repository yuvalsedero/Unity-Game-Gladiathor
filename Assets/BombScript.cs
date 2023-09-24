using UnityEngine;

public class BombScript : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public GameObject explosionPrefab; // Reference to the explosion animation prefab
    public float destroyTime = 2.0f; // Time after which the bomb will be destroyed
    

    private bool hasExploded = false;

    private void Start()
    {
        // Start the countdown to destroy the bomb
        Invoke("Explode", destroyTime);
        FindObjectOfType<AudioManager>().Play("Bomb");
    }

    private void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;

            // Instantiate the explosion animation prefab
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Apply damage to enemies within the explosion radius using 2D physics
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponData.radius);
            foreach (Collider2D col in colliders)
            {
                // Assuming enemies have a specific tag, e.g., "Enemy"
                if (col.CompareTag("Enemy"))
                {
                    // Apply damage to enemies within the radius
                    EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(weaponData.damage, transform.position);
                    }
                }
            }

            // Destroy the bomb GameObject
            Destroy(gameObject);
        }
    }
}
