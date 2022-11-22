using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float rotateBy = 0f;

    public InputActionReference controllerActionTrigger;

    public XRBaseController controller;
    public float amplitude = 0.5f;
    public float duration = 0.5f;

    public Transform raycastOrigin;

    AudioSource gunAudio;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
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
        controller.SendHapticImpulse(amplitude, duration);
        gunAudio.PlayOneShot(gunAudio.clip);
        Debug.Log("Raycast was fired");
        Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * 100, Color.green);

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
