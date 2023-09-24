using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    private Transform target;
    private Rigidbody2D rb;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target != null && weaponData.homing)
        {
            Vector2 direction = ((Vector2)target.position) - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            rb.angularVelocity = -rotateAmount * weaponData.homingSpeed;
            rb.velocity = transform.right * weaponData.speed;
        }
    }
}
