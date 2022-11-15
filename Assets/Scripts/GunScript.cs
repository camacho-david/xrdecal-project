using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float rotateBy = 0f;

    public InputActionReference controllerActionTrigger;

    public XRBaseController controller;
    public Transform raycastOrigin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per physics update
    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (controllerActionTrigger.action.ReadValue<float>() != 0)
        {
            Debug.Log("Trigger from " + gameObject.name.ToString());
            fireGun();
        }
    }

    // FireGun is called when gun is fired
    void fireGun()
    {
        controller.SendHapticImpulse(0.5f, 0.1f);

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit))
        {
            Debug.Log("Raycast was fired");
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Tag of target")
            {
                Debug.Log("I've hit the target!");
            }
        }
    }
}
