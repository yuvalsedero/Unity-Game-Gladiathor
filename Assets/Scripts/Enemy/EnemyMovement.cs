using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    public Rigidbody2D body;
    public Animator animator;
    Vector2 positionToMoveTo;
    // Start is called before the first frame update
    private void Awake()
    {

        body = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        positionToMoveTo = GameObject.FindGameObjectWithTag("Player").transform.position; // get Player position
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, positionToMoveTo, characterStats.speed * Time.deltaTime); // enemy moving torwads player
        animator.SetFloat("Speed", Mathf.Abs(targetPosition.x));
        
    }
    private void FixedUpdate() // set body movement to run speed and the directions from update
    {
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, positionToMoveTo, characterStats.speed * Time.deltaTime); // enemy moving torwads player
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
