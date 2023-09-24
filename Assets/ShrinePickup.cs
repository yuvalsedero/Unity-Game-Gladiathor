using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShrinePickup : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    public GameObject Speed;
    public GameObject Hp;
    public GameObject Dash;
    public GameObject damagePopup;
     private PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("PickUpShrine");
            if (Speed != null)
            {
                characterStats.speed += 1f;
                GameObject damagepopupValue = Instantiate(damagePopup, transform.position, Quaternion.identity);
                damagepopupValue.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Speed Up";
                Destroy(gameObject);
            }

            if (Hp != null)
            {
                characterStats.maxHealth += 10f;
                GameObject damagepopupValue = Instantiate(damagePopup, transform.position, Quaternion.identity);
                damagepopupValue.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Max Hp Up";
                Destroy(gameObject);
            }

            if (Dash != null)
            {
                characterStats.dashAmount += 1f;
                characterStats.dashCooldown -= 0.5f;
                playerMovement.dashCounter = characterStats.dashAmount;
                GameObject damagepopupValue = Instantiate(damagePopup, transform.position, Quaternion.identity);
                damagepopupValue.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Dash Up";
                Destroy(gameObject);
            }
        }
    }
}
