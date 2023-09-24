using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    private int gold;

    void Update()
    {
        gold = PlayerPrefs.GetInt("Gold"); // Correctly update the class field
        goldText.text = gold.ToString();
    }
}
