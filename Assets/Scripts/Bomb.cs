using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem destroyParticle;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            Invoke("DestroyBomb", 0.5f);
        }
    }

    private void DestroyBomb()
    {
        
        Instantiate(destroyParticle.gameObject, transform.position, transform.rotation);

        
        Destroy(gameObject);
    }
}


