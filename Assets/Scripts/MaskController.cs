using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    public float speed = 4f;
    //public float distance = 10f;
    private float maxZ = 5f;
    private float minZ = -1f;
    private bool movingForward = true;
    public AudioClip deathSound;
    public AudioSource bgmAudioSource;
    private Vector3 startPosition;
    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;

        
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true; 
        }
        else
        {
            Debug.LogError("No Collider found on Mask object.");
        }

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        //float xOffset = Mathf.Sin(Time.time * speed) * distance;
        //transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + xOffset);
        if (movingForward)
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            if (transform.position.z >= maxZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
                movingForward = false;
            }
        }
        else
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            if (transform.position.z <= minZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
                movingForward = true;
            }
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
}
