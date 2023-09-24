using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorForWeapons : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && spriteRenderer.sortingLayerName == "AboveFloor")
            {
            if (!other.CompareTag("Enemy") && !other.CompareTag("Player") && !other.isTrigger)
            {
                Debug.Log("Destroying projectile.");
                Destroy(gameObject); // Destroy the projectile
            }
            
        }
    }
}
