using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    [SerializeField] Button _back;

   public GameObject audiomanagerGO;

    private AudioManager audioManager;
    void Start()
    {
      
      audioManager = audiomanagerGO.GetComponent<AudioManager>();

      // DontDestroyOnLoad(audiomanagerGO);  
      
        _back.onClick.AddListener(Back);
    }

   private void Back()
   {
      if (audioManager!= null)
   {

      audioManager.Play("Ui Menu Click");
   }
      ScenesManager.Instance.LoadMainMenu();
   }
   
   public void Hover()
   {
        if (audioManager!= null)
   {
      
      audioManager.Play("Ui Menu Hover");
   }
   }
}
