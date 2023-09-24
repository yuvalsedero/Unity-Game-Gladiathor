using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpDropScript : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
          FindObjectOfType<AudioManager>().Play("PickUpHp");
          other.GetComponent<PlayerHealth>().pickUpHp();
          Destroy(this.gameObject);
        }
    }
}
