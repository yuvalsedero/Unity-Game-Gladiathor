using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float destroyTime = 2.0f; // Time after which the bomb will be destroyed
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("destroyExplosion", destroyTime);
    }

    // Update is called once per frame
    private void destroyExplosion()
    {
        Destroy(gameObject);
    }
}
