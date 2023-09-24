using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;
    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = weaponData.cooldownDuration;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f) // once the cooldown is 0 - attack
        {
            Attack();
        }
    }
    void Attack() {
        currentCooldown = weaponData.cooldownDuration;
    }
}
