using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light lightSource;
    private float initialIntensity = 1.5f;
    private float darkenRate = 0.2f;
 
    

    void Start()
    {
        lightSource = this.GetComponent<Light>();
        lightSource.intensity = initialIntensity;
    }

    
    void Update()
    {
        if (lightSource.intensity > 0) {
            lightSource.intensity -= darkenRate * Time.deltaTime;
        }
        //Debug.Log(lightSource.intensity);
    }
    
}
