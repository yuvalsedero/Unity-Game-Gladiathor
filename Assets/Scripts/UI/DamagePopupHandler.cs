using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
        // transform.localPosition+= new Vector3(0)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
