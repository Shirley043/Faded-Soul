using UnityEngine;

public class OldComp : MonoBehaviour
{
    public float floatSpeed = 0.5f;    
    public float floatHeight = 0.5f;   
    public float rotationSpeed = 10f; 

    private Vector3 startPosition;
    private float offset;

    void Start()
    {
        startPosition = transform.position;
        
        offset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed + offset) * floatHeight;

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
