using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject TextGameObject;

    private Text speedUpText;
    private Vector3 originalScale;
    private Color originalColor;

    void Start()
    {
        speedUpText = TextGameObject.GetComponent<Text>();
        speedUpText.text = " "; 
        originalScale = speedUpText.transform.localScale; 
        originalColor = speedUpText.color; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedUpCoroutine());
        }
    }

    private IEnumerator SpeedUpCoroutine()
    {
        
        speedUpText.text = "Press Shift to Speed Up";

        
        speedUpText.transform.localScale = originalScale * 1.3f; 
        speedUpText.color = Color.yellow; 

        yield return new WaitForSeconds(5f); 

        
        speedUpText.transform.localScale = originalScale; 
        speedUpText.color = originalColor; 
        speedUpText.text = " "; 
    }
}
