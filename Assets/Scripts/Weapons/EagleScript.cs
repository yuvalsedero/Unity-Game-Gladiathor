


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public Rigidbody2D body;
    public Animator animator;
    Vector2 positionToMoveTo;
    Vector2 playerPosition;

    GameObject target;
    bool isChasingPlayer = false;

    private float timeSinceLastChange = 0f;
    private float randomChangeInterval = 1.5f;
    private Vector2 initialPosition;
    public float verticalOffset = 0.25f;
    // public float turnSpeed = 2.0f; // Adjust the turn speed as needed
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        FindObjectOfType<AudioManager>().Play("Eagle Spawn");
    }
   void Update()
    {
        GameObject playerPosition = GameObject.FindGameObjectWithTag("Player");
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");

        if (isChasingPlayer)
        {
            // If chasing the player, move towards the player until in range
            if (Vector2.Distance(transform.position, playerPosition.transform.position) <= weaponData.range - 5f)
            {
                isChasingPlayer = false; // Stop chasing the player
                StartCoroutine(WaitCoroutine()); // Wait for some time before going to haunt enemies again
            }
            else
            {
                positionToMoveTo = playerPosition.transform.position;
                Vector2 targetPosition = Vector2.MoveTowards(transform.position, positionToMoveTo, weaponData.speed * Time.deltaTime);
            }
        }
        else
        {
            // If not chasing the player, find the closest enemy to haunt
            if (allTargets != null && allTargets.Length > 0)
            {
                target = allTargets[0];
                foreach (GameObject tmpTarget in allTargets)
                {
                    if (Vector2.Distance(transform.position, tmpTarget.transform.position) < Vector2.Distance(transform.position, target.transform.position))
                    {
                        target = tmpTarget;
                    }
                }

                bool enemyAnimator = target.GetComponent<Animator>().GetBool("Dead");
                if (Vector2.Distance(transform.position, target.transform.position) < weaponData.range && enemyAnimator == false)
                {
                    isChasingPlayer = false;
                    positionToMoveTo = target.transform.position;
                    Vector2 targetPosition = Vector2.MoveTowards(transform.position, positionToMoveTo, weaponData.speed * Time.deltaTime);
                }
                else
                {
                    isChasingPlayer = true; // Start chasing the player
                }
            }
        }
        if (Time.time - timeSinceLastChange > randomChangeInterval)
        {
            positionToMoveTo = initialPosition + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 3f;
            timeSinceLastChange = Time.time;
        }
    }


    private void FixedUpdate() // set body movement to run speed and the directions from update
    {
        
            // Vector2 targetPosition = Vector2.MoveTowards(transform.position, positionToMoveTo, weaponData.speed * Time.deltaTime); // eagle moving torwads enemy
            // body.transform.position = targetPosition;
            // Smoothly interpolate the position
        // Smoothly interpolate the position
        Vector2 targetPosition = Vector2.Lerp(transform.position, positionToMoveTo, weaponData.speed * Time.deltaTime);
        body.transform.position = targetPosition;

        // Add sinusoidal vertical movement
        float verticalMovement = Mathf.Sin(Time.time) * verticalOffset;
        body.transform.position += Vector3.up * verticalMovement;
            if (body.transform.position.x - positionToMoveTo.x < body.transform.position.y - positionToMoveTo.y)
            {
                animator.SetBool("Horizontal", true);
            }
            else if (body.transform.position.x - positionToMoveTo.x > body.transform.position.y - positionToMoveTo.y)
            {
                animator.SetBool("Horizontal", false);
            }
            if (body.transform.position.y < positionToMoveTo.y)
            {
                animator.SetBool("Up", true);
            }
            else if (body.transform.position.y > positionToMoveTo.y)
            {
                animator.SetBool("Up", false);
            }
            if (body.transform.position.x < positionToMoveTo.x)
            {
                transform.localScale = new Vector2(0.5f, 0.5f);
            }
            else if (body.transform.position.x > positionToMoveTo.x)
            {
                transform.localScale = new Vector2(-0.5f, 0.5f);
            }
        


    }
 

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2f);
        isChasingPlayer = false; // After waiting, set the flag to false to haunt enemies again
    }
}
