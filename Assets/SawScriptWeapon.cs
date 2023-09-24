using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScriptWeapon : MonoBehaviour
{
    public float rotationSpeed = 20.0f; // Adjust this value in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Saw Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        // Apply rotation on the z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
