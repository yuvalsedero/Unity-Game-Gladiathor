using UnityEngine;

public class SorcererMovement : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    public Rigidbody2D body;
    public Animator animator;
    public float stoppingRange = 2f; // The range at which the sorcerer stops chasing and stays in position
    public float minimumDistance = 0.5f; // The minimum distance the sorcerer should maintain from the player within stoppingRange
    private Transform playerTransform;
    // private bool isChasing = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector2 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= stoppingRange)
        {
            if (distanceToPlayer < stoppingRange - minimumDistance)
            {
                // Move away from the player at half speed
                // isChasing = false;
                Vector2 awayDirection = -directionToPlayer.normalized;
                body.velocity = awayDirection * characterStats.speed * 0.5f;
            }
            else
            {
                // Stop movement within the minimum distance
                // isChasing = false;
                body.velocity = Vector2.zero;
            }
        }
        else
        {
            // Move towards the player with the characterStats speed
            // isChasing = true;
            body.velocity = directionToPlayer.normalized * characterStats.speed;

            // Face the direction of movement
            if (directionToPlayer.x < 0f)
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
            else if (directionToPlayer.x > 0f)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
        }

        // Update animator for the sorcerer's movement
        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
    }
}
