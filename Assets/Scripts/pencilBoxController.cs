using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PencilBoxController : MonoBehaviour
{
    public GameObject enemyTemplate;
    public float enemySpeed = 5f; 
    
    void Start()
    {
        StartCoroutine(GenerateSwarm());
    }

    private IEnumerator GenerateSwarm()
    {
        while (true) 
        {
            GameObject enemy = Instantiate(enemyTemplate, transform);
            enemy.transform.localPosition = new Vector3(0, 0, 0);
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);

            StartCoroutine(MoveEnemy(enemy));
            yield return new WaitForSeconds(2.0f); 
        }
    }

    IEnumerator MoveEnemy(GameObject enemy)
{
    float gravity = 23f;  
    float verticalSpeed = 5f;  
    
    while (enemy != null)
    {
        enemy.transform.Translate(Vector3.back * enemySpeed * Time.deltaTime);

        verticalSpeed -= gravity * Time.deltaTime;
        enemy.transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);

        yield return null; 
    }
}


}