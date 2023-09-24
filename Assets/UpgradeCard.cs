using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public static List<WeaponScriptableObject> availableWeapons = new List<WeaponScriptableObject>();
    // private static int nextWeaponIndex = 0;
    public TextMeshProUGUI infoHeader;
    public TextMeshProUGUI infoDescription;
    public Image infoImage;

    void Start()
    {
        if (availableWeapons.Count == 0)
        {
            availableWeapons.AddRange(XPmanager.Instance.selectedWeapons);
            // Shuffle the list
            Shuffle(availableWeapons);
        }
        RefreshCardData();
    }
    public void ReshuffleIfEmpty()
    {
        if (availableWeapons.Count == 0)
        {
            availableWeapons.AddRange(XPmanager.Instance.selectedWeapons);
            Shuffle(availableWeapons);
        }
    }
    public void RefreshCardData()
    {
        // Check if there are ScriptableObjects in the list
        if (availableWeapons.Count > 0)
        {
            WeaponScriptableObject chosenWeapon = GetUniqueRandomWeapon();

            if (chosenWeapon != null)
            {
                // Populate UI elements with data from the chosen ScriptableObject
                infoHeader.text = chosenWeapon.header;
                infoImage.sprite = chosenWeapon.image;

                if (chosenWeapon.header == "Rock")
                {
                    infoDescription.text = "Level " + chosenWeapon.level + " -> "+ (chosenWeapon.level + 1) +"\nA Rock.\nDamage: " + chosenWeapon.damage + " Speed: " + chosenWeapon.speed + " Ricochet: " + chosenWeapon.ricochet + 
                    " Cooldown: " + chosenWeapon.cooldownDuration + "\nOn Upgrade: Damage + 5, Ricochet + 1";
                }
                if (chosenWeapon.header == "Boomerang")
                {
                    infoDescription.text = "Level " + chosenWeapon.level + " -> "+ (chosenWeapon.level + 1) +"\nBoomerang, will he come back to me?.\nDamage: " + chosenWeapon.damage +
                    " Cooldown: " + chosenWeapon.cooldownDuration + "\nOn Upgrade: Damage + 5, Cooldown - 0.3";
                }
                if (chosenWeapon.header == "Bomb")
                {
                    infoDescription.text = "Level " + chosenWeapon.level + " -> "+ (chosenWeapon.level + 1) +"\nBomb that deals Area of damage after it explodes.\nDamage: " + chosenWeapon.damage + " Radius: " + chosenWeapon.radius +
                                      " Cooldown: " + chosenWeapon.cooldownDuration + "\nOn Upgrade: Damage + 5, Radius + 1";
                }
                else if (chosenWeapon.header == "Eagle")
                {
                    infoDescription.text = "Level " + chosenWeapon.level + " -> "+ (chosenWeapon.level + 1) +"\nIndependent eagle that seeks out enemies and deals periodically damage.\nDamage: " + chosenWeapon.damage + " Speed: " + chosenWeapon.speed +
                                      "\nOn Upgrade: Damage + 5, Speed + 1";
                }
                else if (chosenWeapon.header == "Trident")
                {
                    infoDescription.text = "Level " + chosenWeapon.level + " -> "+ (chosenWeapon.level + 1) +"\nWeapon that pierces through enemies.\nDamage: " + chosenWeapon.damage + " Speed: " + chosenWeapon.speed +
                                      " Cooldown: " + chosenWeapon.cooldownDuration + "\nOn Upgrade: Damage + 5, Speed + 1";
                }
                else if (chosenWeapon.header == "Lightning")
                {
                    infoDescription.text = "Level " + chosenWeapon.level + " -> "+ (chosenWeapon.level + 1) +"\nLightning Strike that deals area of damage.\nDamage: " + chosenWeapon.damage + " Radius: " + chosenWeapon.radius +
                                      " Cooldown: " + chosenWeapon.cooldownDuration + "\nOn Upgrade: Damage + 5, Radius + 1";
                }
                else if (chosenWeapon.header == "Saw")
                {
                    infoDescription.text = "Level " + chosenWeapon.level + " -> "+ (chosenWeapon.level + 1) +"\nLobs a Saw that pierce.\nDamage: " + chosenWeapon.damage + 
                    " Cooldown: " + chosenWeapon.cooldownDuration + "\nOn Upgrade: Damage + 5, Cooldown - 0.3\nLevel 3 - Add Saw.";
                }
                // Handle other card-specific UI elements here
                  availableWeapons.Remove(chosenWeapon);

            }
        }
    }
    
  WeaponScriptableObject GetUniqueRandomWeapon()
    {
        if (availableWeapons.Count == 0)
        {
            // Refill the availableWeapons list if it's empty
            availableWeapons.AddRange(XPmanager.Instance.selectedWeapons);
        }

        int randomIndex = Random.Range(0, availableWeapons.Count);
        WeaponScriptableObject chosenWeapon = availableWeapons[randomIndex];

        return chosenWeapon;
    
    }
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}

