using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{  
    
    private float timeToSpawn;
    public float defaultSpawnTime = 5f;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public StatsScriptableObject characterStats; // Reference to your character's stats
    public AnimationCurve SpawnScalingCurve; // Add a reference to your animation curve here

    // Adjust these percentages according to your requirements
    private float[] spawnTimeReductions = { 0.0f, 0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.2f };

    void Start()
    {
        
        timeToSpawn = defaultSpawnTime;
    }

    void Update()
    {
        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn <= 0)
        {
            timeToSpawn = 0;

            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            TimeCounter timeCounter = FindObjectOfType<TimeCounter>();
            
            if ( timeCounter.minutes >= 2f)//spawn only all if timer is more then 1 minute
            {
            int randEnemyPoint = Random.Range(0, enemyPrefabs.Length);
            var enemy = Instantiate(enemyPrefabs[randEnemyPoint], spawnPoints[randSpawnPoint].position, transform.rotation);
            int nameofenemy = Random.Range(0, 999999999);
            enemy.name = (nameofenemy.ToString("F0"));
            }
            else if ( timeCounter.minutes >= 1f)//spawn only Orc, Sorcerer if timer is less then 2 minute
            {
            int randEnemyPoint = Random.Range(0,2);
            var enemy = Instantiate(enemyPrefabs[randEnemyPoint], spawnPoints[randSpawnPoint].position, transform.rotation);
            int nameofenemy = Random.Range(0, 999999999);
            enemy.name = (nameofenemy.ToString("F0"));
            }
            else //spawn only orc if timer is less then 1 minute
            {
            var enemy = Instantiate(enemyPrefabs[0], spawnPoints[randSpawnPoint].position, transform.rotation);
             int nameofenemy = Random.Range(0, 999999999);
            enemy.name = (nameofenemy.ToString("F0"));

            SpriteRenderer rend = enemy.GetComponent<SpriteRenderer>();  // color Randomizer    
            Color currentColor = rend.color;          
            Color alteredColor = CalculateColor(currentColor);
            rend.color = alteredColor; 
            }
            CalculateNextSpawnTime(); // Calculate the spawn time for the next spawn

        
    }
    }


   void CalculateNextSpawnTime()
{
    float currentLevel = characterStats.level;

    float spawnTimeMultiplier = SpawnScalingCurve.Evaluate(currentLevel);

    float adjustedSpawnTime =  spawnTimeMultiplier;
    
    timeToSpawn = adjustedSpawnTime;
}
private Color CalculateColor(Color currentColor)
{
            float colorVariation = 0.2f; // You can adjust this value
            currentColor.r += Random.Range(-colorVariation, colorVariation);
            currentColor.g += Random.Range(-colorVariation, colorVariation);
            currentColor.b += Random.Range(-colorVariation, colorVariation);

            // Ensure the color channels stay within the [0, 1] range
            currentColor.r = Mathf.Clamp01(currentColor.r);
            currentColor.g = Mathf.Clamp01(currentColor.g);
            currentColor.b = Mathf.Clamp01(currentColor.b);
            return currentColor;
}


}
