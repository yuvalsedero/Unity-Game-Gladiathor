using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyTime = 2.0f;
    public GameObject explosionPrefab;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("LightningStrike");
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponData.radius);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
                
                if (enemyHealth != null)
                {
                    // Pass the appropriate parameters to the TakeDamage method
                    enemyHealth.TakeDamage(weaponData.damage, transform.position);
                }
            }
        }

        Invoke("DestroyLightning", destroyTime);
    }

    private void DestroyLightning()
    {
        Destroy(gameObject);
    }
}
