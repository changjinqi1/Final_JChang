using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maximizeammo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Shootbullet1 playerShootScript = other.GetComponent<Shootbullet1>();

            if (playerShootScript != null)
            {
                playerShootScript.ApplyPowerUp();
            }

            Destroy(gameObject);
        }
    }
}