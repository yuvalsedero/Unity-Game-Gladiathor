// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyProjectileHitDetector : MonoBehaviour
// {
//     public WeaponScriptableObject weaponData;
//     PlayerMovement player;
//     bool doDmg = true;
//     // Start is called before the first frame update
//     void Start()
//     {
//         if (weaponData.destroy)
//         {
//             Destroy(gameObject, weaponData.destroyAfter);
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }
//     void OnTriggerEnter2D(Collider2D other)
//     {

//         if (other.gameObject.tag == ("Player"))
//         {
//             player = other.gameObject.GetComponent<PlayerMovement>();
//             player.TakeDamage(weaponData.damage);
//             if (!weaponData.pierce)
//                 Destroy(this.gameObject);
//         }
//     }
//     void OnTriggerStay2D(Collider2D other)
//     {
        
//         if (other.gameObject.tag == ("Player") && doDmg)
//         {
//             doDmg = false;
//             //Coroutine for delay between shooting
//             StartCoroutine("Delay");
//             player = other.gameObject.GetComponent<PlayerMovement>();
//             player.TakeDamage(weaponData.damage);
//             if (!weaponData.pierce)
//                 Destroy(this.gameObject);
//         }
//     }
//     IEnumerator Delay()
//     {
//         yield return new WaitForSeconds(weaponData.cooldownDuration);
//         doDmg = true;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHitDetector : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    PlayerMovement player;
    bool doDmg = true;

    // Additional variables for homing behavior
    // private bool isHoming = false;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        if (weaponData.destroy)
        {
            Destroy(gameObject, weaponData.destroyAfter);
        }
        FindObjectOfType<AudioManager>().Play("Sorcerer Energy Ball");

        
    }

    // Update is called once per frame
    void Update()
    {
        // If homing is enabled, adjust the projectile's direction towards the player
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            player = other.gameObject.GetComponent<PlayerMovement>();
            player.TakeDamage(weaponData.damage);
            if (!weaponData.pierce)
                Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player") && doDmg)
        {
            doDmg = false;
            //Coroutine for delay between shooting
            StartCoroutine("Delay");
            player = other.gameObject.GetComponent<PlayerMovement>();
            player.TakeDamage(weaponData.damage);
            if (!weaponData.pierce)
                Destroy(this.gameObject);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(weaponData.cooldownDuration);
        doDmg = true;
    }
}
