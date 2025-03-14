using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlController : MonoBehaviour
{
    private CharacterController controller;
    public GameObject player;
    private Vector3 playerVelocity;

    public AudioClip deathSound;
    public AudioSource bgmAudioSource;
    private AudioSource audioSource;

    private bool groundedPlayer;
    private float playerSpeed = 5.0f;    
    private float normalSpeed = 5.0f;  
    private float runSpeed = 15.0f;      
    private float jumpHeight = 5.0f;
    private float gravityValue = -9.81f;
    private Animator animator;
    public GameObject gameOverPanel;  
    float curJumpTime = 0f;

    public float fallThreshold;

    private bool canMove = true;
    private bool canSpeedUp = false; 
    private float speedUpTimer = 0f; 
    private bool isSlowedDown = false; 
    private float slowDownDuration = 3f; 
    private float slowDownTimer = 0f; 
    private float slowDownFactor = 0.5f; 

    private bool hasPlayedDeathSound = false;  

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);  
        }
        
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!canMove) return;

        groundedPlayer = controller.isGrounded;

        if (transform.position.y < fallThreshold && !hasPlayedDeathSound)
        {
            ShowGameOverPanel();
            if (bgmAudioSource != null)
            {
                bgmAudioSource.Stop(); 
            }
            
            PlayDeathSound();
            hasPlayedDeathSound = true;  
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            if ((curJumpTime == 0f) || ((Time.time - curJumpTime) > 1.0f))
            {
                curJumpTime = Time.time;
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue); 
            }       
        }

        // Handle speed up
        if (canSpeedUp)
        {
            speedUpTimer += Time.deltaTime;
            if (speedUpTimer >= 5f) 
            {
                canSpeedUp = false;
                playerSpeed = normalSpeed; 
            }
        }

        // Handle slow down
        if (isSlowedDown)
        {
            slowDownTimer += Time.deltaTime;
            if (slowDownTimer >= slowDownDuration)
            {
                isSlowedDown = false;
                playerSpeed = normalSpeed; 
            }
        }

        // Check for run speed activation
        if (Input.GetKey(KeyCode.LeftShift) && canSpeedUp)
        {
            playerSpeed = runSpeed; 
        }
        else if (!isSlowedDown) 
        {
            playerSpeed = normalSpeed; 
        }

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f);
        controller.Move(moveDirection * Time.deltaTime * playerSpeed);

        if (!groundedPlayer)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
        controller.Move(playerVelocity * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            gameObject.transform.forward = moveDirection;
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") && !hasPlayedDeathSound) 
        {
            //Debug.Log("Monster.");
            ShowGameOverPanel();
            PlayDeathSound();
            hasPlayedDeathSound = true;  
        }
        else if (other.gameObject.CompareTag("SwitchLevel")) 
        {
            //Debug.Log("SwitchLevel");
            canMove = false;  
        }
        else if (other.gameObject.CompareTag("SpeedUp")) 
        {
            //Debug.Log("SpeedUp");
            canSpeedUp = true; 
            speedUpTimer = 0f; 
        }
        else if (other.gameObject.CompareTag("SlowDown")) 
        {
            //Debug.Log("SlowDown");
            isSlowedDown = true; 
            playerSpeed *= slowDownFactor; 
            slowDownTimer = 0f; 
        }
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);  
            Time.timeScale = 0;  
        }
    }

    void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            //Debug.Log("Playing death sound...");
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            //Debug.Log("no audio source.");
        }
    }

    public void ResetDeathSound()
    {
        hasPlayedDeathSound = false;  
    }
}