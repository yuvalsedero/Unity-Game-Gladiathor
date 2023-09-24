using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelText : MonoBehaviour
{
    public Text textComponent;
    // Start is called before the first frame update
    public void ChangeText(string newText)
    {
        if (textComponent != null)
        {
            textComponent.text = newText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
