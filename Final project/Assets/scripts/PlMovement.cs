using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlMovement : MonoBehaviour
{
    public float regularSpeed = 8f;
    public float boostedSpeed = 35f;
    public float boostDuration = 1.5f;
    public GameObject trailEffect; // Reference to the trail effect GameObject
    private float currentSpeed;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool isBoosting = false;

    GameObject shield;

    void Start()
    {
        shield = transform.Find("Shield").gameObject;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        currentSpeed = regularSpeed;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (Input.GetKeyDown(KeyCode.Space) && !isBoosting)
        {
            StartCoroutine(BoostPlayer());
            StartCoroutine(EnableTrailEffectForDuration()); // Enable trail effect for 1 second
            playerCollider.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerCollider.enabled = true;
        }

        rb.velocity = movement.normalized * currentSpeed;

        RotatePlayerTowardsCursor();
    }

    IEnumerator BoostPlayer()
    {
        currentSpeed = boostedSpeed;
        isBoosting = true;
        yield return new WaitForSeconds(boostDuration);
        currentSpeed = regularSpeed;
        isBoosting = false;
    }

    IEnumerator EnableTrailEffectForDuration()
    {
        trailEffect.SetActive(true); // Enable the trail effect GameObject
        yield return new WaitForSeconds(1f); // Wait for 1 second
        trailEffect.SetActive(false); // Disable the trail effect GameObject
    }

    void RotatePlayerTowardsCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        transform.up = direction;
    }
}