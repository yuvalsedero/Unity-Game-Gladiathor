using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    
    public Rigidbody2D rb;
    public Animator animator;
    [Header("Movement Variables")]
    float moveX;
    float moveY;
    public Vector2 moveDir;
    public bool isFacingRight = true;
    public float dashCounter;
    private bool canDash = true;
    private bool isDashing;
    private float lastDashTime;
    private float dashingTime = 0.2f;
    private float lastDashCooldownTime;
    public GameObject cameraToShake;
    private CameraFollow cameraShake;
    private float lastDamageTime = 0f;
    private float damageCooldown = 1f; // 0.5 seconds cooldown
    private bool isDead = false;


    // public GameObject DashCounterUi;    
    
    public LevelText DashCounterNumUI;
    public Image imageCooldown;
    public TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        
        cameraShake = cameraToShake.GetComponent<CameraFollow>();
        characterStats.maxHealth = 100f;
        characterStats.speed = 8f;
        characterStats.dashAmount = 1f;
        characterStats.dashCooldown = 5f;

        // lastDashCooldownTime = Time.time;
        lastDashCooldownTime = characterStats.dashCooldown;
        // DashCounterNumUI = FindObjectOfType<LevelText>();
        dashCounter = characterStats.dashAmount;
        rb = GetComponent<Rigidbody2D>();
        imageCooldown.fillAmount = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
{
    
    if (isDashing)
    {
        return; // Exit the method if already dashing
    }

    InputManagement();
    CheckPlayerDirection();
    if (!isDead)
    {
        if(dashCounter != characterStats.dashAmount)
        {
            lastDashCooldownTime -= Time.deltaTime;
        }
        // Check if enough time has passed for dash cooldown
        if (lastDashCooldownTime <= 0)
        {
            lastDashCooldownTime = characterStats.dashCooldown;
            imageCooldown.fillAmount = 0.0f;
            IncrementDashCounter();
        }
        else if(dashCounter != characterStats.dashAmount)
        {
            imageCooldown.fillAmount =  lastDashCooldownTime / characterStats.dashCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


}

// Add this new method to increment the dash counter while not exceeding the dash amount
void IncrementDashCounter()
{
    if (dashCounter < characterStats.dashAmount)
    {
        dashCounter++;
    }
}
void LateUpdate()
{
    DashCounterNumUI.ChangeText(dashCounter.ToString());
}


    private void FixedUpdate() // set body movement to run speed and the directions from update
    {
        if (isDashing)
        {
            return;
        }
        Move();
    }
    
    void InputManagement()
    {
        if (isDead)
    {
        moveX = 0f;
        moveY = 0f;
    }
    else
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

        moveDir = new Vector2(moveX, moveY).normalized;
        animator.SetFloat("Speed", Mathf.Abs(moveDir.x)); //set animation of walking
        animator.SetFloat("Speed", Mathf.Abs(moveDir.y)); //set animation of walking
    }

    void Move()
    {
        if (isDead)
    {
        rb.velocity = Vector2.zero;
    }
        else
    {
        rb.velocity = new Vector2(moveDir.x * characterStats.speed, moveDir.y * characterStats.speed);
    }
        
    }

    void CheckPlayerDirection()
    {
        if (moveX < 0 && isFacingRight){
            FlipPlayer();
        }
        else if (moveX > 0 && !isFacingRight)
        {
            FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        // Flip the player's sprite horizontally
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    void OnCollisionEnter2D(Collision2D other)// take damage if collide with enemy
   {
      if(other.gameObject.tag == "Enemy")
      {
        var sorcerer = other.gameObject.GetComponent<SorcererMovement>();
        var orc = other.gameObject.GetComponent<EnemyMovement>();
        var flyingEye = other.gameObject.GetComponent<FlyingEyeMovement>();
        if(orc != null)
        {
        orc.body.drag = 15;
        TakeDamage(orc.characterStats.damage);
        }
        if(sorcerer != null)
        {
        sorcerer.body.drag = 15;
        }
        if(flyingEye != null)
        {
        flyingEye.body.drag = 15;
        TakeDamage(flyingEye.characterStats.damage);
        }
        
      }
  
   }
  public void TakeDamage(float damage)
{

    // Check if enough time has passed since the last damage
    if (Time.time - lastDamageTime >= damageCooldown)
    {
        // Update the last damage time
        lastDamageTime = Time.time;

        // Reduce health based on the damage taken
        characterStats.health -= damage;
        cameraShake.ShakeCamera(0.1f, 0.2f);

        // Check if the character is dead and not already dead
        if (characterStats.health <= 0 && !isDead)
        {
            Die();
            
        }
        else if (animator != null && !isDead)
        {
            int randomSoundValue = Random.Range(0, 2);
            if (randomSoundValue == 0)
            {
                FindObjectOfType<AudioManager>().Play("Player Hurt 1");
            }
            else if (randomSoundValue == 1)
            {
                FindObjectOfType<AudioManager>().Play("Player Hurt 2");
            }
            animator.SetTrigger("Hurt");
        }
    }
}

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Player Death");
        animator.SetTrigger("Dead");
        // Disable movement input
            isDead = true;
    }
    private IEnumerator Dash()
{
    if (dashCounter > 0)
    {
        FindObjectOfType<AudioManager>().Play("Dash");
        dashCounter--; // Decrement dash counter
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDir.x * characterStats.dashPower, moveDir.y * characterStats.dashPower);
        tr.emitting = true;
        lastDashTime = Time.time; // Record the time of the dash
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(0.2f); // Delay between dashes
        canDash = true;
    }
}
public bool IsDead()
{
    return isDead;
}
}


