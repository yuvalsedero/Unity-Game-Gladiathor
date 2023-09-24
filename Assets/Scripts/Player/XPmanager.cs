using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPmanager : MonoBehaviour
{
   public static XPmanager Instance { get; private set; }

    public List<WeaponScriptableObject> selectedWeapons = new List<WeaponScriptableObject>();
    public StatsScriptableObject characterStats;
    public GameObject wineBarUI;
    public Slider slider;
    public GameObject gameManager;
    private LevelUpMenu levelupMenu;
    LevelText levelTextComponent;
    public UpgradeCard[] upgradeCards;
    public AnimationCurve maxWineScalingCurve; // Add a reference to your animation curve here
    private float previousLevelValue; // Store the previous curve value
    public TextMeshProUGUI goldText;
    private int gold;
    // Start is called before the first frame update
    void Start()
{
    Instance = this; // Set the instance reference
    gold = PlayerPrefs.GetInt("Gold"); // Correctly update the class field
    levelupMenu = gameManager.GetComponent<LevelUpMenu>();
    characterStats.level = 1f;
    levelTextComponent = FindObjectOfType<LevelText>();

    // Calculate the initial maxWine value based on the animation curve
    characterStats.maxWine = CalculateScaledMaxWine();

    // Load the selected weapons list
    selectedWeapons.AddRange(UpgradeCard.availableWeapons);

    // Refresh the upgrade cards
    RefreshUpgradeCards();

    // Set the slider value and initial wine
    slider.value = CalculateWine();
    characterStats.wine = 0;
}


    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateWine();
        CheckWine();
        levelTextComponent.ChangeText(characterStats.level.ToString());
        goldText.text = gold.ToString();
        
    }

    void LevelUp()
    {
        characterStats.level ++;
        CalculateXPToProgress();
        // characterStats.maxWine = characterStats.maxWine * 1.2f;
        foreach (UpgradeCard card in upgradeCards)
        {
            card.ReshuffleIfEmpty();
            card.RefreshCardData(); // Refresh card data as well
        }
        RefreshUpgradeCards();
        levelupMenu.LoadLevelUpMenu();
    }

    float CalculateXPToProgress()
    {
        // Calculate the next max wine value using the curve value for the next level
        float nextMaxWine = CalculateScaledMaxWine();

        // Calculate the XP required to progress to the next level
        float requiredXPToProgress = nextMaxWine - characterStats.maxWine;

        // Update the max wine value for the next level
        characterStats.maxWine = nextMaxWine;

        return requiredXPToProgress;
    }


    float CalculateScaledMaxWine()
    {
        // Evaluate the animation curve using the character's level
        float curveValue = maxWineScalingCurve.Evaluate(characterStats.level);

        // Calculate the maxWine (required XP to level up) using the curve value
        float maxWine = 1f; // Set the starting maxWine value

        return maxWine * curveValue;
    }



    public void pickUp()
    {
        characterStats.wine += 10;
       

        // Update the PlayerPrefs value for gold
        IncreaseGold(1);

    }
    public void pickUpBigWine()
    {
        characterStats.wine += 300;
    }

    public void IncreaseGold(int amount)
    {
        gold += amount;
        PlayerPrefs.SetInt("Gold", gold);
    }

    public void DecreaseGold(int amount)
    {
        gold -= amount;
        PlayerPrefs.SetInt("Gold", gold);
    }

    private void CheckWine()
    {
        float maxWine = CalculateScaledMaxWine(); // Calculate maxWine for the current level

        if (characterStats.wine >= maxWine)
        {
            characterStats.wine -= maxWine; // Deduct required XP
            LevelUp();
            maxWine = CalculateScaledMaxWine(); // Recalculate maxWine after leveling up
        }

        if (characterStats.wine > maxWine)
        {
            characterStats.wine = maxWine; // Cap wine at the current maxWine value
        }

        if (characterStats.wine < 0)
        {
            characterStats.wine = 0;
        }
    }


    float CalculateWine()
    {
        return characterStats.wine / characterStats.maxWine;
    }

    void RefreshUpgradeCards()
{
    foreach (UpgradeCard card in upgradeCards)
    {
        card.RefreshCardData();
    }
}
}
