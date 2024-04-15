using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class healthbarmanager : MonoBehaviour
{
    public Image playerhealthBar;
    public float healthAmount = 100f;
    private bool isHealing = false;

    void Update()
    {
        if (healthAmount <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (!isHealing)
        {
            StartCoroutine(HealOverTime(1f));
        }
    }

    IEnumerator HealOverTime(float interval)
    {
        isHealing = true;
        Heal(2f);
        yield return new WaitForSeconds(interval);
        isHealing = false;
    }


    public void PlayerTakeDamage(float damage)
    {
        healthAmount -= damage;
        playerhealthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        playerhealthBar.fillAmount = healthAmount / 100f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerTakeDamage(25);
        }
    }
}