using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScriptPlayer : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    GameObject target;
    bool canShoot = true;
    // public Animator anim;
    void Update ()
    {
        if (canShoot) {
            canShoot = false;
            //Coroutine for delay between shooting
            StartCoroutine("AllowToShoot");
            //array with enemies
            //you can put in start, iff all enemies are in the level at beginn (will be not spawn later)
            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
            if (allTargets != null && allTargets.Length > 0)
            {
            target = allTargets[0];
            // look for the closest
                foreach (GameObject tmpTarget in allTargets)
            {
                if (Vector2.Distance(transform.position, tmpTarget.transform.position) < Vector2.Distance(transform.position, target.transform.position))
                {
                    target = tmpTarget;
                }
            }
                // Animator targetAnimation = target.GetComponent<Stats>().animator;
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
    Vector2 initialDirection = target.transform.position - transform.position;
    GameObject tmpBoomerang = Instantiate(weaponData.prefab, transform.position, Quaternion.identity);
    BoomerangScriptWeapon boomerangScript = tmpBoomerang.GetComponent<BoomerangScriptWeapon>();
    boomerangScript.MoveBoomerang(initialDirection); // Call the MoveBoomerang function
    
    // Start the boomerang's movement
    // tmpBoomerang.transform.right = initialDirection;
    // tmpBoomerang.GetComponent<Rigidbody2D>().velocity = initialDirection.normalized * weaponData.speed;
    
}


   
    IEnumerator AllowToShoot ()
    {
        yield return new WaitForSeconds(weaponData.cooldownDuration);
        canShoot = true;
    }

    
}
