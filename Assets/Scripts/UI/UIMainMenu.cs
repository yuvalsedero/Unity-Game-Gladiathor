using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Button _newGame;
    [SerializeField] Button _settings;
    [SerializeField] Button _exit;
    public GameObject audiomanagerGO;

    private AudioManager audioManager;

    private void Start()
    {
        // Initialize audioManager
        audioManager = audiomanagerGO.GetComponent<AudioManager>();
        // Make sure the AudioManager object persists across scenes
        // DontDestroyOnLoad(audiomanagerGO);  
        // Add listeners to buttons
        _newGame.onClick.AddListener(GoToLevelChooser);
        _settings.onClick.AddListener(GoToSettings);
        _exit.onClick.AddListener(ExitGame);
    }

    private void GoToLevelChooser()
    {
        audioManager.Play("Ui Menu Click");
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.LevelChooser);
    }

    private void GoToSettings()
    {
        audioManager.Play("Ui Menu Click");
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Settings);
    }

    private void ExitGame()
    {
        audioManager.Play("Ui Menu Click");
        ScenesManager.Instance.QuitGame();
    }
    public void Hover()
   {
      audioManager.Play("Ui Menu Hover");
   }

}
