using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossmovement : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 20f;
    [SerializeField] enemyhealthbar healthBar;

    public float chaseSpeed = 3f;
    public float bulletSpeed = 8f; // Speed of the bullet
    public float shootInterval = 3f; // Time interval between shots
    public float shootPauseDuration = 1f; // Duration of pause while shooting
    public GameObject bulletPrefab; // Prefab of the bullet to shoot
    public Transform firePoint; // Point from which bullets will be shot
    public scoresystem scoreSystem;
    public healthbarmanager healthBarManager;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<enemyhealthbar>();
        scoreSystem = GameObject.FindObjectOfType<scoresystem>();
        healthBarManager = GetComponent<healthbarmanager>();
        player = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Move towards the player
            rb.MovePosition(rb.position + direction * chaseSpeed * Time.deltaTime);

            // Flip sprite if necessary
            spriteRenderer.flipX = direction.x < 0;
        }

        // Start shooting coroutine if not already shooting
        if (!IsInvoking("ShootAtPlayer"))
        {
            InvokeRepeating("ShootAtPlayer", 0f, shootInterval);
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
        scoreSystem.IncreaseScore(5);
    }

    // Coroutine to shoot at the player
    void ShootAtPlayer()
    {
        // Pause movement while shooting
        StartCoroutine(PauseMovementWhileShooting());

        // Shoot bullet at player
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // Adjust bullet direction towards player
        Vector2 direction = (player.position - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    // Coroutine to pause movement while shooting
    IEnumerator PauseMovementWhileShooting()
    {
        // Pause movement
        chaseSpeed = 0f;
        // Wait for shooting pause duration
        yield return new WaitForSeconds(shootPauseDuration);
        // Resume movement
        chaseSpeed = 3f;
    }
}