using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerhealth : MonoBehaviour
{
    public int maxHealth = 15;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
