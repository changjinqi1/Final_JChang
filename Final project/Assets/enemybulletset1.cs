using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybulletset1 : MonoBehaviour

{
    public float bulletSpeed = 10f; // Speed of the bullet
    private Transform target; // Reference to the player transform

    void Start()
    {
        // Find the player object by tag
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // If player object is found
        if (target != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = (target.position - transform.position).normalized;

            // Apply velocity to the bullet to move towards the player
            GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }

        // Start the coroutine to destroy the bullet after 7 seconds
        StartCoroutine(DestroyAfterDelay(7f));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the bullet collides with the player, destroy it
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the bullet
        Destroy(gameObject);
    }
}