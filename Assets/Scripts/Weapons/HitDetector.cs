using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    EnemyHealth enemy;
    private float ricochetCount= 0;
    private bool canDamage = true;
    GameObject player;
    GameObject target;
    private bool canRicochet = false;
    // Start is called before the first frame update
    void Start()
    {
        ricochetCount = weaponData.ricochet;
        player = GameObject.FindGameObjectWithTag("Player");
        if (weaponData.destroy)
        {
            Destroy(gameObject, weaponData.destroyAfter);
        }
        
    }

 void Update()
 {
    if (ricochetCount > 0)
    {
        canRicochet = true;
    }
    else
    {
        canRicochet = false;
    }
 }


void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.tag == "Enemy")
    {
        enemy = other.gameObject.GetComponent<EnemyHealth>();
        DealDamage(other.ClosestPoint(transform.position)); // Pass the point of contact
        if (weaponData.hitSound != null)
        {
            FindObjectOfType<AudioManager>().Play(weaponData.hitSound);
        }
        if (ricochetCount > 0 && canRicochet)
        {
            ricochetCount--;
            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
            if (allTargets != null && allTargets.Length > 0)
            {
                // Look for the closest target that is not the current one
                float minDistance = float.MaxValue;
                foreach (GameObject tmpTarget in allTargets)
                {
                    if (tmpTarget != other.gameObject) // Avoid hitting the same enemy
                    {
                        float distance = Vector2.Distance(transform.position, tmpTarget.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = tmpTarget;
                        }
                    }
                }

                if (target != null)
                {
                    bool enemyAnimator = target.GetComponent<Animator>().GetBool("Dead");
                    if (Vector2.Distance(transform.position, target.transform.position) < weaponData.range && !enemyAnimator)
                    {
                        Vector2 direction = target.transform.position - transform.position;
                        transform.right = direction;
                        GetComponent<Rigidbody2D>().velocity = direction.normalized * weaponData.speed;
                       
                    }
                }
                else
                {
                    
                    Destroy(gameObject); // No more eligible targets, destroy the object
                }
            }
            
        }
        else if (weaponData.ricochet != 0)
        {
            Destroy(gameObject); 
        }
    }
}




    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && canDamage)
        {
            enemy = other.gameObject.GetComponent<EnemyHealth>();
             DealDamage(other.ClosestPoint(transform.position)); // Pass the point of contact
        }
    }

    void DealDamage(Vector3 pointOfContact)
    {
        canDamage = false; // Set the flag to prevent further damage
        if (!weaponData.knockBack)
        {
            enemy.TakeDamage(weaponData.damage);
        }
        else
        {
            enemy.TakeDamage(weaponData.damage, pointOfContact);
        }

        if (!weaponData.pierce)
        {
            Destroy(gameObject);
        }
        else if (weaponData.pierceSound != null)
        {
            FindObjectOfType<AudioManager>().Play(weaponData.pierceSound);
        }

        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(weaponData.cooldownDuration);
        canDamage = true; // Reset the flag to allow damage again
    }
}

