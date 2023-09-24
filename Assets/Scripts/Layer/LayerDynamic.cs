using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDynamic : MonoBehaviour
{
    float yPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yPosition= gameObject.transform.position.y * -100;
        gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)yPosition;
    }
}
