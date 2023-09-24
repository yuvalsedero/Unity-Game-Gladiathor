using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShauliMovement : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    public Rigidbody2D body;
    public Animator animator;
    public float attackRange; // New variable to adjust attack range in the Inspector
    Vector2 positionToMoveTo;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        positionToMoveTo = GameObject.FindGameObjectWithTag("Player").transform.position;
        float distanceToPlayer = Vector2.Distance(transform.position, positionToMoveTo); // Calculate distance to player

        // Check if the boss is in attack range and perform the attack
        if (distanceToPlayer <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, positionToMoveTo, characterStats.speed * Time.deltaTime);
        animator.SetFloat("Speed", Mathf.Abs(targetPosition.x));
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, positionToMoveTo, characterStats.speed * Time.deltaTime);
        body.transform.position = targetPosition;

        if (body.transform.position.x < positionToMoveTo.x)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (body.transform.position.x > positionToMoveTo.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }
}
