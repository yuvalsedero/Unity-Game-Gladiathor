using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChooseLevelMenu : MonoBehaviour
{
    [SerializeField] Button _back;
    [SerializeField] Button _Dungeon;
    [SerializeField] Button _Forest;
   public GameObject audiomanagerGO;

    private AudioManager audioManager;
    void Start()
    {
      
      audioManager = audiomanagerGO.GetComponent<AudioManager>();

      // DontDestroyOnLoad(audiomanagerGO);  
      
        _back.onClick.AddListener(Back);
        _Dungeon.onClick.AddListener(LoadDungeon);
        _Forest.onClick.AddListener(LoadForest);
    }

   private void Back()
   {
      if (audioManager!= null)
   {

      audioManager.Play("Ui Menu Click");
   }
      ScenesManager.Instance.LoadMainMenu();
   }
   private void LoadDungeon()
   {
        if (audioManager!= null)
   {
      
      audioManager.Play("Ui Menu Click");
      ScenesManager.Instance.LoadScene(ScenesManager.Scene.Dungeon);
   }
   }
   private void LoadForest()
   {
        if (audioManager!= null)
   {
      
      audioManager.Play("Ui Menu Click");
   }
      ScenesManager.Instance.LoadScene(ScenesManager.Scene.Forest);
   }
   public void Hover()
   {
        if (audioManager!= null)
   {
      
      audioManager.Play("Ui Menu Hover");
   }
   }
}
