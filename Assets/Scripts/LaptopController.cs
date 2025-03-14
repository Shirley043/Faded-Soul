using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopController : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private GameObject Target;
    [SerializeField] private float Range;
    [SerializeField] private float openSpeed;
    [SerializeField] private GameObject enemyTemplate;
    [SerializeField] private int enemyNum;
    [SerializeField] private float attackSpeed;
    float bondWidth = 2.0f;  // X-axis
    float bondHeight = 3.0f;  // Y-axis

    private bool swarmGenerated;

    void Start()
    {
        //get player transform
        playerPos = Target.transform;

        transform.localEulerAngles = new Vector3(105, 0, 0);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);

        var angle = openSpeed * Time.deltaTime;
        var axis = new Vector3(-1.0f, 0.0f, 0.0f);

        if (distance < Range)
        {
            //stop at Vector3(0, 0, 0)
            if (transform.localEulerAngles.x >= 15 && transform.localEulerAngles.x < 105)
            {
                transform.localRotation *= Quaternion.AngleAxis(angle, axis);
            }
        }


        //generate enemies when the laptop is open
        if (transform.localEulerAngles.x < 20 && !swarmGenerated)
        {
            StartCoroutine(GenerateSwarm());
            swarmGenerated = true;
        }
    }

    private IEnumerator GenerateSwarm()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            GameObject enemy = Instantiate(enemyTemplate, transform);

            // Generate random x and z coordinates
            float randomX = Random.Range(-bondWidth, bondWidth);
            float randomY = Random.Range(1f, bondHeight);

            enemy.transform.position = transform.position + new Vector3(randomX, randomY, 0);
            enemy.transform.localEulerAngles = new Vector3 (-15, 0, 0);

            StartCoroutine(MoveEnemy(enemy));
            yield return new WaitForSeconds(1.0f);
        }
    }

    // Attack Player
    IEnumerator MoveEnemy(GameObject enemy)
    {
        while (true)
        {
            // Move the enemy along the z-axis
            enemy.transform.Translate(Vector3.forward * attackSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
