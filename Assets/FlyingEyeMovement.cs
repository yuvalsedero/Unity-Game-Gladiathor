using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEyeMovement : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    public Rigidbody2D body;
    public Animator animator;
    Vector2 playerPosition;
    Vector2 chargeTargetPosition;
    enum EnemyState { Following, PreparingToCharge, Charging }
    EnemyState currentState = EnemyState.Following;
    float chargeStartTime;
    float chargeDuration = 1.5f; // Adjust as needed
    private bool isDead = false;
    public float deceleration = 5f; // Adjust this value to control how fast it slows down

    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentState = EnemyState.Following;
    }
    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // State-based movement
        if (!isDead)
        {
            switch (currentState)
            {
                case EnemyState.Following:
                    FollowPlayer();
                    break;
                case EnemyState.PreparingToCharge:
                    PrepareToCharge();
                    break;
                case EnemyState.Charging:
                    Charge();
                    break;
            }
        }
    }
public void SetIsDead(bool deadStatus)
    {
        isDead = deadStatus;
        if (isDead)
        {
            // Start decreasing speed to zero over 1 second
            StartCoroutine(Decelerate());
        }
    }

private IEnumerator Decelerate()
    {
        while (body.velocity.magnitude > 0.1f)
        {
            Vector2 oppositeForce = -body.velocity.normalized * deceleration;
            body.AddForce(oppositeForce, ForceMode2D.Force);
            yield return null;
        }

        // Ensure the Rigidbody2D comes to a complete stop
        body.velocity = Vector2.zero;
    }

void FollowPlayer()
{
    // Update the player's position in each frame
    playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

    // Move towards the player's current position
    Vector2 targetPosition = Vector2.MoveTowards(transform.position, playerPosition, characterStats.speed * Time.deltaTime);
    body.transform.position = targetPosition;
    if (body.transform.position.x < playerPosition.x)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (body.transform.position.x > playerPosition.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    animator.SetFloat("Speed", Mathf.Abs(targetPosition.x));

    // Check if the enemy should start preparing to charge
    if (Vector2.Distance(transform.position, playerPosition) <= characterStats.range)
    {
        currentState = EnemyState.PreparingToCharge;
        animator.SetTrigger("Prepare");
        chargeStartTime = Time.time;
        chargeTargetPosition = playerPosition; // Set the charge target to the player's current position
    }
}






   void PrepareToCharge()
{
    // Enemy prepares to charge for a set duration
    animator.SetFloat("Speed", 0f);
    if (Time.time - chargeStartTime >= chargeDuration)
    {
        currentState = EnemyState.Charging;
        animator.SetTrigger("Charge");
        chargeTargetPosition = playerPosition; // Set the charge target to the player's current position
        int randomSoundValue = Random.Range(0, 3);
        if (randomSoundValue == 0)
        {
            FindObjectOfType<AudioManager>().Play("Flying Eye Charge 1");
        }
        else if (randomSoundValue == 1)
        {
            FindObjectOfType<AudioManager>().Play("Flying Eye Charge 2");
        }
        else if (randomSoundValue == 2)
        {
            FindObjectOfType<AudioManager>().Play("Flying Eye Charge 3");
        }
            
    }
}


    void Charge()
    {
        // Move towards the charge target position at a higher speed
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, chargeTargetPosition, (characterStats.speed * 2.5f) * Time.deltaTime);
        body.transform.position = targetPosition;
        
        
        // Check if the enemy has reached the charge target
        if (Vector2.Distance(transform.position, chargeTargetPosition) < 0.1f)
        {
            currentState = EnemyState.Following;
        }
    }
}
