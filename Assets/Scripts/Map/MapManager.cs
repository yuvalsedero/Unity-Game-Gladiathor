using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    PlayerMovement pm;
    public GameObject currentChunk;


    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; //must be greater then the length and with of the tilemap (80)
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }
    
    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }
        if(pm.moveDir.x > 0 && pm.moveDir.y == 0) // right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("right").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("right").position;    
                SpawnChunk();       
                     
            }
        }
        else if(pm.moveDir.x < 0 && pm.moveDir.y == 0) // left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("left").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("left").position;  
                SpawnChunk();       
            }
        }
        else if(pm.moveDir.x == 0 && pm.moveDir.y > 0) // up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("up").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("up").position; 
                SpawnChunk();                 
            }
        }
        else if(pm.moveDir.x == 0 && pm.moveDir.y < 0) // down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("down").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("down").position;   
                SpawnChunk();               
            }
        }
        else if(pm.moveDir.x > 0 && pm.moveDir.y > 0) // right up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("right up").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("right up").position;     
                SpawnChunk();             
            }
        }
        else if(pm.moveDir.x > 0 && pm.moveDir.y < 0) // right down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("right down").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("right down").position;     
                SpawnChunk();             
            }
        }
        else if(pm.moveDir.x < 0 && pm.moveDir.y > 0) // left up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("left up").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("left up").position;   
                SpawnChunk();              
            }
        }
        else if(pm.moveDir.x < 0 && pm.moveDir.y < 0) // left down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("left down").position,checkerRadius,terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("left down").position; 
                SpawnChunk();                 
            }
        }
    }


    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }
    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown <= 0)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
