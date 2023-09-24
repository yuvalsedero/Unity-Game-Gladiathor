using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrident : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    GameObject target;
    bool canShoot = true;

    // public Animator anim;
    void Start()
    {

    }
    void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            //Coroutine for delay between shooting
            StartCoroutine("AllowToShoot");
            //array with enemies
            //you can put in start, iff all enemies are in the level at beginn (will be not spawn later)
            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
            // Animator anim = GetComponent<EnemyHealth>().animator;
            if (allTargets != null)
            {
                target = allTargets[0];
                //look for the closest
                foreach (GameObject tmpTarget in allTargets)
                {
                    if (Vector2.Distance(transform.position, tmpTarget.transform.position) < Vector2.Distance(transform.position, target.transform.position))
                    {
                        target = tmpTarget;
                    }
                }
                bool enemyAnimator = target.GetComponent<Animator>().GetBool("Dead");
                //shoot if the closest is in the fire range
                if (Vector2.Distance(transform.position, target.transform.position) < weaponData.range && enemyAnimator == false)
                {
                    Fire();
                }
            }
        }
    }

    void Fire()
    {
        Vector2 direction = target.transform.position - transform.position;
        //link to spawned arrow, you dont need it, if the arrow has own moving script
        GameObject tmpTrident = Instantiate(weaponData.prefab, transform.position, transform.rotation);
        tmpTrident.transform.rotation = Quaternion.LookRotation(target.transform.position);
        tmpTrident.transform.right = direction;
        tmpTrident.GetComponent<Rigidbody2D>().velocity = direction.normalized * weaponData.speed;
        // tmpTrident.transform.rotation = Quaternion.LookRotation(direction);
        
        
    }

    IEnumerator AllowToShoot()
    {
        yield return new WaitForSeconds(weaponData.cooldownDuration);
        canShoot = true;
    }
}
