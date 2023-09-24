using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponScriptableObject tridentStats;
    public WeaponScriptableObject bombStats;
    public WeaponScriptableObject lightningStats;
    public WeaponScriptableObject eagleStats;
    public WeaponScriptableObject sawStats;
    public WeaponScriptableObject rockStats;
    public WeaponScriptableObject boomerangStats;
    public StatsScriptableObject characterStats;
    public GameObject eagleGameObject;
    private GameObject eagleSpawn;
    private LightningStrike lightningStrike;
    private BoomerangScriptPlayer boomerang;
    // private EagleScript eagle;
    private ProjectileTrident tridentWeapon;
    private ProjectileRock rock;
    private SawScriptPlayer sawWeapon;
    private BombWeaponScript bomb;
    private bool hasEagleSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        lightningStrike = GetComponent<LightningStrike>();
        boomerang = GetComponent<BoomerangScriptPlayer>();
        // eagle = GetComponent<EagleScript>();
        tridentWeapon = GetComponent<ProjectileTrident>();
        sawWeapon = GetComponent<SawScriptPlayer>();
        bomb = GetComponent<BombWeaponScript>();
        rock = GetComponent<ProjectileRock>();
        rock.enabled = true;
        tridentWeapon.enabled = false;
        boomerang.enabled = false;
        bomb.enabled = false;
        lightningStrike.enabled = false;
        rockStats.level = 1f;
        rockStats.pierce = false;
        rockStats.ricochet = 0f;
        rockStats.damage = 15;
        boomerangStats.level = 0f;
        tridentStats.level = 0f;
        bombStats.level = 0f;
        eagleStats.level = 0f;
        lightningStats.level = 0f;
        sawStats.level = 0f;
        // eagleSpawn.SetActive(false);
    }

    
    public void UpgradeRock()
    {
        rockStats.level ++;
        rockStats.pierce = true;
        rockStats.damage += 5;
        rockStats.ricochet +=1;

    }
    public void ActivateTrident()
    {
        tridentWeapon.enabled = true;
        tridentStats.level ++;
        tridentStats.damage = 10f;
        tridentStats.speed = 15f;
    }
    public void UpgradeTrident()
    {
        tridentStats.level ++;
        tridentStats.damage += 5;
        tridentStats.speed += 1;

    }
    public void ActivateBoomerang()
    {
        boomerang.enabled = true;
        boomerangStats.level ++;
        boomerangStats.damage = 5f;
        boomerangStats.cooldownDuration = 3f;
    }
    public void UpgradeBoomerang()
    {
        boomerangStats.level ++;
        boomerangStats.damage += 5;
        boomerangStats.cooldownDuration -= 0.3f;

    }
    public void ActivateSaw()
    {
        sawWeapon.enabled = true;
        sawStats.level ++;
        sawStats.damage = 15f;
        sawStats.cooldownDuration = 3f;
    }
    public void UpgradeSaw()
    {
        sawStats.level ++;
        sawStats.damage += 5;
        sawStats.cooldownDuration -= 0.3f;

    }
    public void ActivateBomb()
    {
        bomb.enabled = true;
        bombStats.level ++;
        bombStats.damage = 15f;
        bombStats.radius = 3f;
    }
    public void UpgradeBomb()
    {
        bombStats.level ++;
        bombStats.damage += 5;
        bombStats.radius += 1;

    }
    public void ActivateEagle()
    {  
        if (!hasEagleSpawned)
        {
            eagleStats.damage = 5f;
            eagleStats.speed = 2f;
            eagleStats.level ++;
            eagleGameObject.SetActive(true);
            eagleGameObject.transform.position = transform.position;
            hasEagleSpawned = true;
            
        }
    }
    public void UpgradeEagle()
    {
        eagleStats.level ++;
        eagleStats.damage += 5;
        eagleStats.speed += 1;

    }
    public void ActivateLightningStrike()
    {
        lightningStrike.enabled = true;
        lightningStats.damage = 10f;
        lightningStats.radius = 4f;   
        lightningStats.level ++;
    }
    public void UpgradeLightningStrike()
    {
        lightningStats.damage += 5;
        lightningStats.radius += 1;
        lightningStats.level ++;
    }
}

