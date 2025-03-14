using UnityEngine;

public class RotateLightning : MonoBehaviour
{
    // Speed of the rotation
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the object around the Y-axis at the specified speed
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
