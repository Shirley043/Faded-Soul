using UnityEngine;

public class CharacterWindEffect : MonoBehaviour
{
    public Material skirtMaterial;  
    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        
        float speed = (transform.position - previousPosition).magnitude / Time.deltaTime;
        previousPosition = transform.position;

        
        skirtMaterial.SetFloat("_MoveSpeed", speed);

        
        float waveAmplitude = Mathf.Clamp(speed * 0.1f, 0.0f, 0.0f); 
        skirtMaterial.SetFloat("_WaveAmplitude", waveAmplitude); 
    }
}
