using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    // Start is called before the first frame update
    void Start()
    {
       float randomValue = Random.Range(0f, 1f);

        // Check if the random value is less than 0.3 (30% chance)
        if (randomValue < 0.3f)
        {
            // Perform the action for the event to happen
            SpawnProps();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
