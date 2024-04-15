using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiganticBullet : MonoBehaviour

{
    public float expansionSpeed = 2f;
    public float explosionRadius = 5f;
    public GameObject explosionPrefab;
    private scoresystem scoreSystem;

    private bool collidedWithEnemy = false;

    private void Awake()
    {
        scoreSystem = GameObject.FindObjectOfType<scoresystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !collidedWithEnemy)
        {
            collidedWithEnemy = true;

            StartCoroutine(ExpandBullet());

            GenerateExplosion();
        }
    }

    private IEnumerator ExpandBullet()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 2f;

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime);
            elapsedTime += Time.deltaTime * expansionSpeed;
            yield return null;
        }
    }

    private void GenerateExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                scoreSystem.IncreaseScore(1);
                Destroy(collider.gameObject);
            }
        }

        Destroy(explosion, 1f);
    }
}