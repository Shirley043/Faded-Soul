using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float moveSpeed = 2f;  
    public float moveDistance = 1f;  
    public float amplitudeMultiplier = 1.5f;  

    public AudioClip deathSound;
    public AudioSource bgmAudioSource;
    private AudioSource audioSource;

    private Vector3 startPosition;

    
    void Start()
    {
        startPosition = transform.position;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    
    void Update()
    {
        
        float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance * amplitudeMultiplier;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bgmAudioSource != null)
            {
                bgmAudioSource.Stop(); 
            }
            PlayDeathSound();
            
        }
    }

    void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}