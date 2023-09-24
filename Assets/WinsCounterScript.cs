using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinsCounterScript : MonoBehaviour
{
    public TextMeshProUGUI dungeonWinsText;
    public TextMeshProUGUI forestWinsText;
    public TextMeshProUGUI EnemiesKilledTotalText;

    void Start()
    {
        EnemiesKilledTotalText.text = "Total Enemies Killed: "+PlayerPrefs.GetInt("EnemiesKilledTotal", 0);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateWinCounters();
    }

    void UpdateWinCounters()
    {
        dungeonWinsText.text = "Wins: "+PlayerPrefs.GetInt("DungeonWinsCount", 0);
        forestWinsText.text = "Wins: "+PlayerPrefs.GetInt("ForestWinsCount", 0);
        EnemiesKilledTotalText.text = "Total Enemies Killed: "+PlayerPrefs.GetInt("EnemiesKilledTotal", 0);
    }
}
