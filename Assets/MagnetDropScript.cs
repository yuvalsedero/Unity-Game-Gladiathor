using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDropScript : MonoBehaviour
{
    // private bool isAttracting = false;
    private bool magnetPickedUp = false;
    private WineMagnetManager wineAttractionScript;

    private void Start()
    {
        wineAttractionScript = FindObjectOfType<WineMagnetManager>();
    }

    private void Attractwines()
    {
       
            GameObject[] wineVases = GameObject.FindGameObjectsWithTag("Wine");
            List<GameObject> wineVaseList = new List<GameObject>(wineVases);
            wineAttractionScript.StartWineAttraction(wineVaseList);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!magnetPickedUp)
            {
                Attractwines();
                magnetPickedUp = true;
                // Here you could play a sound or particle effect to indicate attraction

                // Destroy the magnet GameObject
                Destroy(gameObject);
            }
        }
    }
}
