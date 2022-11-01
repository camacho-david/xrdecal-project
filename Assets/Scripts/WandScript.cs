using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandScript : MonoBehaviour
{
    Rigidbody rb;
    public Transform controller;
    [Range(0.0f, 360.0f)]
    public float rotateBy = 90f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per physics update
    void FixedUpdate()
    {
        rb.MovePosition(controller.position);
        rb.MoveRotation(controller.rotation * Quaternion.Euler(rotateBy, 0, 0));
    }
}
