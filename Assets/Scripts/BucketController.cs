using UnityEngine;
using System.Collections;

public class BucketMonsterController : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3.0f;  
    public float minFollowDistance = 0.1f;  
    public float yOffset = 0.5f;  

    public AudioClip deathSound;
    public AudioSource bgmAudioSource;
    private AudioSource audioSource;

    public float swingAmplitude = 0.5f;  
    public float swingFrequency = 2f;  

    private Vector3 targetPosition;
    private float swingTimer = 0f;
    private bool canFollow = false;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        StartCoroutine(StartFollowing());  
    }

    void Update()
    {
        if (player == null || !canFollow) return;  

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minFollowDistance)
        {
            targetPosition = new Vector3(player.position.x, player.position.y + yOffset, player.position.z);
            swingTimer += Time.deltaTime;
            float swingOffset = Mathf.Sin(swingTimer * swingFrequency) * swingAmplitude;
            Vector3 swungTargetPosition = targetPosition + transform.right * swingOffset;

            transform.position = Vector3.MoveTowards(transform.position, swungTargetPosition, followSpeed * Time.deltaTime);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.rotation *= Quaternion.Euler(90, 0, -swingOffset * 15f);
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

    IEnumerator StartFollowing()  
    {
        yield return new WaitForSeconds(2f);  
        canFollow = true;  
    }
}
