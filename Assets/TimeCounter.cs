using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private float startTime;
    private float elapsedTime;
    public float minutes;
    public float seconds;
    private int dungeonWinsCounter; // Changed to int
    private int forestWinsCounter;  // Changed to int
    private bool isFinish;
    public GameObject finishGameMenu;
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        PlayerPrefs.SetInt("EnemiesKilledInstance", 0);
        finishGameMenu.SetActive(false);
        isFinish = false;
        startTime = Time.time;
        dungeonWinsCounter = 0; // Initialize the counters
        forestWinsCounter = 0;
         if (!PlayerPrefs.HasKey("DungeonWinsCount"))
        {
            PlayerPrefs.SetInt("DungeonWinsCount", 0);
        }

        if (!PlayerPrefs.HasKey("ForestWinsCount"))
        {
            PlayerPrefs.SetInt("ForestWinsCount", 0);
        }
        
        dungeonWinsCounter = PlayerPrefs.GetInt("DungeonWinsCount");
        forestWinsCounter = PlayerPrefs.GetInt("ForestWinsCount");
    }

    void Update()
    {
        elapsedTime = Time.time - startTime;
        UpdateCounterText();

        if (!isFinish &&( minutes >= 5f || (playerMovement.IsDead() && playerMovement != null))) // Check if not already won before and reached time condition
        {
            isFinish = true;
            if (playerMovement.IsDead() && playerMovement != null)
            {
                StartCoroutine(FinishGame());
            }
            else
            {
                IncrementWinCounter(); // Call the function to increment the win counter
                StartCoroutine(FinishGame());
            }
            
        }
            
    }
   
    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(1f); // Adjust this to match your animation duration
        finishGameMenu.SetActive(true);
        yield return new WaitForSeconds(1f); // Adjust this to match your animation duration
        Time.timeScale = 0f;
    }
    void IncrementWinCounter()
    {
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            dungeonWinsCounter += 1;
            PlayerPrefs.SetInt("DungeonWinsCount", dungeonWinsCounter);
        }
        else if (SceneManager.GetActiveScene().name == "Forest")
        {
            forestWinsCounter += 1;
            PlayerPrefs.SetInt("ForestWinsCount", forestWinsCounter);
        }
    }

    void UpdateCounterText()
    {
        minutes = Mathf.Floor(elapsedTime / 60);
        seconds = elapsedTime % 60;
        string strMinutes = minutes.ToString("00");
        string strSeconds = seconds.ToString("00");
        counterText.text = $"{strMinutes}:{strSeconds}";
    }
}

