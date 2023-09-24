using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineScript : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PickUpWine");
            other.GetComponent<XPmanager>().pickUp();
            Destroy(this.gameObject);
        }
    }
}
