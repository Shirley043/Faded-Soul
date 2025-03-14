using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IntroScrollingText : MonoBehaviour
{
    public float scrollSpeed = 50f;  
    private RectTransform rectTransform;
    private bool isScrollingFinished = false;

    private TMP_Text tmpText; 
    public Image fadeImage; 

    public float fadeDuration = 2f; 
    public float delayBeforeFade = 1f; 
    public float waitForFinalLineDuration = 2f; 

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        tmpText = GetComponent<TMP_Text>(); 

        if (tmpText == null)
        {
            Debug.LogError("TextMeshPro component is missing on the scrolling text GameObject!");
        }

        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f; 
            fadeImage.color = color;
        }
    }

    void Update()
    {
        if (!isScrollingFinished)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            Debug.Log("Current Position: " + rectTransform.anchoredPosition.y);

            if (rectTransform.anchoredPosition.y >= tmpText.preferredHeight)
            {
                Debug.Log("Scrolling Finished");
                isScrollingFinished = true; 
                
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, tmpText.preferredHeight);
                StartCoroutine(ShowLastLineAndFadeOut()); 
            }
        }
    }

    private IEnumerator ShowLastLineAndFadeOut()
    {
        yield return new WaitForSeconds(waitForFinalLineDuration);

        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            float fadeSpeed = 1f / fadeDuration;

            while (color.a < 1f)
            {
                color.a += fadeSpeed * Time.deltaTime; 
                fadeImage.color = color;
                yield return null; 
            }
        }

        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}
