using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    private float timeSinceLastSpawn;

    private void Start()
    {
        timeSinceLastSpawn = weaponData.cooldownDuration;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= weaponData.cooldownDuration)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponData.range);

            // Initialize variables to keep track of closest enemy
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);

                    // Update closest enemy if the current enemy is closer
                    if (distanceToEnemy < closestDistance)
                    {
                        closestEnemy = col.transform;
                        closestDistance = distanceToEnemy;
                    }
                }
            }

            // Spawn lightning on the closest enemy if one was found
            if (closestEnemy != null)
            {
                Vector3 spawnOffset = Vector3.up * 1.5f; // Adjust this offset as needed
                SpawnLightning(closestEnemy.position + spawnOffset);
                timeSinceLastSpawn = 0.0f;
            }
        }
    }

    void SpawnLightning(Vector3 spawnPosition)
    {
        GameObject clonedLightning = Instantiate(weaponData.prefab, spawnPosition, Quaternion.identity);
        Destroy(clonedLightning, 0.6f);
    }
}
