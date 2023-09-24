using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    private float currentHealth;
    public GameObject healthBarUI;
    public Slider slider;
    public GameObject WineVase; 
    public Animator animator;
    public float knockbackStr = 16;
    public float knockbackDelay = 0.5f;
    public GameObject damagePopup;
    public Rigidbody2D rb2d;
    public GameObject hpDropPrefab;
    public GameObject magnetDropPrefab;
    public GameObject bigWineDropPrefab;
    public GameObject timerGameObject;
    float timeCounterMinutes;
    private int enemiesKilledInstanceCounter;
    private int enemiesKilledTotalCounter;
    // Start is called before the first frame update
    void Start()
    {
        enemiesKilledInstanceCounter = 0;
        enemiesKilledTotalCounter = 0;
        
        timeCounterMinutes = timerGameObject.GetComponent<TimeCounter>().minutes;
        
        currentHealth = characterStats.maxHealth;
        slider.value = (CalculateHealth()/ characterStats.maxHealth);
        GetComponent<Collider2D>().enabled = true;
        healthBarUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
        slider.value = (CalculateHealth() / characterStats.maxHealth);
        if (slider.value <= 0)
        {
            Die();
        }
    }
    float CalculateHealth()
    {
        return currentHealth;
    }
    public void Die()
    {
       if (characterStats.deathSound != "")
        {
            int randomSoundValue; // Declare the randomSoundValue variable outside of the if block.
            
            if (characterStats.deathSound == "FlyingEyeRandom")
            {
                randomSoundValue = Random.Range(0, 3);
                if (randomSoundValue == 0)
                {
                    FindObjectOfType<AudioManager>().Play("Flying Eye Death 1");
                }
                else if (randomSoundValue == 1)
                {
                    FindObjectOfType<AudioManager>().Play("Flying Eye Death 2");
                }
                else if (randomSoundValue == 2)
                {
                    FindObjectOfType<AudioManager>().Play("Flying Eye Death 3");
                }
            }
            else
            {
                // You can set a default value for randomSoundValue here if needed.
                // randomSoundValue = someDefaultValue;
                
                FindObjectOfType<AudioManager>().Play(characterStats.deathSound);
            }
        }



    var enemymovement = GetComponent<EnemyMovement>();
    var sorcererMovement = GetComponent<SorcererMovement>();
    GetComponent<Collider2D>().enabled = false;
    slider.value = CalculateHealth();

    // Calculate a random value between 0 and 1
    float randomValue = Random.value;
   
    var flyingEyeMovement = GetComponent<FlyingEyeMovement>();
        if (flyingEyeMovement != null)
        {
            flyingEyeMovement.SetIsDead(true);
        }

    
    // Rest of your code that uses timeCounterMinutes
    

    // Define the drop probabilities
    float hpDropProbability = 0.05f;
    float magnetDropProbability = 0.01f;
    float bigWineDropProbability = 0.01f;

    // Determine which drop to instantiate based on probabilities
    GameObject dropPrefab = null;

    if (randomValue <= hpDropProbability)
    {
        dropPrefab = hpDropPrefab;
    }
    else if (randomValue <= hpDropProbability + magnetDropProbability)
    {
        dropPrefab = magnetDropPrefab;
    }
    else if (randomValue <= hpDropProbability + magnetDropProbability + bigWineDropProbability && timeCounterMinutes >= 5)
    {
        dropPrefab = bigWineDropPrefab;
    }
    else
    {
        dropPrefab = WineVase;
    }

    // Instantiate the chosen drop prefab
    Instantiate(dropPrefab, transform.position, Quaternion.identity);

    // Disable movement components and destroy enemy
    if (enemymovement != null)
    {
        enemymovement.enabled = false;
    }
    if (sorcererMovement != null)
    {
        sorcererMovement.enabled = false;
    }
    
    healthBarUI.SetActive(false);
    enemiesKilledInstanceCounter += PlayerPrefs.GetInt("EnemiesKilledInstance", 0);
    enemiesKilledTotalCounter += PlayerPrefs.GetInt("EnemiesKilledTotal", 0);
    enemiesKilledInstanceCounter ++;
    enemiesKilledTotalCounter ++;
    PlayerPrefs.SetInt("EnemiesKilledInstance", enemiesKilledInstanceCounter);
    PlayerPrefs.SetInt("EnemiesKilledTotal", enemiesKilledTotalCounter);
    Destroy(this.gameObject, 1.3f);
    this.enabled = false;
}
    public void TakeDamage(float damage, Vector3? sender = null)
    {

        // Reduce health based on the damage taken
        GameObject damagepopupValue = Instantiate(damagePopup, transform.position, Quaternion.identity);
        damagepopupValue.transform.GetChild(0).GetComponent<TextMeshPro>().text = damage.ToString();
        // Instantiate(damagePopup, transform.position, Quaternion.identity);
        currentHealth -= damage;
        var sorcererMovement = GetComponent<SorcererMovement>();
        // Check if the character is dead
        if (currentHealth <= 0 && animator !=null)
                    // handle death hereif (animator != null)
        {
            animator.SetTrigger("Dead");
            EnergyBall energyBall = GetComponent<EnergyBall>();
            energyBall.enabled = false;

        }
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
        if( sorcererMovement == null)
        {

        StopAllCoroutines();
        // if the weapon has knockback = true apply knockback on enemy
        if (sender != null)
        {
        Vector3 nonNullableSender = sender.Value;
         Vector2 direction = (transform.position - nonNullableSender).normalized;
         rb2d.AddForce(direction * knockbackStr, ForceMode2D.Impulse);
        }
       
        
        StartCoroutine(Reset());
        }  
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(knockbackDelay);
        rb2d.velocity = Vector3.zero;
    }
}
