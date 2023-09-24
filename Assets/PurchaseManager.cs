using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PurchaseManager : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    public Text armorUpPrice;
    public Text speedUpPrice;
    public Text maxHpUpPrice;
    public Text DashUpPrice;
    private int gold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gold = PlayerPrefs.GetInt("Gold"); // Correctly update the class field
    }

    private void BuyDamageUp()
    {

    }
}
