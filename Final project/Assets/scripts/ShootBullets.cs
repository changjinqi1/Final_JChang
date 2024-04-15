using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootBullets : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootRate = 0.5f;
    public float bulletLifetime = 5f;
    public int maxBulletAmount = 15;
    private int bulletAmount;
    private float shootTimer;
    private bool canShoot = true;
    private SpriteRenderer playerSpriteRenderer;
    private Color originalColor;

    public TextMeshProUGUI bulletCountText; // Change TMP_text to TextMeshProUGUI

    void Start()
    {
        bulletAmount = maxBulletAmount;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = playerSpriteRenderer.color;

        UpdateBulletCountText();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && shootTimer >= shootRate && canShoot && bulletAmount > 0)
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ReloadAndDisableShooting());
        }
    }

    void Shoot()
    {
        shootTimer = 0f;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 shootDirection = (mousePosition - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = shootDirection * bulletSpeed;

        StartCoroutine(DestroyBulletAfterDelay(bullet, bulletLifetime));

        bulletAmount--;

        UpdateBulletCountText();
    }

    IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    IEnumerator ReloadAndDisableShooting()
    {
        canShoot = false;
        bulletAmount = maxBulletAmount;

        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);

        playerSpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.3f);

        playerSpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(1.4f);

        playerSpriteRenderer.color = Color.black;

        canShoot = true;

        UpdateBulletCountText();
    }

    void UpdateBulletCountText()
    {
        bulletCountText.text = "Bullets: " + bulletAmount.ToString();
    }
}