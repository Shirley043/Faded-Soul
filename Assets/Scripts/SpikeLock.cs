using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLock : MonoBehaviour
{
    public float moveSpeed = 2f;   
    public float maxHeight = 7f;   
    public float minHeight = 5f;   

    public AudioClip deathSound;
    public AudioSource bgmAudioSource;
    private AudioSource audioSource;

    private Vector3 startPosition; 
    private float amplitude; 
    private float midPoint;  
    
    public Transform player; 
    public float range = 5f; 
    private bool isMoving = false; 

    void Start()
    {
        transform.position = new Vector3(transform.position.x, minHeight, transform.position.z); 

        midPoint = (maxHeight + minHeight) / 2f;  
        amplitude = (maxHeight - minHeight) / 2f; 

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < range && !isMoving)
        {
            StartCoroutine(StartSineWaveMovement(20f)); 
        }

        if (isMoving)
        {
            float newY = midPoint + Mathf.Sin(Time.time * moveSpeed) * amplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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

    private IEnumerator StartSineWaveMovement(float delay)
    {
        yield return new WaitForSeconds(delay); 
        isMoving = true; 
    }
}
