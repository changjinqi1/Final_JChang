using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhurtsound : MonoBehaviour
{
    public AudioClip collisionSound;

    private AudioSource audioSource;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = collisionSound;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }
}
