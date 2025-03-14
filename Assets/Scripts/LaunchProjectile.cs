// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LaunchProjectile : MonoBehaviour
// {
//     public Transform launchPoint;
//     public GameObject projectile;
//     public float launchVelocity = 10f;
//     public float launchInterval = 2f;  // Time between each launch

//     void Start()
//     {
//         StartCoroutine(AutoLaunch());
//     }

//     IEnumerator AutoLaunch()
//     {
//         while(true)
//         {
//             var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
//             _projectile.GetComponent<Rigidbody>().velocity = launchPoint.up * launchVelocity;

//             yield return new WaitForSeconds(launchInterval);  // Wait before launching the next projectile
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour 
{
    public Transform launchPoint;
    public GameObject projectile;

    public AudioClip deathSound;
    public AudioSource bgmAudioSource;
    private AudioSource audioSource;
    
    public float launchVelocity = 20f;    
    public float launchInterval = 2f;     

    void Start()
    {
        StartCoroutine(AutoLaunch());
    }

    IEnumerator AutoLaunch()
    {
        while(true)
        {
            var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            _projectile.GetComponent<Rigidbody>().velocity = launchPoint.forward * launchVelocity;  
            
            yield return new WaitForSeconds(launchInterval);
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

