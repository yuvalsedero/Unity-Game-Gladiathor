using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineMagnetManager : MonoBehaviour
{
    private Transform playerTransform;
    public float attractionSpeed = 10.0f;
    private List<GameObject> attractedWineVases = new List<GameObject>();

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Attract wine vases only if the list is not empty
        if (attractedWineVases.Count > 0)
        {
            AttractWineVases();
        }
    }

    public void AttractWineVases()
    {
        attractedWineVases.RemoveAll(wineVase => wineVase == null);

        foreach (GameObject wineVase in attractedWineVases)
        {
            Vector3 directionToPlayer = playerTransform.position - wineVase.transform.position;
            Vector3 moveDirection = directionToPlayer.normalized;

            float distance = directionToPlayer.magnitude;

            if (distance > 0.1f) // To prevent very close objects from shaking
            {
                wineVase.transform.position += moveDirection * attractionSpeed * Time.deltaTime;
            }
        }
    }

    public void StartWineAttraction(List<GameObject> wineVases)
    {
        attractedWineVases = wineVases;
    }
}
