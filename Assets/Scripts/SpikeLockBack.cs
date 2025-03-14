using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLockBack : MonoBehaviour
{
    public float moveSpeed = 2f;  
    public float moveDistance = 1f;  
    public float amplitudeMultiplier = 1.5f;  

    public AudioClip deathSound;
    public AudioSource bgmAudioSource;
    private AudioSource audioSource;

    private Vector3 startPosition;
    
    public Transform player; 
    public float targetHeight = 5f; 

    private bool isHeightLocked = false; 

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
        
        if (!isHeightLocked)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance * amplitudeMultiplier;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (player.position.x > transform.position.x + 1f)
            {
                StartCoroutine(LockHeightAfterDelay(0.5f)); 
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
        }
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

    private IEnumerator LockHeightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 

        isHeightLocked = true; 
    }
}
