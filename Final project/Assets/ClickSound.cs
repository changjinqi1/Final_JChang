using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioClip soundClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = soundClip;
    }

    public void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {

            audioSource.PlayOneShot(soundClip);
        }
    }
}
