using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishGameMenuScript : MonoBehaviour
{
    public StatsScriptableObject playerStats;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI WinsText;
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.IsDead())
        {
            titleText.text = "You Died!";
            LevelText.text = "Level: "+playerStats.level;
            enemiesKilledText.text = "Enemies Killed: "+PlayerPrefs.GetInt("EnemiesKilledInstance", 0);
            TimeCounter timeCounter = FindObjectOfType<TimeCounter>();
            WinsText.text ="Time survived: " + timeCounter.minutes.ToString("00") + ":" + timeCounter.seconds.ToString("00");
        }
        else
        {
        UpdateWinCounters();
        }
    }
    void UpdateWinCounters()
    {
        titleText.text = "You Won!";
        LevelText.text = "Level: "+playerStats.level;
        enemiesKilledText.text = "Enemies Killed: "+PlayerPrefs.GetInt("EnemiesKilledInstance", 0);
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            WinsText.text = "Dungeon Wins: " + (PlayerPrefs.GetInt("DungeonWinsCount", 0) - 1) + " -> " + PlayerPrefs.GetInt("DungeonWinsCount", 0);
        }
        else if (SceneManager.GetActiveScene().name == "Forest")
        {
            int winsCount = PlayerPrefs.GetInt("ForestWinsCount", 0);
            WinsText.text = "Forest Wins: " + (winsCount - 1) + " -> " + winsCount;
        }
        else
        {
            WinsText.text ="";
        }
    }
}
