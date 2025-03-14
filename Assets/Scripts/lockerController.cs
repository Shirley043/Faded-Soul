using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockerController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    //public float attackSpeed;
    private float currentYRotation;
    private bool rotatingForward = true;
    private bool isPaused = false;

    void Start()
    {
        currentYRotation = transform.eulerAngles.y;

    }

    void Update()
    {
        if (!isPaused)
        {
            if (rotatingForward)
            {
                currentYRotation += rotationSpeed * Time.deltaTime;
                if (currentYRotation >= 180f)
                {
                    currentYRotation = 180f;
                    rotatingForward = false;
                    StartCoroutine(PauseAtRotation(2f));  // Stop for 2 seconds
                }
            }
            else
            {
                currentYRotation -= rotationSpeed * Time.deltaTime;
                if (currentYRotation <= 20f)
                
                {
                    
                    currentYRotation = 20f;
                    rotatingForward = true;
                    
                }
            }

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentYRotation, transform.eulerAngles.z);
        }
    }

    IEnumerator PauseAtRotation(float duration)
    {
        isPaused = true;
        yield return new WaitForSeconds(duration);
        isPaused = false;
    }

    


}