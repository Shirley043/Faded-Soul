using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; 
using System.Collections; 

public class ScrollingText : MonoBehaviour
{
    public float scrollSpeed = 50f;  
    private RectTransform rectTransform;
    private bool isScrollingFinished = false; 

    private TMP_Text tmpText; 
    public Image fadeImage; 

    public float fadeDuration = 2f; 
    public float delayBeforeFade = 1f; 

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

            if (rectTransform.anchoredPosition.y > Screen.height)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -tmpText.preferredHeight);
            }

            if (rectTransform.anchoredPosition.y <= -tmpText.preferredHeight)
            {
                isScrollingFinished = true; 
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0); 
                StartCoroutine(FadeOutAndLoadScene()); 
            }
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        yield return new WaitForSeconds(delayBeforeFade);

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
        SceneManager.LoadScene(0); 
    }
}
