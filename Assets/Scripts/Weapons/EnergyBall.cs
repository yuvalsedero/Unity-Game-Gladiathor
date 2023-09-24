using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    GameObject target;
    bool isCooldown = false;

    void Start()
    {
        // Destroy(weaponData.prefab, weaponData.destroyAfter);
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            Animator enemyAnimator = target.GetComponent<Animator>();
            bool isEnemyDead = enemyAnimator.GetBool("Dead");

            if (!isEnemyDead && Vector2.Distance(transform.position, target.transform.position) < weaponData.range)
            {
                if (!isCooldown)
                {
                    Fire();
                    StartCoroutine(StartCooldown());
                }
            }
        }
    }

    void Fire()
    {
        Vector2 direction = target.transform.position - transform.position;
        GameObject tmpRock = Instantiate(weaponData.prefab, transform.position, transform.rotation);
        tmpRock.transform.right = direction;
        tmpRock.GetComponent<Rigidbody2D>().velocity = direction.normalized * weaponData.speed;
    }

    IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(weaponData.cooldownDuration);
        isCooldown = false;
    }
}
