using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // mans shit wasnt working so did this 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        Debug.Log("Tag is: '" + collision.gameObject.tag + "'");

        bool isDamage = collision.gameObject.CompareTag("Damage");
        Debug.Log("CompareTag result: " + isDamage);

        if (isDamage)
        {
            Debug.Log("Inside damage block");

            health -= 10;
            Debug.Log("Health now: " + health);

            if (health <= 0)
            {
                Debug.Log("Enemy dying!");
                Die();
            }
         }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}