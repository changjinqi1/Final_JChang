using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossscript1 : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 15f;
    [SerializeField] enemyhealthbar healthBar;

    public float chaseSpeed = 3f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private scoresystem scoreSystem;
    public healthbarmanager healthBarManager;

    Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<enemyhealthbar>();
        scoreSystem = GameObject.FindObjectOfType<scoresystem>();
        healthBarManager = GetComponent<healthbarmanager>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * chaseSpeed * Time.deltaTime);

            if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            StartCoroutine(FlashRed());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            healthbarmanager healthManager = collision.gameObject.GetComponent<healthbarmanager>();

            if (healthManager != null)
            {
                healthManager.PlayerTakeDamage(35);
            }
        }
    }

    IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = originalColor;
    }

    void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        scoreSystem.IncreaseScore(2);
    }
}