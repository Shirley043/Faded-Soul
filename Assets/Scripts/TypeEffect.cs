using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f; 
    public string message = "@#!$&#!^&$#%^!&%#$*!#%$!#*#$*!%^$!#%$!(%$#%!*^#$(%^*$(!#%!!$#@!!@$^%^!$#*#$!#^&%$!"; 
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>(); 
        StartCoroutine(LoopTypeMessage()); 
    }

    IEnumerator LoopTypeMessage()
    {
        while (true)  // loop
        {
            yield return StartCoroutine(TypeMessage()); 

            yield return new WaitForSeconds(1f); 
        }
    }

    IEnumerator TypeMessage()
    {
        textComponent.text = ""; 

        foreach (char letter in message)
        {
            textComponent.text += letter; 
            yield return new WaitForSeconds(typingSpeed); 
        }
    }
}
