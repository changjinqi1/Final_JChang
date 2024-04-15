using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootbullet1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootRate = 0.5f;
    public float bulletLifetime = 5f;
    public int maxBulletAmount = 15;
    private int bulletAmount;
    private float shootTimer;
    private bool canShoot = true;

    void Start()
    {
        bulletAmount = maxBulletAmount;
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
        Vector2 baseDirection = (mousePosition - transform.position).normalized;

        float angleDifference = 10f;

        for (int i = 0; i < 4; i++)
        {
            float angle = angleDifference * i;
            Vector2 shootDirection = Quaternion.Euler(0f, 0f, angle) * baseDirection;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * bulletSpeed;

            StartCoroutine(DestroyBulletAfterDelay(bullet, bulletLifetime));
        }

        bulletAmount -= 4;

    }

    IEnumerator ReloadAndDisableShooting()
    {
        canShoot = false;
        bulletAmount = maxBulletAmount;
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }

    IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);

    }

    public void ApplyPowerUp()
    {
        maxBulletAmount += 8;
        bulletAmount = maxBulletAmount;
    }
}
