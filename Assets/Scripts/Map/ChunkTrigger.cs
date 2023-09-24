using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapManager mc;
    public GameObject targetMap;
    // Start is called before the first frame update
    void Start()
    {
        mc = FindObjectOfType<MapManager>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(mc.currentChunk== targetMap)
            {
                mc.currentChunk = null;
            }
        }   
    }
}
