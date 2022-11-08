using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript : MonoBehaviour
{
    Rigidbody rb;
    public Transform controller;
    [Range(0.0f, 360.0f)]
    public float rotateBy = 0f;
    

    public GameObject FPSCam;
    public GameObject targetObject;

    [SerializeField] public InputActionReference controllerActionTrigger;

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

    // Update is called once per frame
    private void Update()
    {
        //find out how to check for vr input to call firegun
        /*if (Input.GetButtonDown("FireGun"))
        {
            fireLaserGun();
            laserLineRenderer.enabled = true;
            Debug.Log("firing laser gun");
        }
        else
        {
            laserLineRenderer.enabled = false;
        }*/

        if (controllerActionTrigger.action.ReadValue<float>() != 0)
        {
            Debug.Log("Trigger from " + this.gameObject.name.ToString());
        }
    }

    // FireGun is called when gun is fired
    void fireGun()
    {
        RaycastHit hit;
        Vector3 origin = FPSCam.transform.position;
        Vector3 direction = FPSCam.transform.forward;
        if (Physics.Raycast(origin, direction, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Tag of target")
            {
                Debug.Log("I've hit the target!");
            }
        }
    }
}
