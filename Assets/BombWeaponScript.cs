using UnityEngine;

public class BombWeaponScript : MonoBehaviour

{
    public WeaponScriptableObject weaponData;
    private Transform playerTransform;
    private float timeSinceLastSpawn;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastSpawn = weaponData.cooldownDuration;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= weaponData.cooldownDuration)
        {
            SpawnBomb();
            timeSinceLastSpawn = 0.0f;
        }
    }

    private void SpawnBomb()
    {
        Vector3 spawnPosition = playerTransform.position;
        GameObject clonedbomb = Instantiate(weaponData.prefab, spawnPosition, Quaternion.identity);
        Destroy(clonedbomb, 2.3f);
    }


    

   
}
