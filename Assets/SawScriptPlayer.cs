using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScriptPlayer : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    bool canShoot = true;

    void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(ShootCooldown());
            Fire();
        }
    }

   void Fire()
{
    

    Vector2 forceDirection;
    Vector2 forceDirection2;

    if (GetComponent<PlayerMovement>().isFacingRight && weaponData.level < 3) // If player is moving right
    {
        GameObject tmpSaw = Instantiate(weaponData.prefab, transform.position, transform.rotation);
        Rigidbody2D sawRigidbody = tmpSaw.GetComponent<Rigidbody2D>();
        forceDirection = Quaternion.Euler(0, 0, -15) * transform.up;
        sawRigidbody.AddForce(forceDirection * weaponData.speed, ForceMode2D.Impulse);
    }
    else if (weaponData.level < 3)// If player is not moving right
    {
        GameObject tmpSaw = Instantiate(weaponData.prefab, transform.position, transform.rotation);
        Rigidbody2D sawRigidbody = tmpSaw.GetComponent<Rigidbody2D>();
        forceDirection = Quaternion.Euler(0, 0, 15) * transform.up;
        sawRigidbody.AddForce(forceDirection * weaponData.speed, ForceMode2D.Impulse);
    }
    else
    {
        GameObject tmpSaw1 = Instantiate(weaponData.prefab, transform.position, transform.rotation);
        GameObject tmpSaw2 = Instantiate(weaponData.prefab, transform.position, transform.rotation);
        Rigidbody2D sawRigidbody = tmpSaw1.GetComponent<Rigidbody2D>();
        Rigidbody2D sawRigidbody2 = tmpSaw2.GetComponent<Rigidbody2D>();
        forceDirection = Quaternion.Euler(0, 0, 15) * transform.up;
        forceDirection2 = Quaternion.Euler(0, 0, -15) * transform.up;
        sawRigidbody.AddForce(forceDirection * weaponData.speed, ForceMode2D.Impulse);
        sawRigidbody2.AddForce(forceDirection2 * weaponData.speed, ForceMode2D.Impulse);
    }

    
}


    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(weaponData.cooldownDuration);
        canShoot = true;
    }
}
