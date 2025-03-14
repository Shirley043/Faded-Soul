using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Vector3 offset;
    [SerializeField] private Vector3 offset;
    public GameObject player;

    void Update()
    {
        transform.position = player.transform.position - offset;
    }
}
