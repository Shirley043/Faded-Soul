using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{

    public ParticleSystem particleSystem; 

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision detected with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(IncreaseBurstCountOverTime()); 
            StartCoroutine(LoadNextSceneWithDelay(2f));   
        }
    }

    private IEnumerator IncreaseBurstCountOverTime()
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emissionModule.burstCount];
        emissionModule.GetBursts(bursts);

        if (bursts.Length > 0)
        {
            for (int i = 0; i < 2; i++) 
            {
                bursts[0].count = new ParticleSystem.MinMaxCurve(bursts[0].count.constant + 100); 
                emissionModule.SetBursts(bursts); 
                //Debug.Log("Burst count updated to: " + bursts[0].count.constant);
                yield return new WaitForSeconds(1f); 
            }
        }
    }

    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}