using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    
    public GameObject upgradeCard;
    public GameObject upgradeCard1;
    public GameObject upgradeCard2;
    private UpgradeCard cardScript;
    private UpgradeCard cardScript1;
    private UpgradeCard cardScript2;
    public GameObject levelupMenu;
    public GameObject playerGO;
    public GameObject eagle;
    private WeaponManager weaponManager;
    
    // Start is called before the first frame update
    void Start()
    {
        levelupMenu.SetActive(false);
        cardScript = upgradeCard.GetComponentInChildren<UpgradeCard>();
        cardScript1 = upgradeCard1.GetComponentInChildren<UpgradeCard>();
        cardScript2 = upgradeCard2.GetComponentInChildren<UpgradeCard>();

        weaponManager = playerGO.GetComponent<WeaponManager>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevelUpMenu()
    {
        levelupMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ActivateWeapon(int card)
    {
        
        if ((cardScript.infoHeader.text == "Rock" && card == 0 )|| (cardScript1.infoHeader.text == "Rock" && card == 1)|| (cardScript2.infoHeader.text == "Rock" && card == 2))
        {
                weaponManager.UpgradeRock();        
        }
        if ((cardScript.infoHeader.text == "Trident" && card == 0 )|| (cardScript1.infoHeader.text == "Trident" && card == 1)|| (cardScript2.infoHeader.text == "Trident" && card == 2))
        {
            ProjectileTrident tridentScript = playerGO.GetComponent<ProjectileTrident>();
             if (tridentScript != null && !tridentScript.enabled)
            {

            weaponManager.ActivateTrident();
            }
            else 
            {
                weaponManager.UpgradeTrident();
            }
        }
        if ((cardScript.infoHeader.text == "Boomerang" && card == 0 )|| (cardScript1.infoHeader.text == "Boomerang" && card == 1)|| (cardScript2.infoHeader.text == "Boomerang" && card == 2))
        {
            BoomerangScriptPlayer boomerangScript = playerGO.GetComponent<BoomerangScriptPlayer>();
             if (boomerangScript != null && !boomerangScript.enabled)
            {

            weaponManager.ActivateBoomerang();
            }
            else 
            {
                weaponManager.UpgradeBoomerang();
            }
        }
        if ((cardScript.infoHeader.text == "Saw" && card == 0 )|| (cardScript1.infoHeader.text == "Saw" && card == 1)|| (cardScript2.infoHeader.text == "Saw" && card == 2))
        {
            SawScriptPlayer sawScript = playerGO.GetComponent<SawScriptPlayer>();
             if (sawScript != null && !sawScript.enabled)
            {

            weaponManager.ActivateSaw();
            }
            else 
            {
                weaponManager.UpgradeSaw();
            }
        }
        else if ((cardScript.infoHeader.text == "Bomb" && card == 0 )|| (cardScript1.infoHeader.text == "Bomb" && card == 1)|| (cardScript2.infoHeader.text == "Bomb" && card == 2))
        {
            BombWeaponScript bombScript = playerGO.GetComponent<BombWeaponScript>();
             if (bombScript != null && !bombScript.enabled)
            {

            weaponManager.ActivateBomb();
            }
            else 
            {
                weaponManager.UpgradeBomb();
            }
        }
        else if ((cardScript.infoHeader.text == "Eagle" && card == 0 )|| (cardScript1.infoHeader.text == "Eagle" && card == 1)|| (cardScript2.infoHeader.text == "Eagle" && card == 2))
        {
            EagleScript eagleScript = eagle.GetComponent<EagleScript>();
             if (eagleScript != null && !eagle.activeInHierarchy)
            {
                Debug.Log("asdw");
            weaponManager.ActivateEagle();
            }
            else 
            {
                weaponManager.UpgradeEagle();
            }
        }
        else if ((cardScript.infoHeader.text == "Lightning" && card == 0 )|| (cardScript1.infoHeader.text == "Lightning" && card == 1)|| (cardScript2.infoHeader.text == "Lightning" && card == 2))
        {
            LightningStrike lightningScript = playerGO.GetComponent<LightningStrike>();
             if (lightningScript != null && !lightningScript.enabled)
            {

            weaponManager.ActivateLightningStrike();
            }
            else 
            {
                weaponManager.UpgradeLightningStrike();
            }
        }
        levelupMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
}
