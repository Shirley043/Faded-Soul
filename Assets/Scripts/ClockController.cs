using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    private float rotateSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(0, 90, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angles = this.transform.localEulerAngles;
        angles.x += rotateSpeed * Time.deltaTime;
        this.transform.localEulerAngles = angles;
    }
}
