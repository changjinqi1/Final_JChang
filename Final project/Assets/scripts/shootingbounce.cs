using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingbounce : MonoBehaviour
{
    public GameObject smallBulletPrefab;
    public float smallBulletSpeed = 8f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Generate two small bullets when the bullet collides with an enemy
            GenerateSmallBullets(transform.position);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }

    void GenerateSmallBullets(Vector3 position)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject smallBullet = Instantiate(smallBulletPrefab, position, Quaternion.identity);
            Rigidbody2D rb = smallBullet.GetComponent<Rigidbody2D>();

            // Apply a random direction to the small bullet
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.velocity = randomDirection * smallBulletSpeed;
        }
    }
}