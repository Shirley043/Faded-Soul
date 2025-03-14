using System.Collections;
using UnityEngine;

public class RelightController : MonoBehaviour
{
    public Light lightSource;
    private float intensityIncrease = 0.5f;
    void Start()
    {

    }


     private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
            //Increase the intensity
            lightSource.intensity += intensityIncrease;
            //Debug.Log(lightSource.intensity);
            StartCoroutine(HideAfterDelay(0.2f));
        } 
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

}
