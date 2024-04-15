using System.Collections;
using UnityEngine;

public class zombiemovement : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 5f;
    [SerializeField] enemyhealthbar healthBar;

    public float chaseSpeed = 2f;
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
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            StartCoroutine(FlashRed());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            healthbarmanager healthManager = collision.gameObject.GetComponent<healthbarmanager>();

            if (healthManager != null)
            {
                healthManager.PlayerTakeDamage(20);
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