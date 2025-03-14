using UnityEngine;

public class TextureScaler : MonoBehaviour
{
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float scaleX = transform.localScale.x;
        material.SetFloat("_TextureScale", scaleX);
        material.SetFloat("_AlphaScale", scaleX);  
    }
}